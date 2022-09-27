using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class increaseSpeed : MonoBehaviour
{
    public bool bad;
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

        if((collision.gameObject.CompareTag("Player")))
        {

            if(!bad)
            {
                collision.GetComponent<characterController>()._maxMoveSpeed += 1;
                Destroy(gameObject);
            }
            else
            {
                collision.GetComponent<characterController>()._maxMoveSpeed -= 0.5f;
                Destroy(gameObject);
            }
            
        }
    }
}
