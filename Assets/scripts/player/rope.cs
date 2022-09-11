using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rope : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _hook;
    [SerializeField] private GameObject _ropeSegs;
    [SerializeField] private int _numOfSegs;
    [SerializeField] private GameObject _body;
    private List<GameObject> segList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        _body = GameObject.FindGameObjectWithTag("Body");
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
            GameObject newSeg = Instantiate(_ropeSegs);
            segList.Add(newSeg);

            if(i % 2 == 0)
            {
              //newSeg.GetComponent<BoxCollider2D>().enabled = false;
            }
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;

            prevBod = newSeg.GetComponent<Rigidbody2D>();

            
        }

        HingeJoint2D lastSegJoint = segList[_numOfSegs - 1].AddComponent(typeof(HingeJoint2D)) as HingeJoint2D;
        lastSegJoint.autoConfigureConnectedAnchor = false;
        lastSegJoint.anchor = new Vector2(0, -1);

        lastSegJoint.connectedBody = GameObject.FindGameObjectWithTag("Hook2").GetComponent<Rigidbody2D>();
    }
}
