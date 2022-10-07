using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] private float maxMoveDist;
    [SerializeField] private float panBorderThickness;
    [SerializeField] private float panSpeed;
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private Texture2D _panCursor;
    [SerializeField] private Texture2D _defualtCursor;

    private float _lerpTime;

    private GameObject _Player;
    // Start is called before the first frame update
    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.mousePosition.y >= Screen.height - panBorderThickness) )
        {
            //Cursor.SetCursor(_panCursor, Vector2.zero, CursorMode.ForceSoftware);
            camMove();
        }
        else if((Input.mousePosition.y <=  panBorderThickness) )
        {

            camMove();
        }
        else if((Input.mousePosition.x >= Screen.width - panBorderThickness))
        {
            camMove();
        }
        else if((Input.mousePosition.x <= panBorderThickness))
        {
            camMove();
        }
        else
        {
            Cursor.SetCursor(_defualtCursor, Vector2.zero, CursorMode.ForceSoftware);
            if (transform.position != _Player.transform.position)
            {
                Debug.Log("SHOULD LERP BACK!");
                if (_lerpTime > 1)
                {
                    _lerpTime = 1;
                }

                _lerpTime += Time.deltaTime / _lerpSpeed;
                transform.position = Vector3.Lerp(transform.position, new Vector3(_Player.transform.position.x, _Player.transform.position.y, -10), _lerpTime);

                
                
            }
        }

        _lerpTime = 0;

    }



    private void camMove()
    {
        Cursor.SetCursor(_panCursor, Vector2.zero, CursorMode.ForceSoftware);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 dir = (worldPosition - gameObject.transform.position).normalized;

        transform.position += dir * (panSpeed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _Player.transform.position.x - 5, _Player.transform.position.x + 5), Mathf.Clamp(transform.position.y, _Player.transform.position.y - 5, _Player.transform.position.y + 5), -10);


    }
}
