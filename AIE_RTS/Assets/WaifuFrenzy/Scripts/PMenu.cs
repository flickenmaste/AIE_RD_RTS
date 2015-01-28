using System;
using UnityEngine;
using System.Collections;

public class PMenu : MonoBehaviour
{
	bool paused = false;
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
			paused = togglePause();
	}
	
	void OnGUI()
	{
		if(paused)
		{
			if(GUI.Button(new Rect(((Screen.width / 2) - 50), ((Screen.height / 2) - 25), 200, 25 ), "Resume"))
			{
				paused = togglePause();
			}

			if(GUI.Button(new Rect(((Screen.width / 2) - 50), ((Screen.height / 2)), 200, 25 ), "Restart"))
			{
				Application.LoadLevel(Application.loadedLevel);
				paused = togglePause();
			}

			if (GUI.Button (new Rect(((Screen.width / 2) - 50), ((Screen.height / 2) +25), 200, 25 ), "Exit")) 
			{
				Application.Quit ();
			}
		}
	}
	
	bool togglePause()
	{
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			return(false);
		}
		else
		{
			Time.timeScale = 0f;
			return(true);    
		}
	}
}