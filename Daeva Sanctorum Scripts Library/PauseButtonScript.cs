using UnityEngine;
using System.Collections;

//Script Objective: Handle the buttons of the pause screen

public class PauseButtonScript : MonoBehaviour 
{
	public bool continueGame = false;
	public bool mainMenu = false;
	public bool quitGame = false;
	public bool quitYesButton = false;
	public bool quitNoButton = false;
	public bool mainMenuPrompt = false;
	public bool lastCheckpoint = false;

	public Transform pauseButtonPrompt;
	public Transform mainMenuWarn;
	public Transform quitGamePrompt;


	public Transform hoverParticles; //particle on hover text


	//When Mouse hovers on text
	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.CompareTag ("Menu Cursor"))
		{
			//Change the colour of the text
			renderer.material.color = new Color(255f,255f,255f);

			if(hoverParticles)
			{
				hoverParticles.gameObject.SetActive (true); //Reveal Particles
			}

			//If player presses the interact or jump button
			if(Input.GetButtonDown("Fire1") || Input.GetButtonUp ("Jump"))
			{
				if(continueGame)
				{
					//Send PauseScript a message to unpause
					GameObject.FindGameObjectWithTag("Level Manager").SendMessage ("Unpause");
				}
				if(mainMenuPrompt)
				{
					mainMenuWarn.gameObject.SetActive (true);
					pauseButtonPrompt.gameObject.SetActive (false);
				}

				//If Main Menu button is pressed
				if(mainMenu)
				{
					//Go to Main Menu
					Application.LoadLevel (0);
				}

				//if quit button is clicked
				if(quitGame)
				{
					quitGamePrompt.gameObject.SetActive (true);
					pauseButtonPrompt.gameObject.SetActive (false);
				}

				if(quitYesButton)
				{				
					//Quit the god damn game
					Application.Quit ();
				}

				if(quitNoButton)
				{
					quitGamePrompt.gameObject.SetActive (false);
					pauseButtonPrompt.gameObject.SetActive (true);
				}

				//If restart last checkpoint
				if(lastCheckpoint)
				{
					Application.LoadLevel (1);
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		//Change the colour of the text
		renderer.material.color = new Color(0.85f, 0.85f, 0.85f);

		if (hoverParticles) 
		{
			hoverParticles.gameObject.SetActive (false); //Hide particles
		}
	}
}
