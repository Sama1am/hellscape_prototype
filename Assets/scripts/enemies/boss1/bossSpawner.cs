using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossSpawner : MonoBehaviour
{
    private GameObject _target;
    private float _distFromPlayer;

    [SerializeField] private float _activationDist;
    [SerializeField] private float _instanDist;
    [SerializeField] private GameObject _Boss;
    [SerializeField] public GameObject _BossHealthSlider;
    [SerializeField] public Slider _bossSlider;
    [SerializeField] private float xValue;
    [SerializeField] private GameObject _bossDoor;

    private GameObject actualBoss;
    
    // Start is called before the first frame update
    void Start()
    {
        
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        door();
        checkDist();

        if (_distFromPlayer <= _instanDist)
        {
            if(actualBoss == null)
            {
                actualBoss = Instantiate(_Boss, transform.position, Quaternion.identity, transform);
                _BossHealthSlider.SetActive(true);
            }
            
        }

        if(_distFromPlayer <= _activationDist)
        {
            GetComponentInChildren<bossCombat>().active = true;
        }

        if(actualBoss.GetComponent<bossManager>().currentHeaalth <= 0)
        {
            Destroy(gameObject);
            _BossHealthSlider.SetActive(false);
        }
    }

    void checkDist()
    {
        _distFromPlayer = Vector3.Distance(transform.position, _target.transform.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(xValue, _activationDist, 0));
        Gizmos.DrawWireCube(transform.position, new Vector3(xValue, _instanDist, 0));



    }


    void door()
    {
        if(actualBoss != null)
        {
            if (actualBoss.GetComponent<bossCombat>().active == true)
            {
                _bossDoor.SetActive(true);
            }
            else if (actualBoss.GetComponent<bossManager>().isdead)
            {
                Destroy(_bossDoor);


            }
        }
       
    }

}
