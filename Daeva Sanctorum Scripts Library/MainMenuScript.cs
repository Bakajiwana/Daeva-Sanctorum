using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour 
{
	//MAIN MENU SCRIPT
	public bool newGame = false;
	public bool newGameCheck = false; //This new game button checks if the player has progress or not
	public bool continueGame = false;
	public bool quitGame = false;
	public bool credits = false;

	//Menu Switch Buttons
	public bool switchMenu;
	public Transform toMenu;
	public Transform fromMenu;

	public bool selectChapter;
	public int setCheckpoint;

	//When Mouse hovers on text
	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.CompareTag ("Menu Cursor"))
		{
			if(!continueGame && !selectChapter|| continueGame && PlayerPrefs.GetInt("Checkpoint") > 0 
			   ||selectChapter && setCheckpoint <= PlayerPrefs.GetInt ("FurthestCheckpoint"))
			{
				//Change the colour of the text if it is not continue game with no progression
				renderer.material.color = new Color(255f,255f,255f);
			}
			
			//If player presses the interact or jump button
			if(Input.GetButtonDown("Fire1") || Input.GetButtonUp ("Jump"))
			{
				//If play game is clicked
				if(newGame)
				{	
					PlayerPrefs.SetInt ("Checkpoint", 0); //Restart progress
					//Go to game
					Application.LoadLevel (1);
				}

				if(newGameCheck)
				{
					if(PlayerPrefs.GetInt ("Checkpoint") == 0)
					{
						//Go to game
						Application.LoadLevel (1);
					}
					else
					{
						fromMenu.gameObject.SetActive (false);
						toMenu.gameObject.SetActive (true);
					}
				}

				if(continueGame && PlayerPrefs.GetInt("Checkpoint") > 0)
				{
					//Go to game
					Application.LoadLevel (1);
				}

				//if quit button is clicked
				if(quitGame)
				{
					//Quit the god damn game
					Application.Quit ();
				}

				if(switchMenu)
				{
					fromMenu.gameObject.SetActive (false);
					toMenu.gameObject.SetActive (true);
				}

				//If credits is clicked
				if(credits)
				{
					Application.LoadLevel (2);
				}

				//If Select Chapter and current checkpoint is greater 
				if(selectChapter && setCheckpoint <= PlayerPrefs.GetInt ("FurthestCheckpoint"))
				{
					PlayerPrefs.SetInt ("Checkpoint", setCheckpoint);
					//Go to game
					Application.LoadLevel (1);
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(!continueGame && !selectChapter|| continueGame && PlayerPrefs.GetInt("Checkpoint") > 0
		   ||selectChapter && setCheckpoint <= PlayerPrefs.GetInt ("FurthestCheckpoint"))
		{
			//Change the colour of the text
			renderer.material.color = new Color(0.85f, 0.85f, 0.85f);
		}
	}

	void Start()
	{
		Screen.showCursor = false;

		if(continueGame && PlayerPrefs.GetInt("Checkpoint") <= 0
		   ||selectChapter && setCheckpoint > PlayerPrefs.GetInt ("FurthestCheckpoint"))
		{
			//Change the colour of the text
			renderer.material.color = new Color(0.2f, 0.2f, 0.2f);
		}

		//For selecting chapters, find the furthest level
		int currCheckpoint = PlayerPrefs.GetInt ("Checkpoint");
		int furthestCheckpoint = PlayerPrefs.GetInt ("FurthestCheckpoint");
		
		//If the current checkpoint is greater than the furthest then change the furthest checkpoint to curr
		if(currCheckpoint > furthestCheckpoint)
		{
			PlayerPrefs.SetInt ("FurthestCheckpoint", currCheckpoint);
		}
	}
}
