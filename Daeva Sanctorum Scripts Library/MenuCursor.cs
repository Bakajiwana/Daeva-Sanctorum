using UnityEngine;
using System.Collections;

//Script Objective: Create an artificial Cursor for the Menus

public class MenuCursor : MonoBehaviour 
{
	public Transform interactCursor;

	//Movement Variables
	public float cursorSpeed = 1000.0f; 
	Vector3 cursorVelocity; //Main variable that controls movement using physics

	//Clamped axis variables
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Calculate the joystick speed in a positive 0-1 scale
		float Xon = Mathf.Abs (Input.GetAxis ("Horizontal"));
		float Yon = Mathf.Abs (Input.GetAxis ("Vertical"));

		if(Xon > 0.20f || Yon > 0.20f)
		{
			//Create an input variable where the basic movement keys and joysticks are called
			Vector3 inputVector = new Vector2(Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));

			//Assign the speed and input variable into the movement variable
			cursorVelocity = inputVector * cursorSpeed;
		}
		else
		{
			//Create an input variable where the basic movement keys and joysticks are called
			Vector3 inputVector = new Vector2(Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"));

			//Assign the speed and input variable into the movement variable
			cursorVelocity = inputVector * cursorSpeed;
		}

		transform.localPosition = new Vector3(
			Mathf.Clamp (transform.localPosition.x, minX, maxX),
			Mathf.Clamp (transform.localPosition.y, minY, maxY),
			transform.localPosition.z);
	}

	// FixedUpdate is called once per physics update
	void FixedUpdate()
	{
		//Use Rigidbody to control an objects position through physics simulation
		rigidbody.AddForce (cursorVelocity);
	}

	//When cursor collides with something
	void OnTriggerStay()
	{
		interactCursor.gameObject.SetActive (true);
	}

	//When cursor is no longer colliding with anything
	void OnTriggerExit()
	{
		interactCursor.gameObject.SetActive (false);
	}
}
