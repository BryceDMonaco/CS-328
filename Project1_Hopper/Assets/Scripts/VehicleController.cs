/*
 *	Author: Bryce Monaco
 *
 *	Last Updated: 9/14/2017
 *
 *	Description: A simple script that moves the vehicles in a direction at a static speed
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour 
{
	public float speed = 5f; 

	public Vector3 startRotation;

	public Color[] vehicleColors;

	public MeshRenderer[] coloredParts;

	void Start ()
	{
		transform.rotation = Quaternion.Euler (startRotation);

		if (vehicleColors.Length > 0)
		{
			int index = Random.Range (0, vehicleColors.Length);

			//Debug.Log (index);

			Color thisColor = vehicleColors [index];

			foreach (MeshRenderer rend in coloredParts)
			{
				rend.material.color = thisColor;

			}

		}



	}
	
	void Update () 
	{
		transform.position += Vector3.right * Time.deltaTime * speed;
		
	}
}