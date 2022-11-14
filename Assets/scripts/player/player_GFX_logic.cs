using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_GFX_logic : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sr;
    private Rigidbody2D _RB;
    // Start is called before the first frame update
    void Start()
    {
        _RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        flip();
    }


    void flip()
    {
        if(_RB.velocity.x >= 0)
        {
            _sr.flipX = false;
        }
        else if(_RB.velocity.x < 0)
        {
            _sr.flipX = true;
        }
    }
}
