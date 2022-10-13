using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    [SerializeField] private float _bombTime;
    [SerializeField] private float _explodeDist;
    [SerializeField] private float _damage;
    [SerializeField] private float _explodeTime;
    [SerializeField] private bool _explode;
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();

    [SerializeField] private CircleCollider2D _bombCollider;

    // Start is called before the first frame update
    void Start()
    {
        _explode = false;
        _bombCollider.radius = _explodeDist;
    }

    // Update is called once per frame
    void Update()
    {
        _explodeTime -= Time.deltaTime;

        if(_explodeTime <= _bombTime && _explodeTime > 0)
        {
            _explode = true;
            
        }
        else if(_explodeTime <= 0)
        {
            Destroy(gameObject);
        }

        if(_explode)
        {
            explode();
        }
    }


    void explode()
    {
        for(int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].GetComponent<enemyManager>().takeDamage(_damage);
            Vector2 difference = transform.position - _enemies[i].transform.position;
            Vector2 diff = difference * 1.5f;
            _enemies[i].transform.position = new Vector2(transform.position.x + diff.x, transform.position.y + diff.y);
            _enemies[i].gameObject.GetComponent<enemyManager>().setStunStatus(true);
            Debug.Log("BOMB DID THE THINGS");
        }

        Destroy(gameObject);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            _enemies.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            _enemies.Remove(collision.gameObject);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explodeDist);
    }
}
