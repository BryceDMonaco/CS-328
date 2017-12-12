using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

	public const int mapSize = 16; //The size of the map in one dimension (map is square)

	public GameObject groundEnemy;

	private Transform[] edgeHexes;

	public Transform homeBase;
	public GameObject homeBaseObject;

	public int maxEnemiesSpawned = 4;
	private int currentEnemiesSpawned = 0;

	private bool canSpawn = false;

	public Text tipText;

	// Use this for initialization
	void Start () 
	{
		//tipText.text = "Select Home Base Position";
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (canSpawn && currentEnemiesSpawned < maxEnemiesSpawned)
		{
			currentEnemiesSpawned++;

			GameObject enemy = Instantiate (groundEnemy, edgeHexes [Random.Range (0, edgeHexes.Length)].position, Quaternion.identity);

			enemy.GetComponent<NavMeshAgent> ().SetDestination (homeBase.position);

			StartCoroutine (SpawnWait (Random.Range (0.5f, 1.5f)));

		}
		
	}

	IEnumerator SpawnWait (float time)
	{
		canSpawn = false;

		yield return new WaitForSeconds (time);

		canSpawn = true;

	}

	//This is called by the map generator after it has completed map generation
	//Note this function is hardcoded to assume map size of 16
	public void GetHexArraySize16 (GameObject[] hexArray)
	{
		//int numEdgeHexes = 4 * (mapSize - 1);
		int numEdgeHexes = 60; //Hardcoded Values

		edgeHexes = new Transform[numEdgeHexes]; //A map with size x has a border of 2n+2(n-2)=4(n-1) objects

		for (int i = 0; i < numEdgeHexes; i++)
		{
			if ((i >= 0) && (i <= 15))
			{
				edgeHexes [i] = hexArray [i].transform;

			} else if ((i >= 16) && (i <= 29))
			{
				edgeHexes [i] = hexArray [(16 * i) - 240].transform;

			} else if ((i >= 30) && (i <= 43))
			{
				edgeHexes [i] = hexArray [(16 * i) - 449].transform;

			} else if ((i >= 44) && (i <= 59))
			{
				edgeHexes [i] = hexArray [i + 196].transform;

			}

		}
			
		/*
		//Debug To Display Edge Hexes
		for (int i = 0; i < numEdgeHexes; i++)
		{
			if (edgeHexes[i] != null)
			{
				edgeHexes [i].GetComponent<MeshRenderer> ().material.color = Color.red;

			}

		}
		*/

	}

	public void SetHomeBase (Transform sentBase)
	{
		homeBase = sentBase;

		Vector3 hbPos = homeBase.position;
		hbPos.y = homeBase.localScale.y / 2;

		GameObject hb = Instantiate (homeBaseObject, hbPos, Quaternion.identity);

		canSpawn = true;

		tipText.text = "";

	}
}
