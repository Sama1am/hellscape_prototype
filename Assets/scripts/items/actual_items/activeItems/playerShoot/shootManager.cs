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
                
                
                if (_PIM.getItemStatus() == true)
                {
                    if (!_hasShoot)
                    {
                        shoot();
                        _hasShoot = true;
                        StartCoroutine("shootWait");
                        _PIM.useItemCharge();
                        
                    }





                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
               
                   
            }
        }
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

    private IEnumerator shootWait()
    {
        _hasShoot = true;
        yield return new WaitForSeconds(5f);
        _hasShoot = false;
    }
}
