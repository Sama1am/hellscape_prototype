using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField] private GameObject _haloLight;

    private GameObject _player;
    private bool win;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(win)
        {
            //SceneManager.LoadScene(2);
            StartCoroutine("spawnHalo");
        }
    }

    public void setWin(bool state)
    {
        win = true;

        
    }


    private IEnumerator spawnHalo()
    {
        Instantiate(_haloLight, _player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(2);
    }


}
