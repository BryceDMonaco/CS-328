/*
 *	Author: Bryce Monaco
 *	Last Updated: 11/14/2017
 *
 *	Description:	This script handles the camera behavior for every level.
 *					It is adapted from a script from Monaco's Project 2.
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
	private Transform target;
	public Transform widePlayer;
	public Transform tallPlayer;

	public Transform farLeft;
	public Transform farRight;
	public Transform cameraStartPoint;

	private bool shouldSwitchTargets = false;
	private bool targetTall = false;
	private bool targetWide = false;

	static private float t = 0f;
	private float startPos;
	public float returnSpeed = 0.5f;

	void Start ()
	{
		Vector3 newPosition = transform.position;
		newPosition.x = cameraStartPoint.position.x;

		transform.position = newPosition;

	}

	void Update () 
	{
		if (shouldSwitchTargets)
		{
			Vector3 newPosition = transform.position;

			newPosition.x = Mathf.SmoothStep (startPos, target.position.x, t);

			t += returnSpeed * Time.deltaTime;

			transform.position = newPosition;

			if (t >= 1f)
			{
				shouldSwitchTargets = false;
				Time.timeScale = 1f;

				FreezePlayers (false);

			}

		} else if (target != null)
		{
			Vector3 newPosition = transform.position;
			newPosition.x = target.position.x;

			newPosition.x = Mathf.Clamp (newPosition.x, farLeft.position.x, farRight.position.x);

			transform.position = newPosition;

		}

	}

	public void SwitchCameraTargets (bool goTall, bool goWide)
	{
		targetTall = goTall;
		targetWide = goWide;

		if (targetTall)
		{
			target = tallPlayer;

		} else if (targetWide)
		{
			target = widePlayer;

		}

		shouldSwitchTargets = true;

		t = 0f;

		startPos = transform.position.x;

		FreezePlayers (true);

	}

	public void SetTarget (bool goTall, bool goWide) //Used to manually changethe target
	{
		targetTall = goTall;
		targetWide = goWide;

		if (targetTall)
		{
			target = tallPlayer;

		} else if (targetWide)
		{
			target = widePlayer;

		}

	}

	void FreezePlayers (bool state) //Send T to freeze players, F to unfreeze
	{
		tallPlayer.GetComponent<PlatformController> ().enabled = !state;
		tallPlayer.GetComponent<Rigidbody2D> ().simulated = !state;

		widePlayer.GetComponent<PlatformController> ().enabled = !state;
		widePlayer.GetComponent<Rigidbody2D> ().simulated = !state;

	}
}