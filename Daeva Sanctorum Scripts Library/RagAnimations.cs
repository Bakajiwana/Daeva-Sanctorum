using UnityEngine;
using System.Collections;

//Script Objective: To control the animations of the rag or cloth of the character

public class RagAnimations : MonoBehaviour 
{
	//Animations variables
	private Animator anim;				//A variable reference to the animator of the character

	// Use this for initialization
	void Start () 
	{
		//Initialise player animator
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(CharacterMotorC.playerSprint)	//if sprinting
		{
			anim.SetBool ("Sprinting", true);
			anim.SetBool ("Walking", false);
		}
		//If player is moving, use animation
		else if(LanturnAnimations.playerSpeedX > 0f ||
		   LanturnAnimations.playerSpeedX < 0f ||
		   LanturnAnimations.playerSpeedY > 0f ||
		   LanturnAnimations.playerSpeedY < 0f)
		{
			anim.SetBool ("Walking" , true);
			anim.SetBool ("Sprinting", false);
		}
		else
		{
			anim.SetBool ("Sprinting", false);
			anim.SetBool ("Walking", false);
		}
	}
}
