using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerManager : MonoBehaviour
{

    [Header("set")]
    [SerializeField] public float health;
    public float falseHeart;
    public bool hasFalseHeart;

    [Header("do not set")]
    [SerializeField] public float damage;
    public float currentHealth;
    [SerializeField] public float damageTakenOffset;
    [SerializeField] private Slider playerHealth;

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
        setUI();
    }

    public void takeDamage(float dam)
    {
        //if(hasFalseHeart)
        //{
        //    falseHeart -= dam;

        //    if(falseHeart <= 0)
        //    {
        //        hasFalseHeart = false;
        //        falseHeart = 0;
        //    }
        //}
        currentHealth -= dam;
        StartCoroutine("changeColour");
        //Debug.Log("THE PLAYER TOOK " + dam + "DAMAGE!");

        checkHealth();
    }

    public void checkHealth()
    {
        if(currentHealth <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            _rb.velocity = Vector2.zero;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //collision.gameObject.GetComponent<enemyManager>().takeDamage(damage);
        }
    }

    private IEnumerator changeColour()
    {
        _sp.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        //_sp.color = Color.white;
        _sp.color = ogColor;
    }


    void setUI()
    {
        playerHealth.maxValue = health;
        playerHealth.value = currentHealth;
    }

}
