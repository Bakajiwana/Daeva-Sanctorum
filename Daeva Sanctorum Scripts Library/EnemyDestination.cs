using UnityEngine;
using System.Collections;

//Script Objective: Make the enemy follow a destination using nav mesh agent
//Reference: Digital Tutors New Features in Unity 3.5: http://www.digitaltutors.com/tutorial/693-New-Features-in-Unity-3.5#play-14898

public class EnemyDestination : MonoBehaviour 
{
	//Nav mesh Variables
	public NavMeshAgent agent;
	public Transform booboo;
	public int moveSpeed = 8;
	public EnemyHunt enemyHunt;

	public Transform player; //Player location


	// Use this for initialization
	void Awake () 
	{
		agent.destination = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//If Booboo is active
		if(booboo.gameObject.activeSelf)
		{
			//Find location of agent
			CheckLocation();
		}

		//Track the players location
		if(player && PauseScript.isPaused == false && PlayerHealth.gameOver == false 
		   && enemyHunt.isIdle == false && enemyHunt.isAttacking == false)
		{
			transform.position = player.position;
			agent.speed = moveSpeed;
		}
		else
		{
			agent.speed = 0;
		}

		//If Attacking, disable nav mesh agent rotation
		if(enemyHunt.isAttacking || enemyHunt.castingSpell)
		{
			agent.updateRotation = false;
		}
		else
		{
			agent.updateRotation = true;
		}
	}

	//Find location of the nav mesh agent
	void CheckLocation()
	{
		//make the agents position equal the enemy's position
		agent.destination = transform.position;
	}
}
