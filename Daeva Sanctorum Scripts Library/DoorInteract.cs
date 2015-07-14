using UnityEngine;
using System.Collections;

public class DoorInteract : MonoBehaviour 
{
	//Door Variables
	public bool isOpen = false;
	public int lockedByKey = 0; // Zero means not locked... > 0 means locked by a certain key ID


	//Puzzle Variables
	public MouseLook mouseLook1;
	public MouseLook mouseLook2;
	public CharacterMotorC move;
	public Transform keyPuzzle1;
	public Transform keyLock1;
	public Transform unlockPuzzle;
	private bool unlocking;



	//Unlock Puzzle Timer
	private float unlockTimer;
	public float unlockMaxTimer = 1f;
	public int setCheckpoint;


	//Audio Variables
	public AudioClip openSound;
	public AudioClip closeSound;
	public AudioClip lockSound;


	void Start()
	{
		//Initiate TImers
		unlockTimer = unlockMaxTimer;

		//Unlock if it is locked in the wrong checkpoint
		if(PlayerPrefs.GetInt ("Checkpoint") >= setCheckpoint)
		{
			lockedByKey = 0; 
		}
	}

	void Update()
	{
		if(unlocking && unlockTimer > 0f) //If the door is unlocking then countdown
		{
			unlockTimer -= Time.deltaTime; 
			unlockPuzzle.gameObject.SetActive (true);
		}
		else if (unlocking && unlockTimer < 0f)
		{ 
			unlockPuzzle.gameObject.SetActive (false); //Set unlock puzzle object to inactive
			mouseLook1.enabled = true; //Enable all player controlls
			mouseLook2.enabled = true;
			PlayerInteract.busy = false;
			move.enabled = true;
			keyLock1.gameObject.SetActive (false);
			lockedByKey = 0; //Door is now unlocked
			PlayerInteract.hasKey1 = false;
			PlayerInteract.hasKey2 = false;
			PlayerInteract.hasKey3 = false;
			unlocking = false; //The door is no longer unlocking and should repeatedly call the interact function
			//Set as checkpoint
			PlayerPrefs.SetInt ("Checkpoint", setCheckpoint);
			Interact (); //This function will open the door
		}
	}


	//Function that is activated from the player's interaction script.
	public void Interact()
	{
		if(isOpen && lockedByKey == 0) //If the door is open and not locked then close door
		{
			animation.Rewind ();
			animation.Play ("doorClose");
			isOpen = false;

			PlaySound (closeSound);		//Play the closing sound
		}
		else if(!isOpen && lockedByKey == 0) //If the door is closed and not locked then open door
		{
			animation.Rewind ();
			animation.Play ("doorOpen");
			isOpen = true;

			PlaySound (openSound);		//Play the opening sound
		}
		else if(lockedByKey == 1 && PlayerInteract.hasKey1 ||
		        lockedByKey == 2 && PlayerInteract.hasKey2 ||
		        lockedByKey == 3 && PlayerInteract.hasKey3) //if the door is locked and player has key to open it then do key puzzle
		{
			//Disable all movement scripts and enable the keypuzzle
			mouseLook1.enabled = false;
			mouseLook2.enabled = false;
			PlayerInteract.busy = true;
			move.enabled = false;
			keyPuzzle1.gameObject.SetActive (true);
			keyLock1.gameObject.SetActive (true);
		}
		else
		{
			PlaySound (lockSound);		//Play the locking sound
		}
	}


	//This function is called to play sound
	public void PlaySound (AudioClip _sound)
	{
		//Stop current sound and play appropriate sound
		if(audio.isPlaying)
		{
			audio.Stop ();
		}

		audio.clip = _sound; 	//Set audio clip
		audio.Play ();			//Play Audio Clip
	}


	//Function recieved from Key puzzle script is used when the player solves the puzzle to unlock the door
	public void UnlockDoor()
	{
		unlocking = true;
		keyPuzzle1.gameObject.SetActive (false); 
	}
}
