using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class active_items : MonoBehaviour
{
    private bool _currentActiveItem;
    public Sprite UISprite;
    public CircleCollider2D itemCollider;
    public SpriteRenderer SR;
    [SerializeField] private bool _onBody;
    [SerializeField] private GameObject _UI;
   
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!_onBody)
        {
            _UI.SetActive(false);
        }
    }

    public bool getBodyStatus()
    {
        return _onBody;
    }

    public bool getCurrentStatus()
    {
        return _currentActiveItem;
    }

    public void pickedUpItem()
    {
        itemCollider.enabled = false;
        SR.enabled = false;
        _currentActiveItem = true;
        _onBody = true;


    }

    public void dropItem()
    {
        itemCollider.enabled = true;
        SR.enabled = true;
        _currentActiveItem = false;
        _onBody = false;
    }
}
