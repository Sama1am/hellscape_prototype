using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keyManager : MonoBehaviour
{
    public int keys;
    [SerializeField] private GameObject[] keyImages;
    // Start is called before the first frame update
    void Start()
    {
        keys = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(keys == 1)
        {
            keyImages[0].SetActive(true);
        }
        if(keys == 2)
        {
            keyImages[1].SetActive(true);
        }
        else if(keys == 0)
        {
            keyImages[0].SetActive(false);
            keyImages[1].SetActive(false);
        }
    }
}
