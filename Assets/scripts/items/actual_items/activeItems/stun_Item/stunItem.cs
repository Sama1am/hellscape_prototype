using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stunItem : MonoBehaviour
{
    //[SerializeField] private float _damage;
    [SerializeField] private float _maxTime;
    [SerializeField] private float _currentTime;
    private GameObject _body;
    private bool doDamage;
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private List<GameObject> _bosses = new List<GameObject>();
    private playerItemManager _PIM;
    private bool _stunning;

    //private PolygonCollider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        _PIM = GameObject.FindGameObjectWithTag("Player").GetComponent<playerItemManager>();
        //_collider = GetComponent<PolygonCollider2D>();
        //_collider.radius = _radius;
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
            stun();
        }
    }

    void stun()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].GetComponent<enemyManager>().setStunStatus(true);
            Debug.Log("SHOULD OF STUNNED ENEMY1");
        }

        for (int i = 0; i < _bosses.Count; i++)
        {
            _bosses[i].GetComponent<bossManager>().setStunnStatus(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log(collision.name);
            _enemies.Add(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Boss1") || collision.gameObject.CompareTag("FinalBoss"))
        {
            _bosses.Add(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log(collision.name);
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
}
