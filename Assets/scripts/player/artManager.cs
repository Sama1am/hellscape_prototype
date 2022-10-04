using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class artManager : MonoBehaviour
{

    [SerializeField] private Sprite[] _playerSprite;

    Rigidbody2D rb;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        changeSprite();
    }


    void changeSprite()
    {
        if(rb.velocity.y > 0)
        {
            sr.sprite = _playerSprite[1];
            sr.flipX = false;
        }
        else if(rb.velocity.y < 0)
        {
            sr.sprite = _playerSprite[0];
            sr.flipX = false;
        }
        //else if(rb.velocity.x > 0)
        //{
        //    sr.sprite = _playerSprite[2];
        //    sr.flipX = false;
        //}
        //else if(rb.velocity.x < 0)
        //{
        //    sr.sprite = _playerSprite[2];
        //    sr.flipX = true;
        //}
    }
}
