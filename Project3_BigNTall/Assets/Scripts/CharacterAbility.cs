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
	private RaycastHit2D playerCheck;

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

		if (!isActive)
		{
			FreezeThisPlayer (true);

		}
	}

	void FixedUpdate ()
	{
		if (isActive)
		{
			RaycastHit2D playerCheckTemp;

			if (beTall)
			{
				Vector2 checkEndPoint = new Vector2 (transform.parent.position.x, transform.parent.position.y + transform.localScale.y + 1f);


				Debug.DrawLine (transform.parent.position, checkEndPoint, Color.blue);
				playerCheckTemp = Physics2D.Linecast(transform.parent.position, checkEndPoint, 1 << LayerMask.NameToLayer("Player_Wide"));

				playerCheck = playerCheckTemp;

			} else if (beWide)
			{
				Vector2 checkEndPoint = new Vector2 (transform.parent.position.x + transform.localScale.x + 1f, transform.parent.position.y);


				Debug.DrawLine (transform.parent.position, checkEndPoint, Color.blue);
				playerCheckTemp = Physics2D.Linecast(transform.parent.position, checkEndPoint, 1 << LayerMask.NameToLayer("Player_Tall"));

				playerCheck = playerCheckTemp;

			}

			if (Input.GetKey(sizeUpKey))
			{


				Vector3 scaleFactor = transform.localScale;
				Vector3 positionOffset = transform.localPosition;

				if (beTall && scaleFactor.y < maxLength)
				{
					if (playerCheck)
					{
						Debug.Log ("PC: " + transform.parent.name + " hit " + playerCheck.collider.name);

						//Below isn't the most efficient line, but it's either this or a bunch of ifs with at least one using this
						playerCheck.collider.transform.parent.GetComponentInChildren<CharacterAbility> ().FreezeThisPlayer (false); //Unfreeze the player so the active can move the inactive

					}

					scaleFactor.y += (growthSpeed * Time.fixedDeltaTime);

					positionOffset.y = (0.639817f * scaleFactor.y) - 0.639474f;


				} else if (beWide && scaleFactor.x < maxLength)
				{
					if (playerCheck)
					{
						Debug.Log ("PC: " + transform.parent.name + " hit " + playerCheck.collider.name);

						//Below isn't the most efficient line, but it's either this or a bunch of ifs with at least one using this
						playerCheck.collider.transform.parent.GetComponentInChildren<CharacterAbility> ().FreezeThisPlayer (false); //Unfreeze the player so the active can move the inactive

					}

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

	public void FreezeThisPlayer (bool state) //T to freeze player, F to unfreeze
	{
		if (!state && beWide)
		{
			myController.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;

		} else if (!state && beTall)
		{
			myController.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;

		}else 
		{
			myController.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;

		}

	}
}