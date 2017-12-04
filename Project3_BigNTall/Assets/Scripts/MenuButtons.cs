/*
 *	Author: Bryce Monaco
 *	Last Updated: 12/4/2017
 *
 *	Description:
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour 
{

	public void PlayGame ()
	{
		SceneManager.LoadScene ("Scene1");

	}

	public void ReturnToMenu ()
	{
		SceneManager.LoadScene ("Menu");

	}
}