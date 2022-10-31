using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaBoss : MonoBehaviour
{
    
    [SerializeField] private float _OrbitSpeed;
    [SerializeField] private GameObject[] _lasers;
    [SerializeField] private GameObject _simpleEnemies;
    //[SerializeField] private float _rotateTime;
    [SerializeField] private float _attackDist;
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _miny;
    [SerializeField] private float _maxy;
    [SerializeField] private float _speed;
    [SerializeField] private float _increasedSpeed;
    [SerializeField] private GameObject _lavaAOE;
    [SerializeField] private float _stateTime;
    private Vector3 randomPos;

    private int state = 0;
    [SerializeField] private float bossTimer = 0f;
    private bool canRotateLaser = false;
    private bool _canSpawnLava;
    private bool _goHome;
    #region position stuff
    private Vector3 ogPos;
    private Vector3 _ogRightPos;
    private Vector3 _ogLeftPos;
    private Vector3 _ogUpPos;
    private Vector3 _ogDownPos;

    private Quaternion _originalRotRight;
    private Quaternion _originalRotLeft;
    private Quaternion _originalRotUp;
    private Quaternion _originalRotDown;
    #endregion

    [SerializeField] private float _damage;
    private float _dist;
    private GameObject target;

    public bossManager BM;

    // Start is called before the first frame update
    void Start()
    {
        BM = GetComponent<bossManager>();
        ogPos = this.transform.position;
        target = GameObject.FindGameObjectWithTag("Body");
        getOgPos();
    }

    // Update is called once per frame
    void Update()
    {
        spawnLava();

        //AI
        BossAI();

        //GFX
        rotatePoints();
        goToCenter();

        //Engine things
        removeEnemies();
        checkDist();

        if(BM.isdead)
        {
            for(int i = 0; i < _enemies.Count; i++)
            {
                Destroy(_enemies[i]);
            }
        }

    }

    void BossAI()
    {
        if(BM.attackStatus() == true)
        {
            bossTimer += Time.deltaTime;
            if(BM.stageOne)
            {
                if(bossTimer > _stateTime)
                {

                    Debug.Log("State Change! " + state);

                    switch (state)
                    {

                        case 1:
                            //Debug.Log("State 1");
                            _stateTime = 4;
                            activateLasers();
                            canRotateLaser = false;
                            break;
                        case 2:
                            //Debug.Log("Laser state");
                            //bossTimer += 1.5f;
                            _stateTime = 5;
                            canRotateLaser = true;
                            break;
                        case 3:
                            _stateTime = 4;
                            canRotateLaser = false;
                            _goHome = true;
                            break;
                        case 0:
                            _stateTime = 5;
                            _goHome = false;
                            deactivateLasers();
                            spawnEnemy();
                            break;
                    }
                    state++;
                    if(state > 3)
                    {
                        state = -1;
                    }
                    bossTimer = 0;
                }
            }
            else if(BM.stageTwo)
            {
                if (bossTimer > _stateTime)
                {

                    Debug.Log("State Change! " + state);

                    switch (state)
                    {

                        case 1:
                            //Debug.Log("State 1");
                            _stateTime = 4;
                            _canSpawnLava = true;
                            canRotateLaser = false;
                            break;
                        case 2:
                            //Debug.Log("Laser state");
                            _stateTime = 5;
                            _speed = _increasedSpeed;
                            _canSpawnLava = false;
                            resetLava();
                            activateLasers();
                            //bossTimer += 1.5f;
                            canRotateLaser = true;
                            break;
                        case 3:
                            _stateTime = 4;
                            canRotateLaser = false;
                            _goHome = true;
                            break;
                        case 0:
                            //this.transform.position = ogPos;
                            _stateTime = 5;
                            _goHome = false;
                            deactivateLasers();
                            spawnEnemy();
                            break;
                    }
                    state++;
                    if (state > 3)
                    {
                        state = -1;
                    }
                    bossTimer = 0;
                }
            }
            
        }
       
    }

   
    void spawnLava()
    {
        if(_canSpawnLava)
        {
            _lavaAOE.SetActive(true);
            _lavaAOE.GetComponent<Transform>().localScale += new Vector3(0.008f, 0.008f, 0);
        }
        

    }

    void resetLava()
    {
        _lavaAOE.SetActive(false);
        _lavaAOE.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 0);
    }

    void spawnEnemy()
    {
        if(_enemies.Count >= 4)
        {
            
        }
        else if(_enemies.Count <= 3)
        {
            float a = Random.Range(1, 4);
            spawnEnemies(a);
        }
       
    }

    void activateLasers()
    {
        for (int i = 0; i < _lasers.Length; i++)
        {
            _lasers[i].SetActive(true);
        }
    }

    void deactivateLasers()
    {
        for (int i = 0; i < _lasers.Length; i++)
        {
            _lasers[i].SetActive(false);
        }

        backToOgPos();
    }

    private void generatePos()
    {

        float randomX = Random.Range(_minX, _maxX);
        float randomY = Random.Range(_miny, _maxy);

        randomPos = new Vector3(randomX, randomY, 0);
    }

    private void spawnEnemies(float _amount)
    {
       
            for (int i = 0; i < _amount; i++)
            {
                generatePos();
                if (randomPos == transform.position)
                {
                    generatePos();
                    // Debug.Log("POS WAS SAME AS BOSS");
                }
                _enemies.Add(Instantiate(_simpleEnemies, randomPos, Quaternion.identity));
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

    public void checkDist()
    {
        _dist = Vector3.Distance(gameObject.transform.position, target.transform.position);


    }

    void rotatePoints()
    {
        if(canRotateLaser)
        {
            _lasers[0].transform.RotateAround(gameObject.transform.position, Vector3.forward, _OrbitSpeed * Time.deltaTime);
            _lasers[1].transform.RotateAround(gameObject.transform.position, Vector3.forward, _OrbitSpeed * Time.deltaTime);
            _lasers[2].transform.RotateAround(gameObject.transform.position, Vector3.forward, _OrbitSpeed * Time.deltaTime);
            _lasers[3].transform.RotateAround(gameObject.transform.position, Vector3.forward, _OrbitSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Body").transform.position, _speed * Time.deltaTime);
        }
       

    }

    void goToCenter()
    {
        if(_goHome)
        {
            _lasers[0].transform.RotateAround(gameObject.transform.position, Vector3.forward, _OrbitSpeed * Time.deltaTime);
            _lasers[1].transform.RotateAround(gameObject.transform.position, Vector3.forward, _OrbitSpeed * Time.deltaTime);
            _lasers[2].transform.RotateAround(gameObject.transform.position, Vector3.forward, _OrbitSpeed * Time.deltaTime);
            _lasers[3].transform.RotateAround(gameObject.transform.position, Vector3.forward, _OrbitSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, ogPos, _speed * Time.deltaTime);

            
        }
    }

    private void getOgPos()
    {
        _ogRightPos = _lasers[0].transform.position;
        _ogLeftPos = _lasers[1].transform.position;
        _ogUpPos = _lasers[2].transform.position;
        _ogDownPos = _lasers[3].transform.position;

        _originalRotRight = _lasers[0].transform.rotation;
        _originalRotLeft = _lasers[1].transform.rotation;
        _originalRotUp = _lasers[2].transform.rotation;
        _originalRotDown = _lasers[3].transform.rotation;


    }

    private void backToOgPos()
    {
        _lasers[0].transform.position = _ogRightPos;
        _lasers[1].transform.position = _ogLeftPos;
        _lasers[2].transform.position = _ogUpPos;
        _lasers[3].transform.position = _ogDownPos;

        _lasers[0].transform.rotation = _originalRotRight;
        _lasers[1].transform.rotation = _originalRotLeft;
        _lasers[2].transform.rotation = _originalRotUp;
        _lasers[3].transform.rotation = _originalRotDown;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Body"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if ((collision.gameObject.GetComponent<bodyController>().isAttacking() == false))
            {
                collision.gameObject.GetComponent<playerManager>().takeDamage(_damage);
                //knockBackPlayer();
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                //StartCoroutine("velocityDelay");


            }
            else if (collision.gameObject.GetComponent<bodyController>().attacking == true)
            {
                collision.gameObject.GetComponent<playerManager>().takeDamage(_damage / collision.gameObject.GetComponent<playerManager>().damageTakenOffset);
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                //StartCoroutine("velocityDelay");
                //knockBack();
                //StartCoroutine("velocityDelay");
            }

        }

        if(collision.gameObject.CompareTag("enemy"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackDist);

    }


}
