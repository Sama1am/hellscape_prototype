using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holyLightLogic : MonoBehaviour
{
    private playerItemManager _PIM;
    private GameObject _body;
    // Start is called before the first frame update
    void Start()
    {
        _PIM = GameObject.FindGameObjectWithTag("Player").GetComponent<playerItemManager>();
        _body = GameObject.FindGameObjectWithTag("Body");

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void holyLight()
    {

    }
}
