/*
 *	Author: Bryce Monaco
 *
 *	Last Updated: 9/30/17
 *
 *	Description:
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeleter : MonoBehaviour 
{
	public bool playersOnly = false; //Allows the script to also be used as out of bounds walls

	void OnTriggerEnter (Collider col)
	{
		if (!playersOnly && (col.CompareTag("Vehicle") || col.CompareTag("Log")))
		{
			Destroy (col.gameObject);

		} else if (col.CompareTag("Player"))
		{
			col.GetComponent<PlayerControl> ().RespawnPlayer ();

		}

	}
}