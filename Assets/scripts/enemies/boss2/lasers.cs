using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lasers : MonoBehaviour
{
    [SerializeField] private float _damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Body"))
        {
            collision.gameObject.GetComponent<playerManager>().takeDamage(_damage);
            Debug.Log("PLAYER TOO " + _damage + " FROM LASERS");
        }
    }

   
}
