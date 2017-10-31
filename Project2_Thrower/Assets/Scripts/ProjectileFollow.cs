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

	private bool shouldGoBack = false;
	static private float t = 0f;
	private float startPos;
	public float returnSpeed = 0.5f;
	
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

			}

		} else if (projectile != null)
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


}