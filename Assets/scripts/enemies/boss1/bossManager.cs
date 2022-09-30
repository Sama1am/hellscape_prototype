using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossManager : MonoBehaviour
{

    [SerializeField] private float _maxHealth;
    public float currentHeaalth;
    public bool stageOne, stageTwo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(currentHeaalth > _maxHealth / 2)
        //{
        //    stageOne = true;
        //    stageTwo = false;
        //}
        //if(currentHeaalth <= _maxHealth / 2)
        //{
        //    stageTwo = true;
        //    stageOne = false;
        //}
    }


    public void takeDamage(int dam)
    {
        currentHeaalth -= dam;

        if(currentHeaalth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
