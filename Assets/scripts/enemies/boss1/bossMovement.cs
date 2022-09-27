using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMovement : MonoBehaviour
{
    
    #region movementStuff
    [Header("movementStuff")]

    public Vector3 targetPos;

    [SerializeField]
    private float knockBackForce;

    [SerializeField]
    public bool canMove;

    [SerializeField]
    public float speed;
    #endregion


    #region circularShit
    [Header("stageTwoStuff")]

    [SerializeField]
    int numberOfProjectiles;

    [SerializeField]
    private float nextShot;

    [SerializeField]
    private float timeBetweenShots;

    [SerializeField]
    private float numOfShots;

    Vector2 startPoint;

    [SerializeField]
    private bool reachTargetPos, hasTarget, canShoot;

    [SerializeField]
    private float radius, moveSpeed;

    [SerializeField]
    private float closeEnough;
    #endregion


    #region ClusterShit
    [Header("stageOneShit")]

    [SerializeField]
    private float radiusCluster;

    private float moveSpeedCluster;

    [SerializeField]
    private float nextShotCluster;

    [SerializeField]
    private float timeBetweenShotsCluster;

    [SerializeField]
    private GameObject simpleEnemy;

    [SerializeField]
    private int numberOfProjectilesCluster;

    [SerializeField]
    private float numOFEnemies;

    private bool spawnEnemies, error;

    public Transform target;

    private float angle, rawAngle, dirRight, dirUp;

    private int randomAngleRangeMin, randomAngleRangeMax, randomAngle;

    #endregion


    public bool endStage;
    [SerializeField]
    GameObject projectile;
    private float damage;
    private GameObject player;
    Rigidbody2D rb;


    private void Start()
    {
        endStage = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        canMove = false;
        numOfShots = 0;
        StartCoroutine("initalStartWait");
        //StartCoroutine("spawnDely");
        //spawnSimpleEnemies();

       
    }

    private void Update()
    {
        if(endStage)
        {


            try
            {
                if((canMove) && (canShoot == false))
                {

                    move();

                    if ((gameObject.transform.position == targetPos) || (Vector2.Distance(transform.position, targetPos)) <= (closeEnough))
                    {
                        canMove = false;
                        canShoot = true;
                    }

                }


                if((canShoot) && (canMove == false))
                {
                    if (Time.time > nextShot)
                    {
                        radialShoot(numberOfProjectiles);
                    }

                }
            }
            catch
            {
                error = true;
            }

        }
        else if (!endStage)
        {
            try
            {
                Vector3 heading = target.position - transform.position;
                dirRight = checkLeftRight(transform.forward, heading, transform.up);
                dirUp = checkUpDown(transform.up, heading);

                determineAngle();

                if (spawnEnemies)
                {
                    //spawnSimpleEnemies();
                }


                if (Time.time > nextShotCluster)
                    clusterShoot(numberOfProjectilesCluster);
            }
            catch
            {
                error = true;
            }
        }

    }

    private void move()
    {
         canShoot = false;
         transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
         Debug.Log("BOSS SHOULD MOVE");
    }

    

    public void knockBack()
    {
        rb.AddForce(-targetPos * knockBackForce, ForceMode2D.Impulse);
    }



    public void selectTarget()
    {
        targetPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        hasTarget = true;
        canShoot = false;
        
    }


    private void radialShoot(int numberOfProjectiles)
    {
        //canMove = false;
        //startPoint = gameObject.transform.position;
        ////numberOfProjectiles = Random.Range(8, 13);

        //float angleStep = 360f / numberOfProjectiles;
        //float angle = 0f;

        //for (int i = 0; i <= numberOfProjectilesCluster - 1; i++)
        //{

        //    float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
        //    float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

        //    Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
        //    Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;

        //    var proj = Instantiate(projectile, startPoint, Quaternion.identity);
        //    proj.GetComponent<Rigidbody2D>().velocity =
        //        new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

        //    proj.transform.SetParent(gameObject.transform);

        //    angle += angleStep;
        //}

        //nextShot = Time.time + timeBetweenShots;
        




        canMove = false;
        startPoint = gameObject.transform.position;
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i <= numberOfProjectiles - 1; i++)
        {
            startPoint = gameObject.transform.position;
            float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;

            var proj = Instantiate(projectile, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity =
                new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            proj.transform.SetParent(gameObject.transform);

            angle += angleStep;
        }

        nextShot = Time.time + timeBetweenShots;
        numOfShots++;

        if (numOfShots >= 2)
        {
            numOfShots = 0;
            StartCoroutine("moveBuildUp");
        }


    }


    float checkLeftRight(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }

    //checks if the player is above the boss 
    float checkUpDown(Vector3 up, Vector3 targetDir)
    {

        float dir = Vector3.Dot(up, targetDir);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }

    void determineAngle()
    {
        //arctan gets an angle between the x and y pointd then we add 360 and mode it by 360 to give us a range between 0 and 360 no negative numbers 
        rawAngle = Mathf.Atan2(dirUp, dirRight) * Mathf.Rad2Deg;
        angle = (rawAngle + 360) % 360;

        if (angle == 45)
        {
            randomAngleRangeMin = 1;
            randomAngleRangeMax = 87;
        }
        else if (angle == 135)
        {
            randomAngleRangeMin = 270;
            randomAngleRangeMax = 358;
        }
        else if (angle == 225)
        {
            randomAngleRangeMin = 180;
            randomAngleRangeMax = 268;
        }
        else if (angle == 315)
        {
            randomAngleRangeMin = 90;
            randomAngleRangeMax = 178;
        }
    }

    void clusterShoot(int numOfProjectiles)
    {

        startPoint = gameObject.transform.position;

        numOfProjectiles = Random.Range(5, 8);
        determineAngle();

        for (int i = 0; i <= numOfProjectiles - 1; i++)
        {
            moveSpeedCluster = Random.Range(8, 12);
            randomAngle = Random.Range(randomAngleRangeMin, randomAngleRangeMax);
            float projectileDirXposition = startPoint.x + Mathf.Sin((randomAngle * Mathf.PI) / 180) * radiusCluster;
            float projectileDirYposition = startPoint.y + Mathf.Cos((randomAngle * Mathf.PI) / 180) * radiusCluster;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeedCluster;

            var proj = Instantiate(projectile, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            proj.transform.SetParent(gameObject.transform);
        }

        nextShotCluster = Time.time + timeBetweenShotsCluster;
        
    }


    void spawnSimpleEnemies()
    {
       
        for (int i = 0; i <= numOFEnemies - 1; i++)
        {
            float randX = Random.Range(-46f, -69f);
            float randY = Random.Range(-6f, 6f);

            Vector2 spawnPoint = new Vector2(randX, randY);

            if (spawnPoint == new Vector2(gameObject.transform.position.x, gameObject.transform.position.y))
            {
                return;
            }

            Instantiate(simpleEnemy, spawnPoint, Quaternion.identity);
        }

        StartCoroutine("spawnDely");

    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log("COLLIDED WITH AN ENEMY!");
            rb.velocity = Vector3.zero;
        }

        if (collision.gameObject.CompareTag("Body"))
        {
            Debug.Log("COLLIDED WITH THE PLAYER!");
            player.GetComponent<playerManager>().takeDamage(damage);
            //knockBack();
            rb.velocity = Vector3.zero;

        }

        if(collision.gameObject.CompareTag("obstacle"))
        {
            knockBack();
            canMove = false;
            canShoot = true;
        }
    }


    private IEnumerator initalStartWait()
    {
        canMove = false;

        yield return new WaitForSeconds(4f);

        canMove = false;
        canShoot = true;
        //spawnEnemies = true;
    }

    private IEnumerator moveBuildUp()
    {
        canMove = false;
        canShoot = false;

        yield return new WaitForSeconds(2f);

        selectTarget();
        canMove = true;
        
    }

    private IEnumerator spawnDely()
    {
        spawnEnemies = false;
        yield return new WaitForSeconds(8f);
        spawnEnemies = true;
        

    }

}
