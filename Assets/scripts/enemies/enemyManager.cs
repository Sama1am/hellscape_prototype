using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{

    public float health;
    public float damage;
    public float knockBackForce;
    Rigidbody2D rb;
    public float force;

    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Body");
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.x > 2 || rb.velocity.y > 2 || rb.velocity.x > -2 || rb.velocity.y > -2)
        {
            rb.velocity = Vector2.zero;
            Debug.Log("velocity is high!");
        }
    }


    public void takeDamage(float dam)
    {
        health -= dam;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }


    void knockBack()
    {
        //Debug.Log("SHOULD KNOCK BACK!");
        Vector2 direction = (transform.position - _player.transform.position).normalized;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
       // Debug.Log("complete");

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
                
                collision.gameObject.GetComponent<playerManager>().takeDamage(damage);
                knockBack();
                Debug.Log("Should knock back!");
            }
            else if(collision.gameObject.GetComponent<bodyController>().isAttacking == true)
            {
                StartCoroutine("velocityDelay");
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
