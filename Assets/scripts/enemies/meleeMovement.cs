using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class meleeMovement : MonoBehaviour
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

    private Transform _targetPos;
    private Transform _player;
    [SerializeField] private float knockBackForce;
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float rushMoveSpeed;
    [SerializeField] private bool velocityMove;
    [SerializeField] private bool transformMove, moveWaitDone;
    [SerializeField] private float _rushSpeed;
    #endregion

    #region privateVar
    [SerializeField] private float damage;
    #endregion

    #region distacne shit
    [Header("distacne shit")]
    [SerializeField] private float _attackDist;
    [SerializeField] private float _chaseDist;
    [SerializeField] private float _closeEnough;
    //[SerializeField] private float _retreatDist;
    [SerializeField] private float _dist;
    public bool active;
    public bool stunned;
    #endregion

    private Vector3 _rushTarget;
    [SerializeField] private bool rush;
    [SerializeField]  private bool _rushing;
    private GameObject _target;
    Rigidbody2D rb;
    Seeker seeker;
    enemy_spawner ES;
    enemyManager em;
    // Start is called before the first frame update
    void Start()
    {
        em = GetComponent<enemyManager>();
        _player = GameObject.FindGameObjectWithTag("Body").GetComponent<Transform>();
        active = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        ES = GetComponentInParent<enemy_spawner>();

        try
        {
            _targetPos = GameObject.FindGameObjectWithTag("Body").GetComponent<Transform>();
        }
        catch
        {

        }

        _target = GameObject.FindGameObjectWithTag("Body");
        //InvokeRepeating("updatePath", 0f, .5f);

    }

    void Update()
    {
        checkDist();
        changeTarget();

        if(_dist <= _chaseDist)
        {
            //chasing = true;
            ES.chasing = true;
            //targetPos = player;
        }

        if(ES.chasing)
        {
            if((Vector2.Distance(transform.position, _player.GetComponent<Transform>().position)) <= (_attackDist) && (!rush))
            {
                rush = true; 
                
            }
            else if((Vector2.Distance(transform.position, _player.GetComponent<Transform>().position)) > (_attackDist))
            {
                rush = false;
            }

            if(rush)
            {
                //stop 
                canMove = false;
                rushToPlayer();
            }

            if(_rushing)
            {
                rush = false;
                if ((gameObject.transform.position == _rushTarget) || (Vector2.Distance(transform.position, _rushTarget)) <= (_closeEnough))
                {
                    rush = false;
                    StartCoroutine("rushWait");
                }
            }
        }

        if ((ES.returning) || (ES.chasing) && (em.checkStunStatus() != true) && (!rush))
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


        if (ES.active)
        {
            pathFinding();
        }




    }

    private void FixedUpdate()
    {
        if (ES.active)
        {
            updatePath();
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
        if (transformMove)
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
            seeker.StartPath(transform.position, _targetPos.position, OnPathComplete);
        }

    }

    public void knockBackPlayer()
    {
        //rb.AddForce(-targetPos.position * knockBackForce, ForceMode2D.Impulse);

        Vector2 direction = _player.transform.position - transform.position;
        Vector2 dir = (-direction).normalized;
        //Debug.Log(dir);
        rb.AddForce(dir * knockBackForce, ForceMode2D.Impulse);

    }

    public void checkDist()
    {
        _dist = Vector3.Distance(gameObject.transform.position, _target.transform.position);


    }

    private void rushToPlayer()
    {
        rush = false;
        _rushing = true;
        _rushTarget = _target.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, _rushTarget, _rushSpeed * Time.deltaTime);
    }

    void changeTarget()
    {
        if(ES.returning)
        {
            _targetPos = GetComponentInParent<Transform>();
        }
        else if (ES.chasing)
        {
            _targetPos = _player;
        }
        else if(!rush)
        {
            _targetPos = GetComponentInParent<Transform>();
        }

        if (transform.position == ES.GetComponent<Transform>().position && !ES.home)
        {
            ES.home = true;
            _targetPos = _player;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Body"))
        {
            if (collision.gameObject.GetComponent<bodyController>().attacking == false)
            {
                //knockBackPlayer();
                //retreating = true;
                Debug.Log("SHOULD RETREAT!");
                rush = false;
                _targetPos = ES.GetComponent<Transform>();
                speed = maxMoveSpeed;
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _chaseDist);
        Gizmos.DrawWireSphere(transform.position, _attackDist);
        
    }

    IEnumerator stunnedwait()
    {
        canMove = false;
        yield return new WaitForSeconds(1.5f);
        canMove = true;
        em.setStunStatus(false);

    }

    IEnumerator rushWait()
    {
        canMove = false;
        yield return new WaitForSeconds(1f);
        rush = false;
        _rushing = false;
        canMove = true;
    }
}
