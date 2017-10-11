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

public class GameController : MonoBehaviour 
{
	public Color[] playerColors;
	private Color player1Color;
	private Color player2Color;

	public PlayerControl[] players;

	private bool[] colorUsed;
	private bool gameOver = false;

	public int player1Webs = 0;
	public int player2Webs = 0;

	public int webMaxPerPlayer = 6;

	[Header ("Gameplay Variables")]
	public int player1Lives = 3;
	public int player2Lives = 3;

	//CTF
	public int player1Score = 0;
	public int player2Score = 0;

	[Header ("UI Variables")]
	public Text p1Header;
	public Text p1Score;
	public Image[] p1LifeSprites;

	[Space (10)]
	public Text p2Header;
	public Text p2Score;
	public Image[] p2LifeSprites;

	[Space (10)]
	public Text victoryText;
	public Text victorySubtitles;
	public Image victoryBackground;

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
		if (gameOver && Input.GetKeyDown (KeyCode.Space))
		{
			//Reload the level

		} else if (gameOver && Input.GetKeyDown (KeyCode.Escape))
		{
			//Return to menu

		}

		if (player1Score >= 3 || player2Lives <= 0) //Player 1 wins
		{
			victoryText.text = "Player 1 Wins!";

			Color tempColor = player1Color;

			tempColor.a = 0.5f;

			victoryBackground.color = tempColor;

			victoryText.enabled = true;
			victorySubtitles.enabled = true;
			victoryBackground.enabled = true;

			if (player2Lives > 0)
			{
				foreach (PlayerControl player in players)
				{
					player.FreezePlayer (true);

				}

			} else
			{
				players [0].FreezePlayer (true);

			}

			HideMiscUI ();

		} else if (player2Score >= 3 || player1Lives <= 0) //Player 2 wins
		{
			victoryText.text = "Player 2 Wins!";

			Color tempColor = player2Color;

			tempColor.a = 0.5f;

			victoryBackground.color = tempColor;

			victoryText.enabled = true;
			victorySubtitles.enabled = true;
			victoryBackground.enabled = true;

			if (player1Lives > 0)
			{
				foreach (PlayerControl player in players)
				{
					player.FreezePlayer (true);

				}

			} else
			{
				players [1].FreezePlayer (true);

			}

			HideMiscUI ();

		}
		
	}

	void PickColors ()
	{
		colorUsed = new bool[playerColors.Length];

		for (int i = 0; i < playerColors.Length; i++)
		{
			colorUsed [i] = false;

		}

		int onPlayer = 0;

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

					if (onPlayer == 0)
					{
						p1Header.color = playerColors [val];
						player1Color = playerColors [val];

					} else
					{
						p2Header.color = playerColors [val];
						player2Color = playerColors [val];

					}

				}

			}

			onPlayer++;

		}

	}

	public bool CanPlaceWeb (int player)
	{
		if (player == 1)
		{
			if (player1Webs < webMaxPerPlayer)
			{
				player1Webs++;

				return true;

			} else 
			{
				return false;

			}

		} else if (player == 2)
		{
			if (player2Webs < webMaxPerPlayer)
			{
				player2Webs++;

				return true;

			} else 
			{
				return false;

			}

		}

		return false;

	}

	public void AddScore (int playerNumber, int amount)
	{
		if (playerNumber == 1)
		{
			player1Score += amount;
			p1Score.text = "Score: " + player1Score;

		} else if (playerNumber == 2)
		{
			player2Score += amount;
			p2Score.text = "Score: " + player2Score;

		}

	}

	public void UpdateLives (int playerNumber, int amount)
	{
		if (playerNumber == 1)
		{
			player1Lives += amount;

			for (int i = 0; i < 5; i++)
			{
				if (i < player1Lives)
				{
					p1LifeSprites [i].enabled = true;

				} else
				{
					p1LifeSprites [i].enabled = false;

				}



			}

		} else if (playerNumber == 2)
		{
			player2Lives += amount;

			for (int i = 0; i < 5; i++)
			{
				if (i < player2Lives)
				{
					p2LifeSprites [i].enabled = true;

				} else
				{
					p2LifeSprites [i].enabled = false;

				}



			}

		}

	}

	void HideMiscUI ()
	{
		foreach (Image img in p1LifeSprites)
		{
			img.enabled = false;

		}

		foreach (Image img in p2LifeSprites)
		{
			img.enabled = false;

		}

		p1Score.enabled = false;
		p2Score.enabled = false;

		p1Header.enabled = false;
		p2Header.enabled = false;
	}
}