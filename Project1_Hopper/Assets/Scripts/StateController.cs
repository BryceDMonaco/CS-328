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

public class StateController : MonoBehaviour 
{
	public int stateNumber = -1; //0 is log, 1 is water

	public bool becomeParent = false;

	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag("Player"))
		{
			col.GetComponent<PlayerControl> ().SetSpecialState (stateNumber, true); //True because it is entering the region

			if (becomeParent)
			{
				col.transform.SetParent (transform);

			}

		}

	}

	void OnTriggerExit (Collider col)
	{
		if (col.CompareTag("Player"))
		{
			col.GetComponent<PlayerControl> ().SetSpecialState (stateNumber, false);

		}

	}
}