using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropeSegement : MonoBehaviour
{
    [SerializeField] private GameObject _connectAbove, _connectBelow;

    // Start is called before the first frame update
    void Start()
    {
        //makes sure that each segment is positioned in the right spot
        _connectAbove = gameObject.GetComponent<HingeJoint2D>().connectedBody.gameObject;
        ropeSegement aboveSegment = _connectAbove.GetComponent<ropeSegement>();

        if(aboveSegment != null)
        {
            aboveSegment._connectBelow = gameObject;
            float spriteBott = _connectAbove.GetComponentInChildren<SpriteRenderer>().bounds.size.y;
            gameObject.GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, spriteBott * -1);
        }
        else
        {
            gameObject.GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        }

    }
     
    
   
}