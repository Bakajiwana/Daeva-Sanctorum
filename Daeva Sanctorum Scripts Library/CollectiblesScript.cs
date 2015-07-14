using UnityEngine;
using System.Collections;

//SCRIPT OBJECTIVE:
//This is a script that controls the functionality of various collectibles when picked up

public class CollectiblesScript : MonoBehaviour 
{
	//Item Variables
	public bool oilCan;
	public bool healthPack;
	public bool godFire;

	//Oil Can Variables
	public float oil;
	public Transform oilSound;

	//Health Variables
	public float health;
	public Transform healthSound;

	//God Fire Variables
	public Transform lanturnFire;
	public Transform lanturnGodFire;
	public Transform ghostObjects;

	public void Interact()
	{
		//If oil can then refil players light
		if(oilCan)
		{
			GameObject.FindGameObjectWithTag("Player Light").SendMessage ("addLight", oil);
			Instantiate (oilSound, transform.position, transform.rotation);	//Instantiate oil sound
			transform.gameObject.SetActive (false);
		}

		//If Health pack restore player's health
		if(healthPack)
		{
			GameObject.FindGameObjectWithTag("Player").SendMessage ("ApplyPlayerRestore", health);
			Instantiate (healthSound, transform.position, transform.rotation); //Instantiate health sound
			transform.gameObject.SetActive (false);
		}

		//If God fire was picked up
		if(godFire)
		{
			lanturnFire.gameObject.SetActive(false);
			lanturnGodFire.gameObject.SetActive (true);
			ghostObjects.gameObject.SetActive (true);
			transform.gameObject.SetActive (false);
		}
	}
}
