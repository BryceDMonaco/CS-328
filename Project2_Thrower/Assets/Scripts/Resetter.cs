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
using UnityEngine.SceneManagement;

public class Resetter : MonoBehaviour 
{
	int myLevel = 0;

	public float resetSpeed = 0.025f;
	private float resetSpeedSqr;
	private SpringJoint2D spring;

	public Rigidbody2D projectile;

	void Start () 
	{
		resetSpeedSqr = resetSpeed * resetSpeed;

		spring = projectile.GetComponent<SpringJoint2D> ();

	}
	
	void Update () 
	{
		if (Input.GetKey(KeyCode.R))
		{
			Reset ();

		}

		if (spring == null && projectile.velocity.sqrMagnitude < resetSpeedSqr)
		{
			Reset ();

		}

	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.attachedRigidbody == projectile)
		{
			Reset ();

		}

	}

	void Reset ()
	{
		SceneManager.LoadScene (myLevel);

	}
}