/*
 *	Author: Bryce Monaco
 *
 *	Last Updated: 9/14/2017
 *
 *	Description: Handles input/controls for each player
 *
 *	TODO: 	Movement 'animation', attack, make ReturnToGrid() more predictable
 *
 *	BUGS:	Leaving a log onto water doesn't kill the player
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour 
{
	public int player = 1; //Assume P1
	public int health = 3;

	public float minMoveTime = 0.2f;
	public float maxMoveTime = 2f;

	[Header ("Movement Variables")]
	public int moveDistance = 4;
	public float moveSpeed = 2;

	private Vector3 spawnPoint;

	private int xBounds = 16;			//To prevent the player from escaping the game area on the x axis
	private int zBounds = 32;			//To prevent the player from escaping on the z axis

	private int direction;

	[Header ("Attack Variables")]
	private GameController controller;

	[Header ("Status Variables")]
	public bool onLog = false;
	public bool overWater = false;
	private bool isMoving = false;
	private bool canMove = false;
	public bool hasFly = false;

	[Header ("Art Variables")]
	public MeshRenderer[] bodyParts;
	public Transform bodyPartsPivot;
	private Color myColor;

	void Start () 
	{
		controller = FindObjectOfType<GameController> ();

		spawnPoint = transform.position;

		bodyParts = GetComponentsInChildren<MeshRenderer> ();

		StartCoroutine (MoveWaitTimer ());

	}

	void Update () 
	{
		Vector3 fromPos = Vector3.zero;
		Vector3 toPos = Vector3.zero;
		float startTime = 0f;
		float journeyLength = 0f;
		float fracJourney = 0f;

		if (!isMoving && canMove)
		{
			fromPos = transform.position;
			toPos = transform.position + GenerateInputs ();
			//Quaternion newRotation = Quaternion.Euler (90 * GetNewRotation ());
			transform.position = toPos;
			//bodyPartsPivot.rotation = newRotation;

			Quaternion newRotation = Quaternion.Euler (90 * GetNewRotation ());
			bodyPartsPivot.rotation = newRotation;

			StartCoroutine (MoveWaitTimer ());

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
		bool inPlay;
		bool isObstructed;

		RaycastHit hit;

		if (Physics.Raycast(transform.position, direction, out hit, 4f) && hit.collider.CompareTag("Player")) //Should only be able to hit other players
		{
			Debug.Log ("Hit!");

			isObstructed = true;

		} else
		{
			isObstructed = false;

		}

		Vector3 newPos = transform.position + (moveDistance * direction);

		Debug.Log ("Checking position for validity: " + newPos);

		if ((newPos.x >= -xBounds && newPos.x <= xBounds) && (newPos.z >= -zBounds && newPos.z <= zBounds))
		{
			inPlay = true;

		} else
		{
			inPlay = false;

		}

		Debug.Log ("Position is valid: " + inPlay + " Obstructed: " + isObstructed);

		return (isObstructed || !inPlay);

	}

	bool DoPositionCheck (Vector3 offset)
	{

		//Debug.Log ("Checking position for validity: " + newPos);

		bool inPlay;

		Vector3 newPos = transform.position + offset;

		if ((newPos.x >= -xBounds && newPos.x <= xBounds) && (newPos.z >= -zBounds && newPos.z <= zBounds))
		{
			inPlay = true;

		} else
		{
			inPlay = false;

		}

		return inPlay;

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

		//Vector3 newGridPos = new Vector3 ((4 * ((int) (transform.position.x / 4))), transform.position.y, transform.position.z);
		Vector3 newGridPos = new Vector3 (GetRoundedValue(transform.position.x, moveDistance), transform.position.y, GetRoundedValue(transform.position.z, moveDistance));

		transform.position = newGridPos;



	}

	public void RespawnPlayer ()
	{
		transform.SetParent (null);

		transform.position = spawnPoint;

		onLog = false;

	}

	//Deprecated
	IEnumerator WaitForLogCheck ()
	{
		//yield return new WaitForEndOfFrame ();
		yield return new WaitForEndOfFrame ();

		if (!onLog)
		{
			RespawnPlayer ();

		}

	}

	//Deprecated
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

			if (state)
			{
				//transform.localPosition = new Vector3 (0.75f, transform.localPosition.y, transform.localPosition.z);

			} else
			{
				//transform.position = new Vector3 (transform.position.x, 0.88f, transform.position.z);

			}

			break;

		case 1:
			overWater = state;

			break;

		case 2:
			onLog = state;

			ReturnToGrid ();

			break;

		default:
			Debug.Log ("Unrecognized special state requested: " + stateNum);

			break;

		}

	}

	private Vector3 GenerateInputs ()
	{
		direction = Random.Range (0, 5); //4 and 4 are no move

		if (direction == 0 && !DoDirectionCheck(Vector3.forward))
		{
			//transform.Translate (new Vector3 (0, 0, moveDistance));

			//Vector3 fromPos = transform.position;
			//Vector3 toPos = transform.position + new Vector3 (0, 0, moveDistance);

			return new Vector3 (0, 0, moveDistance);

		} else if (direction == 1 && !DoDirectionCheck(Vector3.back))
		{
			//transform.Translate (new Vector3 (0, 0, -moveDistance));

			return new Vector3 (0, 0, -moveDistance);

		} else if (direction == 2 && !DoDirectionCheck(Vector3.right))
		{
			//transform.Translate (new Vector3 (moveDistance, 0, 0));

			return new Vector3 (moveDistance, 0, 0);

		} else if (direction == 3 && !DoDirectionCheck(Vector3.left))
		{
			//transform.Translate (new Vector3 (-moveDistance, 0, 0));

			return new Vector3 (-moveDistance, 0, 0);

		}

		return Vector3.zero;

	}

	private Vector3 GetNewRotation ()
	{
		if (direction == 0)
		{
			//transform.Translate (new Vector3 (0, 0, moveDistance));

			//Vector3 fromPos = transform.position;
			//Vector3 toPos = transform.position + new Vector3 (0, 0, moveDistance);

			return new Vector3 (0, 0, 0);

		} else if (direction == 1)
		{
			//transform.Translate (new Vector3 (0, 0, -moveDistance));

			return new Vector3 (0, -2f, 0);

		} else if (direction == 2)
		{
			//transform.Translate (new Vector3 (moveDistance, 0, 0));

			return new Vector3 (0, 1, 0);

		} else if (direction == 3)
		{
			//transform.Translate (new Vector3 (-moveDistance, 0, 0));

			return new Vector3 (-moveDistance, -1, 0);

		}

		return Vector3.zero;

	}

	//Takes the sent value and finds the nearest multiple of target. Ex. if value = 13 and target = 4 then 12 is returned
	int GetRoundedValue (float value, int target)
	{
		int halfway = (target / 2) - 1;

		int tempValue = (int) value;

		if (tempValue % target <= halfway)
		{
			tempValue -= (tempValue % target);

		} else
		{
			tempValue += target - (tempValue % target);

		}

		return tempValue;

	}

	public void AssignColor (Color newColor)
	{
		GetComponent<MeshRenderer> ().material.color = newColor;

		myColor = newColor;

	}

	IEnumerator MoveWaitTimer ()
	{
		canMove = false;

		yield return new WaitForSeconds (Random.Range (minMoveTime, maxMoveTime));

		canMove = true;

	}

}