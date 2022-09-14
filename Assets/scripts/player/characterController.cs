using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{


    #region Movement
    [Header("Movement")]
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _moveAcceleration;
    [SerializeField] private float _dragForce;
    [SerializeField] private float _startBoost;
    private float _horizontal;
    private float _vertical;
    Vector2 _move;
    //checks to see if we are turning or have turned 
    private bool changeDir;

    #endregion

    
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        changeDirection();
    }

    private void FixedUpdate()
    {
        if (_horizontal != 0 || _vertical != 0)
        {
            movement();
            
        }

        drag();
    }

    public void movement()
    {
        
        _move = new Vector2(_horizontal, _vertical).normalized;
        _move *= (_moveAcceleration );

        //to make beinging moevment snappy 
        if (rb.velocity.magnitude == 0)
        {
            rb.AddForce(_move * _startBoost);
        }
        else
        {
            rb.AddForce(_move);
        }

        //checks to see what velcoity the character is at, if it is bigger or equal to max move speed it then clamps it to the max move speed 

        if ((Mathf.Abs(rb.velocity.x) >= _maxMoveSpeed) || (Mathf.Abs(rb.velocity.y) >= _maxMoveSpeed))
        {
           // Debug.Log("AT MAX MOVE SPEED");
            //rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -_maxMoveSpeed, _maxMoveSpeed), rb.velocity.y);
            rb.velocity = new Vector2((Mathf.Clamp(rb.velocity.x, -_maxMoveSpeed, _maxMoveSpeed)), Mathf.Clamp(rb.velocity.y, -_maxMoveSpeed, _maxMoveSpeed));
            //Debug.Log("VELOCITY IS " + rb.velocity.x);
        }
        

    }
    
    private void changeDirection()
    {
        if((rb.velocity.x > 0f && _horizontal < 0f) || (rb.velocity.x < 0f &&  _horizontal> 0f))
        {
            changeDir = true;
        }

        if((rb.velocity.y > 0f && _vertical < 0f) || (rb.velocity.y < 0f && _vertical > 0f))
        {
            changeDir = true;
        }
    }

    private void drag()
    {
        
        if((_horizontal == 0) || (changeDir) || (_vertical == 0))
        {
            rb.drag = _dragForce;
        }
    }
}
