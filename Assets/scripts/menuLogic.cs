using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuLogic : MonoBehaviour
{
    public Transform[] points;
    private LineRenderer LR;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            LR.SetPosition(i, points[i].position);
        }
    }

    public void setUpLine(Transform[] points)
    {
        LR.positionCount = points.Length;
        this.points = points;
    }
}
