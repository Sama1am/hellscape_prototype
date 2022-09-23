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
    private SpriteRenderer _sp;
    private Color ogColor;
    public bool stunned;

    // Start is called before the first frame update
    void Start()
    {
        _sp = GetComponentInChildren<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Body");
        ogColor = _sp.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void takeDamage(float dam)
    {
        health -= dam;
        StartCoroutine("changeColour");

        if(health <= 0)
        {
            Destroy(gameObject);
            GetComponentInParent<enemy_spawner>().isdead = true;
            GetComponentInParent<enemy_spawner>().spawnedNewEnemy = false;
        }
    }


    void knockBack()
    {
        Vector2 difference = transform.position - _player.transform.position;
        Vector2 diff = difference * 1.5f;
        transform.position = new Vector2(transform.position.x + diff.x, transform.position.y + diff.y);
        stunned = true;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Body"))
        {
            rb.velocity = Vector2.zero;
            if(collision.gameObject.GetComponent<bodyController>().isAttacking == false)
            {
                collision.gameObject.GetComponent<playerManager>().takeDamage(damage);
                knockBack();

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

    private IEnumerator changeColour()
    {
        _sp.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        // _sp.color = Color.white;
        _sp.color = ogColor;
    }
}
