using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class active_items : MonoBehaviour
{
    public Sprite UISprite;
    public CircleCollider2D itemCollider;
    public SpriteRenderer SR;
    [SerializeField] private bool _onBody;
   
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool getBodyStatus()
    {
        return _onBody;
    }

    public void pickedUpItem()
    {
        itemCollider.enabled = false;
        SR.enabled = false;
        
    }

    public void dropItem()
    {
        itemCollider.enabled = true;
        SR.enabled = true;
    }
}
