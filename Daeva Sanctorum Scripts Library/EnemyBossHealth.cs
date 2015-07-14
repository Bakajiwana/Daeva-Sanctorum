using UnityEngine;
using System.Collections;

//Script objective: the lives of enemy

public class EnemyBossHealth : MonoBehaviour 
{
	//Life variables
	public int maxLife = 3;
	private int life;

	//Set the checkpoint after death
	public int setCheckpoint;

	public Transform fireWall;

	public Transform[] footprintKey;

	// Use this for initialization
	void Start () 
	{
		//Spawn booboo in the right position
		transform.localPosition = Vector3.zero;

		//Initiate variables
		life = maxLife;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//If out of lives then spawn random footprints and orb
		if(life <= 0)
		{
			fireWall.gameObject.SetActive (false);

			int randomNumber = Random.Range(0, footprintKey.Length); //Create a random numver
			footprintKey[randomNumber].gameObject.SetActive (true);  //Spawn random footprint and key
			PlayerPrefs.SetInt ("Checkpoint", setCheckpoint);		 //Set the checkpoint
			transform.gameObject.SetActive (false); 				 //Set inactive
		}

		//If Player is dead then move back to starting position
		if(PlayerHealth.gameOver)
		{
			//Spawn booboo in the right position
			transform.localPosition = Vector3.zero;
			life = maxLife;	//Restore health ready for the next battle
			transform.gameObject.SetActive (false); //Set inactive
		}
	}

	//Function is called when hit
	void OnCollisionEnter(Collision other)
	{
		//If hit by an explosion potion
		if(other.gameObject.CompareTag ("Explosive Potion"))
		{
			life--; 	//Lose a life
		}
	}
}
