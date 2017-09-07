/*
 *	Author: Bryce Monaco (Tutorial Based for CS 328)
 *
 *	Last Updated: 9/6/2017
 *
 *	Description: Handles Scoring
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{

	public static int playerScore1 = 0;
	public static int playerScore2 = 0;

	public GUISkin layout;

	BallControl ball;

	void Start () 
	{
		ball = GameObject.FindGameObjectWithTag ("Ball").GetComponent<BallControl>();
		
	}
	
	void Update () 
	{
		
	}

	public static void Score (string wallID)
	{
		if (wallID == "RightWall")
		{
			playerScore1++;

		} else
		{
			playerScore2++;

		}

	}

	void OnGUI ()
	{
		GUI.skin = layout;
		GUI.Label (new Rect (Screen.width / 2 - 150 - 12, 20, 100, 100), playerScore1.ToString ());
		GUI.Label (new Rect (Screen.width / 2 + 150 + 12, 20, 100, 100), playerScore2.ToString ());

		if (GUI.Button (new Rect (Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
		{
			playerScore1 = 0;
			playerScore2 = 0;

			ball.RestartGame ();

		}

		if (playerScore1 == 10)
		{
			GUI.Label (new Rect (Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER ONE WINS");
			ball.ResetBall ();

		} else if (playerScore2 == 10)
		{
			GUI.Label (new Rect (Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER Two WINS");
			ball.ResetBall ();

		}

	}
}