using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class interacting : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] questions;
    public string[] answers;

    public int index;

    public GameObject dialogPanel;
    public TextMeshProUGUI interact;
    public float speedOfText;
    public bool playerInRange;

    public GameObject nextButton;

    public int temp;


    void Start()
    {
        //index = 0;
    }

    public void zeroBars()
    {
        interact.text = "";
        //index = 0;
        dialogPanel.SetActive(false);
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
                dialogPanel.SetActive(true);
                interact.text = "Greetings, young soul!";
                for (int i = 0; i < questions.Length - 1; i++)
                {
                    interact.text = questions[i] + "<br>";
                }
                
                //StartCoroutine(TypingEffect(temp));
            }
        }

     

        //give clickable lines to player

        //check fixed updates if selected any

        //on trigger event for each line clicked
    }

    IEnumerator TypingEffect(int temp)
    {
        foreach (char letter in answers[temp].ToCharArray())
        {
            interact.text += letter;
            yield return new WaitForSeconds(speedOfText);
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
