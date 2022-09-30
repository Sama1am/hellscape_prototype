using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSpawner : MonoBehaviour
{
    private GameObject _target;
    private float _distFromPlayer;

    [SerializeField] private float _activationDist;
    [SerializeField] private GameObject _Boss;

    private GameObject actualBoss;

    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(_distFromPlayer <= _activationDist)
        {
            actualBoss = Instantiate(_Boss, transform.position, Quaternion.identity, transform);
        }

        if(actualBoss.GetComponent<bossManager>().currentHeaalth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void checkDist()
    {
        _distFromPlayer = Vector3.Distance(transform.position, _target.transform.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _activationDist);

    }

}
