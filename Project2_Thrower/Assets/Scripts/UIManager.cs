/*
 *	Author:
 *	Last Updated:
 *
 *	Description:
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour 
{
	public string thisLevelName;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

	public void ChangeLevel (int levelIndex)
	{
		SceneManager.LoadScene(levelIndex);

	}

	public void ChangeLevel (string levelName)
	{
		SceneManager.LoadScene(levelName);

	}

	public void ReloadLevel ()
	{
		SceneManager.LoadScene (thisLevelName);

	}
}