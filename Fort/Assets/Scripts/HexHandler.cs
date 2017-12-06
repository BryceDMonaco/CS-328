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

		if (!isObstructed && lastColor == glowColor)
		{
			myRenderer.material.SetColor ("_EmissionColor", Color.green);	

		} else if (isObstructed && lastColor == glowColor)
		{
			myRenderer.material.SetColor ("_EmissionColor", Color.red);

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

	void OnTriggerExit (Collider col)
	{
		myRenderer.material.SetColor ("_EmissionColor", Color.black);

		lastColorStored = false;

	}
	
	void Update () 
	{

	}

	void OnTriggerExit ()
	{


	}
}