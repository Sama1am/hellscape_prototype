using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class telaport : MonoBehaviour
{
    [SerializeField] private GameObject _twin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.GetComponentInChildren<bodyController>().StartCoroutine("velocityDelay");
            collision.transform.position = _twin.transform.position;
        }
    }
}
