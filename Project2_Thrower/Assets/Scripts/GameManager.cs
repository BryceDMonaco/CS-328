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
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	public int remainingEnemies = 0;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		if (remainingEnemies <= 0)
		{
			SceneManager.LoadScene (0);

		}
		
	}
}