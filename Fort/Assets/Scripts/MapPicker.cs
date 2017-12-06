/*
 *	Author:
 *	Last Updated:
 *
 *	Description: TODO: Do not generate map until choice is made (need to get colorized version of noise map from generator to display and need to also store B and W version)
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPicker : MonoBehaviour 
{
	public MapGenerator generatorA;
	public MapGenerator generatorB;
	public MapGenerator generatorC;

	public GameObject islandRootA;
	public GameObject islandRootB;
	public GameObject islandRootC;

	private Vector3 cameraIslandPosition;
	private Quaternion cameraIslandRotation;

	void Start () 
	{
		cameraIslandRotation = transform.rotation;
		cameraIslandPosition = transform.position;

		transform.position = new Vector3 (29f, 11.3538f, 7.6f);
		transform.rotation = Quaternion.Euler (new Vector3 (90f, -90f, 0f));

	}
	
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			PickMap ('A');

		} else if (Input.GetKeyDown(KeyCode.B))
		{
			PickMap ('B');

		} else if (Input.GetKeyDown(KeyCode.C))
		{
			PickMap ('C');

		}

	}

	void PickMap (char choice)
	{
		if (choice == 'A')
		{
			Destroy (islandRootB);
			Destroy (islandRootC);

			transform.position = cameraIslandPosition;
			transform.rotation = cameraIslandRotation;

		} else if (choice == 'B')
		{
			Destroy (islandRootA);
			Destroy (islandRootC);

			transform.position = cameraIslandPosition;
			transform.rotation = cameraIslandRotation;

		} else if (choice == 'C')
		{
			Destroy (islandRootA);
			Destroy (islandRootB);

			transform.position = cameraIslandPosition;
			transform.rotation = cameraIslandRotation;

		}

	}
}