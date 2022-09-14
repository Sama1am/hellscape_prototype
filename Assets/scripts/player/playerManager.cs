using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{

    [SerializeField] public float damage;
    [SerializeField] public float health;


    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
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
            //gameOver
        }
    }

    public void checkHealth()
    {
        if(health <= 0)
        {
            //gameOver
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            _rb.velocity = Vector2.zero;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.GetComponent<enemyManager>().takeDamage(damage);
        }
    }

}
