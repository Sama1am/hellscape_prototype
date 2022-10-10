using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class bombEnemy : MonoBehaviour
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
    //[SerializeField] private float _activationDist;
    [SerializeField] private float _chaseDist;
    [SerializeField] private float _explodeDist;
    //[SerializeField] private float _maxChaseDist;
    [SerializeField] private float _dist;
    public bool active;
    public bool stunned;
    #endregion

    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    //[SerializeField] private GameObject _bombEffect;
    [SerializeField] private CircleCollider2D _explodeRadius;
    public GameObject target;
    Rigidbody2D rb;
    Seeker seeker;
    enemy_spawner ES;
    enemyManager em;
    // Start is called before the first frame update
    void Start()
    {
        _explodeRadius.radius = _explodeDist;
        em = GetComponent<enemyManager>();
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

        if(_dist <= _chaseDist)
        {
            //chasing = true;
            ES.chasing = true;
            targetPos = player;
        }

        if(_dist <= _explodeDist)
        {
            canMove = false;
            explode();
        }

        if((ES.returning) || (ES.chasing) && (em.checkStunStatus() != true))
        {
            canMove = true;
        }
        else if (em.checkStunStatus())
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
        if (ES.returning)
        {
            targetPos = GetComponentInParent<Transform>();
        }
        else if (ES.chasing)
        {
            targetPos = player;
        }

        if (transform.position == ES.GetComponent<Transform>().position && !ES.home)
        {
            ES.home = true;
            targetPos = player;
        }
    }

    void explode()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            //_bombEffect.SetActive(true);
            _enemies[i].GetComponentInChildren<playerManager>().takeDamage(damage);
            knockBack();
            em.die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Body"))
        {
            _enemies.Add(collision.gameObject);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Body"))
        {
            _enemies.Remove(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Body"))
        {
            //_bombEffect.SetActive(true);
            //collision.gameObject.GetComponent<playerManager>().takeDamage(damage);
            //em.die();
            //knockBack();

            explode();
        }
    }

    void knockBack()
    {
        Vector2 difference = target.transform.position - transform.position;
        Vector2 diff = difference * 1.5f;
        target.transform.position = new Vector2(transform.position.x + diff.x, transform.position.y + diff.y);

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _chaseDist);
        Gizmos.DrawWireSphere(transform.position, _explodeDist);

        //Gizmos.DrawWireSphere(transform.position, _maxChaseDist);
    }


    IEnumerator stunnedwait()
    {
        canMove = false;
        yield return new WaitForSeconds(2f);
        canMove = true;
        em.setStunStatus(false);

    }

}
