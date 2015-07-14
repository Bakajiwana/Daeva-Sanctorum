using UnityEngine;
using System.Collections;

//SCRIPT OBJECTIVE: 
//At the start of the game there is a black screen that fades away
//This script maintains the fade and then sets this inactive

public class StartTransition : MonoBehaviour 
{
	public float fade = 1f;
	public float fadeRate = 0.2f;

	// Use this for initialization
	void Start () 
	{
		fade = 1f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Color fadeAway = renderer.material.color;
		fadeAway.a -= fadeRate * Time.deltaTime;
		renderer.material.color = fadeAway;

		if(fade <= 0f)
		{
			transform.gameObject.SetActive (false);
		}
	}
}
