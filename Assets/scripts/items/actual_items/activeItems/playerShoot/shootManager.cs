using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootManager : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] private float speed;
    private playerItemManager _PIM;
    [SerializeField] private active_items _AI;
    private bool _hasShoot;
    private bool _showIndicator;

    [SerializeField] private float _speed;
    [SerializeField] private float _rotationModifirt;
    [SerializeField] private GameObject _itemIndicator;
    // Start is called before the first frame update
    void Start()
    {
        _PIM = GameObject.FindGameObjectWithTag("Player").GetComponent<playerItemManager>();
        _AI = gameObject.GetComponent<active_items>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_AI.getCurrentStatus() == true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                _showIndicator = false;

                if (_PIM.getItemStatus() == true)
                {
                    if (!_hasShoot)
                    {
                        _showIndicator = false;
                        shoot();
                        _hasShoot = true;
                        StartCoroutine("shootWait");
                        _PIM.useItemCharge();
                        
                    }





                }
            }
            else if (Input.GetMouseButtonDown(0))
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
        setindicator();
    }

    void shoot()
    {
        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
        bullet.transform.parent = gameObject.transform;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 dir = (worldPosition - gameObject.transform.position).normalized;
        
        Vector2 velocity = dir * speed;
        //rb.velocity = velocity;
        bullet.GetComponent<Rigidbody2D>().velocity = velocity;

        
    }


    void setindicator()
    {
        if (_showIndicator)
        {
            _itemIndicator.SetActive(true);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            //Vector3 dir = (worldPosition - gameObject.transform.position).normalized;
            float angle = Mathf.Atan2(worldPosition.y, worldPosition.x) * Mathf.Rad2Deg - _rotationModifirt;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _speed);
        }
        else if(!_showIndicator)
        {
            _itemIndicator.SetActive(false);
        }

    }

    private IEnumerator shootWait()
    {
        _hasShoot = true;
        yield return new WaitForSeconds(5f);
        _hasShoot = false;
    }
}
