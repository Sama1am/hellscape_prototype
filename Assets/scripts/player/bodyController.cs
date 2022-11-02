using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class bodyController : MonoBehaviour
{
    #region Movement stuff
    [Header("Movement Stuff")]
    private float _horizontal;
    private float _vertical;
    private bool _changeDir;
    [SerializeField] private float _dragForce;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _mediumSpeed;
    [SerializeField] private float _minimuimSpeed;
    #endregion

    #region Throw body stuff
    [Header("throw body stuff")]
    [SerializeField] public float multiplyer;
    [SerializeField] private float _time;
    [SerializeField] private float _timeEleapsed;
    [SerializeField] private float _speed;
    #endregion

    private float _dam;
    public bool attacking;
    private bool _bodyhit;

    #region crit
    public int critChance;
    [SerializeField] private TextMeshProUGUI _critText;
    [SerializeField] private GameObject _textObject;
    #endregion

    [SerializeField] private SpriteRenderer _pointerSprite;

    [SerializeField] private camera_shake _CS;
    private GameObject _player;
    private Rigidbody2D _rb;
    private playerManager _PM;
    private playerItemManager _PIM;
    // Start is called before the first frame update
    void Start()
    {
        _PIM = GameObject.FindGameObjectWithTag("Player").GetComponent<playerItemManager>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _PM = gameObject.GetComponent<playerManager>();
        //dam = _PM.damage;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        setSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        changeDirection();
        reelIn();
        shootOut();
        setPointerSprite(_timeEleapsed);

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

    public bool isAttacking()
    {
        return attacking;
    }

    public void setAttackingStatus(bool status)
    {
        attacking = status;
    }

    private void setSpeed()
    {
        _maxSpeed = 125f;
        _mediumSpeed = 100f;
        _minimuimSpeed = 75f;
    }

    void determineVelocity(float time)
    {
        if(time >= 0.6f)
        {
            _speed = _maxSpeed;
            //125
            //PM.damage = 2;
            crit();
        }
        else if((time < 06f) && (time >= 0.4f))
        {
            _speed = _mediumSpeed;
            //100
            // _PM.damage = 1;
            _dam = 1;
        }
        else if(time <= 0.3f)
        {
            
            _speed = _minimuimSpeed;
            //75
            // _PM.damage = 0.5f;
            _dam = 0.5f;
        }

    }

    private void setPointerSprite(float time)
    {
        if(Input.GetMouseButton(1))
        {
            if (time >= 0.6f)
            {

                _pointerSprite.color = Color.green;

            }
            else if ((time < 06f) && (time >= 0.4f))
            {

                _pointerSprite.color = Color.yellow;

            }
            else if (time <= 0.3f)
            {
                
                _pointerSprite.color = Color.red;

            }
            else if(!Input.GetMouseButton(1))
            {
                _pointerSprite.color = Color.gray;
            }
        }
    }

    private IEnumerator attackDelay()
    {
        attacking = true;
        yield return new WaitForSeconds(0.3f);
        attacking = false;
    }

    public void crit()
    {
        float temp;

        temp = Random.Range(0, 101);

        if(temp <= critChance)
        {
            _dam = 1;
            _critText.text = _dam.ToString();
        }
        else if(temp > critChance && temp <= 80)
        {
            _dam = 2;
            _critText.text = _dam.ToString();
            Debug.Log("PLAYER CRITTED!");
        }
        else if(temp > 80)
        {
            _dam = 3;
            
            _critText.text = _dam.ToString();
            Debug.Log("PLAYER CRITTED!");
        }
        
    }

    private void checkCrit()
    {
        if(_dam == 3)
        {
            _CS.setShake(true);
        }
    }

    public void increaseSpeed(float maxSpeed, float medSpeed, float minSpeed)
    {
        _maxSpeed += maxSpeed;
        _mediumSpeed += medSpeed;
        _minimuimSpeed += minSpeed;

        if(_maxSpeed >= 225f)
        {
            _maxSpeed = 225f;
        }

        if(_mediumSpeed >= 200f)
        {
            _mediumSpeed = 200f;
        }

        if(_minimuimSpeed >= 175f)
        {
            _minimuimSpeed = 175f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((attacking) && (collision.gameObject.CompareTag("enemy")) && (!_bodyhit))
        {
            _bodyhit = true;
            //_rb.velocity = Vector2.zero;
            checkCrit();
            collision.gameObject.GetComponent<enemyManager>().takeDamage(_dam);
            StartCoroutine("critPopUp");
            _PIM.setItemCharge();
            collision.gameObject.GetComponent<enemyManager>().setStunStatus(true);
            _bodyhit = false;
        }
        else if((!attacking) && (collision.gameObject.CompareTag("enemy")))
        {
            
        }

        if((collision.gameObject.CompareTag("Boss1")) || (collision.gameObject.CompareTag("FinalBoss")))
        {
            //_CS.setShake(true);
            checkCrit();
            _PIM.setItemCharge();
            collision.gameObject.GetComponent<bossManager>().takeDamage(_dam);
            StartCoroutine("critPopUp");
            Debug.Log("BOSS TOOK DAMAGE " + _dam);
            _rb.velocity = Vector2.zero;
        }
    }

    public IEnumerator velocityDelay()
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

    public IEnumerator critPopUp()
    {
        _textObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _textObject.SetActive(false);
    }

}
