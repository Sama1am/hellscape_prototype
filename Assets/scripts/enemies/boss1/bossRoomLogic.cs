using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossRoomLogic : MonoBehaviour
{
    [SerializeField] private GameObject _bossDoor;

    bossManager BM;
    // Start is called before the first frame update
    void Start()
    {
        BM = GetComponentInParent<bossManager>();
    }

    // Update is called once per frame
    void Update()
    {
       if(BM.isdead)
       {
            _bossDoor.SetActive(false);
       }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if((collision.gameObject.CompareTag("Player")))
        {
            _bossDoor.SetActive(true);
        }
    }


}
