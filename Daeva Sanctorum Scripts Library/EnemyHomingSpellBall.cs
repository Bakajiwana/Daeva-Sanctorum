using UnityEngine;
using System.Collections;

//Script Objective: Handle the homing effect of this magic cast

public class EnemyHomingSpellBall : MonoBehaviour 
{
	public float damage = 40f;

	private GameObject player;

	public float speed = 20f;


	// Use this for initialization
	void Start () 
	{
		//Return player location
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		float step = speed * Time.deltaTime;

		transform.position = Vector3.MoveTowards (transform.position, player.transform.position, step);
	}

	//This function is called when it hits something
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag ("Player"))
		{
			//Apply damage to the player
			GameObject.FindGameObjectWithTag ("Player").SendMessage ("ApplyPlayerDamage", damage);

			Destroy (gameObject);
		}

		if(other.gameObject.CompareTag ("Obstacle"))
		{
			Destroy (gameObject);
		}
	}
}
