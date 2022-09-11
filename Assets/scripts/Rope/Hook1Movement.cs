using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook1Movement : MonoBehaviour
{
    private GameObject Player1;
    // Start is called before the first frame update
    void Start()
    {
        Player1 = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = Player1.transform.position;
    }
}
