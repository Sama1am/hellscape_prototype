using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(GetComponent<ParticleSystem>().isPlaying == false)
       {
            Destroy(gameObject);
       }
    }
}
