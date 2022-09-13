using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringJointManager : MonoBehaviour
{
    public GameObject player;
    public GameObject body;
    private float distance;
    //public float maxdis;
    public SpringJoint2D SJ;

    [SerializeField] public float maxDist;
    public float ogMaxDist;
    //public playerManager pM;
    // Start is called before the first frame update
    void Start()
    {
        ogMaxDist = maxDist;
        SJ.enabled = true;
        SJ.distance = maxDist;
        //pM = this.gameObject.GetComponent<playerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(player.transform.position, body.transform.position);
        // SJ.distance = maxDist;

        SJ.distance = maxDist;

        //if (distance >= maxDist)
        //{
        //    SJ.enabled = true;
        //}
        //else
        //{
        //    SJ.enabled = false;
        //}


    }
}
