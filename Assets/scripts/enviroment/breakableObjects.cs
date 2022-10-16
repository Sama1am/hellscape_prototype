using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableObjects : MonoBehaviour
{
    [SerializeField] private float _health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_health <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Body"))
        {
            _health -= 1;
        }
    }
}
