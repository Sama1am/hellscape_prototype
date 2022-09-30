using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossCombat : MonoBehaviour
{
    #region circular shot
    [Header("Circular shot")]
    [SerializeField] int numberOfProjectiles;

    [SerializeField] GameObject projectile;

    [SerializeField] private float nextShot;

    [SerializeField] private float timeBetweenShots;

    Vector2 startPoint;

    [SerializeField] private float radius, moveSpeed;

    private bool canShoot;
    #endregion

    #region ClusterShit
    [Header("Cluster shot")]

    [SerializeField]
    private float radiusCluster;

    private float moveSpeedCluster;

    [SerializeField]
    private float nextShotCluster;

    [SerializeField]
    private float timeBetweenShotsCluster;

    [SerializeField]
    private int numberOfProjectilesCluster;

    private bool spawnEnemies, error;

    public Transform target;

    private float angle, rawAngle, dirRight, dirUp;

    private int randomAngleRangeMin, randomAngleRangeMax, randomAngle;

    #endregion

    #region rush Movement
    private GameObject _target;
    [SerializeField] private float _speed;
    [SerializeField] private float closeEnough;

    private Vector3 _ogPos;
    #endregion

    private bool _canMove;
    public bool shoot;
    public int num;
    bossManager BM;
    void Start()
    {
        BM = GetComponent<bossManager>();
        startPoint = gameObject.transform.position;
        _target = GameObject.FindGameObjectWithTag("Body");
        _ogPos = transform.position;

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if(BM.stageOne)
        {
            try
            {
                if ((_canMove) && (canShoot == false))
                {

                    rushTowardsPlayer();

                    if ((gameObject.transform.position == target.position) || (Vector2.Distance(transform.position, target.position)) <= (closeEnough))
                    {
                        _canMove = false;
                        canShoot = true;
                    }

                }


                if ((canShoot) && (_canMove == false))
                {
                    if (Time.time > nextShot)
                    {
                        circularShot(numberOfProjectiles);
                    }

                }
            }
            catch
            {
                error = true;
            }
        }
        else if(BM.stageTwo)
        {
            try
            {

                if ((_canMove) && (canShoot == false))
                {

                    rushTowardsPlayer();

                    if ((gameObject.transform.position == target.position) || (Vector2.Distance(transform.position, target.position)) <= (closeEnough))
                    {
                        _canMove = false;
                        canShoot = true;
                    }

                }
                else if ((canShoot) && (_canMove == false))
                {
                    Vector3 heading = target.position - transform.position;
                    dirRight = checkLeftRight(transform.forward, heading, transform.up);
                    dirUp = checkUpDown(transform.up, heading);

                    determineAngle();

                    if (Time.time > nextShotCluster)
                        clusterShoot(numberOfProjectilesCluster);
                }

               
            }
            catch
            {
                error = true;
            }
        }


    }


    void circularShot(int numberOfProjectiles)
    {
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
        canShoot = false;
        _canMove = true;
    }



    void rushTowardsPlayer()
    {
        Vector3 target = _target.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);

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
        canShoot = false;
        _canMove = true;

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

    IEnumerator startDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }
}
