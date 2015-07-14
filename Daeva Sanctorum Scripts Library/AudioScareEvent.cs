using UnityEngine;
using System.Collections;

//Script Objective when player spots booboo, emit sound

public class AudioScareEvent : MonoBehaviour 
{
	private float delayTimer;
	public float delayMaxTimer;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//If enemy seen and delayTimer is equal or less than zero
		if(PlayerSight.enemyInSight && delayTimer <= 0f)
		{
			audio.Play ();	//Play sound
			delayTimer = delayMaxTimer; //Reset countdown
		}

		//If delay Timer is greater than 0 then countdown
		if(delayTimer > 0f)
		{
			delayTimer -= Time.deltaTime;
		}
	}
}
