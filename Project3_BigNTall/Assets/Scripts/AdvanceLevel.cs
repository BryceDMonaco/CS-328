/*
 *	Author: Alex Kastanek
 *	Last Updated: 11/29/2017
 *
 *	Description:	This script handles the scene advancing when the players make it
 *	                to the end of the level.
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdvanceLevel : MonoBehaviour {

    public int currentLevel;

    private bool player1Triggering = false, player2Triggering = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (player1Triggering && player2Triggering)
        {
            switch (currentLevel)
            {
                case 1:
                    SceneManager.LoadScene("Scene2", LoadSceneMode.Single);
                    break;
                case 2:
                    SceneManager.LoadScene("Scene3", LoadSceneMode.Single);
                    break;
                case 3:
                    SceneManager.LoadScene("WinScreen", LoadSceneMode.Single);
                    break;
            }
        }
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.parent.tag == "player1")
        {
            player1Triggering = true;
        }

        if (col.transform.parent.tag == "player2")
        {
            player2Triggering = true;
        }
    }
}
