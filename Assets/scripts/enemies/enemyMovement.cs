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
    private bool velocityMove;

    [SerializeField]
    private bool transformMove, moveWaitDone;
    #endregion

    #region privateVar
    [SerializeField] private float damage;
    #endregion

    #region distacne shit
    [Header("distacne shit")]
    [SerializeField] private float _activationDist;
    [SerializeField] private float _chaseDist;
    [SerializeField] private float _maxChaseDist;
    [SerializeField] private float _dist;
    public bool active;
    #endregion

    public GameObject target;
    Rigidbody2D rb;
    Seeker seeker;
    enemy_spawner ES;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Body").GetComponent<Transform>();
        active = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        ES = GetComponentInParent<enemy_spawner>();

        try
        {
            targetPos = GameObject.FindGameObjectWithTag("Body").GetComponent<Transform>();
        }
        catch
        {

        }
        
        target = GameObject.FindGameObjectWithTag("Body");
        //InvokeRepeating("updatePath", 0f, .5f);

    }

    // Update is called once per frame
    void Update()
    {
        checkDist();
        changeTarget();

        if (_dist <= _chaseDist)
        {
            //chasing = true;
            ES.chasing = true;
            targetPos = player;
        }

        if((ES.returning) || (ES.chasing))
        {
            canMove = true;
        }
        else
        {
            canMove = false;
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
        else if(velocityMove)
        {
            Vector3 posA = target.transform.position;
            Vector3 posB = rb.position;
            Vector3 direction = (posA - posB).normalized;


            Vector2 force = direction * speed * Time.deltaTime;
            rb.velocity = rb.velocity + force;

            if (Mathf.Abs(rb.velocity.x) >= maxMoveSpeed)
            {
                Debug.Log("AT MAX MOVE SPEED");
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxMoveSpeed, maxMoveSpeed), rb.velocity.y);
                Debug.Log("VELOCITY IS " + rb.velocity.x);
            }


            
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
        Debug.Log(dir);
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
            targetPos = GetComponentInParent<Transform>();
        }
        else if(ES.chasing)
        {
            targetPos = player;
        }

        if(transform.position == ES.GetComponent<Transform>().position && !ES.home)
        {
            ES.home = true;
            targetPos = player;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Body"))
        {
            if (collision.gameObject.GetComponent<bodyController>().isAttacking == false)
            {
                knockBackPlayer();
            }
        }

    }
}
