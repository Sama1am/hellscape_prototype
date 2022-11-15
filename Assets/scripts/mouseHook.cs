using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseHook : MonoBehaviour
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
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.gameObject.transform.position = worldPosition;
    }


}
