/*
 *	Author: Alex Kastanek
 *	Last Updated: 11/28/2017
 *
 *	Description:	This script handles the player's ability to step on pressure plates
 *	                or put a weight on pressure plates that will trigger an event while
 *	                the pressure plate has weight on it.
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate1 : MonoBehaviour 
{
	public GameObject door, platform1, platform2;

	private bool isTriggered = false;
    private bool stayTriggered = false;

    void Update()
    {
        if (isTriggered || stayTriggered)
        {
            door.GetComponent<SpriteRenderer>().enabled = false;
            door.GetComponent<BoxCollider2D>().enabled = false;

            platform1.GetComponent<SpriteRenderer>().enabled = true;
            platform1.GetComponent<BoxCollider2D>().enabled = true;

            platform2.GetComponent<SpriteRenderer>().enabled = true;
            platform2.GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (!isTriggered && !stayTriggered)
        {
            door.GetComponent<SpriteRenderer>().enabled = true;
            door.GetComponent<BoxCollider2D>().enabled = true;

            platform1.GetComponent<SpriteRenderer>().enabled = false;
            platform1.GetComponent<BoxCollider2D>().enabled = false;

            platform2.GetComponent<SpriteRenderer>().enabled = false;
            platform2.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OnTriggerEnter2D (Collider2D col)
	{
        isTriggered = true;
        if (col.gameObject.tag == "weight")
        {
            stayTriggered = true;
        }
	}

    void OnTriggerExit2D (Collider2D col)
    {
        isTriggered = false;
    }
}