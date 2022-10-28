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
    #endregion

    #region ClusterShit
    [Header("Cluster shot")]

    [SerializeField]
    private float radiusCluster;

    [SerializeField] private float moveSpeedCluster;

    [SerializeField]
    private float nextShotCluster;

    [SerializeField]
    private float timeBetweenShotsCluster;

    [SerializeField]
    private int numberOfProjectilesCluster;

    [SerializeField] private bool spawnEnemies, error;

    public Transform target;

    private float angle, rawAngle, dirRight, dirUp;

    private int randomAngleRangeMin, randomAngleRangeMax, randomAngle;

    #endregion

    #region rush Movement
    private GameObject _target;
    [SerializeField] private float _speed;
    [SerializeField] private float closeEnough;
    private Vector3 _rushPos;
    private Vector3 _ogPos;
    public bool _canMove;
    #endregion

    [SerializeField] private GameObject simpleEnemy;
    [SerializeField] private float numOFEnemies;
    public bool active;
    private Vector3 targetPos;
    
    public int num;
    [SerializeField] private int _numOfShots;
    bossManager BM;

    [SerializeField] private float _minX, _maxX, _minY, _maxY;
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private GameObject _posionAOEObject;
    private int state = 0;
    private float bossTimer = 0f;

    private bool _circleShoot;
    private bool _clusterShoot;
    private bool _circleClusterShoot;
    [SerializeField] private bool _spawnAOE;
    
    void Start()
    {
        active = false;
        BM = GetComponent<bossManager>();
        startPoint = gameObject.transform.position;
        _target = GameObject.FindGameObjectWithTag("Body");
        _ogPos = transform.position;
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        //attacks
        clusterShoot(numberOfProjectilesCluster);
        secdonryCircleShot(numberOfProjectiles);
        circularShot(numberOfProjectiles);
        removeEnemies();
        rushToPos();
        returnPos();
        
        //AI
        bossAI();
    }

    void bossAI()
    {
        if(BM.attackStatus() == true)
        {
            bossTimer += Time.deltaTime;
            if (BM.stageOne)
            {
                if(bossTimer > 2)
                {

                    Debug.Log("State Change! " + state);

                    switch (state)
                    {

                        case 1:
                            //Debug.Log("State 1");
                            _circleShoot = true;

                            break;
                        case 2:
                            //Debug.Log("Laser state");
                            _circleShoot = false;
                            pickRandomPoint();
                            spawnPosion();
                            break;
                        case 0:
                            //Debug.Log("Enemy State");
                            _canMove = false;
                            spawnEnemy();
                            break;
                    }
                    state++;
                    if (state > 2)
                    {
                        state = -1;
                    }
                    bossTimer = 0;
                }
            }
            else if (BM.stageTwo)
            {
                if (bossTimer > 3)
                {

                    Debug.Log("State Change! " + state);

                    switch (state)
                    {

                        case 1:
                            //Debug.Log("State 1");
                            _canMove = false;
                            _circleClusterShoot = true;
                            break;
                        case 2:
                            //Debug.Log("Laser state");
                            _circleClusterShoot = false;
                            _clusterShoot = true;
                            break;
                        case 0:
                            //Debug.Log("Enemy State");
                            _clusterShoot = false;
                            _speed = 20;
                            pickRandomPoint();
                            break;

                    }
                    state++;
                    if (state > 2)
                    {
                        state = -1;
                    }
                    bossTimer = 0;
                }
            }
        }
       
    }

    private void removeEnemies()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i] == null)
            {
                _enemies.Remove(_enemies[i]);
            }
        }
    }

    void spawnEnemy()
    {
        if (_enemies.Count > 4)
        {
            
        }
        else if(_enemies.Count <= 3)
        {
            float a = Random.Range(1, 5);
            spawnSimpleEnemies(a);
        }

    }

    void spawnSimpleEnemies(float numOFEnemies)
    {
        for (int i = 0; i <= numOFEnemies - 1; i++)
        {
            float randX = Random.Range(_minX, _maxX);
            float randY = Random.Range(_minY, _maxY);

            Vector2 spawnPoint = new Vector2(randX, randY);

            if (spawnPoint == new Vector2(gameObject.transform.position.x, gameObject.transform.position.y))
            {
                return;
            }

           _enemies.Add(Instantiate(simpleEnemy, spawnPoint, Quaternion.identity));
        }

        
    }

    void circularShot(int numberOfProjectiles)
    {
        if(_circleShoot)
        {
            if (Time.time > nextShot)
            {
                Vector3 heading = _target.transform.position - transform.position;
                dirRight = checkLeftRight(transform.forward, heading, transform.up);
                dirUp = checkUpDown(transform.up, heading);

                determineAngle();


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

                    //proj.transform.SetParent(gameObject.transform);

                    angle += angleStep;
                }

                nextShot = Time.time + timeBetweenShots;
               
            }
        }
        
    }

    void secdonryCircleShot(int numOfProjectiles)
    {
        if(_circleClusterShoot)
        {
            if (Time.time > nextShot)
            {
                startPoint = gameObject.transform.position;

                numOfProjectiles = Random.Range(8, 15);
                //determineAngle();

                for (int i = 0; i <= numOfProjectiles - 1; i++)
                {
                    moveSpeedCluster = Random.Range(8, 12);
                    randomAngle = Random.Range(0, 360);
                    float projectileDirXposition = startPoint.x + Mathf.Sin((randomAngle * Mathf.PI) / 180) * radiusCluster;
                    float projectileDirYposition = startPoint.y + Mathf.Cos((randomAngle * Mathf.PI) / 180) * radiusCluster;

                    Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
                    Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeedCluster;

                    var proj = Instantiate(projectile, startPoint, Quaternion.identity);
                    proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

                    //proj.transform.SetParent(gameObject.transform);
                }

                nextShot = Time.time + timeBetweenShotsCluster;
               
            }
        }
       
    }

    void clusterShoot(int numOfProjectiles)
    {
        if(_clusterShoot)
        {
            if(Time.time > nextShot)
            {
                startPoint = gameObject.transform.position;
                Vector3 heading = _target.transform.position - transform.position;
                dirRight = checkLeftRight(transform.forward, heading, transform.up);
                dirUp = checkUpDown(transform.up, heading);
                numOfProjectiles = Random.Range(8, 15);
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

                    //proj.transform.SetParent(gameObject.transform);
                }

                nextShot = Time.time + timeBetweenShotsCluster;
               
            }
        }
    }

    void spawnPosion()
    {
        float a = Random.Range(4, 10);
        posionAOE(a);
        Debug.Log("SHOULD SPAWN POISION!");
    }

    void pickRandomPoint()
    {
        float randX = Random.Range(_minX, _maxX);
        float randY = Random.Range(_minY, _maxY);
        _rushPos = new Vector3(randX, randY, 0f);
        _canMove = true;
    }

    void rushToPos()
    {
        if (_canMove)
        {
            Debug.Log("SHOULD BE RUSHING!");
            transform.position = Vector3.MoveTowards(transform.position, _rushPos, _speed * Time.deltaTime);
        }
        
    }

    void returnPos()
    {
        if(!_canMove)
        {
            if (transform.position != _ogPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _ogPos, _speed * Time.deltaTime);
            }
        }
        
    }

    void posionAOE(float numOFEnemies)
    {
        for (int i = 0; i <= numOFEnemies - 1; i++)
        {
            float randX = Random.Range(_minX, _maxX);
            float randY = Random.Range(_minY, _maxY);

            Vector2 spawnPoint = new Vector2(randX, randY);

            if (spawnPoint == new Vector2(gameObject.transform.position.x, gameObject.transform.position.y))
            {
                return;
            }

            Instantiate(_posionAOEObject, spawnPoint, Quaternion.identity);
        }

    }

    void determineAngle()
    {
        //arctan gets an angle between the x and y pointd then we add 360 and mode it by 360 to give us a range between 0 and 360 no negative numbers 
        rawAngle = Mathf.Atan2(dirUp, dirRight) * Mathf.Rad2Deg;
        angle = (rawAngle + 360) % 360;

        if (angle == 45)
        {
            randomAngleRangeMin = 0;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Body"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
