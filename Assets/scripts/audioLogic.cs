using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioLogic : MonoBehaviour
{
    [SerializeField] private AudioClip soundEffect;
    [SerializeField] private AudioSource _AS;
    // Start is called before the first frame update
    void Start()
    {
        _AS.PlayOneShot(soundEffect);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
