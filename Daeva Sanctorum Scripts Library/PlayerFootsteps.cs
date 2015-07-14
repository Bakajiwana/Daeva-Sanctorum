using UnityEngine;
using System.Collections;

//Script Objective: Calculate footsteps and then output footstep sound

public class PlayerFootsteps : MonoBehaviour 
{
	public AudioClip[] footsteps; 	//Footstep sound
	public float audioStepLengthWalk = 0.25f;
	public float audioStepLengthSprint = 0.15f;
	private float footStepVolume = 0.5f;
	public float footStepMaxVolume = 1f; 
	private bool step = true;

	private float jumpDelay;
	public float jumpMaxDelay = 1f;
	
	//Reference: http://answers.unity3d.com/questions/11486/footstep-sounds-when-walking.html
	
	// Use this for initialization
	void Start () 
	{
		//Initialise volume
		footStepVolume = footStepMaxVolume;
	}
	
	// Update is called once per frame
	void Update () 
	{		
		//Play Sprint step
		if(LanturnAnimations.playerSpeedX > 0f && step == true && jumpDelay <= 0f && CharacterMotorC.playerSprint == true || 
		   LanturnAnimations.playerSpeedX < 0f && step == true && jumpDelay <= 0f && CharacterMotorC.playerSprint == true ||
		   LanturnAnimations.playerSpeedY > 0f && step == true && jumpDelay <= 0f && CharacterMotorC.playerSprint == true ||
		   LanturnAnimations.playerSpeedY < 0f && step == true && jumpDelay <= 0f && CharacterMotorC.playerSprint == true)
		{
			sprint ();
		}

		//Play Walk step
		if(LanturnAnimations.playerSpeedX > 0f && step == true && jumpDelay <= 0f && CharacterMotorC.playerSprint == false || 
		   LanturnAnimations.playerSpeedX < 0f && step == true && jumpDelay <= 0f && CharacterMotorC.playerSprint == false ||
		   LanturnAnimations.playerSpeedY > 0f && step == true && jumpDelay <= 0f && CharacterMotorC.playerSprint == false ||
		   LanturnAnimations.playerSpeedY < 0f && step == true && jumpDelay <= 0f && CharacterMotorC.playerSprint == false)
		{
			walk ();
		}

		//If the player jumps create a delay 
		if(Input.GetButtonDown ("Jump"))
		{
			jumpDelay = jumpMaxDelay;
		}

		//When jump delay is greater then 0
		if(jumpDelay > 0f)
		{
			jumpDelay -= Time.deltaTime; //Countdown
		}
	}
	
	
	
	IEnumerator WaitForFootSteps(float stepsLength)
	{
		step = false; 
		yield return new WaitForSeconds(stepsLength);
		step = true;
	}
	
	void walk()
	{
		audio.clip = footsteps[Random.Range (0, footsteps.Length)];
		audio.volume = footStepVolume;
		audio.Play ();
		StartCoroutine (WaitForFootSteps(audioStepLengthWalk));
	}
	
	void sprint()
	{
		audio.clip = footsteps[Random.Range (0, footsteps.Length)];
		audio.volume = footStepVolume;
		audio.Play ();
		StartCoroutine (WaitForFootSteps(audioStepLengthSprint));
	}
}
