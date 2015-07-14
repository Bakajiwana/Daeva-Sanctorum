using UnityEngine;
using System.Collections;

//Script Objective: The function of the arc balls that appear on Booboo during the EPIC BOSS FIGHT

public class EnemyArcScript : MonoBehaviour 
{
	private GameObject player;
	public float speed = 20f;

	public Transform spellBall;
	public float spellCastSpeed = 1000f;

	//Yield time variables
	public float waitTime;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");

		StartCoroutine (ExecuteObective());
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 targetDir = player.transform.position - transform.position;
		float step = speed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
		Debug.DrawRay(transform.position, newDir, Color.red);
		transform.rotation = Quaternion.LookRotation(newDir);
	}

	//For optimisation only check if scripts need to be enabled and disabled every x seconds using coroutine
	IEnumerator ExecuteObective()
	{
		while(true) //loop forever
		{
			yield return new WaitForSeconds (waitTime);

			if(!PlayerHealth.gameOver && !PauseScript.isPaused)
			{
				//Shoot at player for every delay
				Transform castSpell = Instantiate(spellBall, transform.position, transform.rotation) as Transform;
				castSpell.rigidbody.AddForce (transform.forward * spellCastSpeed);
			}
		}
	}
}
