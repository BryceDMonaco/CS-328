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
	public bool waitForChoice = false;
	[Range (0f, 1f)]
	public float acceptableWaterRatio = 0.5f; //The maximum amount of underwater tiles allowed in a map, used to prevent small generations
	public float xOrg;
	public float yOrg;
	private Color[] pix;
	public Texture2D noiseImg;

	[Range (5f, 30f)]
	public float accuracy = 10f; //Set to 20 in editor
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

		//myRenderer = transform.GetComponent<MeshRenderer> ();

		noiseImg = new Texture2D (size, size);
		noiseImg.filterMode = FilterMode.Point;
		pix = new Color[noiseImg.width * noiseImg.height];


		//myRenderer.material.mainTexture = noiseImg;

		//CalcNoise ();


	}
	
	void Update () 
	{

	}

	public Texture2D CalcNoise () 
	{
		do 
		{
			noiseImg = new Texture2D (size, size);
			noiseImg.filterMode = FilterMode.Point;
			pix = new Color[noiseImg.width * noiseImg.height];

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

		} while (!CheckGenerationTolerances (noiseImg) && !useSeed);

		//GenerateMap ();

		if (!waitForChoice)
		{
			GenerateMap (null);
			
		}
			
		return noiseImg;

	}

	public Texture2D GetColoredNoise (Texture2D sentNoise)
	{
		//For some reason this function was modifying the sent texture because the variables were only copying the reference and not duplicating the texture.
		//The code below forces unity to create a duplicate texture, why this isn't a constructor I do not know.
		Texture2D thisNoise = new Texture2D (size, size);
		thisNoise.filterMode = FilterMode.Point;
		thisNoise.SetPixels (sentNoise.GetPixels ());
		thisNoise.Apply ();

		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				Color thisColor = Color.magenta;

				float newScale = 5 * Mathf.Round (thisNoise.GetPixel (i, j).r * accuracy) / accuracy;

				float scaleTest = newScale / 5;

				if ((scaleTest > 0f) && (scaleTest <= 0.4f))
				{
					thisColor = sandColor;

				} else if ((scaleTest > 0.4f) && (scaleTest <= 0.6f))
				{
					//tile.GetComponent<MeshRenderer> ().material.color = grassColor;
					thisColor = grassGradient.Evaluate((scaleTest - 0.4f) / (0.6f - 0.4f));

				} else if ((scaleTest > 0.6f) && (scaleTest <= 1f))
				{
					//tile.GetComponent<MeshRenderer> ().material.color = mountainColor;
					thisColor = mountainGradient.Evaluate((scaleTest - 0.6f) / (1f - 0.6f));

				}

				if (newScale < 2f) //The tile is under water (this value is arbitrary and will beed to be changed if the water is moved
				{
					thisColor = new Color(48/255f, 93/255f, 119/255f);


				}

				pix [(int)(i * thisNoise.width + j)] = thisColor;

			}
		}

		thisNoise.SetPixels(pix);
		thisNoise.Apply();

		return thisNoise;

	}

	public void GenerateMap (Texture2D sentTexture)
	{
		Texture2D thisTexture = sentTexture;

		DeleteGrid ();

		if (thisTexture == null)
		{
			thisTexture = noiseImg;

		}

		originPos = mapOrigin.position;

		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				GameObject tile = Instantiate (hexObject, mapOrigin);

				hexArray [(size * i) + j] = tile;

				float newScale = 5 * Mathf.Round (thisTexture.GetPixel (i, j).r * accuracy) / accuracy;

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

					//pix [(int)(i * thisTexture.width + j)] = new Color(48/255f, 93/255f, 119/255f);


				} else
				{
					tile.transform.localPosition = new Vector3 ((0.75f * i), (newScale / 4), (zOffset * (i % 2)) + (2 * j * zOffset));

					tile.transform.localScale = new Vector3 (tile.transform.localScale.x, newScale, tile.transform.localScale.z);

					//pix [(int)(i * thisTexture.width + j)] = tile.GetComponent<MeshRenderer>().material.color;

				}




			}


		}

		//thisTexture.SetPixels(pix);
		//thisTexture.Apply();

		if (!waitForChoice)
		{
			//noiseImg = thisTexture;

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

	bool CheckGenerationTolerances (Texture2D sentNoise)
	{
		float mapSize = size * size;
		float waterCount = 0;

		Color[] noisePixels = sentNoise.GetPixels ();

		for (int i = 0; i < mapSize; i++)
		{
			float newScale = 5 * Mathf.Round (noisePixels[i].r * accuracy) / accuracy;

			if (newScale < 2f) //The tile would be deleted on generation
			{
				waterCount++;

			}

			if ((waterCount / mapSize) > acceptableWaterRatio)
			{
				break;

			}
		}

		if ((waterCount / mapSize) > acceptableWaterRatio)
		{
			Debug.Log ("Ratio not met. Water count: " + waterCount);

			return false; //The map does not meet the ratio

		} else
		{
			Debug.Log ("Ratio met. Water count: " + waterCount);

			return true; //The map meets the ratio (below the max)

		}




	}
}