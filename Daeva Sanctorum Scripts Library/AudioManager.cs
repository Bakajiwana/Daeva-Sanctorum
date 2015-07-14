using UnityEngine;
using System.Collections;

//Script Objective: Controls the levels audio

public class AudioManager : MonoBehaviour 
{

	public bool mainMenu;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		//If paused or game over turn off audio listenr
		if(!mainMenu)
		{
			if(PauseScript.isPaused)
			{
				AudioListener.pause = true;
			}
			else
			{
				AudioListener.pause = false;
			}
		}
		else
		{
			AudioListener.pause = false;
		}

		//IF IN MAIN MENU, MAKE SURE AUDIO LISTENER IS ON
	}
}
