using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableObjects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _collisionParticle;
    [SerializeField] private float _health;
    [SerializeField] private GameObject[] _HealthItem;
    [SerializeField] private AudioClip _audio;

    private AudioSource _AS;
    // Start is called before the first frame update
    void Start()
    {
        _AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_health <= 0)
        {
            determineDrop();
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Body"))
        {
            _health -= 1;
            AudioSource.PlayClipAtPoint(_audio, transform.position, 0.5f);
            Instantiate(_collisionParticle, transform.position, Quaternion.identity);
        }
    }



    private void determineDrop()
    {
        float num = Random.Range(0, 101);
        float rand = Random.Range(0, 101);

        if(num <= 50)
        {

        }
        else if(num > 50)
        {
            if(rand <= 50)
            {
                Instantiate(_HealthItem[0], transform.position, Quaternion.identity);
            }
            else if(rand > 50)
            {
                Instantiate(_HealthItem[1], transform.position, Quaternion.identity);
            }
            
        }
    }
}
