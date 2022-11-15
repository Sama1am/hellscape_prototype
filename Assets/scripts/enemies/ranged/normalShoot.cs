using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalShoot : MonoBehaviour
{

    [SerializeField] GameObject projectile;
    [SerializeField] private float nextShot;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float speed;
    [SerializeField] private float shootRange;
    [SerializeField] private AudioClip _sound;

    private Vector2 playerPos;
    private Vector2 pos;
    private Vector2 enemyToPlayer;
    private AudioSource _AS;
    private bool canShoot;

    GameObject player;
    enemyManager em;
    // Start is called before the first frame update
    void Start()
    {
        _AS = GetComponent<AudioSource>();
        em = GetComponent<enemyManager>();
        player = GameObject.FindGameObjectWithTag("Body");
        StartCoroutine("startDelay");
        canShoot = true;
    }
    private void Update()
    {
        if((Vector2.Distance(transform.position, player.GetComponent<Transform>().position)) <= (shootRange))
        {
            if(canShoot)
            {
                if (Time.time > nextShot)
                {
                    shoot();
                }
            }
        }

        if(em.checkStunStatus() == true)
        {
            canShoot = false;
            StartCoroutine("stunnedwait");
        }
        
    }

    private void FixedUpdate()
    {
        
        
    }

    void shoot()
    {
        _AS.PlayOneShot(_sound);
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

    IEnumerator stunnedwait()
    {
        canShoot = false;
        yield return new WaitForSeconds(1f);
        canShoot = true;
        em.setStunStatus(false);

    }

    IEnumerator startDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }
}
