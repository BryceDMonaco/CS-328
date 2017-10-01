/*
 *	Author: Bryce Monaco
 *
 *	Last Updated: 9/30/17
 *
 *	Description:
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	public Color[] playerColors;

	public PlayerControl[] players;

	private bool[] colorUsed;

	void Start () 
	{
		if (players.Length >= playerColors.Length)
		{
			Debug.LogError ("Error: More players than available colors!");

		}

		PickColors ();
		
	}
	
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			PickColors ();

		}
		
	}

	void PickColors ()
	{
		colorUsed = new bool[playerColors.Length];

		for (int i = 0; i < playerColors.Length; i++)
		{
			colorUsed [i] = false;

		}

		foreach (PlayerControl thisPlayer in players)
		{
			bool validColor = false;

			while (!validColor)
			{
				int val = Random.Range (0, playerColors.Length);

				if (!colorUsed[val])
				{
					thisPlayer.AssignColor (playerColors [val]);

					colorUsed [val] = true;

					validColor = true;

				}

			}

		}

	}
}