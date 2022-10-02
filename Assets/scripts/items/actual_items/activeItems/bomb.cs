using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    [SerializeField] private float _explodeDist;
    [SerializeField] private float _damage;
    [SerializeField] private float _explodeTime;
    private bool _explode;


    // Start is called before the first frame update
    void Start()
    {
        _explode = false;
        GetComponent<CircleCollider2D>().radius = _explodeDist;
    }

    // Update is called once per frame
    void Update()
    {
        _explodeTime -= Time.deltaTime;

        if(_explodeTime <= 2 && _explodeTime > 0)
        {
            _explode = true;
            
        }
        else if(_explodeTime <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("SOMETHING IS IN RADIUS!");
        if(_explode)
        {
            if(collision.gameObject.CompareTag("enemy"))
            {
                Debug.Log("ENEMY IN RADIUS");
                collision.gameObject.GetComponent<enemyManager>().takeDamage(_damage);
                Vector2 difference = transform.position - collision.transform.position;
                Vector2 diff = difference * 1.5f;
                collision.transform.position = new Vector2(transform.position.x + diff.x, transform.position.y + diff.y);
                collision.GetComponent<enemyManager>().stunned = true;
                Destroy(gameObject);

            }
            
            if((collision.gameObject.CompareTag("Boss1")) || (collision.gameObject.CompareTag("Boss2")) || (collision.gameObject.CompareTag("FinalBoss")))
            {
                collision.gameObject.GetComponent<bossManager>().takeDamage(_damage);
                Vector2 difference = transform.position - collision.transform.position;
                Vector2 diff = difference * 1.5f;
                collision.transform.position = new Vector2(transform.position.x + diff.x, transform.position.y + diff.y);
                collision.GetComponent<enemyManager>().stunned = true;
                Destroy(gameObject);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explodeDist);
    }
}
