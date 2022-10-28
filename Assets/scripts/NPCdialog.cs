using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using TMPro;

public class NPCdialog : MonoBehaviour
{
    public GameObject dialogPanel;
    public TextMeshProUGUI dialog;
    public string[] linesForTalking;
    private int temp;
    private int index;

    public float speedOfText;
    public bool playerInRange;

    public TextMeshProUGUI interact;

    public GameObject nextButton;
  

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Image>().sprite = profilePic;
        zeroBars();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange == true)
        {
            if (dialogPanel.activeInHierarchy)
            {
                zeroBars();
            }
            else
            {
                interact.text = "";
                dialogPanel.SetActive(true);
                StartCoroutine(TypingEffect());
            }
        }
        
        if(dialog.text == linesForTalking[index])
        {
            nextButton.SetActive(true);
        }
    }

    public void zeroBars()
    {
        dialog.text = "";
        index = 0;
        dialogPanel.SetActive(false);
    }

    IEnumerator TypingEffect()
    {
        foreach(char letter in linesForTalking[index].ToCharArray())
        {
            dialog.text += letter;
            yield return new WaitForSeconds(speedOfText);
        }
    }

    public void NextLine()
    {
        nextButton.SetActive(false);
        if(index < linesForTalking.Length-1)
        {
            index++;
            dialog.text = "";
            StartCoroutine(TypingEffect());
        }
        else
        {
            zeroBars();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        interact.text = "";
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        interact.text = "[press space to interact]";
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
