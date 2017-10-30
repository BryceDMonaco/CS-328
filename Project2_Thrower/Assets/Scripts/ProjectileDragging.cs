/*
 *	Author:
 *	Last Updated:
 *
 *	Description:
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDragging : MonoBehaviour 
{
	public float maxStretch = 25f;

	public LineRenderer catapultLineFront;
	public LineRenderer catapultLineBack;
	public Transform catapult;

	public SpringJoint2D spring;
	public bool clickedOn = false;
	private Vector2 prevVelocity;

	private Ray rayToMouse;
	private Ray leftCatapultToProjectile;
	private float maxStrethSqr;
	private Rigidbody2D myRB;

	public float leftLineExtra = 0f;

	void Start () 
	{
		myRB = GetComponent<Rigidbody2D> ();

		prevVelocity = Vector2.zero;

		LineRendererSetup ();

		rayToMouse = new Ray (catapult.position, Vector3.zero);

		maxStrethSqr = maxStretch * maxStretch;

		leftCatapultToProjectile = new Ray (catapultLineFront.transform.position, Vector3.zero);

	}
	
	void Update () 
	{
		if (clickedOn)
		{
			Dragging ();

		}

		if (spring != null)
		{
			if (myRB.bodyType != RigidbodyType2D.Kinematic && prevVelocity.sqrMagnitude > myRB.velocity.sqrMagnitude)
			{
				//spring.enabled = false;
				Destroy(spring);

				myRB.velocity = prevVelocity;

			}

			if (!clickedOn)
			{
				prevVelocity = myRB.velocity;

			}

			LineRendererUpdate ();

		} else
		{
			catapultLineFront.enabled = false;
			catapultLineBack.enabled = false;

		}
		
	}

	void LineRendererSetup ()
	{
		catapultLineFront.SetPosition (0, catapultLineFront.transform.position);
		catapultLineBack.SetPosition (0, catapultLineBack.transform.position);

		catapultLineFront.sortingLayerName = "Grass";
		catapultLineBack.sortingLayerName = "Background";

		catapultLineFront.sortingOrder = -1;
		catapultLineBack.sortingOrder = -1;

	}

	void OnMouseDown ()
	{
		spring.enabled = false;
		clickedOn = true;

	}

	void OnMouseUp ()
	{
		spring.enabled = true;
		clickedOn = false;
		GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;

		Camera.main.orthographicSize = 40f;

	}

	void Dragging ()
	{
		Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 catapultToMouse = mouseWorldPoint - catapult.position;

		if (catapultToMouse.sqrMagnitude > maxStrethSqr)
		{
			rayToMouse.direction = catapultToMouse;
			mouseWorldPoint = rayToMouse.GetPoint (maxStretch);

		}

		mouseWorldPoint.z = 0f;

		transform.position = mouseWorldPoint;

		Camera.main.orthographicSize = Mathf.SmoothStep (40f, 45f, catapultToMouse.magnitude / maxStretch);

	}

	void LineRendererUpdate ()
	{
		Vector2 catapultToProjectile = transform.position - catapultLineFront.transform.position;
		leftCatapultToProjectile.direction = catapultToProjectile;

		Vector3 holdPoint = leftCatapultToProjectile.GetPoint (catapultToProjectile.magnitude + leftLineExtra);

		catapultLineFront.SetPosition (1, holdPoint);
		catapultLineBack.SetPosition (1, holdPoint);


	}
}