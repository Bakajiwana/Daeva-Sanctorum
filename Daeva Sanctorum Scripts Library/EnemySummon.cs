using UnityEngine;
using System.Collections;

//Script Objective: Booboo's spawn system that summons him when player enters trigger randomly

public class EnemySummon : MonoBehaviour 
{
	//Booboo Variables
	public Transform booboo;
	public Transform[] summonPoints;
	public EnemyHunt enemyHunt;		//Booboo's hunt script

	//Event Variables
	public int summonChance;
	public int minBehaviour = 1;
	public int maxBehaviour = 5;
	public Transform lobbySpawnHaunt;

	//Delay Timer, for when player triggers event, which goes through a delay so player doesn't reactivate it.
	public float maxDelayTimer = 10f;
	private float delayTimer;

	// Use this for initialization
	void Start () 
	{
		booboo.gameObject.SetActive (false);

		delayTimer = 0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//If the delay is greater than 0 then countdown
		if(delayTimer > 0f)
		{
			delayTimer -= Time.deltaTime;
		}
	}

	//OnTriggerEnter is called when player enters trigger
	void OnTriggerEnter(Collider other)
	{
		//If player steps into trigger
		if(other.gameObject.CompareTag ("Player") && delayTimer <= 0f)
		{
			//Calculate a random chance
			int randomNumber = Random.Range (0, 100);

			//If random number is within the summon chance range
			if(randomNumber <= summonChance)
			{
				booboo.gameObject.SetActive (false); //disable if being chased

				//Calculate a random variable between the number of behaviours Booboo has. This int variable has to be global
				int randomBehaviour = Random.Range (minBehaviour, maxBehaviour); 

				//Calculate a random spawn point
				int randomSpawn = Random.Range (0, summonPoints.Length);

				//If Random behaviour is at 3 when Booboo is meant to just stand in the lobby
				if(randomBehaviour == 3)
				{
					booboo.position = lobbySpawnHaunt.position;
					booboo.gameObject.SetActive (true);
				}
				else
				{
					//Spawn booboo to one of those spawn points 
					booboo.position = summonPoints[randomSpawn].position; 
					booboo.gameObject.SetActive (true);
				}

				//Set Booboo's random behaviour
				enemyHunt.RandomBehaviour (randomBehaviour);

				//This trigger will be inactive for as long as the delay timer
				delayTimer = maxDelayTimer;
			}
		}
	}
}
