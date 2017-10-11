/*
 *	Author: Bryce Monaco
 *
 *	Last Updated: 10/7/17
 *
 *	Description:
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour 
{
	public Color myColor;
	public int health;
	private GameObject myOwner;
	private bool hasPlayerTrapped = false;
	int targetPlayerNumber;
	PlayerControl target;

	public Transform webArt;

	public int maxHeath = 10;
	public int minHealth = 5;
	private int topHealth;

	public float stableLifetime = 10f; 	//The web will not change for this amount of s
	public float degradeTime = 1f;		//After stableLifetime s, the web will lose 1 health every this amount of s
	private bool degradeCycleRunning = false;

	private KeyCode upKey;
	private KeyCode downKey;
	private KeyCode leftKey;
	private KeyCode rightKey;
	private KeyCode attackKey;
	private KeyCode altAttackKey;

	private GameController controller;

	void Start () 
	{
		controller = FindObjectOfType<GameController> ();

		health = Random.Range (minHealth, maxHeath);
		topHealth = health;

		StartCoroutine (Lifetime ());

		degradeCycleRunning = true;

	}
	
	void Update () 
	{
		if (!degradeCycleRunning)
		{
			StartCoroutine (HealthDegradation ());

		}

		if (hasPlayerTrapped)
		{
			if (Input.GetKeyDown (leftKey) || Input.GetKeyDown (rightKey) || Input.GetKeyDown (upKey) || Input.GetKeyDown (downKey))
			{
				health--;

			}

			if (health <= 0)
			{
				target.FreezePlayer (false);

				if (myOwner.GetComponent<PlayerControl>().player == 1)
				{
					controller.player1Webs -= 1;

				} else
				{
					controller.player2Webs -= 1;

				}

				Destroy (gameObject);

			}

		}

		float scaleFactor = (float)health / (float)topHealth;

		webArt.localScale = Vector3.one * scaleFactor;

		if (health <= 0)
		{
			if (myOwner.GetComponent<PlayerControl>().player == 1)
			{
				controller.player1Webs -= 1;

			} else
			{
				controller.player2Webs -= 1;

			}

			Destroy (gameObject);

		}

	}

	public void SetColor (Color newColor, GameObject owner)
	{
		myOwner = owner;

		myColor = newColor;

		GetComponentInChildren<MeshRenderer> ().material.SetColor("_Color", myColor);
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag("Player") && col.gameObject != myOwner)
		{
			target = col.GetComponent<PlayerControl> ();

			target.FreezePlayer (true);

			SetInputs (target.player);

			hasPlayerTrapped = true;

		} else if (col.CompareTag("Vehicle") && hasPlayerTrapped) //A vehicle is hitting the web with a player attached
		{
			target.FreezePlayer (false);

			target.RespawnPlayer ();

			Destroy (gameObject);

		}

	}

	void SetInputs (int playerNum)
	{
		targetPlayerNumber = playerNum;

		if (targetPlayerNumber == 1)
		{
			upKey = KeyCode.W;
			downKey = KeyCode.S;
			leftKey = KeyCode.A;
			rightKey = KeyCode.D;
			attackKey = KeyCode.LeftShift;
			altAttackKey = KeyCode.LeftAlt;

		} else
		{
			upKey = KeyCode.UpArrow;
			downKey = KeyCode.DownArrow;
			leftKey = KeyCode.LeftArrow;
			rightKey = KeyCode.RightArrow;
			attackKey = KeyCode.RightShift;
			altAttackKey = KeyCode.RightAlt;

		}

	}

	IEnumerator Lifetime ()
	{
		yield return new WaitForSeconds (stableLifetime);

		degradeCycleRunning = false;


	}

	IEnumerator HealthDegradation ()
	{
		degradeCycleRunning = true;

		yield return new WaitForSeconds (degradeTime);

		health--;

		degradeCycleRunning = false;

	}

	public void HitByTongue (GameObject tongueOwner)
	{
		if (!hasPlayerTrapped && tongueOwner != myOwner) //If the enemy hits the web with the tongue and isn't already trapped
		{
			health -= 3;

		}

	}

}