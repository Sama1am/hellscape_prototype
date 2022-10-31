using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poisonManager : MonoBehaviour
{
    [SerializeField] private GameObject _poisonObject;
    [SerializeField] private GameObject _objectIndicator;
    private bool _showIndicator;
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
            if(Input.GetMouseButtonUp(0))
            {

                if(_PIM.getItemStatus() == true)
                {

                    if(!hasSpawned)
                    {
                        Instantiate(_poisonObject, _body.transform.position, Quaternion.identity);
                        hasSpawned = true;
                        StartCoroutine("spawnWait");
                        _PIM.useItemCharge();
                        _showIndicator = false;
                        _objectIndicator.SetActive(false);

                    }
                }

                _objectIndicator.SetActive(false);
            }
            else if(Input.GetMouseButtonDown(0))
            {
                if (_PIM.getItemStatus() == true)
                {
                    _showIndicator = true;
                }
                    
            }



        }
        
    }

    private void FixedUpdate()
    {
        showIndicator();
    }

    private void showIndicator()
    {
        if(_showIndicator)
        {
            _objectIndicator.transform.position = _body.transform.position;
            _objectIndicator.SetActive(true);
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
