using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{

    [SerializeField] public float damage;
    [SerializeField] public float health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float dam)
    {
        health -= dam;

        if(health <= 0)
        {
            //gameOver
        }
    }

    public void checkHealth()
    {
        if(health <= 0)
        {
            //gameOver
        }
    }

}
