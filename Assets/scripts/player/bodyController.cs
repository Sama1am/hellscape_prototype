using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyController : MonoBehaviour
{
    #region Movement stuff
    [Header("Movement Stuff")]
    private float _horizontal;
    private float _vertical;
    private bool _changeDir;
    [SerializeField] private float _dragForce;
    #endregion

    #region Throw body stuff
    [Header("throw body stuff")]
    [SerializeField] public float multiplyer;
    [SerializeField] private float _time;
    [SerializeField] private float _timeEleapsed;
    [SerializeField] private float _speed;
    #endregion

    private float _dam;
    public bool isAttacking;
    public bool bodyhit;

    #region crit
    public int critChance;
    #endregion

    private GameObject _player;
    private Rigidbody2D _rb;
    private playerManager _PM;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _PM = gameObject.GetComponent<playerManager>();
        //dam = _PM.damage;
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        changeDirection();
        reelIn();
        shootOut();
    }

    private void FixedUpdate()
    {
        //drag();

    }

    private void drag()
    {
        if ((_horizontal == 0) || (_changeDir) || (_vertical == 0))
        {
            _rb.drag = _dragForce;
        }
    }


    private void changeDirection()
    {
        if ((_rb.velocity.x > 0f && _horizontal < 0f) || (_rb.velocity.x < 0f && _horizontal > 0f))
        {
            _changeDir = true;
        }

        if ((_rb.velocity.y > 0f && _vertical < 0f) || (_rb.velocity.y < 0f && _vertical > 0f))
        {
            _changeDir = true;
        }
    }

    void reelIn()
    {
        if(Input.GetMouseButton(1))
        {
            //Debug.Log("SHOULD REEL IN");
            _timeEleapsed += Time.deltaTime;

            if (_time > 1)
            {
                _time = 1;
            }

            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _player.transform.position, _time);
            _time += Time.deltaTime / multiplyer;

            

            if(gameObject.transform.position == _player.transform.position)
            {
                gameObject.transform.position = _player.transform.position;
            }

            _player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;


        }
        else
        {
            _time = 0;
            
        }

    }


    void shootOut()
    {
        if(Input.GetMouseButtonUp(1))
        {
            StartCoroutine("attackDelay");
            if(_timeEleapsed <= 0.2f)
            {
                _timeEleapsed = 0f;
                return;
            }
            determineVelocity(_timeEleapsed);
            //Debug.Log("SHOULD SHOOT OUT!");
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 dir = (worldPosition - gameObject.transform.position).normalized;


            gameObject.GetComponent<Rigidbody2D>().velocity = dir * _speed;

            //_timeEleapsed += Time.deltaTime / multiplyer;
            _timeEleapsed = 0;
            
        }
        else
        {
            // _timeEleapsed = 0;
            
        }

        
    }


    void spin()
    {
        if(Input.GetMouseButton(0))
        {

        }
    }

    void determineVelocity(float time)
    {
        if(time >= 0.6f)
        {
            _speed = 125f;
            //PM.damage = 2;
            crit();
        }
        else if((time < 06f) && (time >= 0.4f))
        {
            _speed = 100f;
            // _PM.damage = 1;
            _dam = 1;
        }
        else if(time <= 0.3f)
        {
            _speed = 75f;
            // _PM.damage = 0.5f;
            _dam = 0.5f;
        }

    }

    private IEnumerator attackDelay()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.3f);
        isAttacking = false;
    }


    public void crit()
    {
        float temp;

        temp = Random.Range(0, 100);

        if(temp <= critChance)
        {
            _dam = 1;
        }
        else if(temp > critChance && temp <= 100)
        {
            _dam = 2;
            Debug.Log("PLAYER CRITTED!");
        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((isAttacking) && (collision.gameObject.CompareTag("enemy")) && (!bodyhit))
        {
            bodyhit = true;
            _rb.velocity = Vector2.zero;
            crit();
            collision.gameObject.GetComponent<enemyManager>().takeDamage(_dam);
            collision.gameObject.GetComponent<enemyManager>().stunned = true;
            Debug.Log("BODY DID DAMAGE!");
            //Debug.Log("stunned = " + collision.gameObject.GetComponent<enemyManager>().stunned);
            //Debug.Log("ENEMY SHOULD TAKE DAMAGE");
            bodyhit = false;
        }
        else if((!isAttacking) && (collision.gameObject.CompareTag("enemy")))
        {
            _rb.velocity = Vector2.zero;
        }

        if((collision.gameObject.CompareTag("Boss1")) || (collision.gameObject.CompareTag("Boss2")) || (collision.gameObject.CompareTag("FinalBoss")))
        {
            collision.gameObject.GetComponent<bossManager>().takeDamage(_dam);
            _rb.velocity = Vector2.zero;
        }
    }

}
