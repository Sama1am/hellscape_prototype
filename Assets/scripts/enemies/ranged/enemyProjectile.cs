using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    [SerializeField] private float damage;

    [SerializeField]
    private float lifeTime;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //damage = gameObject.GetComponentInParent<EnemyManager>().damage;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;

        if(lifeTime <= 0)
        {
            die();
        }

       if((rb.velocity.x == 0) && (rb.velocity.y == 0))
       {
            Destroy(gameObject);
       }
       
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<playerManager>().takeDamage(damage);
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Body"))
        {
            if ((collision.gameObject.GetComponent<bodyController>().isAttacking() == false))
            {
                collision.gameObject.GetComponent<playerManager>().takeDamage(damage);
                Destroy(gameObject);
            }
            else if((collision.gameObject.GetComponent<bodyController>().isAttacking() == true))
            {
                collision.gameObject.GetComponent<playerManager>().takeDamage(damage / collision.gameObject.GetComponent<playerManager>().damageTakenOffset);
                Destroy(gameObject);
            }

        }
        else if(collision.gameObject.CompareTag("obstacle"))
        {
            Destroy(gameObject);
        }
    }

    void die()
    {
        Destroy(gameObject);
    }
}
