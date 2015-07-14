using UnityEngine;
using System.Collections;

//Script Objective: In attack phase 8 in the EPIC BOOBOO BOSS FIGHT. Spawns this collider that increases Booboo's range.

public class EnemyMeleeSwipe : MonoBehaviour 
{
	public float damage = 30f;

	//This function is called when it hits something
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag ("Player"))
		{
			//Apply damage to the player
			GameObject.FindGameObjectWithTag ("Player").SendMessage ("ApplyPlayerDamage", damage);
		}
	}
}
