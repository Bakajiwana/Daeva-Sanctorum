using UnityEngine;
using System.Collections;

//Script Objective: This script maintains the pillars during the EPIC BOSS FIGHT OF THE CENTURY

public class BossPillarScript : MonoBehaviour 
{

	public Transform crystal;
	private bool activated;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag ("Crystal") && activated == false)
		{
			other.gameObject.SetActive (false);
			crystal.gameObject.SetActive (true);
			BossFightScript.shieldStruck = true;
			activated = true;

			//Fix error with click
			PlayerDragRigidbody.click = false;
		}
	}
}
