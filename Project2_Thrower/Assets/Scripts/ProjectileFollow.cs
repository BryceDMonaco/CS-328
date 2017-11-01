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

public class ProjectileFollow : MonoBehaviour 
{

	public Transform projectile;
	public Transform farLeft;
	public Transform farRight;
	public Transform cameraStartPoint;

	private bool shouldGoBack = false;
	private bool waitForStart = true;
	static private float t = 0f;
	private float startPos;
	public float returnSpeed = 0.5f;

	void Start ()
	{
		waitForStart = true;

		Vector3 newPosition = transform.position;
		newPosition.x = cameraStartPoint.position.x;

		transform.position = newPosition;

		StartCoroutine (StartWait ());

	}

	void Update () 
	{
		if (shouldGoBack)
		{
			Vector3 newPosition = transform.position;

			newPosition.x = Mathf.SmoothStep (startPos, farLeft.position.x, t);

			t += returnSpeed * Time.deltaTime;

			transform.position = newPosition;

			if (t >= 1f)
			{
				shouldGoBack = false;

				if (waitForStart)
				{
					waitForStart = false;

				}

			}

		} else if (projectile != null && !waitForStart)
		{
			Vector3 newPosition = transform.position;
			newPosition.x = projectile.position.x;

			newPosition.x = Mathf.Clamp (newPosition.x, farLeft.position.x, farRight.position.x);

			transform.position = newPosition;

		}

	}

	public void ReturnCamera ()
	{
		shouldGoBack = true;

		t = 0f;

		startPos = transform.position.x;

	}

	IEnumerator StartWait ()
	{

		yield return new WaitForSeconds (3f);

		ReturnCamera ();

	}


}