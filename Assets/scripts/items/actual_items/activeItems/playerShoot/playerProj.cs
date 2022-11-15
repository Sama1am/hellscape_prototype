using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProj : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float lowerDam;
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private AudioClip _sound;

    private AudioSource _AS;

    [SerializeField]
    private float lifeTime;

    [SerializeField] private bool _lowerDamage;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        _AS = GetComponent<AudioSource>();
        //damage = gameObject.GetComponentInParent<EnemyManager>().damage;
        rb = GetComponent<Rigidbody2D>();
        if (_lowerDamage)
        {
            damage = lowerDam;
        }
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            die();
        }

        if ((rb.velocity.x == 0) && (rb.velocity.y == 0))
        {
            Destroy(gameObject);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            AudioSource.PlayClipAtPoint(_sound, transform.position, 0.5f);
            Instantiate(_effect, transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<enemyManager>().takeDamage(damage);
            Destroy(gameObject);
        }

        if((collision.gameObject.CompareTag("Boss1")) || (collision.gameObject.CompareTag("FinalBoss")))
        {
            AudioSource.PlayClipAtPoint(_sound, transform.position, 0.5f);
            Instantiate(_effect, transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<bossManager>().takeDamage(damage);
            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("obstacle"))
        {
            AudioSource.PlayClipAtPoint(_sound, transform.position, 0.5f);
            Instantiate(_effect, transform.position, Quaternion.identity);
            die();
        }
    }

    void die()
    {
        Destroy(gameObject);
    }
}
