using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public const int mapSize = 16; //The size of the map in one dimension (map is square)

	public GameObject groundEnemy;

	private Transform[] edgeHexes;

	public Transform homeBase;

	// Use this for initialization
	void Start () 
	{
		
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		
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
}
