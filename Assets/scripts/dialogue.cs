using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogComp;
    public string[] Lines;
    public float speed;
    private int temp;

    public Vector2 target;
    public GameObject tourGuide;
    int tourGuideSpeed = 2;
    //private bool startTour;

    // Start is called before the first frame update
    void Start()
    {
        dialogComp.text = string.Empty;
        startDialog();
        target = new Vector2(0f, -7f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (dialogComp.text == Lines[temp])
            {
                nextLine();
            }
            else
            {
                StopAllCoroutines();
            }
        }

        if (temp == 5)
        {
            startTour();
        }

        if (temp == 11)
        {
            startTour();
        }

    }

    private void startDialog()
    {
        temp = 0;
        StartCoroutine(typelines());
    }

    IEnumerator typelines()
    {
        foreach (char c in Lines[temp].ToCharArray())
        {
            dialogComp.text += c;
            yield return new WaitForSeconds(speed);
        } 
    }

    void nextLine()
    {
        if (temp < Lines.Length - 1)
        {
            temp++;
            dialogComp.text = string.Empty;
            StartCoroutine(typelines());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void startTour()
    {
        tourGuide.transform.position = Vector2.MoveTowards(transform.position, target, tourGuideSpeed*Time.deltaTime);
    }

    void endTour()
    {
        tourGuide.transform.position = Vector2.MoveTowards(transform.position, new Vector2(0,0), tourGuideSpeed * Time.deltaTime);
        
    }
}
