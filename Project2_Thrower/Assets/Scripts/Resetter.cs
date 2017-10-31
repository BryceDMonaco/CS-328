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
	public SpringJoint2D spring;
	private GameManager myManager;

	public Rigidbody2D projectile;

	void Start () 
	{
		resetSpeedSqr = resetSpeed * resetSpeed;

		spring = projectile.GetComponent<SpringJoint2D> ();

		myManager = FindObjectOfType<GameManager> ();

	}
	
	void Update () 
	{
		if (Input.GetKey(KeyCode.R))
		{
			Reset ();

		}

		if (spring == null && projectile != null && projectile.velocity.sqrMagnitude < resetSpeedSqr)
		{
			projectile.GetComponent<ProjectileDragging> ().Kill ();
			myManager.BallAway ();


		}

	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.attachedRigidbody == projectile)
		{
			//col.GetComponent<ProjectileDragging> ().Kill ();
			Destroy(col.gameObject);
			Reset ();



		} else if (col.CompareTag("Enemy") || col.CompareTag("Damager"))
		{
			col.GetComponent<TargetDamage> ().Kill ();

		}

	}

	void Reset ()
	{
		//SceneManager.LoadScene (myLevel);
		myManager.BallAway();

	}
}