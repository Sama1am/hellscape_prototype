using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossManager : MonoBehaviour
{
   // [SerializeField] private Slider _healthSlider;
    [SerializeField] private float _maxHealth;
    public float currentHeaalth;
    public bool stageOne, stageTwo;

    private GameObject _player;
    [SerializeField] private float _damage;
    public bool isdead;

    SpriteRenderer sr;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Body");
        sr = GetComponent<SpriteRenderer>();
        currentHeaalth = _maxHealth;
        rb = GetComponent<Rigidbody2D>();
       // GetComponentInParent<bossSpawner>()._bossSlider.minValue = 0;
       // GetComponentInParent<bossSpawner>()._bossSlider.maxValue = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
       // GetComponentInParent<bossSpawner>()._bossSlider.value = currentHeaalth;
        if (currentHeaalth > _maxHealth / 2)
        {
            stageOne = true;
            stageTwo = false;
        }
        if (currentHeaalth <= _maxHealth / 2)
        {
            sr.color = Color.red;
            stageTwo = true;
            stageOne = false;
        }

        if(currentHeaalth <= 0)
        {
            Debug.Log("spawned key!");
            Destroy(gameObject);
        }
    }


    public void takeDamage(float dam)
    {
        currentHeaalth -= dam;
    }

    void knockBack()
    {
        Vector2 difference = transform.position - _player.transform.position;
        Vector2 diff = difference * 1.5f;
        transform.position = new Vector2(transform.position.x + diff.x, transform.position.y + diff.y);
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Body"))
        {
            rb.velocity = Vector2.zero;
            if ((collision.gameObject.GetComponent<bodyController>().attacking == false))
            {
                collision.gameObject.GetComponent<playerManager>().takeDamage(_damage);
                rb.velocity = Vector2.zero;
                StartCoroutine("velocityDelay");
                //knockBack();
            }
            else if (collision.gameObject.GetComponent<bodyController>().attacking == true)
            {
                collision.gameObject.GetComponent<playerManager>().takeDamage(_damage / 6);
                rb.velocity = Vector2.zero;
                StartCoroutine("velocityDelay");
            }


        }

        if (collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
        }
    }


    private IEnumerator velocityDelay()
    {
        rb.velocity = Vector2.zero;
        Debug.Log("VELOCITY DELAY!");
        rb.mass = 2;
        rb.drag = 2f;
        yield return new WaitForSeconds(2);
        rb.drag = 0;
        rb.mass = 1;
        rb.velocity = Vector2.zero;

    }
}
