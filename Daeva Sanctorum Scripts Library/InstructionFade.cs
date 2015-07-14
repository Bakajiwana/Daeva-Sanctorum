using UnityEngine;
using System.Collections;

//Script Objective: Read whether this is the players first time and then display these instructions

public class InstructionFade : MonoBehaviour 
{
	public Transform controllerInteract;
	public Transform mouseInteract;
	public float fadeRate = 0.5f;
	private bool display;

	private float timer;
	public float maxTimer = 10f;
	private float disableTimer;

	// Use this for initialization
	void Start () 
	{
		int currCheckpoint = PlayerPrefs.GetInt ("Checkpoint");

		if(currCheckpoint > 0)
		{
			display = false;
		}
		else
		{
			display = true;
			timer = maxTimer;
			disableTimer = maxTimer;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Temporary color variables for instruction fade
		Color mouseFade = mouseInteract.renderer.material.color;
		Color controllerFade = controllerInteract.renderer.material.color;

		if(display)
		{
			//Also display instruction prompt
			if(PlayerInteract.usingController && controllerFade.a <= 1f)
			{
				mouseFade.a = 0f;
				controllerFade.a += fadeRate * Time.deltaTime;
				controllerInteract.renderer.material.color = controllerFade;
			}

			if (!PlayerInteract.usingController && mouseFade.a <= 1f)
			{
				controllerFade.a = 0f;
				mouseFade.a += fadeRate * Time.deltaTime;
				mouseInteract.renderer.material.color = mouseFade;
			}

			if(PlayerInteract.usingController)
			{
				mouseFade.a -= fadeRate * Time.deltaTime;
				mouseInteract.renderer.material.color = mouseFade;
			}

			if(!PlayerInteract.usingController)
			{
				controllerFade.a -= fadeRate * Time.deltaTime;
				controllerInteract.renderer.material.color = controllerFade;
			}

			if(controllerFade.a >= 1f || mouseFade.a >= 1f)
			{
				timer -= Time.deltaTime;
			}

			if(timer <= 0f)
			{
				//Instruction fade
				if(controllerFade.a >= 0f)
				{
					controllerFade.a -= fadeRate * Time.deltaTime;
					controllerInteract.renderer.material.color = controllerFade;
				}


				if(mouseFade.a >= 0f)
				{
					mouseFade.a -= fadeRate * Time.deltaTime;
					mouseInteract.renderer.material.color = mouseFade;
				}

				disableTimer -= Time.deltaTime;

				if(disableTimer <= 0f)
				{
					display = false;
				}
			}
		}
		else
		{
			transform.gameObject.SetActive (false);
		}
	}
}
