/*
 *	Author:
 *	Last Updated:
 *
 *	Description:
 *
 *	TODO: For some reason the cat won't respawn, was working fine until the enemies were set to despawn out of bounds
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	public int remainingEnemies = 0;
	public int score = 0;
	public Text scoreText;

	public int maxMovingObjects = 3;

	public GameObject catPrefab;
	public Transform pult;
	public LineRenderer leftLine;
	public LineRenderer rightLine;
	public LineRenderer defaultLine;

	public Transform respawnPoint;

	public Resetter myResetter;

	private bool alreadyResetting = false;

	void Start () 
	{
		//scoreText = FindObjectOfType<Text> ();
		scoreText.text = "SCORE: 0";
	}
	
	void Update () 
	{
		if (remainingEnemies <= 0)
		{
			//SceneManager.LoadScene (0);

		}
			
	}

	public void ChangeScore (int amount)
	{
		Debug.Log ("Changing score by: " + amount);

		score += amount;

		Debug.Log("New score: " + score);

		scoreText.text = "SCORE: " + score;

	}

	IEnumerator CheckObjectsHaveStopped ()
	{
		Rigidbody2D[] bodies = FindObjectsOfType<Rigidbody2D> ();

		bool allSleeping = false;

		while(!allSleeping)
		{
			allSleeping = true;

			int count = 0;

			foreach (Rigidbody2D GO in bodies) 
			{
				if(GO != null && !GO.CompareTag("Splinter") && !GO.IsSleeping() && GO.bodyType != RigidbodyType2D.Kinematic)
				{
					count++;

					if (count >= maxMovingObjects)
					{
						allSleeping = false;
						yield return null;
						break;

					}
				}
			}

		}
			

		StartCoroutine(ResetCamera());

	}

	public void BallAway ()
	{
		Debug.Log ("Ball away!");
		StartCoroutine (CheckObjectsHaveStopped ());


	}

	public IEnumerator ResetCamera ()
	{
		yield return new WaitForFixedUpdate ();

		ProjectileDragging[] catsInScene = FindObjectsOfType<ProjectileDragging> ();

		if (catsInScene.Length == 0)
		{
			ProjectileFollow myCamFollow = Camera.main.GetComponent<ProjectileFollow> ();

			myCamFollow.ReturnCamera ();

			GameObject fab = Instantiate (catPrefab, respawnPoint.position, respawnPoint.rotation);

			fab.GetComponent<ProjectileDragging> ().GetDefaultValues (pult, leftLine, rightLine, defaultLine);

			myCamFollow.projectile = fab.transform;
			myResetter.projectile = fab.GetComponent<Rigidbody2D>();
			myResetter.spring = fab.GetComponent <SpringJoint2D>();

		} else
		{
			Debug.Log ("Too many cats! Length: " + catsInScene.Length);

			Debug.Log (catsInScene [0].name);

		}



	}
		
}