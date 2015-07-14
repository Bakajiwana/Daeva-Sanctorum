using UnityEngine;
using System.Collections;

//Script Objective: When player walks into trigger. Close the Final Door and activate Booboo the final boss fight.

public class FightTriggerScript : MonoBehaviour 
{
	public GameObject finalDoor;
	public Transform colliderCube;
	public Transform bossAppearParticles;
	public Transform bossBooboo;
	public int setCheckpoint = 8;

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
		if(other.gameObject.CompareTag("Player"))
		{
			//Close door
			finalDoor.animation.Play ("finalDoorClose");
			colliderCube.gameObject.SetActive (true);
			bossBooboo.gameObject.SetActive (true);
			bossAppearParticles.gameObject.SetActive (true);
			PlayerPrefs.SetInt ("Checkpoint", setCheckpoint);
			Destroy(gameObject);
		}
	}
}
