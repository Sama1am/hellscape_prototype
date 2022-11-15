using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineRenderController : MonoBehaviour
{

    private LineRenderer _LR;
    [SerializeField] private Transform[] _points;
    // Start is called before the first frame update
    void Start()
    {
        _LR = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < _points.Length; i++)
        {
            _LR.SetPosition(i, _points[i].position);
        }
    }

    public void setUpPoints(Transform[] points)
    {
        _LR.positionCount = points.Length;
        this._points = points;
    }


   
}
