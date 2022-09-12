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


    GameObject target;
    Rigidbody2D rb;
    Seeker seeker;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();


        try
        {
            targetPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        catch
        {

        }
        
        target = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("updatePath", 0f, .5f);
        //damage = gameObject.GetComponent<EnemyManager>().damage;
        canMove = false;
        StartCoroutine("moveWait");
       
       

    }

    // Update is called once per frame
    void Update()
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
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
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
                    // You can use this to trigger some special code if your game requires that.
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }

        if(canMove)
        {
            movement();
        }


        //if(moveWaitDone)
        //{
        //    if((rb.velocity.x == 0) && (rb.velocity.y == 0))
        //    {
        //        Destroy(gameObject);
        //    }
        //}

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
        else if (velocityMove)
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
        rb.AddForce(-targetPos.position * knockBackForce, ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("enemy"))
        //{
        //    Debug.Log("COLLIDED WITH AN ENEMY!");
        //    //rb.velocity = Vector3.zero;
        //    knockBackPlayer();

        //}

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("COLLIDED WITH THE PLAYER!");
            target.GetComponent<playerManager>().takeDamage(damage);
            knockBackPlayer();
            //rb.velocity = Vector3.zero;

        }

        if(collision.gameObject.CompareTag("obstacle"))
        {
            knockBackPlayer();
        }

        //if(collision.gameObject.CompareTag("boss"))
        //{
        //    rb.velocity = Vector3.zero;
        //    knockBackPlayer();
        //}
    }


    private IEnumerator moveWait()
    {

        yield return new WaitForSeconds(0.5f);
        canMove = true;
        moveWaitDone = true;
    }
}
