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
	public int stateNumber = -1; //0 is log, 1 is water, 2 is rock

	public bool becomeParent = false;
	public bool clearParent = false;

	void Start ()
	{
		if (becomeParent && clearParent)
		{
			Debug.LogError ("StateController Error: Cannot have clearParent and becomeParent true simultaneously!");

		}

	}

	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag("Player"))
		{
			col.GetComponent<PlayerControl> ().SetSpecialState (stateNumber, true); //True because it is entering the region

			if (becomeParent)
			{
				col.transform.SetParent (transform);

				Vector3 temp = col.transform.localPosition;

				col.transform.localPosition = new Vector3 (temp.x, temp.y, 0);

			} else if (clearParent)
			{
				col.transform.SetParent (null);

				Vector3 temp = col.transform.position;

				col.transform.position = new Vector3 (temp.x, temp.y, transform.position.z);

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