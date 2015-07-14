
using UnityEngine;
using System.Collections;

//Script Objective: This script will control the oil count and flickering of the lanturn light
//Pretty much everything to do with the lanturns light

public class LanturnLight : MonoBehaviour 
{
	//Animations variables
	private Animator anim;				//A variable reference to the animator of the character

	//Lanturn light variables
	public float maxOil = 100f;
	public float oilUsage = 2f;
	private float oil;
	public Transform fire;
	public float maxEnemyDist = 50f;

	// Use this for initialization
	void Start () 
	{
		//Initialise player animator
		anim = GetComponent<Animator>();

		//Initialise oil variable
		oil = maxOil;

		anim.SetBool("Danger", false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(oil > maxOil)
		{
			oil = maxOil;
		}

		if(oil < 0f)
		{
			oil = 0f;
		}

		//Decrease Oil overtime and when player isn't busy
		if(!PlayerInteract.busy)
		{
			if(oil > 0)
			{
				oil -= oilUsage * Time.deltaTime;
				anim.SetFloat ("Oil", oil);
				fire.gameObject.SetActive (true);
			}
			else
			{
				fire.gameObject.SetActive (false);
			}
		}

		//FLICKER LIGHT IF ENEMY IS NEAR
		if(alertPlayer())
		{
			anim.SetBool ("Danger", true);
		}
		else
		{
			anim.SetBool("Danger", false);
		}
	}


	//A Boolean method that returns true if enemy nearby else false
	public bool alertPlayer()
	{
		//Find all enemies with Enemy Tag and place into an array
		GameObject [] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		int numberOfEnemies = 0;

		for (int i = 0; i < enemies.Length; i++)
		{
			float dist = Vector3.Distance (enemies[i].transform.position, transform.position);

			if(dist < maxEnemyDist)
			{
				numberOfEnemies++;
			}
		}

		if(numberOfEnemies > 0)
		{
			return true;
		}
		else
		{
			return false; 
		}
	}

	//Add or lose Light Function
	public void addLight(float _add)
	{
		oil += _add;
	}
}
