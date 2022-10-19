using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class itemUIManager1 : MonoBehaviour
{
    [SerializeField] private GameObject _itemUI;
    [SerializeField] private float _popUpTime;

    [SerializeField] private Canvas _itemCanvas;
    private Camera _mainCam;
    private active_items _AT;
    // Start is called before the first frame update
    void Start()
    {
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _itemCanvas.worldCamera = _mainCam;
        _AT = gameObject.GetComponent<active_items>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_AT.getBodyStatus() == false)
        {
            _itemUI.SetActive(false);
        }
    }

    IEnumerator UIPopUp()
    {
        _itemUI.SetActive(true);
        yield return new WaitForSeconds(_popUpTime);
        _itemUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("UIPopUp");
        }
    }
}
