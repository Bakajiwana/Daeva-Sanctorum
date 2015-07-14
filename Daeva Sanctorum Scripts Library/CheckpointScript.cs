using UnityEngine;
using System.Collections;

//Script Objective: Handle the Checkpoint system

public class CheckpointScript : MonoBehaviour 
{
	public Transform player;
	public Transform[] checkpoint;
	public static int currCheckpoint;

	//Respawn Parameter variables
	public Transform instructions;
	private GameObject [] doors;
	public Transform puzzle1;
	public Transform lanturnFire;
	public Transform godFire;
	public Transform godFireCollectible;
	public Transform basementGhostObjects; 
	public Transform[] footprints;
	public Transform teleporters;
	public Transform ceremony;
	public Transform goldKey;

	public Transform keyPuzzle1;
	public Transform keyPuzzle2;
	public Transform keyPuzzle3;

	// Use this for initialization
	void Start () 
	{
		currCheckpoint = PlayerPrefs.GetInt ("Checkpoint");


		//Spawn the player's progress
		Respawn ();
	}

	void Update()
	{
		
	}
	
	public void Respawn()
	{
		currCheckpoint = PlayerPrefs.GetInt ("Checkpoint");		//Get the latest checkpoint
		player.position = checkpoint[currCheckpoint].position;	//Spawn player in the current checkpoint position
		player.rotation = checkpoint[currCheckpoint].rotation;	//Spawn player in the rotation of current checkpoint


		//During this function each checkpoint should have a set parameter.
		//Like making sure every door is closed and player has key puzzle and disabling past objects and
		//Scripts that are finished beyond point to improve performance

		//If the player reaches x checkpoints
		if(currCheckpoint == 0)
		{
			PlayerInteract.hasKey1 = false;	//Makes sure all keys are turned off in the start
			PlayerInteract.hasKey2 = false;
			PlayerInteract.hasKey3 = false;
		}

		if(currCheckpoint >= 1)
		{
			//Shove all objects with Interact Object into an array
			doors = GameObject.FindGameObjectsWithTag ("Interact Object");

			//Ensure each door is closed
			for (int i = 0; i < doors.Length; i++)
			{
				if(doors[i].animation["doorClose"])
				{
					doors[i].animation.Play ("doorClose");
				}
			}

			//Puzzle 1 should be deactivated
			puzzle1.gameObject.SetActive (false);

			//Player has key 1
			PlayerInteract.hasKey1 = true;
			PlayerInteract.hasKey2 = false;
			PlayerInteract.hasKey3 = false;

			//Set the lanturn fires
			lanturnFire.gameObject.SetActive (true);
			godFire.gameObject.SetActive (false);

			//Anything after this checkpoint disable any key puzzle when Booboo kills the player
			keyPuzzle1.gameObject.SetActive (false);
			keyPuzzle2.gameObject.SetActive (false);
			keyPuzzle3.gameObject.SetActive (false);
		}

		if(currCheckpoint >= 2)
		{
			//Player no longer has key 1
			PlayerInteract.hasKey1 = false;

			//Turn off instructions
			instructions.gameObject.SetActive (false);

			//set trigger and god fire collectible
			godFireCollectible.gameObject.SetActive (true);
			
			basementGhostObjects.gameObject.SetActive (false);
		}

		if(currCheckpoint >= 3)
		{
			//Set the lanturn fires
			lanturnFire.gameObject.SetActive (false);
			godFire.gameObject.SetActive (true);

			//set trigger and god fire collectible
			godFireCollectible.gameObject.SetActive (false);

			//Redo the footprint puzzle
			for (int i = 0; i < footprints.Length; i++)
			{
				footprints[i].gameObject.SetActive (false);
			}

			int randomFootprint = Random.Range (0, footprints.Length);
			footprints[randomFootprint].gameObject.SetActive (true);

			//Make sure player has key 2 is false when spawning
			PlayerInteract.hasKey2 = false;
		}

		if(currCheckpoint >= 4)
		{
			//Teleporters should be on
			teleporters.gameObject.SetActive (true);
		}

		if(currCheckpoint >= 5)
		{
			//Teleporters should be off
			teleporters.gameObject.SetActive (false);

			//Player has key 3
			PlayerInteract.hasKey3 = true;
		}

		
		if(currCheckpoint >= 6)
		{
			PlayerInteract.hasKey3 = false;

			//Ceremony needs to be true
			ceremony.gameObject.SetActive (true);
		}


		//In checkpoint 7 only I just want the golden key to be active and reset
		if(currCheckpoint == 7)
		{
			goldKey.gameObject.SetActive (true);
			MasterKeyScript.masterKeyReset = true;
		}
	}
}
