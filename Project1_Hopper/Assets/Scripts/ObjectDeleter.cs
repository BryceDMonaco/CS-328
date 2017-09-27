/*
 *	Author:
 *
 *	Last Updated:
 *
 *	Description:
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeleter : MonoBehaviour 
{

	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag("Vehicle") || col.CompareTag("Log"))
		{
			Destroy (col.gameObject);

		}

	}
}