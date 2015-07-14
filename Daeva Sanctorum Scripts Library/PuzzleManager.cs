using UnityEngine;
using System.Collections;

//Script Objective: disable and enable puzzle scripts for performance

public class PuzzleManager : MonoBehaviour 
{
	//Puzzle objects
	public Transform puzzle01;
	public Transform puzzle02;
	public Transform basementGhostObjects;
	public Transform footPrints; 
	public Transform enemyBooboo;
	public Transform puzzle03;
	public Transform puzzle04;
	public Transform bossBooboo;
	public Transform finalBossRoom;

	public Transform boardings;
	public Transform stairs;

	public Transform ambientSounds;
	public Transform battleSong;

	public Transform stormSystem;

	//Yield time variables
	public float waitTime;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (ExecuteObective());

		int currCheckpoint = PlayerPrefs.GetInt("Checkpoint");

		if(currCheckpoint >= 0)
		{
			boardings.gameObject.SetActive (true);
			stairs.gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	//For optimisation only check if scripts need to be enabled and disabled every x seconds using coroutine
	IEnumerator ExecuteObective()
	{
		while(true) //loop forever
		{
			yield return new WaitForSeconds (waitTime);
			int currCheckpoint = PlayerPrefs.GetInt("Checkpoint");
			if(currCheckpoint >= 0 && currCheckpoint < 3)
			{
				//Booboo is not meant to haunt the house yet
				enemyBooboo.gameObject.SetActive (false);
			}

			if(currCheckpoint >= 0)
			{
				boardings.gameObject.SetActive (true);
				stairs.gameObject.SetActive (false);
			}

			if(currCheckpoint >= 1)
			{
				puzzle01.gameObject.SetActive (false);

				boardings.gameObject.SetActive (false);
			}
			if(currCheckpoint >= 3)
			{
				puzzle02.gameObject.SetActive (false);
				//UNLEASH BOOBOO
				enemyBooboo.gameObject.SetActive (true);
				puzzle03.gameObject.SetActive (true);
				stairs.gameObject.SetActive (true);
			}
			if(currCheckpoint >= 4)
			{
				footPrints.gameObject.SetActive (false);
			}
			if(currCheckpoint >= 5)
			{
				puzzle03.gameObject.SetActive (false);
				puzzle04.gameObject.SetActive (true);
			}

			if(currCheckpoint >= 8)
			{
				enemyBooboo.gameObject.SetActive (false);
				puzzle04.gameObject.SetActive (false);
				finalBossRoom.gameObject.SetActive (true);
			}

			//Prevent Basement glitches
			if(currCheckpoint < 2)
			{
				basementGhostObjects.gameObject.SetActive (false);
			}

			if(currCheckpoint < 8)
			{
				bossBooboo.gameObject.SetActive (false);
			}

			if(currCheckpoint < 7)
			{
				finalBossRoom.gameObject.SetActive (false);
			}

			if(currCheckpoint == 8)
			{
				ambientSounds.gameObject.SetActive (false);
				battleSong.gameObject.SetActive (true);
				stormSystem.gameObject.SetActive (false);
			}
		}
	}
}
