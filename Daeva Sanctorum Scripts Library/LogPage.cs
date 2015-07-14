using UnityEngine;
using System.Collections;

public class LogPage : MonoBehaviour 
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
			Destroy (gameObject);
		}
	}
}
