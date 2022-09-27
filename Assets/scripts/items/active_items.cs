using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class active_items : MonoBehaviour
{
    public bool active;

    public Sprite UISprite;

    //public item itemScript;
    public BoxCollider2D itemCollider;
    public SpriteRenderer SR;
    // dropManager dm;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
