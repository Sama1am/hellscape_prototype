using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossDoorController : MonoBehaviour
{

    [SerializeField] private GameObject _bossDoor;

    [SerializeField] private keyManager _KM;
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
        if(collision.gameObject.CompareTag("Body"))
        {
            if(_KM.keys >= 1)
            {
                _bossDoor.SetActive(false);
            }
        }
    }
}
