using UnityEngine;
using System.Collections;

//Script Objective: Spawn object when in contact with player

public class SpawnTriggerScript : MonoBehaviour 
{
	//Object
	public Transform thing; 
	public int randomChance;
	public bool oneTime;

	void OnTriggerEnter(Collider other)
	{
		//If collide with player then spawn object
		if(other.gameObject.CompareTag ("Player"))
		{
			//Create a random number and if random chance is less than random number then spawn object
			int randomNumber = Random.Range (0,100);

			if(randomNumber <= randomChance)
			{
				thing.gameObject.SetActive (true); //spawn object
			}


			if(oneTime)
			{
				transform.gameObject.SetActive (false);
			}
		}
	}
}
