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

	public int minWait = 5;
	public int maxWait = 15;

	public GameObject pickupArtObject;

	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag("Player"))
		{
			col.GetComponent<PlayerControl> ().AddPickup (thisType);

		}

		StartCoroutine (WaitTimer ());

	}

	IEnumerator WaitTimer ()
	{
		pickupArtObject.SetActive (false);
		GetComponent<BoxCollider> ().enabled = false;

		yield return new WaitForSeconds (Random.Range (minWait, maxWait));

		pickupArtObject.SetActive (true);
		GetComponent<BoxCollider> ().enabled = true;

	}
}
