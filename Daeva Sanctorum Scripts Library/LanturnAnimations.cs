using UnityEngine;
using System.Collections;

//When the player looks or moves, activate animations using mecanim

public class LanturnAnimations : MonoBehaviour 
{
	//Animations variables
	private Animator anim;				//A variable reference to the animator of the character

	//Controller Variables
	public float minInput = 0.20f;
	private float joySmoothX;
	private float joySmoothY;

	//Mouse Variables
	private float mouseSmoothX;
	private float mouseSmoothY;

	public float sensitivity = 0.01f;

	//STATIC Variables that returns player speed
	public static float playerSpeedX;
	public static float playerSpeedY;

	// Use this for initialization
	void Start () 
	{
		//Initialise player animator
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Start by creating if statements that sends information to mecanim if greater or lesser then a point

		//If using controller update mecanim
		//Else
		//If using a mouse update mecanim with mouse inputs
		if(Input.GetAxis ("Joy X") > minInput || Input.GetAxis ("Joy X") < -minInput)
		{
			if(joySmoothX < 1f && joySmoothX > -1f)
			{
				joySmoothX += Input.GetAxis ("Joy X") * sensitivity;
			}
			anim.SetFloat ("LookX", joySmoothX);
			anim.SetBool ("Looking", true);
		}
		else
		{
			joySmoothX = 0f;
		}


		if(Input.GetAxis ("Mouse X") > minInput || Input.GetAxis ("Mouse X") < -minInput)
		{
			if(mouseSmoothX < 1f && mouseSmoothX > -1f)
			{
				mouseSmoothX += Input.GetAxis ("Mouse X") * sensitivity;
			}
			anim.SetFloat ("LookX", mouseSmoothX);
			anim.SetBool ("Looking", true);
		}
		else
		{
			mouseSmoothX = 0f;
		}



		if(Input.GetAxis ("Joy Y") > minInput || Input.GetAxis ("Joy Y") < -minInput)
		{
			if(joySmoothY < 1f && joySmoothY > -1f)
			{
				joySmoothY += Input.GetAxis ("Joy Y") * sensitivity;
			}
			anim.SetFloat ("LookY", -joySmoothY);
			anim.SetBool ("Looking", true);
		}
		else
		{
			joySmoothY = 0f;
		}

		if(Input.GetAxis ("Mouse Y") > minInput || Input.GetAxis ("Mouse Y") < -minInput)
		{
			if(mouseSmoothY < 1f && mouseSmoothY > -1f)
			{
				mouseSmoothY += Input.GetAxis ("Mouse Y") * sensitivity;
			}
			anim.SetFloat ("LookY", mouseSmoothY);
			anim.SetBool ("Looking", true);
		}
		else
		{
			mouseSmoothY = 0f;
		}

		//But if the player goes idle
		if(Input.GetAxis ("Joy X") < minInput && Input.GetAxis ("Joy X") > -minInput && 
		   Input.GetAxis ("Mouse X") < minInput && Input.GetAxis ("Mouse X") > -minInput &&
		   Input.GetAxis ("Joy Y") < minInput && Input.GetAxis ("Joy Y") > -minInput &&
		   Input.GetAxis ("Mouse Y") < minInput && Input.GetAxis ("Mouse Y") > -minInput)
		{
			anim.SetBool ("Looking", false);
		}

		//Movement Animations
		anim.SetFloat ("VelocityX", Input.GetAxis ("Horizontal"));
		anim.SetFloat ("VelocityY", Input.GetAxis ("Vertical"));

		if(CharacterMotorC.playerSprint)
		{
			anim.SetBool ("Sprinting", true);
		}
		else
		{
			anim.SetBool("Sprinting", false);
		}

		//When the player is busy he will have his lanturn down
		anim.SetBool ("Busy", PlayerInteract.busy);


		//---------Static Return Player Speed-------------
		playerSpeedX = anim.GetFloat ("VelocityX");
		playerSpeedY = anim.GetFloat ("VelocityY");


		//Create Sound
		if(playerSpeedX > 0f || playerSpeedX < 0f ||
		   playerSpeedY > 0f || playerSpeedY < 0f)
		{
			if(!audio.isPlaying)
			{
				audio.Play ();
			}
		}
		else
		{
			audio.Stop ();
		}
	}	
}
