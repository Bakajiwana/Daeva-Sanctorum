using UnityEngine;
using System.Collections;

//This Script is to control the Cauldron puzzle 
//When the player throws the correct orbs into the cauldron the 
//Cauldron will spawn new orbs and change colour
//The orbs should be thrown into the cauldron if they have the same colour as the liquid

//This might not be random. More effort figuring that out then just in a sequence.

public class CauldronLiquid : MonoBehaviour 
{
	//Cauldron puzzle phases
	private int phase = 1;
	private int subPhase = 1; //This is used for the colour within the phase

	//Orb Groups
	public Transform orbGroup1;
	public Transform orbGroup2;
	public Transform orbGroup3;

	//Orbs in the groups
	public Transform[] orbs2; //0 = blue, 1 = green
	public Transform[] orbs3; //0 is blue, 1 is green and 2 is red

	public Transform key1; //The key that appears in the end

	//Effects
	public Transform blueEffect;
	public Transform greenEffect;
	public Transform redEffect;

	public GameObject liquidLight;

	//Audio Variables
	public Transform wrongSound;

	// Use this for initialization
	void Start () 
	{
		renderer.material.SetColor ("_RefrColor", Color.blue); //Liquid colour is now blue
		liquidLight.light.color = Color.blue; //Light colour
	}
	
	// Update is called once per frame
	void Update () 
	{


	}

	void OnTriggerEnter(Collider other)
	{
		//We decided to use blue as the first orb puzzle
		if(other.gameObject.CompareTag ("Blue Orb") && phase == 1)
		{
			orbGroup1.gameObject.SetActive (false);
			renderer.material.SetColor ("_RefrColor", Color.green); //Liquid colour is now Green
			liquidLight.light.color = Color.green; //Light colour
			Instantiate (blueEffect, transform.position, transform.rotation); //Create a particle effect
			orbGroup2.gameObject.SetActive (true);
			subPhase = 1;
			PlayerDragRigidbody.click = false;
			phase++;
		}


		/*Now that the player has solved phase 1
		  Time to move into phase 2. Firstly spawn orb group 2 and change the liquid colour */
		if(phase == 2)
		{
			//Phase 2 subphase 1 means the player has to throw the green orb into the green liquid
			if(other.gameObject.CompareTag ("Green Orb"))
			{
				if(subPhase == 1)
				{
					orbs2[1].gameObject.SetActive (false); 
					renderer.material.SetColor ("_RefrColor", Color.blue);
					liquidLight.light.color = Color.blue; //Light colour
					Instantiate (greenEffect, transform.position, transform.rotation); //Create a particle effect
					PlayerDragRigidbody.click = false;  //Disable the drag rigidbody click to prevent error
					subPhase = 2;
				}

				if(subPhase == 2)
				{
					orbs2[1].localPosition = new Vector3(0,0,0); //Spawn object back to start
					orbs2[1].rigidbody.velocity = orbs2[1].rigidbody.velocity * 0f; //Make sure orb doesn't move
					PlayerDragRigidbody.click = false; //Disable the drag rigidbody click to prevent error
					Instantiate (wrongSound, transform.position, transform.rotation); //Create the wrong sound
				}
			}

			//Subphase 2 means throw the blue orb in
			if(other.gameObject.CompareTag ("Blue Orb"))
			{
				if(subPhase == 1)
				{
					PlayerDragRigidbody.click = false; //Disable the drag rigidbody click to prevent error
					orbs2[0].rigidbody.velocity = orbs2[0].rigidbody.velocity * 0f; //Make sure orb doesn't move
					orbs2[0].localPosition = new Vector3(0,0,0); //Spawn object back to start
					Instantiate (wrongSound, transform.position, transform.rotation); //Create the wrong sound
				}

				if (subPhase == 2)
				{
					PlayerDragRigidbody.click = false; //Disable the drag rigidbody click to prevent error
					orbGroup2.gameObject.SetActive (false);
					orbGroup3.gameObject.SetActive (true);
					renderer.material.SetColor ("_RefrColor", Color.green);
					liquidLight.light.color = Color.green; //Light colour
					Instantiate (blueEffect, transform.position, transform.rotation); //Create a particle effect
					subPhase = 1;
					phase++;
				}
			}
		}

		//Phase 3
		if(phase == 3)
		{
			//Subphase 1 of phase 3 is a blue orb
			if(other.gameObject.CompareTag ("Green Orb"))
			{
				if(subPhase == 1)
				{
					PlayerDragRigidbody.click = false; //Disable the drag rigidbody click to prevent error
					orbs3[1].gameObject.SetActive (false); //Set the blue orb to not active
					renderer.material.SetColor ("_RefrColor", Color.blue); //Liquid is now green
					liquidLight.light.color = Color.blue; //Light colour
					Instantiate (greenEffect, transform.position, transform.rotation); //Create a particle effect
					subPhase++;
				}

				if(subPhase != 1)
				{
					PlayerDragRigidbody.click = false; //Disable the drag rigidbody click to prevent error
					orbs3[1].localPosition = new Vector3(0,0,0); //Spawn object back to start
					orbs3[1].rigidbody.velocity = orbs3[1].rigidbody.velocity * 0f; //Make sure orb doesn't move
					Instantiate (wrongSound, transform.position, transform.rotation); //Create the wrong sound
				}
			}

			//Subphase 2 is a green orb
			if(other.gameObject.CompareTag ("Blue Orb"))
			{
				if(subPhase == 2)
				{
					PlayerDragRigidbody.click = false; //Disable the drag rigidbody click to prevent error
					orbs3[0].gameObject.SetActive (false); //Set the green orb to not active
					renderer.material.SetColor ("_RefrColor", Color.red); //Liquid is now red
					liquidLight.light.color = Color.red; //Light colour
					Instantiate (blueEffect, transform.position, transform.rotation); //Create a particle effect
					subPhase++;
				}

				if(subPhase != 2)
				{
					PlayerDragRigidbody.click = false; //Disable the drag rigidbody click to prevent error
					orbs3[0].localPosition = new Vector3(0,0,0); //Spawn object back to start
					orbs3[0].rigidbody.velocity = orbs3[0].rigidbody.velocity * 0f; //Make sure orb doesn't move
					Instantiate (wrongSound, transform.position, transform.rotation); //Create the wrong sound
				}
			}

			//Subphase 3 is a red orb
			if(other.gameObject.CompareTag ("Red Orb"))
			{
				if(subPhase == 3)
				{
					PlayerDragRigidbody.click = false; //Disable the drag rigidbody click to prevent error
					orbs3[2].gameObject.SetActive (false); //Set the green orb to not active
					renderer.material.SetColor ("_RefrColor", Color.white); //Liquid is now red
					liquidLight.light.color = Color.white; //Light colour
					Instantiate (redEffect, transform.position, transform.rotation); //Create a particle effect
					key1.gameObject.SetActive (true); //Reveal the reward
				}

				if(subPhase != 3)
				{
					PlayerDragRigidbody.click = false; //Disable the drag rigidbody click to prevent error
					orbs3[2].localPosition = new Vector3(0,0,0); //Spawn object back to start
					orbs3[2].rigidbody.velocity = orbs3[2].rigidbody.velocity * 0f; //Make sure orb doesn't move
					Instantiate (wrongSound, transform.position, transform.rotation); //Create the wrong sound
				}
			}
		}
	}
}
