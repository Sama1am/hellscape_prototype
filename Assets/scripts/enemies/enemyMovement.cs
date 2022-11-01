using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemyMovement : MonoBehaviour
{
    #region pathFindingShit
    [Header("pathFinding")]

    public Path path;

    public bool canMove;

    public float speed;

    public float nextWaypointDistance;

    private float distanceToWaypoint;

    private int currentWaypoint = 0;

    public bool reachedEndOfPath;
    #endregion

    #region movementStuff
    [Header("movementStuff")]

    public Transform targetPos;

    public Transform player;

    [SerializeField]
    private float knockBackForce;

    [SerializeField]
    private float maxMoveSpeed;

    [SerializeField]
    private bool transformMove, moveWaitDone;
    #endregion

    #region privateVar
    [SerializeField] private float damage;
    #endregion

    #region distacne shit
    [Header("distacne shit")]
    //[SerializeField] private float _activationDist;
    [SerializeField] private float _chaseDist;
    //[SerializeField] private float _maxChaseDist;
    [SerializeField] private float _dist;
    [SerializeField] private float _attackDist;
    public bool active;
    public bool stunned;
    #endregion

    [SerializeField] private float _closeEnough;
    [SerializeField] private float _rushSpeed;
    //[SerializeField] private bool _rush;
    [SerializeField] private bool _isRushing;
  
    public GameObject target;
    Rigidbody2D rb;
    Seeker seeker;
    enemy_spawner ES;
    enemyManager em;
    // Start is called before the first frame update
    void Start()
    {
        em = GetComponent<enemyManager>();
        player = GameObject.FindGameObjectWithTag("Body").GetComponent<Transform>();
        active = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        ES = GetComponentInParent<enemy_spawner>();
        _isRushing = false;
        try
        {
            targetPos = GameObject.FindGameObjectWithTag("Body").GetComponent<Transform>();
        }
        catch
        {

        }
        
        target = GameObject.FindGameObjectWithTag("Body");
        

    }

    // Update is called once per frame
    void Update()
    {
        checkDist(); // checks dist from player
        changeTarget(); // chases the target depedning on the states 
        checkstates(); // chnages the states if the enemy depeneding on distances and certain criteria 
        
        if(_dist < _attackDist)
        {
            speed = _rushSpeed;
        }
        else if(_dist > _attackDist)
        {
            speed = maxMoveSpeed;
        }

        if(ES.active)
        {
            pathFinding();
        }

    }

    private void FixedUpdate()
    {
        if(ES.active)
        {
            updatePath();
        }
        
    }

    void checkstates()
    {

        //if the player is in ranged of chase dist the enemy will chase them 
        if(_dist <= _chaseDist)
        {
            //chasing = true;
            ES.chasing = true;
            ES.returning = false;
            targetPos = player;
        }

        //enemy will still chase player if they are out of max dist if the player is in chase range 
        if((Vector2.Distance(transform.position, ES.transform.position) > ES._maxDistFromSpawner) && (_dist <= _chaseDist))
        {
            ES.chasing = true;
            ES.returning = false;
            targetPos = player;
        }
        else if((Vector2.Distance(transform.position, ES.transform.position) > ES._maxDistFromSpawner) && (_dist > _chaseDist))
        {
            Debug.Log("SHOULD RETURN HOME!");
            ES.goBack();
        }

        if ((ES.returning) || (ES.chasing) && (em.checkStunStatus() != true))
        {
            canMove = true;
        }
        else if (em.checkStunStatus() == true)
        {
            canMove = false;
            StartCoroutine("stunnedwait");
        }
        else
        {
            canMove = false;
        }
    }

    void pathFinding()
    {
        if (path == null)
        {
            // We have no path to follow yet, so don't do anything
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count) // checking to see if we have reached end of path 
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }



        while (true)
        {

            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }

        if (canMove)
        {
            if (ES.chasing)
            {
                movement();
            }
            else if (ES.returning)
            {
                movement();
            }
        }


    }

    public void OnPathComplete(Path p)
    {

        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }

    }

    private void movement()
    {
        if(transformMove)
        {
            //slows down the character as they get close to target 
            var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;

            // Direction to the next waypoint
            // Normalize it so that it has a length of 1 world unit
            Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            // Multiply the direction by our desired speed to get a velocity
            Vector3 velocity = dir * speed * speedFactor;

            transform.position += velocity * Time.deltaTime;

            

        }
    }

    void updatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, targetPos.position, OnPathComplete);
        }
        
    }

    public void knockBackPlayer()
    {
        //rb.AddForce(-targetPos.position * knockBackForce, ForceMode2D.Impulse);

        Vector2 direction = player.transform.position - transform.position;
        Vector2 dir = (-direction).normalized;
        //Debug.Log(dir);
        rb.AddForce(dir * knockBackForce, ForceMode2D.Impulse);
        
    }

    public void checkDist()
    {
        _dist = Vector3.Distance(gameObject.transform.position, target.transform.position);

    }

    void changeTarget()
    {
        if(ES.returning)
        {
            targetPos = ES._EStransform;
            updatePath();
        }
        else if(ES.chasing)
        {
            targetPos = player;
            updatePath();
        }

        if(transform.position == ES.GetComponent<Transform>().position && !ES.home)
        {
            ES.home = true;
            targetPos = player;
            ES.returning = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _chaseDist);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackDist);
        Gizmos.color = Color.green;

    }

    IEnumerator stunnedwait()
    {
        canMove = false;
        yield return new WaitForSeconds(1f);
        canMove = true;
        em.setStunStatus(false);
            
    }

   
}
