using UnityEngine;
using System.Collections;

//Script Objective: Re-usable script to switch between animations on interaction

public class AnimationInteraction : MonoBehaviour 
{
	//Audio Variables
	public AudioClip openSound;
	public AudioClip closeSound;
	public AudioClip lockSound;

	//Animation Variables
	public string openName;
	public string closeName;



	public bool isOpen = false;

	//This function is called when player interacts
	public void Interact()
	{
		if(isOpen) //If the door is open
		{
			if(closeName != "")
			{
				animation.Rewind ();
				animation.Play (closeName);
			}
			isOpen = false;

			if(closeSound)
			{
				PlaySound (closeSound);		//Play the closing sound
			}

			PlayerDragRigidbody.click = false; //turn off interaction symbol
		}
		else if(!isOpen) //If the door is closed
		{
			if(openName != "")
			{
				animation.Rewind ();
				animation.Play (openName);
			}
			isOpen = true;

			if(openSound)
			{
				PlaySound (openSound);		//Play the opening sound
			}

			PlayerDragRigidbody.click = false; //turn off interaction symbol
		}
		else
		{
			if(lockSound)
			{
				PlaySound (lockSound);		//Play the locking sound
			}
		}
	}

	//This function is called to play sound
	public void PlaySound (AudioClip _sound)
	{
		//Stop current sound and play appropriate sound
		if(audio.isPlaying)
		{
			audio.Stop ();
		}
		
		audio.clip = _sound; 	//Set audio clip
		audio.Play ();			//Play Audio Clip
	}
}
