using UnityEngine;
using System.Collections;

//Script Objective: To Control the behaviour of the spell ball cast by Booboo

public class EnemySpellBall : MonoBehaviour 
{
	public float damage = 25f;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	//This function is called when it hits something
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag ("Player"))
		{
			//Apply damage to the player
			GameObject.FindGameObjectWithTag ("Player").SendMessage ("ApplyPlayerDamage", damage);

			CharacterMotorC.playerHit = true;

			Destroy (gameObject);
		}

		if(other.gameObject.CompareTag ("Obstacle"))
		{
			Destroy (gameObject);
		}
	}
}
