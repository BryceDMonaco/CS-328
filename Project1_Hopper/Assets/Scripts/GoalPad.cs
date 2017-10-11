/*
 *	Author: Bryce Monaco
 *
 *	Last Updated: 10/10/17
 *
 *	Description: Handles scoring for the lily pad
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPad : MonoBehaviour 
{
	public GameObject myPlayer; //The player assigned to this is defending the pad
	public GameController controller;
	public GameObject confetti;

	void Start ()
	{
		controller = FindObjectOfType<GameController> ();

	}

	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag("Player") && col.gameObject != myPlayer && col.GetComponent<PlayerControl>().hasFly)
		{
			int playerNum = col.GetComponent<PlayerControl> ().player;

			controller.AddScore (playerNum, 1);

			GameObject fetti = Instantiate (confetti, transform.position, Quaternion.Euler(Vector3.left * 90));

			Destroy (fetti, 10f);

		} else if (col.CompareTag("Fly"))
		{
			col.GetComponent<GoldenFly> ().RespawnFly ();

		}

	}
}