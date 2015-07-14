using UnityEngine;
using System.Collections;

//Script Objective: The key puzzle zooms into the puzzle sequence
// The player is meant to rotate the key to the right position to unlock the door.

//Reference: http://pastebin.com/TYXsRL5t

public class KeyPuzzle : MonoBehaviour 
{	
	//Speed of the rotation
	public float mouseSensitivityX = 100f;
	public float mouseSensitivityY = 100f;

	public float joySensitivityX = 150f;
	public float joySensitivityY = 150f;
	public float joySensitivityTilt = 50f;

	public float maxJoyInfluenceX = 0.20f;
	public float maxJoyInfluenceY = 0.20f;

	//Object Puzzle Variables
	public float tiltRange = 10f;
	private bool lockCollider = false;
	public DoorInteract door;

	public MouseLook mouseLook1;
	public MouseLook mouseLook2;
	public CharacterMotorC move;
	public Transform keyPuzzle;
	public Transform keyLock;

	public Transform character;

	//Instruction variables
	public Transform controllerInstruction;
	public Transform mouseInstruction;

	//hide key variables
	public float hideLocation = 5f;


	void Start ()
	{

	}

	void Update ()
	{
		//Calculate the joystick speed in a positive 0-1 scale
		float Xon = Mathf.Abs (Input.GetAxis ("Joy X"));
		float Yon = Mathf.Abs (Input.GetAxis ("Joy Y"));

		float mouseXon = Mathf.Abs (Input.GetAxis ("Mouse X"));
		float mouseYon = Mathf.Abs (Input.GetAxis ("Mouse Y"));

		//This part of the script rotates the object using look controls
		if(Xon > maxJoyInfluenceX || Yon > maxJoyInfluenceY)
		{
			float joyRotationX = Input.GetAxis ("Joy X") * joySensitivityX;
			float joyRotationY = -Input.GetAxis ("Joy Y") * joySensitivityY; 

			transform.Rotate (character.up, -Mathf.Deg2Rad * joyRotationX, Space.World);
			transform.Rotate (character.right, Mathf.Deg2Rad * joyRotationY, Space.World);

			//Reveal Instructions
			controllerInstruction.gameObject.SetActive (true);
			mouseInstruction.gameObject.SetActive (false);
		}
		else
		{
			float mouseRotationX = Input.GetAxis ("Mouse X") * mouseSensitivityX;
			float mouseRotationY = Input.GetAxis ("Mouse Y") * mouseSensitivityY;

			//If the player holds the right mouse button then he can tilt the object
			// else, he can just rotate the objects x and y axis by using the look controls
			if(Input.GetButton ("Fire2"))
			{
				transform.Rotate (character.forward, -Mathf.Deg2Rad * mouseRotationX, Space.World);
			}
			else
			{
				transform.Rotate (character.up, -Mathf.Deg2Rad * mouseRotationX, Space.World);
				transform.Rotate (character.right, Mathf.Deg2Rad * mouseRotationY, Space.World);
			}
		}

		if(mouseXon > 0f || mouseYon > 0f)
		{
			//Reveal Instructions
			controllerInstruction.gameObject.SetActive (false);
			mouseInstruction.gameObject.SetActive (true);
		}


		//The problem is when the player gets the object to face the right position
		//The object has a high chance of being in the wrong angle.
		//This part of the script will allow the player to tilt the object in a fixed axis for the joystick
		//The mouse tilt is located in the else statement above
		if(Input.GetButton ("Left Button")) //If left button is pressed
		{
			transform.Rotate (-character.forward, -Mathf.Deg2Rad * joySensitivityTilt, Space.World); //tilt left
		}

		if(Input.GetButton ("Right Button")) //iff right button is pressed
		{
			transform.Rotate (character.forward, -Mathf.Deg2Rad * joySensitivityTilt, Space.World); //tilt right
		}


		//Handle the unlock mechanism when the object is placed in the correct position
		float zAngle = transform.localEulerAngles.y;

		//When the z rotation is 360 degrees instead of 0.
		float posTiltRange = 360f - tiltRange;

		//When key hits the right position as the lock then 
		if(zAngle > -tiltRange && zAngle < tiltRange && lockCollider || zAngle > posTiltRange && zAngle < 360f && lockCollider)
		{
			//Hide Instructions
			controllerInstruction.gameObject.SetActive (false);
			mouseInstruction.gameObject.SetActive (false);
			door.UnlockDoor ();
		}

		//If the player presses escape or back
		if(Input.GetKeyDown (KeyCode.Escape) || Input.GetButtonDown ("Back") || Input.GetButtonDown ("Pause"))
		{
			//Hide Instructions
			controllerInstruction.gameObject.SetActive (false);
			mouseInstruction.gameObject.SetActive (false);
			GoBack ();
		}

		if (Input.GetButton ("Jump") || Input.GetKey (KeyCode.Mouse0)) 
		{
			float smoothX = hideLocation;

			transform.localPosition = new Vector3(smoothX, 0f, 0f);
		}
		else if (!Input.GetButton ("Jump") || !Input.GetKey (KeyCode.Mouse0))
		{
			transform.localPosition = Vector3.zero;
		}
	}


	//Function when the player goes out of the puzzle
	void GoBack()
	{
		//enable all movement scripts and enable the keypuzzle
		mouseLook1.enabled = true;
		mouseLook2.enabled = true;
		PlayerInteract.busy = false;
		move.enabled = true;
		keyPuzzle.gameObject.SetActive (false);
		keyLock.gameObject.SetActive (false);
	}

	//Function when the key fits the lock triggers
	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.CompareTag("Lock"))
		{
			lockCollider = true;
		}
	}


	void OnTriggerExit (Collider other)
	{
		if(other.gameObject.CompareTag("Lock"))
		{
			lockCollider = false;
		}
	}
}
