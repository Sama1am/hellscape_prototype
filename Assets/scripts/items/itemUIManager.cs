using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemUIManager : MonoBehaviour
{
    [SerializeField] private float _lifeTim;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        _lifeTim -= Time.deltaTime;
        checkLife();
    }

    private void checkLife()
    {
        if(_lifeTim <= 0)
        {
            Destroy(gameObject);
        }
    }
}
