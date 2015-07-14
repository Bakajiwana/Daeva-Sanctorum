using UnityEngine;
using System.Collections;

//ATTACH SCRIPT TO A TRIGGER WITH A RIGIDBODY.
//Shift the various objects in the house using a trigger system. 

public class ShiftObjects : MonoBehaviour 
{
	//Shift Variables
	public Transform[] variation; 

	//Delay Variables
	private float delayTimer;
	public float delayMaxTimer = 10f;

	public float checkAndClearTime = 5f;

	public Transform player;

	public float maxDistance = 50f;

	//Start with the objects
	public bool startWithObjects = false;

	// Use this for initialization
	void Start () 
	{
		if(startWithObjects)
		{
			Shift ();
		}
		else
		{
			StartCoroutine (CheckAndClear ());	//Start Enumerator
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(delayTimer > 0f)
		{
			delayTimer -= Time.deltaTime; 
		}
	}

	//This function is called when shifting objects
	void Shift()
	{
		//Clear everything
		Clear ();

		//Create random number
		int random = Random.Range (0, variation.Length);

		//That random variation will be revealed
		variation[random].gameObject.SetActive (true);
	}

	//This function is called when all variations need to be hidden
	void Clear()
	{
		foreach (Transform item in variation)
		{
			item.gameObject.SetActive (false);
		}
	}

	//This function is called when something hits trigger
	void OnTriggerEnter(Collider other)
	{
		//If Collide with player
		if(other.gameObject.CompareTag("Player") && delayTimer <= 0f)
		{
			Shift ();

			delayTimer = delayMaxTimer;
		}
	}

	IEnumerator CheckAndClear()
	{
		while(true) //loop forever
		{
			yield return new WaitForSeconds (checkAndClearTime);
			float dist = Vector3.Distance (player.position, transform.position);
			if(maxDistance < dist)
			{
				Clear ();
			}
		}
	}
}
