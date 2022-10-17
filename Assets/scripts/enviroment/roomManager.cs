using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemySpawners;
    [SerializeField] private GameObject[] _doors;

    private bool _allEnemiesDead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkRoomStatus();
    }


    void checkRoomStatus()
    {
        for (int i = 0; i < _enemySpawners.Length; i++)
        {
            if(_enemySpawners[i].GetComponent<enemy_spawner>().checkDeadStatus() == true)
            {
                _allEnemiesDead = true;
            }
        }


        if (_allEnemiesDead)
        {
            for (int i = 0; i < _doors.Length; i++)
            {
                _doors[i].SetActive(false);
            }
        }
        else if (!_allEnemiesDead)
        {
            for (int i = 0; i < _doors.Length; i++)
            {
                _doors[i].SetActive(true);
            }
        }
    }
}
