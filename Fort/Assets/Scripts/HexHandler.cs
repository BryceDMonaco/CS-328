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

public class HexHandler : MonoBehaviour 
{
	private MeshRenderer myRenderer;

	public Color partialGlowColor;
	public Color glowColor;

	private Color lastColor;
	private bool lastColorStored = false;

	public bool isObstructed = false; //Does the tile have trees or some other object which would block it?

	void Start ()
	{
		myRenderer = GetComponent<MeshRenderer> ();

	}
	void OnMouseOver () 
	{
		if (!lastColorStored)
		{
			lastColor = myRenderer.material.GetColor ("_EmissionColor");
			lastColorStored = true;

		}

		if (Input.GetMouseButtonDown(0))
		{
			lastColor = glowColor;

		}

		if (lastColor == glowColor)
		{
			myRenderer.material.SetColor ("_EmissionColor", Color.green);	

		} else
		{
			myRenderer.material.SetColor ("_EmissionColor", partialGlowColor);	

		}
			
	}

	void OnMouseExit ()
	{
		myRenderer.material.SetColor ("_EmissionColor", lastColor);	

		lastColorStored = false;

	}
	
	void Update () 
	{

	}

	void OnTriggerExit ()
	{


	}
}