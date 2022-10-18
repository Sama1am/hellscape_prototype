using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemySpawners;
    [SerializeField] private GameObject[] _doors;
    [SerializeField] private bool _allEnemiesDead;

    [SerializeField] private float _numOfEnemies;

    [SerializeField] private float _X;
    [SerializeField] private float _Y;
    [SerializeField] public List<GameObject> _enemiesInRoom = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _enemySpawners.Length; i++)
        {
            _enemySpawners[i].GetComponent<enemy_spawner>()._currentRoomManager = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        checkenemies();
    }

    void checkenemies()
    {
        if(_enemiesInRoom.Count <= 0)
        {
            for (int i = 0; i < _doors.Length; i++)
            {
                _doors[i].SetActive(false); ;
            }
        }
        else if(_enemiesInRoom.Count >= 1)
        {
            for (int i = 0; i < _doors.Length; i++)
            {
                _doors[i].SetActive(true);
            }
        }
    }

   
}
