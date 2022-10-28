using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemyManager : MonoBehaviour
{
    [SerializeField] private bool simpleEnemy;
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;
    [SerializeField] private float knockBackForce;
    [SerializeField] private bool isdead;
    [SerializeField] private Slider _enemyHealthBar;
    [SerializeField] private GameObject _enemyUI;
    private bool _UIActive;

    private GameObject _player;
    private SpriteRenderer _sp;
    private Color ogColor;
    private bool _stunned;

    private Rigidbody2D _rb;
    private dropManager _DM;
    private SpriteRenderer _SR;
    // Start is called before the first frame update
    void Start()
    {
        _SR = GetComponentInChildren<SpriteRenderer>();
        isdead = false;
        _currentHealth = _maxHealth;
        _DM = gameObject.GetComponent<dropManager>();
        _sp = GetComponentInChildren<SpriteRenderer>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Body");
        ogColor = _sp.color;
        _enemyHealthBar.maxValue = _maxHealth;
        _enemyUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        flipSprite();
        setUI();

        if(_UIActive)
        {
            _enemyUI.SetActive(true);
        }
    }


    private void setUI()
    {
        _enemyHealthBar.value = _currentHealth;
    }

    public void takeDamage(float dam)
    {
       
        _currentHealth -= dam;
        StartCoroutine("changeColour");

        if (!_UIActive)
        {
            _UIActive = true;
        }

        if (_currentHealth <= 0)
        {
            isdead = true;
            
            GetComponent<dropManager>().determineDrop();
            die();
           
        }
    }

    public bool checkDead()
    {
        return isdead;
    }

    public void setDeadStatus(bool status)
    {
        isdead = status;
    }

    public void die()
    {
        if (!simpleEnemy)
        {
            //_DM.determineDrop();
            Destroy(gameObject);
            GetComponentInParent<enemy_spawner>().setDeadStatus(true);
            GetComponentInParent<enemy_spawner>().isdead = true;
            GetComponentInParent<enemy_spawner>().spawnedNewEnemy = false;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void knockBack()
    {
        Vector2 difference = transform.position - _player.transform.position;
        Vector2 diff = difference * 1.2f;
        transform.position = new Vector2(transform.position.x + diff.x, transform.position.y + diff.y);
       // rb.AddForce(difference.x, difference.y, 0);

        _stunned = true;

    }

    public void knockBackPlayer()
    {
        Vector2 difference = transform.position - _player.transform.position;
        _rb.AddForce(-difference * knockBackForce, ForceMode2D.Impulse);
    }

    public void setStunStatus(bool status)
    {
        _stunned = status;
    }

    public bool checkStunStatus()
    {
        return _stunned;
    }

    private void flipSprite()
    {
        if(_rb.velocity.x > 0)
        {
            
            _SR.flipX = false;
        }
        else if (_rb.velocity.x < 0)
        {
            
            _SR.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine("velocityDelay");

        if (collision.gameObject.CompareTag("Body"))
        {
            _rb.velocity = Vector2.zero;
            if((collision.gameObject.GetComponent<bodyController>().isAttacking() == false) && (_stunned == false))
            {
                collision.gameObject.GetComponent<playerManager>().takeDamage(_damage);
                //knockBackPlayer();
                knockBack();
                StartCoroutine("velocityDelay");


            }
            else if(collision.gameObject.GetComponent<bodyController>().attacking == true)
            {
                collision.gameObject.GetComponent<playerManager>().takeDamage(_damage / collision.gameObject.GetComponent<playerManager>().damageTakenOffset);
                _rb.velocity = Vector2.zero;
                //StartCoroutine("velocityDelay");
                //knockBack();
                StartCoroutine("velocityDelay");
            }
            
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            _rb.velocity = Vector2.zero;
            StartCoroutine("velocityDelay");
        }
    }

    private IEnumerator velocityDelay()
    {
        _rb.velocity = Vector2.zero;
        //Debug.Log("VELOCITY DELAY!");
        _rb.mass = 2;
        _rb.drag = 2f;
        yield return new WaitForSeconds(2);
        _rb.drag = 0;
        _rb.mass = 1;
        _rb.velocity = Vector2.zero;

    }

    private IEnumerator changeColour()
    {
        _sp.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        // _sp.color = Color.white;
        _sp.color = ogColor;
    }
}
