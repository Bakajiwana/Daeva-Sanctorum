using UnityEngine;
using System.Collections;

//Credits Script

public class CreditsScript : MonoBehaviour 
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
			Application.LoadLevel (0);
		}
	}
}
