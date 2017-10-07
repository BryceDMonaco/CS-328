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

public enum PickupType {Spider, Other};

public class Pickup : MonoBehaviour 
{
	public PickupType thisType;

	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag("Player"))
		{
			col.GetComponent<PlayerControl> ().AddPickup (thisType);

		}

	}
}