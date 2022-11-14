using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableObjects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _collisionParticle;
    [SerializeField] private float _health;
    [SerializeField] private GameObject _HealthItem;
    // Start is called before the first frame update
    void Start()
    {
        
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
            Instantiate(_collisionParticle, transform.position, Quaternion.identity);
        }
    }



    private void determineDrop()
    {
        float num = Random.Range(0, 101);

        if(num <= 50)
        {

        }
        else if(num < 50)
        {
            Instantiate(_HealthItem, transform.position, Quaternion.identity);
        }
    }
}
