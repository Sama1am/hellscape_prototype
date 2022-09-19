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

    public float dam;
    public bool isAttacking;

    private GameObject _player;
    private Rigidbody2D _rb;
    private playerManager _PM;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _PM = gameObject.GetComponent<playerManager>();
        dam = _PM.damage;
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
        drag();

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
            _PM.damage = 1;
        }
        else if(time <= 0.3f)
        {
            _speed = 75f;
            _PM.damage = 0.5f;
        }

    }

    private IEnumerator attackDelay()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }


    private void crit()
    {
        float temp;

        temp = Random.Range(0, 11);

        if(temp <= 3)
        {
            _PM.damage = 2;
        }
        else if(temp > 7 && temp <= 10)
        {
            _PM.damage = 3;
        }
        
    }

}
