/*
 *	Author: Bryce Monaco
 *
 *	Last Updated: 9/14/2017
 *
 *	Description: A script which handles the spawning of vehicles and ensures correct timing to prevent clipping
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour 
{
	public float minCarWait = 2f;
	public float minSemiWait = 3f;	//The minimum amount of time the spawner should wait AFTER a semi is spawned
	public float maxWait = 5f;

	private bool isWaiting = true;
	private int lastSpawnedVehicle; //0 = car, 1 = semi
	private float lastSpawnedVehicleWait;

	public GameObject[] vehicles; //IMPORTANT: 0 should be car, 1 should be semi

	void Start () 
	{
		StartCoroutine (SpawnTimer (0f));

	}
	
	void Update () 
	{
		if (!isWaiting)
		{

			SpawnNextVehicle ();

		}
		
	}

	IEnumerator SpawnTimer ()
	{
		isWaiting = true;

		yield return new WaitForSeconds (Random.Range(lastSpawnedVehicleWait, maxWait));

		isWaiting = false;

	}

	IEnumerator SpawnTimer (float startRange)
	{
		isWaiting = true;

		yield return new WaitForSeconds (Random.Range(startRange, maxWait));

		isWaiting = false;

	}

	void SpawnNextVehicle ()
	{
		int choice = Random.Range (0, vehicles.Length);

		if (choice == 0)
		{
			lastSpawnedVehicleWait = minCarWait;

		} else if (choice == 1)
		{
			lastSpawnedVehicleWait = minSemiWait;

		} else
		{
			Debug.LogError ("VehicleSpawner has invalid choice " + choice);

		}

		Instantiate (vehicles[choice], transform.position, transform.rotation);

		lastSpawnedVehicle = choice;

		StartCoroutine (SpawnTimer ());

	}
}