using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stun_logic : MonoBehaviour
{
    [SerializeField] private float _stunRadius;
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();

    private bool _stunning;
    [SerializeField] private CircleCollider2D _collider;
    private playerItemManager _PIM;
    private active_items _AI;
    // Start is called before the first frame update
    void Start()
    {
        _PIM = GameObject.FindGameObjectWithTag("Player").GetComponent<playerItemManager>();
        _collider.radius = _stunRadius;
        _AI = GetComponent<active_items>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_AI.getCurrentStatus() == true)
        {
            if (Input.GetMouseButton(0))
            {
                if (_PIM.getItemStatus() == true)
                {
                    _stunning = true;
                    if (_stunning)
                    {
                        stun();
                    }
                }
            }
        }
            
    }

    void stun()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].GetComponent<enemyManager>().setStunStatus(true);
            Debug.Log("SHOULD OF STUNNED ENEMY1");
        }

        _stunning = false;
        _PIM.useItemCharge();

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


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _stunRadius);
    }
}
