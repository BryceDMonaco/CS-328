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

public class FlyKillzone : MonoBehaviour 
{

	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag("Fly"))
		{
			col.GetComponent<GoldenFly>().RespawnFly();

		}

	}
}