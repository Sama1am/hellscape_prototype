using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rope : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _hook;
    [SerializeField] private GameObject _ropeSegs;
    [SerializeField] private int _numOfSegs;
    [SerializeField] private GameObject _body;



    // Start is called before the first frame update
    void Start()
    {
        generateRope();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generateRope()
    {
        Rigidbody2D prevBod = _hook;
        for(int i  = 0; i < _numOfSegs; i++)
        {
            if(i >= _numOfSegs)
            {
                GameObject newSeg = Instantiate(_body);
                newSeg.transform.parent = transform;
                newSeg.transform.position = transform.position;
                HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
                hj.connectedBody = prevBod;

                prevBod = newSeg.GetComponent<Rigidbody2D>();
            }
            else
            {
                //int index = Random.Range(0, _ropeSegs.Length);
                GameObject newSeg = Instantiate(_ropeSegs);
                newSeg.transform.parent = transform;
                newSeg.transform.position = transform.position;
                HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
                hj.connectedBody = prevBod;

                prevBod = newSeg.GetComponent<Rigidbody2D>();
            }
            
        }
    }
}
