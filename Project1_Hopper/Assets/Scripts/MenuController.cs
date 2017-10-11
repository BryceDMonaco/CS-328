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
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour 
{
	public Color[] playerColors;
	private Color player1Color;
	private Color player2Color;

	public BotController[] players;

	private bool[] colorUsed;
	private bool gameOver = false;

	void Start () 
	{
		//players = FindObjectsOfType<PlayerControl> ();

		if (players.Length >= playerColors.Length)
		{
			Debug.LogError ("Error: More players than available colors!");

		}

		PickColors ();

	}

	void Update () 
	{
		

	}

	void PickColors ()
	{
		colorUsed = new bool[playerColors.Length];

		for (int i = 0; i < playerColors.Length; i++)
		{
			colorUsed [i] = false;

		}

		int onPlayer = 0;

		foreach (BotController thisPlayer in players)
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

					if (onPlayer == 0)
					{
						player1Color = playerColors [val];

					} else
					{
						player2Color = playerColors [val];

					}

				}

			}

			onPlayer++;

		}

	}

	public void StartGame ()
	{
		SceneManager.LoadScene ("Game");

	}
}