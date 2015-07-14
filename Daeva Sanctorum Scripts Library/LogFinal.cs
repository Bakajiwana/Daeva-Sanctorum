using UnityEngine;
using System.Collections;

//This script ends the game and places player in the credits after reading it

public class LogFinal : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown ("Fire1") || Input.GetButtonDown ("Back") || Input.GetButtonDown("Jump")
		   || Input.GetButtonDown ("Pause") || Input.GetButtonDown ("Sprint"))
		{
			//Activate the END
			GameObject.FindGameObjectWithTag("Level Manager").SendMessage ("End");
		}
	}
}
