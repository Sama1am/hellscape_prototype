using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falseHeart : MonoBehaviour
{
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
            collision.gameObject.GetComponentInChildren<playerManager>().hasFalseHeart = true;
            collision.gameObject.GetComponentInChildren<playerManager>().falseHeart++;
        }
    }
}