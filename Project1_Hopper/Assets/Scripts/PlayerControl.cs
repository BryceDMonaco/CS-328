/*
 *	Author: Bryce Monaco
 *
 *	Last Updated: 9/14/2017
 *
 *	Description: Handles input/controls for each player
 *
 *	TODO: 	
 *
 *	BUGS:	Leaving a log onto water doesn't kill the player
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour 
{
	public int player = 1; //Assume P1
	public int health = 3;

	[Header("Movement Variables")]
	public int moveDistance = 4;
	public float moveSpeed = 2;

	private Vector3 spawnPoint;

	public bool onLog = false;
	public bool overWater = false;
	private bool isMoving = false;

	private KeyCode upKey;
	private KeyCode downKey;
	private KeyCode leftKey;
	private KeyCode rightKey;

	void Start () 
	{
		spawnPoint = transform.position;

		if (player == 1)
		{
			upKey = KeyCode.W;
			downKey = KeyCode.S;
			leftKey = KeyCode.A;
			rightKey = KeyCode.D;

		} else
		{
			upKey = KeyCode.UpArrow;
			downKey = KeyCode.DownArrow;
			leftKey = KeyCode.LeftArrow;
			rightKey = KeyCode.RightArrow;

		}
		
	}
	
	void Update () 
	{
		Vector3 fromPos = Vector3.zero;
		Vector3 toPos = Vector3.zero;
		float startTime = 0f;
		float journeyLength = 0f;
		float fracJourney = 0f;

		if (!isMoving && (Input.GetKeyDown (leftKey) || Input.GetKeyDown (rightKey) || Input.GetKeyDown (upKey) || Input.GetKeyDown (downKey))) 
		{
			fromPos = transform.position;
			toPos = transform.position + CheckInputs ();
			transform.position = toPos;

		}




		if (!onLog)
		{
			transform.SetParent (null);

			ReturnToGrid ();

		}

		if (overWater && !onLog)
		{
			RespawnPlayer ();

		}
			
	}

	bool DoDirectionCheck (Vector3 direction)
	{
		if (Physics.Raycast(transform.position, direction, 4f, 8))
		{
			Debug.Log ("Hit!");

			return true;

		} else
		{
			return false;

		}

	}
		
	void OnTriggerEnter (Collider col)
	{
		//Debug.Log ("Ding");

		if (col.CompareTag("Vehicle"))
		{
			RespawnPlayer ();

		}

	

	}

	void ReturnToGrid ()
	{
		//Debug.Log ("New X is: " + (4 * ((int)transform.position.x / 4)).ToString());

		Vector3 newGridPos = new Vector3 ((4 * ((int)transform.position.x / 4)), transform.position.y, transform.position.z);

		transform.position = newGridPos;



	}

	void RespawnPlayer ()
	{
		transform.position = spawnPoint;

		transform.SetParent (null);

		onLog = false;

	}

	IEnumerator WaitForLogCheck ()
	{
		//yield return new WaitForEndOfFrame ();
		yield return new WaitForEndOfFrame ();

		if (!onLog)
		{
			RespawnPlayer ();

		}

	}

	IEnumerator ForceTriggerRecheck ()
	{
		GetComponents<BoxCollider> () [1].enabled = false;

		yield return new WaitForEndOfFrame ();

		GetComponents<BoxCollider> () [1].enabled = true;



	}

	public void SetSpecialState (int stateNum, bool state) //0 is onLog, 1 is overWater
	{
		switch (stateNum)
		{
		case 0:
			onLog = state;

			break;

		case 1:
			overWater = state;

			break;

		default:
			Debug.Log ("Unrecognized special state requested: " + stateNum);

			break;

		}

	}

	private Vector3 CheckInputs ()
	{
		if (Input.GetKeyDown(leftKey) && !DoDirectionCheck(Vector3.forward))
		{
			//transform.Translate (new Vector3 (0, 0, moveDistance));

			//Vector3 fromPos = transform.position;
			//Vector3 toPos = transform.position + new Vector3 (0, 0, moveDistance);

			return new Vector3 (0, 0, moveDistance);

		} else if (Input.GetKeyDown(rightKey) && !DoDirectionCheck(Vector3.back))
		{
			//transform.Translate (new Vector3 (0, 0, -moveDistance));

			return new Vector3 (0, 0, -moveDistance);

		} else if (Input.GetKeyDown(upKey) && !DoDirectionCheck(Vector3.right))
		{
			//transform.Translate (new Vector3 (moveDistance, 0, 0));

			return new Vector3 (moveDistance, 0, 0);

		} else if (Input.GetKeyDown(downKey) && !DoDirectionCheck(Vector3.left))
		{
			//transform.Translate (new Vector3 (-moveDistance, 0, 0));

			return new Vector3 (-moveDistance, 0, 0);

		}

		return Vector3.zero;

	}
}