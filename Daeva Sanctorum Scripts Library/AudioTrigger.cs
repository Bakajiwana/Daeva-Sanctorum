using UnityEngine;
using System.Collections;

//Script Objective: Trigger that activates a random sound 

public class AudioTrigger : MonoBehaviour 
{
	public AudioClip[] sound;
	public float volume = 1f;

	public int randomChance;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter(Collider other)
	{
		//If Player collides with this trigger
		if(other.gameObject.CompareTag ("Player"))
		{
			//Create a random sound within a random chance
			int random = Random.Range (0, 100);
			if (random <= randomChance)
			{
				audio.clip = sound[Random.Range (0, sound.Length)];
				audio.volume = volume;
				audio.Play ();
			}
		}
	}
}
