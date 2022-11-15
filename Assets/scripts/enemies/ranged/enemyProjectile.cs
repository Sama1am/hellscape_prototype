using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float lowerDam;
    [SerializeField] private ParticleSystem _effect;

    [SerializeField]
    private float lifeTime;

    [SerializeField] private bool _lowerDamage;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //damage = gameObject.GetComponentInParent<EnemyManager>().damage;
        rb = GetComponent<Rigidbody2D>();
        if(_lowerDamage)
        {
            damage = lowerDam;
        }
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
            Instantiate(_effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Body"))
        {
            Instantiate(_effect, transform.position, Quaternion.identity);
            if ((collision.gameObject.GetComponent<bodyController>().isAttacking() == false))
            {
                Debug.Log("PLAYER TOOK DAMAGE" + damage);
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
            Instantiate(_effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void die()
    {
        Destroy(gameObject);
    }
}
