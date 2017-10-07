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

public class PlayerControl : MonoBehaviour 
{
	public int player = 1; //Assume P1
	public int health = 3;

	[Header ("Movement Variables")]
	public int moveDistance = 4;
	public float moveSpeed = 2;

	private Vector3 spawnPoint;

	private int xBounds = 16;			//To prevent the player from escaping the game area on the x axis
	private int zBounds = 32;			//To prevent the player from escaping on the z axis

	[Header ("Attack Variables")]
	public float maxAttackReach = 4;
	private float actualReach; 			//Calculated with raycast, will range from 0 to maxAttackReach

	public float attackCooldown = .5f;
	public LineRenderer tongueRenderer;
	private bool isAttacking = false;

	[Header ("Status Variables")]
	public bool onLog = false;
	public bool overWater = false;
	private bool isMoving = false;

	[Header ("Art Variables")]
	public MeshRenderer[] bodyParts;
	public Transform bodyPartsPivot;

	private KeyCode upKey;
	private KeyCode downKey;
	private KeyCode leftKey;
	private KeyCode rightKey;
	private KeyCode attackKey;

	void Start () 
	{
		spawnPoint = transform.position;

		if (player == 1)
		{
			upKey = KeyCode.W;
			downKey = KeyCode.S;
			leftKey = KeyCode.A;
			rightKey = KeyCode.D;
			attackKey = KeyCode.LeftShift;

		} else
		{
			upKey = KeyCode.UpArrow;
			downKey = KeyCode.DownArrow;
			leftKey = KeyCode.LeftArrow;
			rightKey = KeyCode.RightArrow;
			attackKey = KeyCode.RightShift;

		}

		bodyParts = GetComponentsInChildren<MeshRenderer> ();
		
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
			Quaternion newRotation = Quaternion.Euler (90 * GetNewRotation ());
			transform.position = toPos;
			bodyPartsPivot.rotation = newRotation;

		}

		if (!isAttacking && Input.GetKey(attackKey))
		{
			AttackCheck ();

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

	//TODO: Can be obtimized by having CheckInputs() call this function
	private Vector3 GetNewRotation ()
	{
		if (Input.GetKeyDown(leftKey) && !DoDirectionCheck(Vector3.forward))
		{
			//transform.Translate (new Vector3 (0, 0, moveDistance));

			//Vector3 fromPos = transform.position;
			//Vector3 toPos = transform.position + new Vector3 (0, 0, moveDistance);

			return new Vector3 (0, 0, 0);

		} else if (Input.GetKeyDown(rightKey) && !DoDirectionCheck(Vector3.back))
		{
			//transform.Translate (new Vector3 (0, 0, -moveDistance));

			return new Vector3 (0, -2f, 0);

		} else if (Input.GetKeyDown(upKey) && !DoDirectionCheck(Vector3.right))
		{
			//transform.Translate (new Vector3 (moveDistance, 0, 0));

			return new Vector3 (0, 1, 0);

		} else if (Input.GetKeyDown(downKey) && !DoDirectionCheck(Vector3.left))
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

	}

	IEnumerator AttackCooldown ()
	{
		isAttacking = true;
		isMoving = true; //Immobilize the player

		//tongueRenderer.SetPosition (0, (actualReach - 1) * Vector3.forward);
		StartCoroutine (TongueAnimation(actualReach - 1));

		yield return new WaitForSeconds (attackCooldown / 2f);

		//tongueRenderer.SetPosition (0, Vector3.zero);
		isMoving = false;

		yield return new WaitForSeconds (attackCooldown / 2f);

		isAttacking = false;


	}

	private bool AttackCheck ()
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position, bodyPartsPivot.forward, out hit, maxAttackReach + 4))
		{
			actualReach = 0f;

			if (hit.collider.CompareTag("Player"))
			{
				Debug.Log (hit.collider.name + " hit by " + transform.name + " from distance " + hit.distance);

				actualReach = hit.distance;

				if (actualReach == 7)
				{
					actualReach = maxAttackReach + 1;

				}

				hit.collider.GetComponent<PlayerControl> ().RecieveHit (bodyPartsPivot.rotation.eulerAngles.y);

			} else
			{
				actualReach = maxAttackReach + 1;

			}


		} else
		{
			actualReach = maxAttackReach + 1;

		}

		//Debug.DrawLine (transform.position, hit.point, Color.blue, .5f);

		StartCoroutine (AttackCooldown ());

		return false;
			
	}

	public void RecieveHit (float direction) //Direction will be a rotation 0 (attacked from right), 90 (attacked from bottom), 180 (attacked from left), 270 (attacked from top)
	{
		Vector3 knockBack = Vector3.zero;

		Debug.Log ("I was hit! Direction " + direction);

		switch ((int) direction)
		{
		case 0:
			knockBack = new Vector3 (0f, 0f, moveDistance);

			break;

		case 90:
			knockBack = new Vector3 (moveDistance, 0f, 0f);

			break;

		case 180:
			knockBack = new Vector3 (0f, 0f, -moveDistance);

			break;

		case 270:
			knockBack = new Vector3 (-moveDistance, 0f, 0f);

			break;

		default:
			Debug.Log ("No case for direction " + direction);

			break;
			

		}

		if (DoPositionCheck(knockBack))
		{
			transform.position = transform.position + knockBack;

		}



	}

	IEnumerator TongueAnimation (float distance)
	{
		float stepTime = attackCooldown / 16f;

		tongueRenderer.SetPosition (0, (actualReach - 1) * Vector3.forward);

		yield return new WaitForSeconds (8 * stepTime);

		for (int i = 1; i <= 4 ; i++) //Out
		{
			tongueRenderer.SetPosition(0, Vector3.forward * (distance / (float) i));

			yield return new WaitForSeconds (stepTime);

		}

		tongueRenderer.SetPosition (0, Vector3.zero);

	}
}