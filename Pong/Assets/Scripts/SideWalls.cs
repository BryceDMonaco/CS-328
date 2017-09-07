/*
 *	Author:
 *
 *	Last Updated:
 *
 *	Description:
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWalls : MonoBehaviour 
{

	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.name == "Ball")
		{
			GameManager.Score (transform.name);

			col.GetComponent<BallControl> ().RestartGame ();

		}

	}
}