using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stun_logic : MonoBehaviour
{
    [SerializeField] private GameObject _stunItem;
    [SerializeField] private float _stunRadius;
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private GameObject _stunIndecator;
    private bool _showIndicator;
    private bool _stunning;
    private GameObject _body;
    [SerializeField] private CircleCollider2D _collider;
    private playerItemManager _PIM;
    [SerializeField] private active_items _AI;
    private bool hasSpawned;

    // Start is called before the first frame update
    void Start()
    {
        _PIM = GameObject.FindGameObjectWithTag("Player").GetComponent<playerItemManager>();
        _collider.radius = _stunRadius;
        _AI = gameObject.GetComponent<active_items>();
        _body = GameObject.FindGameObjectWithTag("Body");
    }

    // Update is called once per frame
    void Update()
    {
        if (_AI.getCurrentStatus() == true)
        {
            if(Input.GetMouseButtonUp(0))
            {
                _showIndicator = false;
                _stunIndecator.SetActive(false);
                if (_PIM.getItemStatus() == true)
                {
                    _showIndicator = false;
                    _stunning = true;
                    if(_stunning)
                    {
                        if(!hasSpawned)
                        {
                            _showIndicator = false;
                            Instantiate(_stunItem, _body.transform.position, Quaternion.identity);
                            hasSpawned = true;
                            _PIM.useItemCharge();
                            StartCoroutine("spawnWait");
                        }
                        
                    }
                }
            }
            else if(Input.GetMouseButtonDown(0))
            {
                if (_PIM.getItemStatus() == true)
                    _showIndicator = true;
            }
        }
            
    }

    private void FixedUpdate()
    {
        showIndicator();
    }

    private void showIndicator()
    {
        if (_showIndicator)
        {
            _stunIndecator.transform.position = _body.transform.position;
            _stunIndecator.SetActive(true);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("enemy"))
        {
            _enemies.Add(collision.gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            _enemies.Remove(collision.gameObject);
        }

    }

    private IEnumerator spawnWait()
    {
        hasSpawned = true;
        yield return new WaitForSeconds(5f);
        hasSpawned = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _stunRadius);
    }
}
