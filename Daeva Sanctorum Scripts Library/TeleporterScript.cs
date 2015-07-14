using UnityEngine;
using System.Collections;

//Script Objective: Teleport the player to a selected location

public class TeleporterScript : MonoBehaviour 
{
	//Teleport Variables
	public Transform teleportTo; 
	public float forwardOffset = 2f;
	public float sideOffset = 0f;

	//Sound Variable
	public Transform teleportSound;

	//Player 
	private GameObject player;

	// Use this for initialization
	void Start () 
	{
		//Find player
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	

	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject.CompareTag ("Player"))
		{
			Instantiate (teleportSound, transform.position, transform.rotation);
			Vector3 pos = new Vector3 (teleportTo.localPosition.x + sideOffset, teleportTo.localPosition.y, teleportTo.localPosition.z + forwardOffset);
			player.transform.position = pos;	//Spawn player in teleportTo position
			player.transform.rotation = teleportTo.rotation;
		}
	}
}
