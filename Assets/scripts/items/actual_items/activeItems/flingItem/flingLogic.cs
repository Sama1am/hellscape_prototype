using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flingLogic : MonoBehaviour
{
    #region Throw body stuff
    [Header("throw body stuff")]
    [SerializeField] public float multiplyer;
    [SerializeField] private float _time;
    [SerializeField] private float _timeEleapsed;
    [SerializeField] private float _speed;
    #endregion

    private GameObject _player;
    private GameObject _body;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _body = GameObject.FindGameObjectWithTag("Body");

    }

    // Update is called once per frame
    void Update()
    {
        reelIn();
        shootOut();
    }

    
    void reelIn()
    {
        if (Input.GetMouseButton(0))
        {
            //Debug.Log("SHOULD REEL IN");
            _timeEleapsed += Time.deltaTime;

            if (_time > 1)
            {
                _time = 1;
            }

            _body.transform.position = Vector3.Lerp(_body.transform.position, _player.transform.position, _time);
            _time += Time.deltaTime / multiplyer;



            if (_body.transform.position == _player.transform.position)
            {
                _body.transform.position = _player.transform.position;
                _body.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }

            _body.GetComponent<Rigidbody2D>().velocity = Vector2.zero;


        }
        else
        {
            _time = 0;

        }

    }


    void shootOut()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine("attackDelay");
            if (_timeEleapsed <= 0.2f)
            {
                _timeEleapsed = 0f;
                return;
            }
            //Debug.Log("SHOULD SHOOT OUT!");
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 dir = (worldPosition - _body.transform.position).normalized;


            _body.GetComponent<Rigidbody2D>().velocity = dir * _speed;

            //_timeEleapsed += Time.deltaTime / multiplyer;
            _timeEleapsed = 0;

        }
        else
        {
            // _timeEleapsed = 0;

        }


    }

    private IEnumerator attackDelay()
    {
        _body.GetComponent<bodyController>().setAttackingStatus(true);
        yield return new WaitForSeconds(0.5f);
        _body.GetComponent<bodyController>().setAttackingStatus(false);
    }


}
