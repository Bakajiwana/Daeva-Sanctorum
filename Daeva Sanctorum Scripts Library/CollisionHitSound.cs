using UnityEngine;
using System.Collections;

//SCRIPT OBJECTIVE: Mainly used for the vials. If collides or hit things then make sounds

public class CollisionHitSound : MonoBehaviour 
{
	public AudioClip[] sounds;
	public float audioVolume;
	public int magnitude = 2; 

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnCollisionEnter(Collision other)
	{
		//If the magnitude of the collision is greater then specified velocity, then emit a collision sound
		if(other.relativeVelocity.magnitude > magnitude)
		{
			audio.clip = sounds[Random.Range (0, sounds.Length)];
			audio.volume = audioVolume;
			audio.Play ();
		}
	}
}
