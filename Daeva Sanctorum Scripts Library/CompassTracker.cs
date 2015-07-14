using UnityEngine;
using System.Collections;

public class CompassTracker : MonoBehaviour 
{
	//Script Objective compass tracker will be located on the player where it will report its rotation to the GUI compass.

	//Door location variables
	public Transform keyDoor01;
	public Transform keyDoor02;
	public Transform keyDoor03;

	//Compass Pointers
	public Transform compassPointer01;
	public Transform compassPointer02;
	public Transform compassPointer03;

	//Full Compass Hierachy
	public Transform compassBackground01;
	public Transform compassBackground02;
	public Transform compassBackground03;

	private Quaternion rotation = Quaternion.identity;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 objective; //Objective

		//----------------------If player has the first compass-------------------------------
		if (PlayerInteract.hasKey1) 
		{
			//The compass should appear and animate
			compassBackground01.gameObject.SetActive (true);

			//The tracker should look at the door 
			objective = keyDoor01.position;
			transform.LookAt (objective);

			//The GUI compass should have the same rotation as the tracker
			rotation.eulerAngles = new Vector3(90f,transform.localEulerAngles.y,0); //Using euler angles to find rotation results
			compassPointer01.localEulerAngles = rotation.eulerAngles; //The compassPointer object should equal the trackers rotations
		}
		else
		{
			//If the player no longer has key1 then the compass should disappear
			compassBackground01.gameObject.SetActive (false);
			compassPointer01.gameObject.SetActive (false);
		}

		//----------------------If player has the second compass-------------------------------
		if (PlayerInteract.hasKey2) 
		{
			//The compass should appear and animate
			compassBackground02.gameObject.SetActive (true);
			
			//The tracker should look at the door 
			objective = keyDoor02.position;
			transform.LookAt (objective);
			
			//The GUI compass should have the same rotation as the tracker
			rotation.eulerAngles = new Vector3(90f,transform.localEulerAngles.y,0); //Using euler angles to find rotation results
			compassPointer02.localEulerAngles = rotation.eulerAngles; //The compassPointer object should equal the trackers rotations
		}
		else
		{
			//If the player no longer has key1 then the compass should disappear
			compassBackground02.gameObject.SetActive (false);
			compassPointer02.gameObject.SetActive (false);
		}

		//----------------------If player has the third compass-------------------------------
		if (PlayerInteract.hasKey3) 
		{
			//The compass should appear and animate
			compassBackground03.gameObject.SetActive (true);
			
			//The tracker should look at the door 
			objective = keyDoor03.position;
			transform.LookAt (objective);
			
			//The GUI compass should have the same rotation as the tracker
			rotation.eulerAngles = new Vector3(90f,transform.localEulerAngles.y,0); //Using euler angles to find rotation results
			compassPointer03.localEulerAngles = rotation.eulerAngles; //The compassPointer object should equal the trackers rotations
		}
		else
		{
			//If the player no longer has key1 then the compass should disappear
			compassBackground03.gameObject.SetActive (false);
			compassPointer03.gameObject.SetActive (false);
		}
	}
}
