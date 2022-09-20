using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalShoot : MonoBehaviour
{

    [SerializeField]
    GameObject projectile;

    [SerializeField]
    private float nextShot;

    [SerializeField]
    private float timeBetweenShots;

    [SerializeField]
    private float speed;

    private Vector2 playerPos;
    private Vector2 pos;
    private Vector2 enemyToPlayer;

    private bool canShoot;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("startDelay");
    }

    private void FixedUpdate()
    {
        if(canShoot)
        {
            if (Time.time > nextShot)
            {
                shoot();
            }
        }
        
    }

    void shoot()
    {
        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
        bullet.transform.parent = gameObject.transform;
        findDirection();
        Vector2 velocity = enemyToPlayer * speed;
        //rb.velocity = velocity;
        bullet.GetComponent<Rigidbody2D>().velocity = velocity;

        nextShot = Time.time + timeBetweenShots;
        //Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
    }


    void findDirection()
    {
        playerPos = player.GetComponent<Transform>().position;
        pos = transform.position;
        enemyToPlayer = playerPos - pos;
        enemyToPlayer.Normalize();
    }

    IEnumerator startDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }
}
