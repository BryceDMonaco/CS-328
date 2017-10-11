/*
 *	Author: Bryce Monaco
 *
 *	Last Updated: 10/10/17
 *
 *	Description: 
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenFly : MonoBehaviour 
{
	public Transform carrier;
	private bool hasCarrier = false;
	private Vector3 spawnPoint;

	void Start () 
	{
		spawnPoint = transform.position;
		
	}
	
	void Update () 
	{
		if (hasCarrier)
		{
			Vector3 newPos = new Vector3 (carrier.position.x, transform.position.y, carrier.position.z);

			transform.position = newPos;

		}
		
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag("Player") && !hasCarrier)
		{
			hasCarrier = true;

			col.GetComponent<PlayerControl> ().PickedFly (true);

			carrier = col.transform;

		}

	}

	public void PlayerDroppedFly ()
	{
		carrier = null;

		hasCarrier = false;

	}

	public void RespawnFly ()
	{
		hasCarrier = false;

		carrier = null;

		transform.position = spawnPoint;


	}
}