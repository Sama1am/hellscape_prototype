using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSegments : MonoBehaviour
{
    public GameObject connectAbove, connectBelow;
    // Start is called before the first frame update
    void Start()
    {
        connectAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        RopeSegments aboveSegment = connectAbove.GetComponent<RopeSegments>();
        if (aboveSegment != null)
        {
            aboveSegment.connectBelow = gameObject;
            float spriteBottom = connectAbove.GetComponent<SpriteRenderer>().bounds.size.y;
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, spriteBottom  - 1.3f);
        }
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        }
    }

  
}
