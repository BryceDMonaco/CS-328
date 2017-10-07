/*
 *	Author: Bryce Monaco
 *
 *	Last Updated: 10/7/17
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
			//Check for any player children and respawn them
			PlayerControl[] attachedPlayers = col.GetComponentsInChildren<PlayerControl>();

			foreach (PlayerControl p in attachedPlayers)
			{
				p.RespawnPlayer ();

			}

			//Once children are taken care of then delete the object after 5s to give it time to go off screen
			Destroy (col.gameObject, 5f);

		} else if (col.CompareTag("Player"))
		{
			col.GetComponent<PlayerControl> ().RespawnPlayer ();

		}

	}
}