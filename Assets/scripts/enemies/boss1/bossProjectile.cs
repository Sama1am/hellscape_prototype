using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossProjectile : MonoBehaviour
{
    private float damage;

    [SerializeField]
    private float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        damage = gameObject.GetComponentInParent<bossManager>().damage;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            die();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<playerManager>().takeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("obstacle"))
        {
            Destroy(gameObject);
        }
    }

    void die()
    {
        Destroy(gameObject);
    }
}
