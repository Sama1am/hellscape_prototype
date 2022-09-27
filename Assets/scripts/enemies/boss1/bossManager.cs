using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bossManager : MonoBehaviour
{

    [SerializeField]
    public float currentHealth;

    [SerializeField]
    public float health;

    [SerializeField]
    public float damage;

    GameObject target;

    public bool bossHasSpawned;

    public AudioClip clips;

    public AudioSource audioSource;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        currentHealth = health;
        bossHasSpawned = true;

        try
        {
            audioSource.clip = clips;
            audioSource.Play();
            audioSource.loop = true;
        }
        catch
        {

        }
       

        
    }

    // Update is called once per frame
    void Update()
    {

        checkStage();
        
    }

    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        checkStage();

        if (currentHealth <= 0)
        {
            win();
            die();
        }
    }

    private void die()
    {
        Destroy(gameObject);
    }

    void win()
    {
        SceneManager.LoadScene(3);
    }


    public void checkStage()
    {
        if (currentHealth <= health / 2)
        {
            gameObject.GetComponent<bossMovement>().endStage = true;
            
        }
        else if (currentHealth > health / 2)
        {
            
            gameObject.GetComponent<bossMovement>().endStage = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
            collision.gameObject.GetComponent<playerManager>().takeDamage(damage);
        }

        if(collision.gameObject.CompareTag("obstacle"))
        {
            rb.velocity = Vector2.zero;
            gameObject.GetComponent<bossMovement>().canMove = false;
        }
    }

}
