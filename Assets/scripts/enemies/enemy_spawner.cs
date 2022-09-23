using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawner : MonoBehaviour
{
    #region GameObjects
    [Header("Game Objects")]
    public GameObject _enemyPrefab;
    public GameObject _enemy;
    #endregion

    #region distance
    [Header("Distance stuff (do not set)")]
    [SerializeField] private float _distFromPlayer;
    [SerializeField] private float _distFromSpawner;
    [SerializeField] private float _enemyDist;
    #endregion

    #region distance Varibles
    [Header("Distacne Variables")]
    [SerializeField] private float _activationDist;
    [SerializeField] private float _maxDist;
    [SerializeField] private float _maxChaseDist;
    #endregion

    private GameObject _player;
    [SerializeField] private float _spawnEnemyDelay;

    public float currentTime;
    #region state
    [Header("State stuff (Do not set)")]
    public bool active;
    public bool returning;
    public bool chasing; 
    public bool home;
    public bool isdead;
    public bool spawnedNewEnemy;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity, transform);
        isdead = false;
        _player = GameObject.FindGameObjectWithTag("Body");
        currentTime = _spawnEnemyDelay;
    }

    // Update is called once per frame
    void Update()
    {

        if(!isdead)
        {
            checkDist();

            activate();

            goBack();


            if (_enemy.transform.position != transform.position)
            {
                home = false;
            }
        }
        
        if((isdead) && (_enemy == null) && (!spawnedNewEnemy))
        {
            currentTime -= Time.deltaTime;
        }
       
        if((currentTime <= 0) && (_enemy == null) && (!spawnedNewEnemy))
        {
            _enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity, transform);
            spawnedNewEnemy = true;
            isdead = false;
            currentTime = _spawnEnemyDelay;
        }
       
    }


    void checkDist()
    {
        _distFromPlayer = Vector3.Distance(transform.position, _player.transform.position);
        _distFromSpawner = Vector3.Distance(transform.position, _enemy.transform.position);
        _enemyDist = Vector3.Distance(_enemy.transform.position, _player.transform.position);

    }


    void activate()
    {
        if(_distFromPlayer <= _activationDist)
        {
            _enemy.SetActive(true);
            active = true;
        }
        else if(_distFromPlayer > _activationDist)
        {
            if((!chasing) && (home == true))
            {
                active = false;
                _enemy.SetActive(false);
            }
            
        }
    }


    void goBack()
    {
        if ((_distFromSpawner >= _maxDist) && (_enemyDist >= _maxChaseDist))
        {
            returning = true;
            chasing = false;
           
        }
    }


    IEnumerator spawnDelay()
    {
        yield return new WaitForSeconds(_spawnEnemyDelay);
        if(_enemy == null && spawnedNewEnemy == false)
        {
            _enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity, transform);
            spawnedNewEnemy = true;
            isdead = false;

        }

    }

    void spawnEnemy()
    {

    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _activationDist);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _maxDist);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _maxChaseDist);
    }
}
