using UnityEngine;
using System.Collections;

//SCRIPT OBJECTIVE: To control the Puzzle 4 script, when all three keys are in place merge and create
//the golden key

public class CeremonyScript : MonoBehaviour 
{
	//Ceremony Variables 
	public Transform [] keys;

	private bool firstKeyReady;
	private bool secondKeyReady;
	private bool thirdKeyReady;

	public Transform masterKey;

	public int setCheckpoint;


	public float ceremonyTimer = 10f;

	public Transform ceremonyEvent;

	//Fix the issue with this script overriding playerprefs by disabling this script
	public CeremonyScript ceremoneyScript;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//If a key is active then set boolean to true
		if(keys[0].gameObject.activeSelf)
		{
			firstKeyReady = true;
		}

		if(keys[1].gameObject.activeSelf)
		{
			secondKeyReady = true;
		}

		if(keys[2].gameObject.activeSelf)
		{
			thirdKeyReady = true;
		}

		//If all keys are ready initiate ceremony and merge all keys into one
		if(firstKeyReady && secondKeyReady && thirdKeyReady)
		{
			ceremonyEvent.gameObject.SetActive (true);
			ceremonyTimer -= Time.deltaTime;
		}

		//When Ceremony finishes
		if(ceremonyTimer <= 0f)
		{
			masterKey.gameObject.SetActive (true);
			foreach(Transform item in keys)
			{
				item.gameObject.SetActive (false);
			}
			//Set as checkpoint
			PlayerPrefs.SetInt ("Checkpoint", setCheckpoint);
			//Disable this script 
			ceremoneyScript.enabled = false;
		}
	}
}
