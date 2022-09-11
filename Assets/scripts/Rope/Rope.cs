using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject[] prefabRopeSegs;
    public int numLinks;
    private List<GameObject> segList = new List<GameObject>();
    private GameObject Centre;
   // public Material[] RopeColour = new Material[3];
    //private playerManager pM;
    // Start is called before the first frame update
    void Start()
    {
        //pM = GameObject.Find("Player").GetComponent<playerManager>();
        //if(pM.Links > 20)
        //{
        //    pM.Links = 20;
        //    numLinks = 20;
        //}else
        //{
        //    numLinks = pM.Links;
        //}
      
        //Centre = GameObject.Find("CameraPoint");
        //this.transform.position = Centre.transform.position;
        //if (pM.playerHealth >= 7)
        //{
        //    prefabRopeSegs[0].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(0.07406545f, 0.6886792f, 0.08937005f, 1);
        //    //green
        //}
        //else if (pM.playerHealth < 7 && pM.playerHealth >= 4)
        //{
        //    prefabRopeSegs[0].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(0.9150943f, 0.668996f, 0.05525092f, 1);
        //    //yellow
        //}
        //else if (pM.playerHealth < 4 && pM.playerHealth >= 1)
        //{
        //    prefabRopeSegs[0].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(0.7264151f, 0.05071197f, 0.05700132f, 1);
        //    //red
        //}
        GenerateRope();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    void GenerateRope()
    {
        Rigidbody2D prevBod = hook;
        for (int i = 0; i < numLinks; i++)
        {
            int index = Random.Range(0, prefabRopeSegs.Length);
            GameObject newSeg = Instantiate(prefabRopeSegs[index]);
            segList.Add(newSeg);
            if(i % 2 == 0)
            {
                newSeg.GetComponent<BoxCollider2D>().enabled = false;
            }
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;

            prevBod = newSeg.GetComponent<Rigidbody2D>();
        }

        HingeJoint2D lastsegjoint = segList[numLinks - 1].AddComponent(typeof(HingeJoint2D)) as HingeJoint2D;
        lastsegjoint.autoConfigureConnectedAnchor = false;
        lastsegjoint.anchor = new Vector2(0, -1);
        
        lastsegjoint.connectedBody = GameObject.FindGameObjectWithTag("Hook2").GetComponent<Rigidbody2D>();



    }
}

