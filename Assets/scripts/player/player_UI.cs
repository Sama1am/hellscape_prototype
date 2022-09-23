using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_UI : MonoBehaviour
{

    [SerializeField] private GameObject _pointer;

    [SerializeField] private float _speed;
    [SerializeField] private float _rotationModifirt;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        
        //transform.parent = pivot;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        

        
    }

    private void FixedUpdate()
    {
        if(Input.GetMouseButton(1))
        {
            _pointer.SetActive(true);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            //Vector3 dir = (worldPosition - gameObject.transform.position).normalized;
            float angle = Mathf.Atan2(worldPosition.y, worldPosition.x) * Mathf.Rad2Deg - _rotationModifirt;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _speed);
        }
        else if(Input.GetMouseButtonUp(1))
        {
            _pointer.SetActive(true);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            //Vector3 dir = (worldPosition - gameObject.transform.position).normalized;
            float angle = Mathf.Atan2(worldPosition.y, worldPosition.x) * Mathf.Rad2Deg - _rotationModifirt;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _speed);
        }
        else
        {
            _pointer.SetActive(false);
        }
       
    }


    void rotatePointer()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - gameObject.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

    }





    
}
