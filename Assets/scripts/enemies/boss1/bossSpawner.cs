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
    //[SerializeField] public Slider _bossSlider;
    [SerializeField] private float xValue;
    //[SerializeField] private GameObject _bossDoor;
    [SerializeField] private GameObject key;

    private GameObject actualBoss;
    private bool hasSpanwedBoss;

    dropManager DM;
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
            if(actualBoss == null && hasSpanwedBoss == false)
            {
                actualBoss = Instantiate(_Boss, transform.position, Quaternion.identity, transform);
                DM = actualBoss.GetComponent<dropManager>();
                hasSpanwedBoss = true;
               // _BossHealthSlider.SetActive(true);
            }
            
        }

        if(hasSpanwedBoss)
        {
            if (actualBoss.GetComponent<bossManager>().currentHeaalth <= 0)
            {
                DM.determineDrop();
                Instantiate(key, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
                Destroy(gameObject);
                //_BossHealthSlider.SetActive(false);
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
        Gizmos.DrawWireCube(transform.position, new Vector3(xValue, _instanDist, 0));



    }


    void door()
    {
        if(actualBoss != null)
        {
            if(actualBoss.GetComponent<bossCombat>().active == true)
            {
                //StartCoroutine("bossDelay");
            }
            else if (actualBoss.GetComponent<bossManager>().isdead)
            {
                //Destroy(_bossDoor);

            }
        }
       
    }

    private IEnumerator bossDelay()
    {
        //_bossDoor.SetActive(false);
        yield return new WaitForSeconds(3f);
        //_bossDoor.SetActive(true);
    }

}
