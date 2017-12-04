/*
 *	Author: Bryce Monaco
 *	Last Updated: 11/14/2017
 *
 *	Description:	This Script handles movement for each player.
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    
	public bool isActive = false;

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public float maxCastDistance = 0.1f;
	public float bottomExtension = 0f;

	public Transform spriteObject; //This is the object that changes size
	public SpriteRenderer feetObject;

	public Sprite feetStanding;
	public Sprite feetJumping;

	public bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d;


	// Use this for initialization
	void Awake () 
	{
		//anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () 
	{
		Vector3 bottomCheck = transform.position;
		bottomCheck.y -= 0.704f + bottomExtension; //0.64f + 0.1 * 0.64f

		RaycastHit2D groundHit;

		Debug.DrawLine (transform.position, bottomCheck, Color.red);
		groundHit = Physics2D.Linecast(transform.position, bottomCheck, 1 << LayerMask.NameToLayer("Ground"));
		grounded = groundHit;

		if (grounded)
		{
			//Debug.Log (transform.name + " is grounded by " + groundHit.collider.name);

			feetObject.sprite = feetStanding;

		} else
		{
			feetObject.sprite = feetJumping;

		}


		if (Input.GetKeyDown(KeyCode.Space) && grounded)
		{
			jump = true;

		}
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal");

		if (h * rb2d.velocity.x < maxSpeed) 
		{
			rb2d.AddForce (Vector2.right * h * moveForce);

		}

		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) 
		{
			rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

		}

		if (isActive)
		{
			if (h == 0f)
			{
				rb2d.velocity = Vector2.up * rb2d.velocity.y;

			} else
			{
				if (h > 0f)
				{
					feetObject.flipX = true;

				} else
				{
					feetObject.flipX = false;

				}

			}

		}



		if (jump)
		{
			//anim.SetTrigger("Jump");
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}
}