/*
 *	Author: Bryce Monaco
 *	Last Updated: 11/14/2017
 *
 *	Description:	This script handles buttons for players to press to trigger events.
 *					NOTE: The triggered object must have a script with a public "Trigger" function.
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour 
{
	public TriggeredObject myTarget;

	public bool beenTriggered = false;

	void OnTriggerEnter2D (Collider2D col)
	{
		if (!beenTriggered && col.CompareTag("Player"))
		{
			myTarget.Trigger ();

			beenTriggered = true;

		}

	}
}