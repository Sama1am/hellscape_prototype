using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPosionAOE : MonoBehaviour
{
    [SerializeField] private bool _perm;
    [SerializeField] private float _damage;
    [SerializeField] private float _maxTime;
    [SerializeField] private float _currentTime;
    [SerializeField] private float _radius;
    private GameObject _body;
    private bool doDamage;
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();

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
        if(_perm)
        {
            AOEPoison();
        }
        else if(!_perm)
        {
            _currentTime -= Time.deltaTime;

            if (_currentTime <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                AOEPoison();
            }
        }
       
    }


    void AOEPoison()
    {
        if(doDamage)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].GetComponent<playerManager>().takeDamage(_damage);
            }

            StartCoroutine("damageWait");
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Body"))
        {
            _enemies.Add(collision.gameObject);
            collision.gameObject.GetComponent<playerManager>().takeDamage(_damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Body"))
        {
            _enemies.Remove(collision.gameObject);
        }
    }

    private IEnumerator damageWait()
    {
        doDamage = false;
        yield return new WaitForSeconds(1f);
        doDamage = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
