using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class item_UI : MonoBehaviour
{

    [SerializeField] private GameObject _UIPanel;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    IEnumerator uiPopUp()
    {
        _UIPanel.SetActive(true);
        yield return new WaitForSeconds(5f);
        _UIPanel.SetActive(false);
    }
}
