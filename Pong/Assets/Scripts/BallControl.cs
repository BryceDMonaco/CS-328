/*
 *	Author: Bryce Monaco (Tutorial-Based for CS 328)
 *
 *	Last Updated: 9/6/2017
 *
 *	Description: Script to handle ball behavior
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour 
{

	public float forceMultiplier = 2f;

	private Rigidbody2D myRigidbody;
	private Vector2 vel;

	void Start () 
	{
		myRigidbody = GetComponent<Rigidbody2D> ();
		Invoke ("GoBall", 2);

	}
	
	void Update () 
	{
		
	}

	void GoBall ()
	{
		float rand = Random.Range (0, 2);

		if (rand < 1)
		{
			myRigidbody.AddForce (new Vector2 (20, -15) * forceMultiplier);

		} else
		{
			myRigidbody.AddForce (new Vector2 (-20, -15) *forceMultiplier);

		}

	}

	public void ResetBall ()
	{
		vel = Vector2.zero;
		myRigidbody.velocity = vel;

		transform.position = Vector2.zero;

	}

	public void RestartGame ()
	{
		ResetBall ();
		Invoke ("GoBall", 1);

	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.collider.CompareTag("Player"))
		{
			vel.x = myRigidbody.velocity.x;
			vel.y = (myRigidbody.velocity.y / 2f) + (col.collider.attachedRigidbody.velocity.y / 3f);
			myRigidbody.velocity = vel;

		}

	}
}