using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaBoss : MonoBehaviour
{


    [SerializeField] private GameObject _endPoints;
    [SerializeField] private bool _shoot;
  
    public Transform[] points;
    [SerializeField] Transform[] linePoint;
    private LineRenderer _LR;
    private EdgeCollider2D _EC;
    // Start is called before the first frame update
    void Start()
    {
        _EC = GetComponent<EdgeCollider2D>();
        _LR = GetComponent<LineRenderer>();
        setUpLine(linePoint);
    }

    // Update is called once per frame
    void Update()
    {
       //if(_shoot)
       //{
       //     _endPoints.SetActive(true);
            
       //     for (int i = 0; i < points.Length; i++)
       //     {
       //         _LR.SetPosition(i, points[i].position);
       //     }

       //     setEdgeCollider(_LR);
       //}
       //else if(!_shoot)
       //{
       //     _endPoints.SetActive(false);
       //     _LR.positionCount = 0;
       //     //_LR.SetPosition(0, new Vector3(0, 0, 0));
       //     setEdgeCollider(_LR);

       // }

        
    }


  

    public void setUpLine(Transform[] points)
    {
        _LR.positionCount = points.Length;
        this.points = points;
    }

    void setEdgeCollider(LineRenderer line)
    {
        List<Vector2> edges = new List<Vector2>();

        for(int point = 0; point<line.positionCount; point++)
        {
            Vector3 lineRendererPoint = line.GetPosition(point);
            edges.Add(new Vector2(lineRendererPoint.x, lineRendererPoint.y));
        }

        _EC.SetPoints(edges);
        _EC.edgeRadius = 1.5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Body"))
        {
            Debug.Log("COLLIDED WITH BODY!");
        }
    }
}
