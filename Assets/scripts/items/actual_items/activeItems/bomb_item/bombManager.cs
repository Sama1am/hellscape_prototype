using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombManager : MonoBehaviour
{
    [SerializeField] private GameObject _bombObject;
    [SerializeField] private Vector3 _offset;
    private GameObject _liveBomb;
    [SerializeField] private GameObject _parent;
    private Vector3 dir;
    private Vector3 worldPosition;

    [SerializeField] private float _spawnRadisu;

    private playerItemManager _PIM;
    // Start is called before the first frame update
    void Start()
    {
        _PIM = GameObject.FindGameObjectWithTag("Player").GetComponent<playerItemManager>();

        _parent = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        dir = (worldPosition - gameObject.transform.position);

        if(Input.GetMouseButtonDown(0))
        {
            if (_PIM.getItemStatus() == true)
            {
                spawnInRadius();
                _PIM.useItemCharge();
            }
                
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _spawnRadisu);
    }
    void spawnInRadius()
    {
        //spawns the bomb in the direction of the mouse but wihtin a certain redius from the player
        Vector3 direction = dir;
        direction = Vector3.ClampMagnitude(direction, _spawnRadisu);
        _liveBomb = Instantiate(_bombObject, transform.position + direction, Quaternion.identity);
    }
   
}
