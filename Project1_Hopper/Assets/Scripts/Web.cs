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

	public int maxHeath = 10;
	public int minHealth = 5;

	private KeyCode upKey;
	private KeyCode downKey;
	private KeyCode leftKey;
	private KeyCode rightKey;
	private KeyCode attackKey;
	private KeyCode altAttackKey;

	void Start () 
	{
		health = Random.Range (minHealth, maxHeath);

	}
	
	void Update () 
	{
		if (hasPlayerTrapped)
		{
			if (Input.GetKeyDown (leftKey) || Input.GetKeyDown (rightKey) || Input.GetKeyDown (upKey) || Input.GetKeyDown (downKey))
			{
				health--;

			}

			if (health <= 0)
			{
				target.FreezePlayer (false);

				Destroy (gameObject);

			}

		}

	}

	public void SetColor (Color newColor, GameObject owner)
	{
		myOwner = owner;

		myColor = newColor;

		GetComponent<MeshRenderer> ().material.SetColor("_Color", myColor);
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag("Player") && col.gameObject != myOwner)
		{
			target = col.GetComponent<PlayerControl> ();

			target.FreezePlayer (true);

			SetInputs (target.player);

			hasPlayerTrapped = true;
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
}