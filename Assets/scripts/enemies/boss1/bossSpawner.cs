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
    //[SerializeField] private GameObject _Boss;
    [SerializeField] public GameObject _BossHealthSlider;
    //[SerializeField] public Slider _bossSlider;
    [SerializeField] private float xValue;
    //[SerializeField] private GameObject _bossDoor;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject _bossDoor;


    [SerializeField] private GameObject actualBoss;
    private bool hasSpanwedBoss;
    private bool _spawnedKey;
    public bool isdead;
    [SerializeField] private dropManager DM;
    // Start is called before the first frame update
    void Start()
    {
        DM = gameObject.GetComponent<dropManager>();
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        

        //door();
        checkDist();

        if (_distFromPlayer <= _instanDist)
        {
            if(actualBoss != null && hasSpanwedBoss == false)
            {
                //actualBoss = Instantiate(_Boss, transform.position, Quaternion.identity, transform);
                actualBoss.SetActive(true);
                //DM = actualBoss.GetComponent<dropManager>();
                hasSpanwedBoss = true;
               // _BossHealthSlider.SetActive(true);
            }
            
        }


        if(isdead)
        {
            Destroy(_bossDoor);
            DM.determineDrop();
            if(!_spawnedKey)
            {
                Instantiate(key, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
                _spawnedKey = true;
            }
        }


        try
        {
            if (_distFromPlayer <= _activationDist)
            {
                GetComponentInChildren<bossCombat>().active = true;
            }
        }
        catch
        {

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
        Gizmos.DrawWireSphere(transform.position, _instanDist);


    }


    void door()
    {
        if(actualBoss != null)
        {
            if(actualBoss.GetComponent<bossManager>().attackStatus() == true)
            {
                //StartCoroutine("bossDelay");
            }
            else if (actualBoss.GetComponent<bossManager>().isdead)
            {
                Destroy(_bossDoor);

            }
        }
       
    }

    private IEnumerator bossDelay()
    {
        //_bossDoor.SetActive(false);
        yield return new WaitForSeconds(3f);
        //_bossDoor.SetActive(true);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Player")))
        {
            if(!isdead)
            {
                _bossDoor.SetActive(true);
                _BossHealthSlider.SetActive(true);
                actualBoss.SetActive(true);
                actualBoss.GetComponent<bossManager>().currentHeaalth = actualBoss.GetComponent<bossManager>()._maxHealth;
                actualBoss.GetComponent<bossManager>().setAttackStatus(true);
                hasSpanwedBoss = true;
            }
           
        }
    }

}
