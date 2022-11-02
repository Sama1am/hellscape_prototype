using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class clusterEnemy : MonoBehaviour
{
    
    [SerializeField] private float damage;
    private float _dist;
    [SerializeField] private float _attackDist;
    public bool active;
    public bool stunned;
    [SerializeField] private float radiusCluster;
    private float moveSpeedCluster;
    [SerializeField] private float nextShotCluster;
    [SerializeField] private float timeBetweenShotsCluster;
    [SerializeField] int numberOfProjectiles;
    [SerializeField] private int randomAngleRangeMin, randomAngleRangeMax;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private bool _center;

    private bool _canShoot;
    private float angle, rawAngle, dirRight, dirUp;
    private bool spawnEnemies, error;
    private int randomAngle;
    private Vector2 startPoint;
    private Transform _targetPos;
    private Transform _player;
    private GameObject _target;
    private Rigidbody2D _rb;
    private enemy_spawner _ES;
    private enemyManager _em;

    void Start()
    {
        _em = GetComponent<enemyManager>();
        _player = GameObject.FindGameObjectWithTag("Body").GetComponent<Transform>();
        active = false;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        
        _ES = GetComponentInParent<enemy_spawner>();
        _canShoot = true;
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
        checkDist(); // checks dist from player

       

    }

    private void FixedUpdate()
    {
        if(_canShoot)
        {
            shoot();
        }

        if(_em.checkStunStatus() == true)
        {
            _canShoot = false;
            StartCoroutine("stunnedwait");
        }
        
        
    }

    private void shoot()
    {
        if (_dist < _attackDist)
        {
            if (Time.time > nextShotCluster)
            {
                if (_center)
                {
                    Vector3 heading = _target.transform.position - transform.position;
                    dirRight = checkLeftRight(transform.forward, heading, transform.up);
                    dirUp = checkUpDown(transform.up, heading);

                    determineAngle();
                    clusterShoot(numberOfProjectiles);
                }
                else
                {
                    clusterShoot(numberOfProjectiles);
                }

            }

        }
    }

    public void checkDist()
    {
        _dist = Vector3.Distance(gameObject.transform.position, _target.transform.position);

    }

    void clusterShoot(int numOfProjectiles)
    {

        startPoint = gameObject.transform.position;

        numOfProjectiles = numberOfProjectiles;
        determineAngle();

        for (int i = 0; i <= numOfProjectiles - 1; i++)
        {
            moveSpeedCluster = Random.Range(8, 12);
            randomAngle = Random.Range(randomAngleRangeMin, randomAngleRangeMax);
            float projectileDirXposition = startPoint.x + Mathf.Sin((randomAngle * Mathf.PI) / 180) * radiusCluster;
            float projectileDirYposition = startPoint.y + Mathf.Cos((randomAngle * Mathf.PI) / 180) * radiusCluster;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeedCluster;

            var proj = Instantiate(_projectile, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            proj.transform.SetParent(gameObject.transform);
        }

        nextShotCluster = Time.time + timeBetweenShotsCluster;
        
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackDist);
    }

    IEnumerator stunnedwait()
    {
        _canShoot = false;
        yield return new WaitForSeconds(1f);
        _canShoot = true;
        _em.setStunStatus(false);

    }
}
