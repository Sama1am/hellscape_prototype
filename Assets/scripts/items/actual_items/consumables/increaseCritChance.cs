using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class increaseCritChance : MonoBehaviour
{
    private SpriteRenderer _SR;
    [SerializeField] private BoxCollider2D _itemCollider;
    [SerializeField] private GameObject _itemUI;
    [SerializeField] private float _popUpTime;

    
    // Start is called before the first frame update
    void Start()
    {
        _SR = GetComponentInChildren<SpriteRenderer>();
        _itemUI.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Player")))
        {
            collision.GetComponentInChildren<bodyController>().critChance -= 5;
            StartCoroutine("UIPopUp");
            //Destroy(gameObject);
        }
    }

    IEnumerator UIPopUp()
    {
        _itemUI.SetActive(true);
        _itemCollider.enabled = false;
        _SR.enabled = false;
        yield return new WaitForSeconds(_popUpTime);
        _itemUI.SetActive(false);
        Destroy(gameObject);
    }

}
