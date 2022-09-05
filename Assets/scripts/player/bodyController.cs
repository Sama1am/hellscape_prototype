using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyController : MonoBehaviour
{

    private float _horizontal;
    private float _vertical;
    private bool changeDir;
    [SerializeField] private float _dragForce;



    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        changeDirection();
    }

    private void FixedUpdate()
    {
        drag();
    }

    private void drag()
    {

        if ((_horizontal == 0) || (changeDir) || (_vertical == 0))
        {
            rb.drag = _dragForce;
        }
    }


    private void changeDirection()
    {
        if ((rb.velocity.x > 0f && _horizontal < 0f) || (rb.velocity.x < 0f && _horizontal > 0f))
        {
            changeDir = true;
        }

        if ((rb.velocity.y > 0f && _vertical < 0f) || (rb.velocity.y < 0f && _vertical > 0f))
        {
            changeDir = true;
        }
    }
}
