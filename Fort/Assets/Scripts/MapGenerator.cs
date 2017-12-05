/*
 *	Author:
 *	Last Updated:
 *
 *	Description: 	Uses modified Perlin noise code from the Mathf.PerlinNoise Scripting Reference page.
 *					This script generates a hex grid based off of Perlin Noise and then assigns colors and heights to the grid based on the noise to make a map.
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour 
{
	private MeshRenderer myRenderer;
	public bool useSeed = false;
	public float xOrg;
	public float yOrg;
	private Color[] pix;
	public Texture2D noiseImg;

	[Range (5f, 30f)]
	public float accuracy = 10f;
	public float scale = 1f;
	public float hexScale = 5f;

	public float xOffset = 0.75f;
	public float zOffset = 0.4330127018922193f;
	public float genOffset = 1.5f;

	public Transform mapOrigin;
	private Vector3 originPos;

	public GameObject hexObject;

	public Color sandColor;
	public Color grassColor;
	public Color mountainColor;
	public Gradient grassGradient;
	public Gradient mountainGradient;

	public GameObject treeGroup;
	public GameObject grassObject;

	public GameObject[] hexArray;

	private int size = 16;

	void Start () 
	{
		hexArray = new GameObject[size * size];

		myRenderer = transform.GetComponent<MeshRenderer> ();

		noiseImg = new Texture2D (size, size);
		noiseImg.filterMode = FilterMode.Point;
		pix = new Color[noiseImg.width * noiseImg.height];


		myRenderer.material.mainTexture = noiseImg;

		CalcNoise ();


	}
	
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			CalcNoise ();

		}
	}

	void CalcNoise () 
	{
		if (!useSeed)
		{
			xOrg = Random.Range (0, 9999);
			yOrg = Random.Range (0, 9999);

		}

		for (float y = 0; y < noiseImg.height; y++)
		{
			for (float x = 0; x < noiseImg.width; x++)
			{
				float xCoord = xOrg + x / noiseImg.width * scale;
				float yCoord = yOrg + y / noiseImg.height * scale;
				float sample = Mathf.PerlinNoise(xCoord, yCoord);

				pix [(int)(y * noiseImg.width + x)] = new Color (sample, sample, sample);

			}

		}

		noiseImg.SetPixels(pix);
		noiseImg.Apply();

		GenerateMap ();

	}

	void GenerateMap ()
	{
		DeleteGrid ();

		originPos = mapOrigin.position;

		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				GameObject tile = Instantiate (hexObject, mapOrigin);

				hexArray [(size * i) + j] = tile;

				float newScale = 5 * Mathf.Round (noiseImg.GetPixel (i, j).r * accuracy) / accuracy;

				float scaleTest = newScale / 5;

				if ((scaleTest > 0f) && (scaleTest <= 0.4f))
				{
					tile.GetComponent<MeshRenderer> ().material.color = sandColor;

				} else if ((scaleTest > 0.4f) && (scaleTest <= 0.6f))
				{
					//tile.GetComponent<MeshRenderer> ().material.color = grassColor;
					tile.GetComponent<MeshRenderer> ().material.color = grassGradient.Evaluate((scaleTest - 0.4f) / (0.6f - 0.4f));

					if (Random.Range(0, 10) >= 8) //~2/10 chance
					{
						Vector3 treePos = tile.transform.position;
						treePos.y = tile.transform.localScale.y / 4;

						Instantiate (treeGroup, treePos, Quaternion.Euler (new Vector3 (0f, Random.Range (0, 360), 0f)), tile.transform);

						tile.GetComponent<HexHandler> ().isObstructed = true;

					} else if (Random.Range(0, 10) >= 8) //Reroll and do another 2/10 chance
					{
						Vector3 treePos = tile.transform.position;
						treePos.y = tile.transform.localScale.y / 4;

						Instantiate (grassObject, treePos, tile.transform.rotation, tile.transform);

					}

				} else if ((scaleTest > 0.6f) && (scaleTest <= 1f))
				{
					//tile.GetComponent<MeshRenderer> ().material.color = mountainColor;
					tile.GetComponent<MeshRenderer> ().material.color = mountainGradient.Evaluate((scaleTest - 0.6f) / (1f - 0.6f));

				}

				if (newScale < 2f) //The tile is under water (this value is arbitrary and will beed to be changed if the water is moved
				{
					//tile.GetComponent<MeshRenderer> ().enabled = false;

					Destroy (tile.gameObject); 

				}

				tile.transform.localPosition = new Vector3 ((0.75f * i), (newScale / 4), (zOffset * (i % 2)) + (2 * j * zOffset));

				tile.transform.localScale = new Vector3 (tile.transform.localScale.x, newScale, tile.transform.localScale.z);


			}



		}

	}

	void DeleteGrid ()
	{
		for (int i = 0; i < (size * size); i++)
		{
			if (hexArray[i] != null)
			{
				Destroy (hexArray [i].gameObject);

			}
		}

	}
}