/*
 *	Author:
 *	Last Updated:
 *
 *	Description: This is the script attached to the tester which goes to where the mouse is clicked
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseGlow : MonoBehaviour 
{
	public Color glowColor;
	private Transform lastHit;
	private Vector3 defaultPosition;

	void Start () 
	{
		lastHit = null;

		defaultPosition = transform.position;
		
	}
	
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;

			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit))
			{
				Transform hitHex = hit.collider.transform;

				if (lastHit == hitHex) //Player clicked the same hex twice
				{
					transform.position = defaultPosition;
					lastHit = null;

				} else
				{
					lastHit = hitHex;

					transform.position = lastHit.position;

				}

			} else
			{

				transform.position = defaultPosition;
				lastHit = null;
			}

		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag("Hex"))
		{
			col.GetComponent<MeshRenderer> ().material.SetColor ("_EmissionColor", glowColor);

		}

	}

	void OnTriggerExit (Collider col)
	{
		if (col.CompareTag("Hex"))
		{
			col.GetComponent<MeshRenderer> ().material.SetColor ("_EmissionColor", Color.black);

		}
	}
}