using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawner : MonoBehaviour
{

    public GameObject _enemyPrefab;
    public GameObject _enemy;
    [SerializeField] private float _distFromPlayer;
    [SerializeField] private float _distFromSpawner;
    [SerializeField] private float _enemyDist;
    private GameObject _player;

    [SerializeField] private float _activationDist;
    [SerializeField] private float _maxDist;
    [SerializeField] private float _maxChaseDist;
    public bool home;
    // Start is called before the first frame update
    void Start()
    {
        _enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity, transform);
        _player = GameObject.FindGameObjectWithTag("Body");
    }

    // Update is called once per frame
    void Update()
    {
        checkDist();

        activate();

        goBack();

        if(_enemy.transform.position == transform.position && !home)
        {
            _enemy.GetComponent<enemyMovement>().targetPos = _enemy.GetComponent<enemyMovement>().player;
            _enemy.GetComponent<enemyMovement>().returning = false;
            home = true;
        }

        if(_enemy.transform.position != transform.position)
        {
            home = false;
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
            _enemy.GetComponent<enemyMovement>().active = true;
        }
        else if(_distFromPlayer > _activationDist)
        {
            if((!_enemy.GetComponent<enemyMovement>().chasing) && (home == true))
            {
                _enemy.GetComponent<enemyMovement>().active = false;
                _enemy.SetActive(false);
            }
            
        }
    }


    void goBack()
    {
        if ((_distFromSpawner >= _maxDist) && (_enemyDist >= _maxChaseDist))
        {
            _enemy.GetComponent<enemyMovement>().returning = true;
            _enemy.GetComponent<enemyMovement>().chasing = false;
            _enemy.GetComponent<enemyMovement>().targetPos = gameObject.transform;
        }
    }

}
