using UnityEngine;
using System.Collections;

//SCRIPT OBJECTIVE: THE CARPET BOMB SPELL BY BOOBOO DURING THE EPIC FINAL BOSS FIGHT! YEEEEEEAAHHHHHH

public class EnemyHomingCarpetBomb : MonoBehaviour 
{
	//Homing Carpet Bomb Spawner
	public bool carpetBombSpawner;

	private GameObject player;
	public float speed = 5f;
	public float superSpeed = 20f;
	public float waitTime = 1f;
	public float superWaitTime = 0.3f;
	public Transform spellBomb;

	public bool carpetBomb;
	public bool superCarpetBombSpawner;

	public float damage = 75f;

	public bool childSpawner;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");

		if(childSpawner || superCarpetBombSpawner)
		{
			speed = superSpeed;
			waitTime = superWaitTime;
		}

		if(carpetBombSpawner || superCarpetBombSpawner)
		{
			StartCoroutine (ExecuteObective());
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(carpetBombSpawner && player && !childSpawner ||superCarpetBombSpawner && player && !childSpawner)
		{
			//Move towards player
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, step);

			//Rotate towards player
			Vector3 targetDir = player.transform.position - transform.position;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
			Debug.DrawRay(transform.position, newDir, Color.red);
			transform.rotation = Quaternion.LookRotation(newDir);
		}
	}


	IEnumerator ExecuteObective()
	{
		while(true) //loop forever
		{
			yield return new WaitForSeconds (waitTime);

			Instantiate (spellBomb, transform.position, transform.rotation);
		}
	}

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
