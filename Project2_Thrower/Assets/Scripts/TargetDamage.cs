/*
 *	Author:
 *	Last Updated:
 *
 *	Description:
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDamage : MonoBehaviour 
{
	public int hitPoints = 2;
	public Sprite damagedSprite;
	public float damageImpactSpeed;
	public bool shouldSplinter = false;
	public GameObject[] splinters;
	public int pointValue = 10;
	public GameManager myManager;

	private int currentHitPoints;
	private float damageImpactSpeedSqr;
	private SpriteRenderer spriteRenderer;
	private bool isAlive = true;
	private float lastVelocitySqr = 0f;
	private Rigidbody2D myRB;

	void Start () 
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();

		currentHitPoints = hitPoints;

		damageImpactSpeedSqr = damageImpactSpeed * damageImpactSpeed;

		myRB = GetComponent<Rigidbody2D> ();
		
	}

	void FixedUpdate ()
	{
		float thisFrameVelocitySqr = myRB.velocity.sqrMagnitude;

		if ((thisFrameVelocitySqr < 0.0025f) && (lastVelocitySqr > damageImpactSpeedSqr))
		{
			Damage (1);

		}

		lastVelocitySqr = thisFrameVelocitySqr;

	}
	
	void OnCollisionEnter2D (Collision2D collision)
	{
		if (!collision.collider.CompareTag("Damager") || collision.collider.name.Contains("Plank"))
		{
			return;

		}

		if (collision.relativeVelocity.sqrMagnitude < damageImpactSpeedSqr)
		{
			return;

		}

		Damage (1);

	}

	public void Damage (int amount)
	{
		//Debug.Log(transform.name + " hit!");

		spriteRenderer.sprite = damagedSprite;

		currentHitPoints -= amount;

		if (currentHitPoints <= 0 && isAlive)
		{
			Kill ();

		}

	}

	public void Kill ()
	{
		isAlive = false;

		spriteRenderer.enabled = false;

		GetComponent<Collider2D> ().enabled = false;
		GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Kinematic;

		ParticleSystem mySystem = GetComponent<ParticleSystem> ();

		if (mySystem != null)
		{
			mySystem.Emit (Random.Range (30, 50));

		}

		if (shouldSplinter)
		{
			for (int i = 0; i < Random.Range(1, 3); i++)
			{
				GameObject splinter = Instantiate (splinters [Random.Range (0, splinters.Length)], transform.position, transform.rotation);
				Destroy (splinter, Random.Range (3f, 5f));

			}

		} else
		{
			//GameManager myManager = FindObjectOfType<GameManager> ();

			myManager.remainingEnemies--;
			myManager.ChangeScore (pointValue);

		}

	}
}