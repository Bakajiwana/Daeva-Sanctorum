using UnityEngine;
using System.Collections;

//SCRIPT OBJECTIVE: This script is the allow the master key to fly away from the player, moving 
//from point to point until it reaches its final destination

public class MasterKeyScript : MonoBehaviour 
{
	//Master Key Variables

	//Speed Variables
	public float slowSpeed;
	public float medSpeed;
	public float fastSpeed;
	private float step;

	//Destination Point Variables
	public Transform[] destination; 

	//Distance Variables
	public float closeRange;
	public float medRange;
	public float longRange; 
	private bool waiting; 

	//Player Variable
	public Transform player;

	//Variable that is reset when respawned
	private int currentDestination = 0;
	public static bool masterKeyReset;

	//Open Final door Variable
	public int setCheckpoint = 8;
	public Transform booboo;
	public Transform doorOpenEffect;
	public FinalDoorOpen finalOpen;

	// Use this for initialization
	void Start () 
	{
		waiting = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Check Players distance
		if(player)
		{
			float dist = Vector3.Distance (player.position,transform.position);

			//If moving towards a point and player is close judge its distance and change speed accordingly
			if(!waiting)
			{
				if(dist < closeRange)
				{
					step = fastSpeed * Time.deltaTime;
				}
				else if (dist < medRange)
				{
					step = medSpeed * Time.deltaTime;
				}
				else if (dist < longRange)
				{
					step = slowSpeed * Time.deltaTime;
				}

				//Move towards destination
				transform.position = Vector3.MoveTowards (transform.position, destination[currentDestination].position, step);
			}
			else
			{
				//IF waiting then don't do anything. But if player is close start moving
				if(dist < closeRange)
				{
					waiting = false; 
				}
			}
		}

		//RESET WHEN TRUE
		if(masterKeyReset)
		{
			waiting = true;
			currentDestination = 0;
			transform.localPosition = Vector3.zero;
			masterKeyReset = false; 
		}
	}

	//Function is called when player interacts with master key
	public void Interact()
	{
		//If all destinations have been found
		if(currentDestination == destination.Length - 1)
		{
			//Set Checkpoint
			PlayerPrefs.SetInt("Checkpoint", setCheckpoint);
			booboo.gameObject.SetActive (false);
			doorOpenEffect.gameObject.SetActive (true);
			finalOpen.FinalOpen ();
			Destroy (gameObject); 
		}
		else
		{
			//If still going away from player, move to next destination
			currentDestination++;
		}
	}
}
