using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class increaseHealth : MonoBehaviour
{
    [SerializeField] private float _healthIncrease;
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
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInChildren<playerManager>().increaseHealth(_healthIncrease);
            StartCoroutine("UIPopUp");
        }
    }

    IEnumerator UIPopUp()
    {
        _itemCollider.enabled = false;
        _SR.enabled = false;
        _itemUI.SetActive(true);
        yield return new WaitForSeconds(_popUpTime);
        _itemUI.SetActive(false);
        Destroy(gameObject);
    }
}
