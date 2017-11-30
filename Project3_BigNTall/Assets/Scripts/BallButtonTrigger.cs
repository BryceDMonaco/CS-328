/*
 *	Authors: Bryce Monaco & Alex Kastanek
 *	Last Updated: 11/22/2017
 *
 *	Description:	This script handles buttons for players to push balls to press to trigger events.
 *					NOTE: The triggered object must have a script with a public "Trigger" function.
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallButtonTrigger : MonoBehaviour 
{
	public TriggeredObject myTarget;

	public bool beenTriggered = false;

	void OnTriggerEnter2D (Collider2D col)
	{
		if (!beenTriggered && col.CompareTag("ball"))
		{
			myTarget.Trigger ();

			beenTriggered = true;

		}

	}
}