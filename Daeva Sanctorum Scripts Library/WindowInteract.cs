using UnityEngine;
using System.Collections;

public class WindowInteract : MonoBehaviour 
{

	//Door Variables
	public bool isOpen = false;

	//Volume Variables - when window is open or close
	public float openVolume;
	public float closeVolume;

	//Curtain variable
	public Transform curtain;
	public Transform curtain2;

	public Transform player;
	public float maxCurtainDist = 15f;
	
	void Start()
	{
		
	}

	void Update()
	{
		//For performance because curtains literally drop fps to 3fps, start of scene
		//When in range turn on curtains
		if(player)
		{
			float dist = Vector3.Distance (player.position, transform.position);
			if(dist < maxCurtainDist)
			{
				if(curtain)
				{
					curtain.gameObject.SetActive (true);
				}
				if(curtain2)
				{
					curtain2.gameObject.SetActive (true);
				}
			}
			else
			{
				if(curtain)
				{
					curtain.gameObject.SetActive (false);
				}
				if(curtain2)
				{
					curtain2.gameObject.SetActive (false);
				}
			}
		}

	}
	
	//Function that is activated from the player's interaction script.
	public void Interact()
	{
		if(isOpen)
		{
			animation.Rewind ();
			animation.Play ("windowClose");
			audio.volume = closeVolume;		//Decrease volume because window is closed.
			if(curtain)
			{
				curtain.GetComponent<Cloth>().randomAcceleration = new Vector3(0f, 0f, 0f);	//Update curtain to stop
			}
			isOpen = false;
		}
		else
		{
			animation.Rewind ();
			animation.Play ("windowOpen");
			audio.volume = openVolume;		//Increase volume because window is open.
			if(curtain)
			{
				curtain.GetComponent<Cloth>().randomAcceleration = new Vector3(15f, 0f, 0f); //Curtain should move
			}
			isOpen = true;
		}
	}
}
