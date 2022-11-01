using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poisonLogic : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _maxTime;
    [SerializeField] private float _currentTime;
    [SerializeField] private float _radius;
    private GameObject _body;
    private bool doDamage;
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private List<GameObject> _bosses = new List<GameObject>();

    private CircleCollider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _collider.radius = _radius;
        _body = GameObject.FindGameObjectWithTag("Body");
        _currentTime = _maxTime;
        doDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime -= Time.deltaTime;

        if(_currentTime <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            AOEPoison();
        }
    }


    void AOEPoison()
    {
        if(doDamage)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].GetComponent<enemyManager>().takeDamage(_damage);
            }

            for (int i = 0; i < _bosses.Count; i++)
            {
                _bosses[i].GetComponent<bossManager>().takeDamage(_damage);
            }

            StartCoroutine("damageWait");
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            _enemies.Add(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Boss1") || collision.gameObject.CompareTag("FinalBoss"))
        {
            _bosses.Add(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            _enemies.Add(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Boss1") || collision.gameObject.CompareTag("FinalBoss"))
        {
            _bosses.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            _enemies.Remove(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Boss1") || collision.gameObject.CompareTag("FinalBoss"))
        {
            _bosses.Remove(collision.gameObject);
        }
    }

    private IEnumerator damageWait()
    {
        doDamage = false;
        yield return new WaitForSeconds(0.5f);
        doDamage = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
