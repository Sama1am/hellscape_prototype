using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook2Movement : MonoBehaviour
{
    private GameObject _body;
    // Start is called before the first frame update
    void Start()
    {
        _body = GameObject.FindGameObjectWithTag("Body");
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = _body.transform.position;
    }
}
