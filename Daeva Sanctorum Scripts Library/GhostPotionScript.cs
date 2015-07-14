using UnityEngine;
using System.Collections;

//Script Objective: This script is attached to ghost potions used to throw at Booboo
//When hit they go spawn back at spawn point

public class GhostPotionScript : MonoBehaviour 
{	
	//Emit variables
	public Transform explosion;

	// Update is called once per frame
	void Update () 
	{
		
	}
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.CompareTag ("Enemy"))
		{
			if(explosion)
			{
				Instantiate (explosion, transform.position, transform.rotation);	//emit explosion
			}
			PlayerDragRigidbody.click = false;
			transform.localPosition = Vector3.zero;
		}
	}

}
