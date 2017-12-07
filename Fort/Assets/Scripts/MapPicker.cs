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
	public MapGenerator generator;

	public MeshRenderer plane1;
	public MeshRenderer plane2;
	public MeshRenderer plane3;

	public GameObject islandRootA;
	public GameObject islandRootB;
	public GameObject islandRootC;

	private Vector3 cameraIslandPosition;
	private Quaternion cameraIslandRotation;

	public Texture2D noise1;
	public Texture2D coloredNoise1;
	public Texture2D noise2;
	public Texture2D coloredNoise2;
	public Texture2D noise3;
	public Texture2D coloredNoise3;

	void Start () 
	{
		cameraIslandRotation = transform.rotation;
		cameraIslandPosition = transform.position;

		transform.position = new Vector3 (29f, 11.3538f, 7.6f);
		transform.rotation = Quaternion.Euler (new Vector3 (90f, -90f, 0f));

		noise1 = generator.CalcNoise ();
		coloredNoise1 = generator.GetColoredNoise (noise1);

		noise2 = generator.CalcNoise ();
		coloredNoise2 = generator.GetColoredNoise (noise2);

		noise3 = generator.CalcNoise ();
		coloredNoise3 = generator.GetColoredNoise (noise3);

		plane1.material.mainTexture = coloredNoise1;
		plane2.material.mainTexture = coloredNoise2;
		plane3.material.mainTexture = coloredNoise3;


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
			generator.GenerateMap (noise1);

			transform.position = cameraIslandPosition;
			transform.rotation = cameraIslandRotation;

		} else if (choice == 'B')
		{
			generator.GenerateMap (noise2);

			transform.position = cameraIslandPosition;
			transform.rotation = cameraIslandRotation;

		} else if (choice == 'C')
		{
			generator.GenerateMap (noise3);

			transform.position = cameraIslandPosition;
			transform.rotation = cameraIslandRotation;

		}

	}
}