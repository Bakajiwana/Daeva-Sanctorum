using UnityEngine;
using System.Collections;

//Script Objective: Make the enemy follow a destination using nav mesh agent
//Reference: Digital Tutors New Features in Unity 3.5: http://www.digitaltutors.com/tutorial/693-New-Features-in-Unity-3.5#play-14898

public class BossDestination : MonoBehaviour 
{
	//Nav mesh Variables
	public NavMeshAgent agent;
	public Transform booboo;
	public int slowSpeed = 8;
	public int medSpeed = 12;
	public int fastSpeed = 16;
	public BossFightScript bossFightScript;

	public Transform player;

	// Use this for initialization
	void Awake () 
	{
		agent.destination = transform.position;
	}

	// Use this for initialization
	void Start () 
	{
		
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
		   && bossFightScript.isIdle == false && bossFightScript.isAttacking == false)
		{
			transform.position = player.position;

			if(bossFightScript.attackPhase == 2 || bossFightScript.attackPhase == 8)
			{
				agent.speed = slowSpeed;
			}

			if(bossFightScript.attackPhase == 4)
			{
				agent.speed = medSpeed;
			}

			if(bossFightScript.attackPhase == 6)
			{
				agent.speed = fastSpeed;
			}
		}
		else
		{
			agent.speed = 0;
		}

		//If Attacking, disable nav mesh agent rotation
		if(bossFightScript.isAttacking || bossFightScript.castingSpell)
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
