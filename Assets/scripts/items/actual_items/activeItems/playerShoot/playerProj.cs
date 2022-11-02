using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProj : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float lowerDam;

    [SerializeField]
    private float lifeTime;

    [SerializeField] private bool _lowerDamage;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //damage = gameObject.GetComponentInParent<EnemyManager>().damage;
        rb = GetComponent<Rigidbody2D>();
        if (_lowerDamage)
        {
            damage = lowerDam;
        }
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            die();
        }

        if ((rb.velocity.x == 0) && (rb.velocity.y == 0))
        {
            Destroy(gameObject);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            collision.gameObject.GetComponent<enemyManager>().takeDamage(damage);
            Destroy(gameObject);
        }

        if((collision.gameObject.CompareTag("Boss1")) || (collision.gameObject.CompareTag("FinalBoss")))
        {
            collision.gameObject.GetComponent<bossManager>().takeDamage(damage);
            Destroy(gameObject);
        }
    }

    void die()
    {
        Destroy(gameObject);
    }
}
