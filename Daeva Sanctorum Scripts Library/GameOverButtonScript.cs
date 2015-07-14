using UnityEngine;
using System.Collections;

//Script Objective: Handle the Game Over:
//Retry last checkpoint button
//and restart buttons
//The other buttons is just reusing the pause scripts quit and main menu buttons

public class GameOverButtonScript : MonoBehaviour 
{
	//Game Over Button Variables
	public bool retryBtn;
	public bool restartBtn;

	public Transform hoverParticles;

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
			if(Input.GetButtonUp("Fire1") || Input.GetButtonUp ("Jump"))
			{
				//When retry button is pressed
				if(retryBtn)
				{
					Application.LoadLevel (1);
					//GameObject.FindGameObjectWithTag("Level Manager").SendMessage ("Respawn");
					//GameObject.FindGameObjectWithTag("Player").SendMessage ("Retry");
				}

				//When the restart button is pressed
				if(restartBtn)
				{
					PlayerPrefs.SetInt ("Checkpoint", 0); //Restart progress
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
