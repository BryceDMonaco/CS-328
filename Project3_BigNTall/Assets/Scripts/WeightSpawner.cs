/*
 *	Authors: Bryce Monaco & Alex Kastanek
 *	Last Updated: 11/28/2017
 *
 *	Description:	This script handles buttons for players to press to trigger events with weights.
 *					NOTE: The triggered object must have a script with a public "Trigger" function.
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightSpawner : MonoBehaviour
{
    public GameObject weight;

    private bool beenTriggered = false;

    void Update()
    {
        if (!beenTriggered)
        {
            weight.GetComponent<SpriteRenderer>().enabled = false;
            weight.GetComponent<BoxCollider2D>().enabled = false;
            weight.GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!beenTriggered && col.CompareTag("Player"))
        {
            weight.GetComponent<SpriteRenderer>().enabled = true;
            weight.GetComponent<BoxCollider2D>().enabled = true;
            weight.GetComponent<Rigidbody2D>().simulated = true;

            beenTriggered = true;

        }

    }
}