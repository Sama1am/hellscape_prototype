using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField] private GameObject _haloLight;
    [SerializeField] private AudioClip _defualtMusic;
    [SerializeField] private AudioSource _camAS;

    private bool _miniBossDead;
    private bool _isPlaying;
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

        if(_miniBossDead && !_isPlaying)
        {
            //_camAS.clip = _defualtMusic;
            _camAS.Play();
            _isPlaying = true;
            Debug.Log("AUDIO SHOULD BE PLAYING!");
        }
       
    }

    public void setWin(bool state)
    {
        win = true;

        
    }

    public void setminiBoss(bool state)
    {
        _miniBossDead = state;
    }

   
    private IEnumerator spawnHalo()
    {
        Instantiate(_haloLight, _player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(2);
    }


}
