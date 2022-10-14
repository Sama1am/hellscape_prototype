using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaEnd : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
       // _rb.AddForce(transform.forward * _speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
