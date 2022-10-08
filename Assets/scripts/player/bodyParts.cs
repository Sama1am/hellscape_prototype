using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyParts : MonoBehaviour
{
    private float _dam;

    Rigidbody2D rb;
    bodyController BC;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        BC = gameObject.GetComponentInParent<bodyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("obstacle"))
        {
            rb.velocity = Vector2.zero;
        }
    }
}
