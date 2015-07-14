using UnityEngine;
using System.Collections;

public class KeyInteract : MonoBehaviour 
{
	public bool key01 = false;
	public bool key02 = false;
	public bool key03 = false;


	//Special event of appearing things and disappearing 
	public Transform appearObject;
	public Transform disappearObject; 

	public int setCheckpoint;
	
	//Function that is activated from the player's interaction script.
	public void Interact()
	{
		if(key01)
		{
			PlayerInteract.hasKey1 = true;
			//Set as checkpoint
			PlayerPrefs.SetInt ("Checkpoint", setCheckpoint);
		}

		if(key02)
		{
			PlayerInteract.hasKey2 = true;
			//Set as checkpoint
			PlayerPrefs.SetInt ("Checkpoint", setCheckpoint);
		}

		if(key03)
		{
			PlayerInteract.hasKey3 = true;
			//Set as checkpoint
			PlayerPrefs.SetInt ("Checkpoint", setCheckpoint);
		}

		//If any specified node to appear or disappear then do those.
		if(appearObject)
		{
			appearObject.gameObject.SetActive (true);
		}

		if(disappearObject)
		{
			disappearObject.gameObject.SetActive (false);
		}

		//Destroy self
		Destroy (gameObject);
	}
}
