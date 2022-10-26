using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerItemManager : MonoBehaviour
{
    public GameObject currentItem;

    public bool hasItem;

    [SerializeField] private Image _UISprite;

    private bool _canUseItem;
    [SerializeField] private float _itemCharge;
    [SerializeField] private float _maxItemCharge;
    [SerializeField] private bool _goesOnBody;
    [SerializeField] private GameObject _body;
    [SerializeField] private Slider _itemChargeSlider;
    [SerializeField] private GameObject _itemChargeUI;
    [SerializeField] private GameObject _itemChargePanel;
    // Start is called before the first frame update
    void Start()
    {
        _body = GameObject.FindGameObjectWithTag("Body");

        _itemChargeSlider.maxValue = _maxItemCharge;
    }

    // Update is called once per frame
    void Update()
    {
        if(_itemCharge >= _maxItemCharge)
        {
            _canUseItem = true;
        }
        else if(_itemCharge < _maxItemCharge)
        {
            _canUseItem = false;
        }

        if(hasItem)
        {
            _itemChargeUI.SetActive(true);
        }

        if(hasItem && Input.GetMouseButtonDown(0) && _canUseItem == false)
        {
            StartCoroutine("chargeFeedback");
        }

        _itemChargeSlider.value = _itemCharge;
    }

    public bool getItemStatus()
    {
        return _canUseItem;
    }

    public void setItemCharge()
    {
        if(hasItem)
        {
            _itemCharge++;
            if (_itemCharge > _maxItemCharge)
            {
                _itemCharge = _maxItemCharge;
            }
        }
        
    }

    public void useItemCharge()
    {
        _itemCharge -= _maxItemCharge;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("item"))
        {
            if(hasItem)
            {
                if(collision.gameObject.GetComponent<active_items>().getBodyStatus() == true)
                {
                    GameObject temp = Instantiate(currentItem, transform.position + new Vector3(-2, 0, 0), Quaternion.identity);
                    temp.GetComponent<active_items>().dropItem();
                    Destroy(currentItem);
                    collision.gameObject.transform.SetParent(_body.transform);
                    currentItem = collision.gameObject;
                    currentItem.GetComponent<active_items>().pickedUpItem();
                    _UISprite.sprite = collision.GetComponent<active_items>().UISprite;
                    _UISprite.color = new Color(_UISprite.color.r, _UISprite.color.g, _UISprite.color.b, 255f);
                }
                else if(collision.gameObject.GetComponent<active_items>().getBodyStatus() == false)
                {
                    GameObject temp = Instantiate(currentItem, transform.position + new Vector3(-2, 0, 0), Quaternion.identity);
                    temp.GetComponent<active_items>().dropItem();
                    Destroy(currentItem);
                    collision.gameObject.transform.SetParent(this.gameObject.transform);
                    currentItem = collision.gameObject;
                    currentItem.GetComponent<active_items>().pickedUpItem();
                    _UISprite.sprite = collision.GetComponent<active_items>().UISprite;
                   _UISprite.color = new Color(_UISprite.color.r, _UISprite.color.g, _UISprite.color.b, 255f);
                }
                
                
            }
            if(!hasItem)
            {
                if(collision.gameObject.GetComponent<active_items>().getBodyStatus() == true)
                {
                    collision.gameObject.transform.SetParent(_body.transform);
                    currentItem = collision.gameObject;
                    currentItem.GetComponent<active_items>().pickedUpItem();
                    _UISprite.sprite = collision.GetComponent<active_items>().UISprite;
                    _UISprite.color = new Color(_UISprite.color.r, _UISprite.color.g, _UISprite.color.b, 255f);
                    hasItem = true;
                }
                else if(collision.gameObject.GetComponent<active_items>().getBodyStatus() == false)
                {
                    collision.gameObject.transform.SetParent(this.gameObject.transform);
                    currentItem = collision.gameObject;
                    currentItem.GetComponent<active_items>().pickedUpItem();
                    _UISprite.sprite = collision.GetComponent<active_items>().UISprite;
                    _UISprite.color = new Color(_UISprite.color.r, _UISprite.color.g, _UISprite.color.b, 255f);
                    hasItem = true;
                }
               
            }
        }
    }

    IEnumerator chargeFeedback()
    {
        _itemChargePanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _itemChargePanel.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        _itemChargePanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _itemChargePanel.SetActive(false);
    }


}
