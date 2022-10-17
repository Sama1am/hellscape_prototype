using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyParts : MonoBehaviour
{
    private float _dam;
    private SpriteRenderer _sp;
    Rigidbody2D rb;
    bodyController BC;
    private Color ogColor;
    // Start is called before the first frame update
    void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
        ogColor = _sp.color;
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

        if(collision.gameObject.CompareTag("enemy"))
        {
            rb.velocity = Vector2.zero;
            StartCoroutine("changeColour");
        }
    }

    private IEnumerator changeColour()
    {
        _sp.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _sp.color = ogColor;
    }
}
