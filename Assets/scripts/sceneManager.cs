using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void play()
    {
        SceneManager.LoadScene(1);
    }


    public void menu()
    {
        SceneManager.LoadScene(0);
    }

    public void win()
    {
        SceneManager.LoadScene(2);
    }

    public void lose()
    {
        SceneManager.LoadScene(3);
    }

    //public void startGame()
    //{
    //    SceneManager.LoadScene("SampleScene");
    //}

    //public void rules()
    //{
    //    SceneManager.LoadScene("main_Menu");
    //}

    public void exit()
    {
        Application.Quit();
    }
}
