/*
 *	Author: Bryce Monaco (Tutorial-Based for CS 328)
 *
 *	Last Updated: 9/6/2017
 *
 *	Description: Player Controls
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour 
{

	public KeyCode moveUp = KeyCode.W;
	public KeyCode moveDown = KeyCode.S;
	public float speed = 10f;
	public float boundY = 2.25f;

	private Rigidbody2D myRigidbody;

	void Start () 
	{
		myRigidbody = GetComponent<Rigidbody2D> ();
		
	}
	
	void Update ()
	{
		Vector2 vel = myRigidbody.velocity;

		if (Input.GetKey(moveUp))
		{
			vel.y = speed;

		} else if (Input.GetKey(moveDown))
		{
			vel.y = -speed;

		} else if (!Input.anyKey)
		{
			vel.y = 0;

		}

		myRigidbody.velocity = vel;

		Vector3 pos = transform.position;

		if (pos.y > boundY)
		{
			pos.y = boundY;

		} else if (pos.y < -boundY)
		{
			pos.y = -boundY;

		}

		transform.position = pos;
		
	}
}