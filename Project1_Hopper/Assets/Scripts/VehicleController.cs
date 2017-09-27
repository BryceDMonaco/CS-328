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

	void Start ()
	{
		transform.rotation = Quaternion.Euler (startRotation);

	}
	
	void Update () 
	{
		transform.position += Vector3.right * Time.deltaTime * speed;
		
	}
}