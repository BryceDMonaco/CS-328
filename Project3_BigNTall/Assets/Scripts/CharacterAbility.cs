/*
 *	Author: Bryce Monaco
 *	Last Updated: Nov 10, 2017
 *
 *	Description: 	This script handles the growth of the characters
 *					as well as switching characters.
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//x = scale y = position

public class CharacterAbility : MonoBehaviour 
{
	public bool beTall = false;
	public bool beWide = false;
	public bool isActive = false;

	public float growthSpeed = 5f;
	public float maxLength = 10f;
	public float minLength = 1f;
	//Scale offset to position offset function: 0.639817 x - 0.639474

	public KeyCode sizeUpKey;
	public KeyCode sizeDownKey;

	public KeyCode switchCharKey;

	public Color inactiveColor;
	public Color activeColor;

	public Sprite inactiveSprite;
	public Sprite activeSprite;
	public SpriteRenderer faceRenderer;

	private SpriteRenderer myRenderer;
	private PlatformController myController;

	void Start () 
	{
		myRenderer = GetComponent<SpriteRenderer> ();
		myController = GetComponentInParent<PlatformController> ();

		UpdateColors (false);
	}
	
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			isActive = !isActive;

			UpdateColors (true);

		}
	}

	void FixedUpdate ()
	{
		if (isActive)
		{
			if (Input.GetKey(sizeUpKey))
			{
				Vector3 scaleFactor = transform.localScale;
				Vector3 positionOffset = transform.localPosition;

				if (beTall && scaleFactor.y < maxLength)
				{
					scaleFactor.y += (growthSpeed * Time.fixedDeltaTime);

					positionOffset.y = (0.639817f * scaleFactor.y) - 0.639474f;


				} else if (beWide && scaleFactor.x < maxLength)
				{
					scaleFactor.x += (growthSpeed * Time.fixedDeltaTime);

					positionOffset.x = (0.639817f * scaleFactor.x) - 0.639474f;

				}

				//Check if over max
				if (scaleFactor.y > maxLength)
				{
					scaleFactor.y = maxLength;

				} else if (scaleFactor.x > maxLength)
				{
					scaleFactor.x = maxLength;

				}

				transform.localScale = scaleFactor;
				transform.localPosition = positionOffset;

			} else if (Input.GetKey(sizeDownKey))
			{
				Vector3 scaleFactor = transform.localScale;
				Vector3 positionOffset = transform.localPosition;

				if (beTall && scaleFactor.y > minLength)
				{
					scaleFactor.y -= (growthSpeed * Time.fixedDeltaTime);

					positionOffset.y = (0.639817f * scaleFactor.y) - 0.639474f;


				} else if (beWide && scaleFactor.x > minLength)
				{
					scaleFactor.x -= (growthSpeed * Time.fixedDeltaTime);

					positionOffset.x = (0.639817f * scaleFactor.x) - 0.639474f;

				}

				//Check if under min
				if (scaleFactor.y < minLength)
				{
					scaleFactor.y = minLength;

				} else if (scaleFactor.x < minLength)
				{
					scaleFactor.x = minLength;

				}

				transform.localScale = scaleFactor;
				transform.localPosition = positionOffset;

			}

		}



	}

	void UpdateColors (bool shouldSwitchCameras) //Should be F at start and T every other time
	{
		if (isActive)
		{
			myRenderer.color = activeColor;
			myController.enabled = true;
			GetComponentInParent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
			faceRenderer.sprite = activeSprite;

			if (shouldSwitchCameras)
			{
				Camera.main.GetComponent<CameraFollow> ().SwitchCameraTargets (beTall, beWide);

			} else 
			{
				Camera.main.GetComponent<CameraFollow> ().SetTarget (beTall, beWide);

			}



		} else
		{
			myRenderer.color = inactiveColor;
			myController.enabled = false;
			GetComponentInParent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;
			faceRenderer.sprite = inactiveSprite;

		}

	}
}