/*
 *	Author:
 *	Last Updated:
 *
 *	Description:
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{

	void Start () 
	{
		FindObjectOfType<GameManager> ().remainingEnemies++;

	}

	void OnDestroy ()
	{
		//FindObjectOfType<GameManager> ().remainingEnemies--;


	}
	
	void Update () 
	{
		
	}
}