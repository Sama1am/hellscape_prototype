using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropManager : MonoBehaviour
{
    enemyManager _EM;
    public ItemManager _itemManager;
    private int _chance;
    private int _itemChance;
    private int _common;
    private int _rare;
    private int _holyShit;

    private bool _hasDropped;
    [SerializeField] private bool _isBoss;
    [SerializeField] private bool drop;

    private bool spanwedHeal = false;
    // Start is called before the first frame update
    void Start()
    {
        _hasDropped = false;
        _EM = gameObject.GetComponent<enemyManager>();
        _itemManager = GameObject.FindGameObjectWithTag("itemManager").GetComponent<ItemManager>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void determineDrop()
    {

        if((_isBoss) && (!_hasDropped))
        {
            _holyShit = Random.Range(0, _itemManager._holyshitRareItems.Length);
            Instantiate(_itemManager._holyshitRareItems[_holyShit], gameObject.GetComponentInParent<Transform>().position, Quaternion.identity);
            if(!spanwedHeal)
            {
                Instantiate(_itemManager.heal, transform.position, Quaternion.identity);
                Instantiate(_itemManager.heal, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
                Instantiate(_itemManager.heal, transform.position + new Vector3(3, 2, 0), Quaternion.identity);
                Instantiate(_itemManager.heal, transform.position + new Vector3(-1, -2, 0), Quaternion.identity);
                spanwedHeal = true;
            }

            _hasDropped = true;
        }
        else if (!_hasDropped)
        {
            //Debug.Log("SHOULD DETERMINE DROP!");

            _itemChance = Random.Range(0, 101);
            //Debug.Log("ITEM DROP CHANCE IS " + _itemChance);

            if (_itemChance <= 50)
            {
                // return;
            }
            else if (_itemChance > 50)
            {
                _chance = Random.Range(0, 101);
                //Debug.Log(_chance);

                if(_chance <= 50)
                {

                    _common = Random.Range(0, _itemManager._commonItems.Length);
                    //Debug.Log(_itemManager._commonItems.Length);
                    //Debug.Log(_common);
                    Instantiate(_itemManager._commonItems[_common], transform.position, Quaternion.identity);
                    _hasDropped = true;
                    _common = 0;
                }
                else if(_chance > 50 && _chance < 80)
                {
                    _rare = Random.Range(0, _itemManager._rareItems.Length);
                    //Debug.Log(_itemManager._rareItems.Length);
                    //Debug.Log(_rare);
                    Instantiate(_itemManager._rareItems[_rare], transform.position, Quaternion.identity);
                    _hasDropped = true;
                }
                else if (_chance > 80)
                {
                    _holyShit = Random.Range(0, _itemManager._holyshitRareItems.Length);
                    //Debug.Log(_itemManager._holyshitRareItems.Length);
                    //Debug.Log(_holyShit);
                    Instantiate(_itemManager._holyshitRareItems[_holyShit], transform.position, Quaternion.identity);
                    _hasDropped = true;
                }
            }



        }



    }



}
