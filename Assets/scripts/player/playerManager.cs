using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{

    [Header("set")]
    [SerializeField] public float health;


    [Header("do not set")]
    [SerializeField] public float damage;
    public float currentHealth;


    private Rigidbody2D _rb;
    private SpriteRenderer _sp;
    private Color ogColor;
    // Start is called before the first frame update
    void Start()
    {
        _sp = GetComponentInChildren<SpriteRenderer>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        ogColor = _sp.color;
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float dam)
    {
        currentHealth -= dam;
        StartCoroutine("changeColour");


        if(currentHealth <= 0)
        {
            //gameOver
        }
    }

    public void checkHealth()
    {
        if(currentHealth <= 0)
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

    private IEnumerator changeColour()
    {
        _sp.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        //_sp.color = Color.white;
        _sp.color = ogColor;
    }

}
