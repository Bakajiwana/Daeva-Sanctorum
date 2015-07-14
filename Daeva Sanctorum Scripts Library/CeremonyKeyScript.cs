using UnityEngine;
using System.Collections;

//SCRIPT OBJECTIVE: During the puzzle 4 ceremony, the player places the keys into pillars

public class CeremonyKeyScript : MonoBehaviour 
{
	public Transform key;

	public int disableAfterCheckpoint = 7;

	// Use this for initialization
	void Start () 
	{
		key.gameObject.SetActive (false);

		if(PlayerPrefs.GetInt("Checkpoint") >= disableAfterCheckpoint)
		{
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	//This function is called when player clicks on this
	public void Interact()
	{
		key.gameObject.SetActive (true);
		Destroy (gameObject);
	}
}
