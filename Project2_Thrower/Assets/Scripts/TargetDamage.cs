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

	private int currentHitPoints;
	private float damageImpactSpeedSqr;
	private SpriteRenderer spriteRenderer;
	private bool isAlive = true;

	void Start () 
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();

		currentHitPoints = hitPoints;

		damageImpactSpeedSqr = damageImpactSpeed * damageImpactSpeed;
		
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

		spriteRenderer.sprite = damagedSprite;

		currentHitPoints--;

		if (currentHitPoints <= 0 && isAlive)
		{
			Kill ();

		}

	}

	void Kill ()
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
			FindObjectOfType<GameManager> ().remainingEnemies--;

		}

	}
}