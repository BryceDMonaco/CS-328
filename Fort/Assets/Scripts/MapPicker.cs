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

	public MeshRenderer planeA;
	public MeshRenderer planeB;
	public MeshRenderer planeC;

	public GameObject islandRootA;

	private Vector3 cameraIslandPosition;
	private Quaternion cameraIslandRotation;

	public Texture2D noiseA;
	public Texture2D colorednoiseA;
	public Texture2D noiseB;
	public Texture2D colorednoiseB;
	public Texture2D noiseC;
	public Texture2D colorednoiseC;

	void Start () 
	{
		cameraIslandRotation = transform.rotation;
		cameraIslandPosition = transform.position;

		transform.position = new Vector3 (29f, 11.3538f, 7.6f);
		transform.rotation = Quaternion.Euler (new Vector3 (90f, -90f, 0f));

		noiseA = generator.CalcNoise ();
		colorednoiseA = generator.GetColoredNoise (noiseA);

		noiseB = generator.CalcNoise ();
		colorednoiseB = generator.GetColoredNoise (noiseB);

		noiseC = generator.CalcNoise ();
		colorednoiseC = generator.GetColoredNoise (noiseC);

		planeA.material.mainTexture = colorednoiseA;
		planeB.material.mainTexture = colorednoiseB;
		planeC.material.mainTexture = colorednoiseC;

		FindObjectOfType<EnemySpawner>().tipText.text = "Select A Map With 'A' 'B' or 'C':";


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
		FindObjectOfType<EnemySpawner>().tipText.text = "Select Home Base Position";

		if (choice == 'A')
		{
			generator.GenerateMap (noiseA);

			transform.position = cameraIslandPosition;
			transform.rotation = cameraIslandRotation;

		} else if (choice == 'B')
		{
			generator.GenerateMap (noiseB);

			transform.position = cameraIslandPosition;
			transform.rotation = cameraIslandRotation;

		} else if (choice == 'C')
		{
			generator.GenerateMap (noiseC);

			transform.position = cameraIslandPosition;
			transform.rotation = cameraIslandRotation;

		}

	}
}