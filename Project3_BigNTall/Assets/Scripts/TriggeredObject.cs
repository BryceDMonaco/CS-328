/*
 *	Author: Bryce Monaco
 *	Last Updated: 11/14/2017
 *
 *	Description:	This script is called by a button trigger to perform an action
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredObject : MonoBehaviour 
{
	public bool triggered = false;
	public bool shouldMove = false;

	[Header ("Moving Object")]
	public bool isMovingObject = false;

	public float startPosition = 0f;
	public float endPosition = 0f;

	public float moveSpeed = 0.5f;

	private float t = 0f;

	[Header ("Scaling Object")]
	public bool isScalingObject = false;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		if (triggered && shouldMove)
		{
			Vector3 newPosition = transform.localPosition;

			newPosition.x = Mathf.SmoothStep (startPosition, endPosition, t);

			t += moveSpeed * Time.deltaTime;

			transform.localPosition = newPosition;

			if (t >= 1f)
			{
				shouldMove = false;

			}

		}
		
	}

	public void Trigger ()
	{
		triggered = true;
		shouldMove = true;

		t = 0f;

	}
}