using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombManager : MonoBehaviour
{
    [SerializeField] private GameObject _bombObject;
    [SerializeField] private GameObject _itemIndicator;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationModifirt;
    private GameObject _liveBomb;
    [SerializeField] private GameObject _parent;
    private Vector3 dir;
    private Vector3 worldPosition;
    private bool _showIndicator;
    [SerializeField] private float _spawnRadisu;
    private active_items _AI;
    private playerItemManager _PIM;
    // Start is called before the first frame update
    void Start()
    {
        _PIM = GameObject.FindGameObjectWithTag("Player").GetComponent<playerItemManager>();
        _AI = GetComponent<active_items>();
        _parent = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        dir = (worldPosition - gameObject.transform.position);

        if(_AI.getCurrentStatus() == true)
        {
            if(Input.GetMouseButtonUp(0))
            {
                _showIndicator = false;
                _itemIndicator.SetActive(false);
                if (_PIM.getItemStatus() == true)
                {
                    spawnInRadius();
                    _PIM.useItemCharge();
                }
                
            }
            else if (Input.GetMouseButtonDown(0))
            {
                if (_PIM.getItemStatus() == true)
                {
                    _showIndicator = true;
                }
                    
            }

            //if(Input.GetMouseButtonDown(0))
            //{
            //    _itemIndicator.SetActive(true);
            //    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            //    //Vector3 dir = (worldPosition - gameObject.transform.position).normalized;
            //    float angle = Mathf.Atan2(worldPosition.y, worldPosition.x) * Mathf.Rad2Deg - _rotationModifirt;
            //    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            //    transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _speed);
            //}
        }

        

    }

    private void FixedUpdate()
    {
        setindicator();
    }

    void setindicator()
    {
        if(_showIndicator)
        {
            _itemIndicator.SetActive(true);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            //Vector3 dir = (worldPosition - gameObject.transform.position).normalized;
            float angle = Mathf.Atan2(worldPosition.y, worldPosition.x) * Mathf.Rad2Deg - _rotationModifirt;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _speed);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _spawnRadisu);

    }
    void spawnInRadius()
    {
        //spawns the bomb in the direction of the mouse but wihtin a certain redius from the player
        Vector3 direction = dir;
        direction = Vector3.ClampMagnitude(direction, _spawnRadisu);
        _liveBomb = Instantiate(_bombObject, transform.position + direction, Quaternion.identity);
    }
   
}
