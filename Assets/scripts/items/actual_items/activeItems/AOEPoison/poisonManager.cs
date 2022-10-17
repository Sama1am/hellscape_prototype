using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poisonManager : MonoBehaviour
{
    [SerializeField] private GameObject _poisonObject;

    private GameObject _body;
    private bool hasSpawned;
    private playerItemManager _PIM;
    private active_items _AI;
    // Start is called before the first frame update
    void Start()
    {
        _AI = GetComponent<active_items>();
        _PIM = GameObject.FindGameObjectWithTag("Player").GetComponent<playerItemManager>();
        _body = GameObject.FindGameObjectWithTag("Body");
    }

    // Update is called once per frame
    void Update()
    {
        if(_AI.getCurrentStatus() == true)
        {
            if (Input.GetMouseButton(0))
            {
                if (_PIM.getItemStatus() == true)
                {

                    if (!hasSpawned)
                    {
                        Instantiate(_poisonObject, _body.transform.position, Quaternion.identity);
                        hasSpawned = true;
                        StartCoroutine("spawnWait");
                        _PIM.useItemCharge();

                    }
                }

            }
        }
        
    }


    public void setSpawnedStatus(bool status)
    {
        hasSpawned = status;
    }

    private IEnumerator spawnWait()
    {
        hasSpawned = true;
        yield return new WaitForSeconds(5f);
        hasSpawned = false;
    }
}
