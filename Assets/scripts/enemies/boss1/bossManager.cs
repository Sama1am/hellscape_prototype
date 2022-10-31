using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class bossManager : MonoBehaviour
{
   //[SerializeField] private Slider _healthSlider;
    [SerializeField] public float _maxHealth;
    public float currentHeaalth;
    public bool stageOne, stageTwo;

    private GameObject _player;
    [SerializeField] private float _damage;
    public bool isdead;

    [SerializeField] private bool _attack;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Collider2D _collider;
    Rigidbody2D rb;
    public bossSpawner _BS;

    [SerializeField] private bool _isfinalBoss;
    //[SerializeField] private GameObject[] _door;
    [SerializeField] private bool _telaport;
    [SerializeField] private GameObject _teleporter;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private Slider _bossHealthSlider;
    // Start is called before the first frame update
    void Start()
    {
        _bossHealthSlider.maxValue = _maxHealth;
        _player = GameObject.FindGameObjectWithTag("Body");
        sr = gameObject.GetComponent<SpriteRenderer>();
        currentHeaalth = _maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        setUI();
        if(currentHeaalth > _maxHealth / 2)
        {
            stageOne = true;
            stageTwo = false;
        }
        if(currentHeaalth <= _maxHealth / 2)
        {
            sr.color = Color.red;
            stageTwo = true;
            stageOne = false;
        }

        if(currentHeaalth <= 0)
        {
            die();
        }
    }
    void die()
    {
        isdead = true;
        _attack = false;
        //_door.SetActive(false);
        _BS.isdead = true;
        _healthBar.SetActive(false);

        if(_telaport)
        {
            _teleporter.SetActive(true);
        }

        Destroy(gameObject);

        if(_isfinalBoss)
        {
            SceneManager.LoadScene(2);
        }
    }

    private void setUI()
    {
        _bossHealthSlider.value = currentHeaalth;
    }

    public bool attackStatus()
    {
        return _attack;
    }

    public void setAttackStatus(bool attack)
    {
        _attack = attack;
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

    private IEnumerable deathDelay()
    {
        _collider.enabled = false;
        sr.enabled = false;
        yield return new WaitForSeconds(0.5f);
        if (_isfinalBoss)
        {
            SceneManager.LoadScene(2);
        }
        Destroy(gameObject);
        
    }
}
