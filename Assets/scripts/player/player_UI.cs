using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_UI : MonoBehaviour
{

    [SerializeField] private GameObject _pointer;

    public Transform orb;
    public float radius;

    private Transform pivot;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        pivot = orb.transform;
        //transform.parent = pivot;
        pivot = gameObject.transform;
        transform.position += Vector3.up * radius;
    }

    // Update is called once per frame
    void Update()
    {
        //rotatePointer();

        Vector3 orbVector = Camera.main.WorldToScreenPoint(orb.position);
        orbVector = Input.mousePosition - orbVector;
        float angle = Mathf.Atan2(orbVector.y, orbVector.x) * Mathf.Rad2Deg;

        pivot.position = orb.position;
        pivot.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }


    void rotatePointer()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - gameObject.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

    }





    
}
