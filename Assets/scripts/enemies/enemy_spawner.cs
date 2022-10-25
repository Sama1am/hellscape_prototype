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
    [SerializeField] public float _maxDistFromSpawner;
    //[SerializeField] private float _maxChaseDist;
    #endregion

    private GameObject _player;
    [SerializeField] private float _spawnEnemyDelay;
    public Transform _EStransform;
    private float _currentTime;

    public GameObject _currentRoomManager;
    [SerializeField] private bool _roomManage;
    [SerializeField] private bool _respawn;
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
        _EStransform = gameObject.transform;
        _enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity, transform);
        if(_roomManage)
        {
            _currentRoomManager.GetComponent<roomManager>()._enemiesInRoom.Add(_enemy);
        }
        isdead = false;
        _player = GameObject.FindGameObjectWithTag("Body");
        _currentTime = _spawnEnemyDelay;
    }

    // Update is called once per frame
    void Update()
    {

        changeStatus();
    }

    private void changeStatus()
    {
        if (_enemy != null)
        {
            if ((isdead == false))
            {
                checkDist();

                //activate();

                //goBack();


                if (_enemy.transform.position != transform.position)
                {
                    home = false;
                }
            }
        }


        if ((isdead == true) && (_enemy == null) && (!spawnedNewEnemy))
        {
            _currentTime -= Time.deltaTime;
            if (_roomManage)
            {
                _currentRoomManager.GetComponent<roomManager>()._enemiesInRoom.Remove(_enemy);
            }

        }

        if ((_currentTime <= 0) && (_enemy == null) && (!spawnedNewEnemy))
        {

            if(_respawn)
            {
                _enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity, transform);
                spawnedNewEnemy = true;
                _enemy.GetComponent<enemyManager>().setDeadStatus(false);
                _currentTime = _spawnEnemyDelay;
            }

        }
    }

    void checkDist()
    {
        _distFromPlayer = Vector3.Distance(transform.position, _player.transform.position);
        _distFromSpawner = Vector3.Distance(transform.position, _enemy.transform.position);
        _enemyDist = Vector3.Distance(_enemy.transform.position, _player.transform.position);

    }


    public void setActive()
    {
        if(!isdead)
        {
            _enemy.SetActive(true);
            active = true;
        }
        
    }

    public void setDeactive()
    {
        active = false;
        _enemy.SetActive(false);
    }

    void activate()
    {
        //if(_distFromPlayer <= _activationDist)
        //{
        //    _enemy.SetActive(true);
        //    active = true;
        //}
        
        
        if(_distFromPlayer > _activationDist)
        {
            if((!chasing) && (home == true))
            {
                active = false;
                _enemy.SetActive(false);
            }
            
        }
    }

    public void goBack()
    {
        //if((_distFromSpawner >= _maxDistFromSpawner)) 
        //{
        //    returning = true;
        //    chasing = false;
           
        //}

        returning = true;
        chasing = false;
    }

    public void setDeadStatus(bool status)
    {
        isdead = status;
    }

    public bool checkDeadStatus()
    {
        return isdead;
    }

    IEnumerator spawnDelay()
    {
        yield return new WaitForSeconds(_spawnEnemyDelay);
        if(_enemy == null && spawnedNewEnemy == false)
        {
            _enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity, transform);
            spawnedNewEnemy = true;
            _enemy.GetComponent<enemyManager>().setDeadStatus(false);
            isdead = false;

        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _activationDist);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _maxDistFromSpawner);
        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, _maxChaseDist);
    }
}
