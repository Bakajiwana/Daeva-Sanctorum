using UnityEngine;
using System.Collections;

//Script Objective: Control the Storm! I AM ZEUS

public class StormScript : MonoBehaviour 
{
	//Storm Variables
	private float waitTime;
	public float waitMaxTime = 50f;

	//Audio Variables
	public AudioClip[] thunderSound;
	public float thunderVolume;


	// Use this for initialization
	void Start () 
	{
		//Start Co Routine 
		StartCoroutine (Storm());

		//Calculate a random lightning strike time
		RandomLightning ();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	IEnumerator Storm()
	{
		while(true) //loop forever
		{
			yield return new WaitForSeconds (waitTime);
			Lightning ();
		}
	}

	void RandomLightning()
	{
		float random = Random.Range (0, waitMaxTime);
		waitTime = random;
	}

	void Lightning()
	{
		//Shove all objects with Storm Light into an array
		GameObject[] lightning = GameObject.FindGameObjectsWithTag ("Storm Light");
		
		//Ensure each point light in the array, play lightning animation
		for (int i = 0; i < lightning.Length; i++)
		{
			if(lightning[i].animation["stormLight"])
			{
				lightning[i].animation.Play("stormLight");
			}
		}

		//Play Thunder Audio
		audio.clip = thunderSound[Random.Range (0, thunderSound.Length)];
		audio.volume = thunderVolume;
		audio.Play();

		RandomLightning();
	}
}
