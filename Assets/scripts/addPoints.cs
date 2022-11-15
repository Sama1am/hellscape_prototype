using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addPoints : MonoBehaviour
{

    [SerializeField] private Transform[] points;
    [SerializeField] private lineRenderController line;
    // Start is called before the first frame update
    void Start()
    {
        line.setUpPoints(points);
    }

}
