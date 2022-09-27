using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropManager : MonoBehaviour
{
    enemyManager _EM;
    ItemManager _itemManager;
    private int _chance;
    private int _itemChance;
    private int _common;
    private int _rare;
    private int _holyShit;

    private bool _hasntDropped;

    // Start is called before the first frame update
    void Start()
    {
        _EM = gameObject.GetComponent<enemyManager>();
        _itemManager = GameObject.FindGameObjectWithTag("itemManager").GetComponent<ItemManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void determineDrop()
    {
        if(_hasntDropped)
        {

            _itemChance = Random.Range(0, 101);

            if(_itemChance <= 65)
            {
                return;
            }
            else if(_itemChance > 65)
            {
                _chance = Random.Range(0, 101);

                if (_chance <= 60)
                {
                    _common = Random.Range(0, _itemManager._commonItems.Length + 1);
                    Instantiate(_itemManager._commonItems[_common], transform.position, Quaternion.identity);
                    _hasntDropped = false;
                }
                else if (_chance > 60 && _chance < 80)
                {
                    _rare = Random.Range(0, _itemManager._rareItems.Length + 1);
                    Instantiate(_itemManager._rareItems[_rare], transform.position, Quaternion.identity);
                    _hasntDropped = false;
                }
                else if (_chance > 80 && _chance <= 100)
                {
                    _holyShit = Random.Range(0, _itemManager._holyshitRareItems.Length + 1);
                    Instantiate(_itemManager._holyshitRareItems[_holyShit], transform.position, Quaternion.identity);
                    _hasntDropped = false;
                }
            }
            
        }
       

    }



}
