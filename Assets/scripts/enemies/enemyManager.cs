using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{

    public float health;
    public float damage;
    public float knockBackForce;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void takeDamage(float dam)
    {
        health -= dam;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Body"))
        {
            rb.velocity = Vector2.zero;
            StartCoroutine("velocityDelay");
            if (collision.gameObject.GetComponent<bodyController>().isAttacking == false)
            {
                //GetComponent<enemyMovement>().knockBackPlayer();
                //takeDamage(collision.gameObject.GetComponent<playerManager>().damage);
                collision.gameObject.GetComponent<playerManager>().takeDamage(damage);
            }
            

        }

        if(collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
        }
    }


    private IEnumerator velocityDelay()
    {
        rb.mass = 2;
        rb.drag = 0.5f;
        yield return new WaitForSeconds(2);
        rb.drag = 0;
        rb.mass = 1;
        
    }

}
