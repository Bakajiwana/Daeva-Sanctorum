using UnityEngine;
using System.Collections;

//Pause the game when the player presses the pause buttons

public class PauseScript : MonoBehaviour 
{
	public Transform pauseScreen;
	public Transform guiScreen;
	public Transform leftArm;
	public MouseLook mouseLook1;
	public MouseLook mouseLook2;
	public CharacterMotorC move;

	public static bool isPaused;

	// Use this for initialization
	void Start () 
	{
		//Initialise variables
		pauseScreen.gameObject.SetActive (false);
		isPaused = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//If escape or the start button is pressed
		if(Input.GetButtonDown ("Pause") && !PlayerHealth.gameOver && !PlayerInteract.busy)
		{
			//If the game is not paused then pause the game
			if(!isPaused)
			{
				Pause ();
			}
			//If the game is paused then unpause the game
			else if (isPaused && !PlayerHealth.gameOver && !PlayerInteract.busy)
			{
				Unpause ();
			}
		}

		if(Input.GetButtonDown ("Back") && !PlayerHealth.gameOver && !PlayerInteract.busy)
		{
			if(isPaused)
			{
				Unpause ();
			}
		}
	}

	public void Pause()
	{
		if(!PlayerHealth.gameOver)
		{
			pauseScreen.gameObject.SetActive (true); //Show pause Screen if not gameover
		}
		else
		{
			pauseScreen.gameObject.SetActive (false); //Show pause Screen if not gameover
		}
		isPaused = true; //the game is now paused
		guiScreen.gameObject.SetActive (false); //Disable gui screen
		mouseLook1.enabled = false; //Disable Mouse look scripts
		mouseLook2.enabled = false;
		move.enabled = false;
		leftArm.gameObject.SetActive (false);
	}

	public void Unpause()
	{
		pauseScreen.gameObject.SetActive (false); //Don't show the pause screen
		guiScreen.gameObject.SetActive (true); //enable gui screen
		mouseLook1.enabled = true; //enable Mouse look scripts
		mouseLook2.enabled = true;
		leftArm.gameObject.SetActive (true);
		move.enabled = true;
		isPaused = false; //The game is no longer paused
		Screen.showCursor = false; //Hide the cursor
	}
}
