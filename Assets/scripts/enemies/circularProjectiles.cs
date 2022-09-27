using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circularProjectiles : MonoBehaviour
{
	[SerializeField]
	int numberOfProjectiles;

	[SerializeField]
	GameObject projectile;

	[SerializeField]
	private float nextShot;

	[SerializeField]
	private float timeBetweenShots;

	Vector2 startPoint;

	[SerializeField]
	private float radius, moveSpeed;

	private bool canShoot;

	// Use this for initialization
	void Start()
	{
		startPoint = gameObject.transform.position;
		StartCoroutine("startDelay");
	}

	// Update is called once per frame
	void Update()
	{
		

	}

    private void FixedUpdate()
    {
		if(canShoot)
        {
			if (Time.time > nextShot)
			{
				shoot(numberOfProjectiles);
			}
		}
		
	}

    void shoot(int numberOfProjectiles)
	{
		float angleStep = 360f / numberOfProjectiles;
		float angle = 0f;

		for (int i = 0; i <= numberOfProjectiles - 1; i++)
		{
			startPoint = gameObject.transform.position;
			float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
			float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

			Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
			Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;

			var proj = Instantiate(projectile, startPoint, Quaternion.identity);
			proj.GetComponent<Rigidbody2D>().velocity =
				new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

			proj.transform.SetParent(gameObject.transform);

			angle += angleStep;
		}

		nextShot = Time.time + timeBetweenShots;
	}

	IEnumerator startDelay()
	{
		canShoot = false;
		yield return new WaitForSeconds(1f);
		canShoot = true;
	}

}
