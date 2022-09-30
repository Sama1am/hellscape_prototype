using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossManager : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private float _maxHealth;
    public float currentHeaalth;
    public bool stageOne, stageTwo;

    [SerializeField ]private float _damage;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        currentHeaalth = _maxHealth;
        rb = GetComponent<Rigidbody2D>();
        _healthSlider.minValue = 0;
        _healthSlider.maxValue = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        _healthSlider.value = currentHeaalth;
        if (currentHeaalth > _maxHealth / 2)
        {
            stageOne = true;
            stageTwo = false;
        }
        if (currentHeaalth <= _maxHealth / 2)
        {
            stageTwo = true;
            stageOne = false;
        }
    }


    public void takeDamage(float dam)
    {
        currentHeaalth -= dam;

        if(currentHeaalth <= 0)
        {
            _healthSlider.value = 0;
            Destroy(_healthSlider);
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Body"))
        {
            rb.velocity = Vector2.zero;
            if ((collision.gameObject.GetComponent<bodyController>().isAttacking == false))
            {
                collision.gameObject.GetComponent<playerManager>().takeDamage(_damage);
            }
            else if (collision.gameObject.GetComponent<bodyController>().isAttacking == true)
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
