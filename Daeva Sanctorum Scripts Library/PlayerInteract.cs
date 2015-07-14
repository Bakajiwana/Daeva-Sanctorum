using UnityEngine;
using System.Collections;

//Purpose of this Script is to act as the main raycasting interaction with most objects.
//Anything the player presses the interaction button on this script will send a message to that object
//Reference: http://answers.unity3d.com/questions/46594/initiate-raycast-from-center-of-camera.html
//Unity Doc on Raycasting

public class PlayerInteract : MonoBehaviour 
{
	//Main Camera Variable
	public Transform cam;

	//Raycast Variables
	public float interactionDistance = 5f;

	public static bool busy = false;

	//Key Variables
	public static bool hasKey1 = false;
	public static bool hasKey2 = false;
	public static bool hasKey3 = false;

	//Pointer GUI Variables
	public Transform pointerInteract;

	public Transform pointer;

	public LayerMask myLayerMask;

	//Instruction variables
	public float maxJoyInfluence = 0.1f; //Decide if the player is using a controller by movement input
	public static bool usingController;
	public float fadeRate = 0.6f;
	public Transform controllerInteract;
	public Transform mouseInteract;
	public Transform mouseDragPrompt;
	public Transform controllerDragPrompt;



	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		//Figure out whether he or she is using a controller 
		//Calculate the joystick speed in a positive 0-1 scale
		float Xon = Mathf.Abs (Input.GetAxis ("Joy X"));
		float Yon = Mathf.Abs (Input.GetAxis ("Joy Y"));

		//This part of the script decides whether the player is using a controller or 
		if(Xon > maxJoyInfluence || Yon > maxJoyInfluence)
		{
			usingController = true;
		}

		//using a mouse detected when the player moves with awsd.
		if(Input.GetKeyDown (KeyCode.A) ||Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.D) ||
		   Input.GetKeyDown (KeyCode.W))
		{
			usingController = false;
		}

		//Temporary color variables for instruction fade
		Color mouseFade = mouseInteract.renderer.material.color;
		Color controllerFade = controllerInteract.renderer.material.color;


		//Emit a raycast from the center of the camera
		RaycastHit interaction;
		if(Physics.Raycast (cam.position, cam.forward, out interaction, interactionDistance, myLayerMask))
		{
			//Indicate to the player that he or she can interact with an object
			pointerInteract.gameObject.SetActive (true);
			//Also display instruction prompt
			if(usingController && controllerFade.a < 1f && PlayerPrefs.GetInt("Checkpoint") == 0 && !PlayerDragRigidbody.click)
			{
				mouseFade.a = 0f;
				controllerFade.a += fadeRate * Time.deltaTime;
				controllerInteract.renderer.material.color = controllerFade;
			}
			else if (!usingController && mouseFade.a < 1f && PlayerPrefs.GetInt("Checkpoint") == 0 && !PlayerDragRigidbody.click)
			{
				controllerFade.a = 0f;
				mouseFade.a += fadeRate * Time.deltaTime;
				mouseInteract.renderer.material.color = mouseFade;
			}



			if(PlayerDragRigidbody.click)
			{
				//Indicate to the player that he or she can interact with an object
				pointerInteract.gameObject.SetActive (true);
			}


			if(interaction.transform.CompareTag ("Interact Object") && !PauseScript.isPaused)
			{
				if(Input.GetButtonDown ("Fire1"))
				{
					//Send message to interact
					interaction.transform.SendMessage ("Interact", SendMessageOptions.DontRequireReceiver);
				}
			}

			if(interaction.transform.CompareTag ("Collectibles") && !PauseScript.isPaused)
			{
				if(Input.GetButtonDown ("Fire1"))
				{
					//Send message to interact
					interaction.transform.SendMessage ("Interact", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		else
		{
			if(!PlayerDragRigidbody.click) //If not dragging an object
			{
				//No longer necessary to interact so turn off interact pointer
				pointerInteract.gameObject.SetActive (false);
			}

			//Instruction fade
			if(controllerFade.a > 0f)
			{
				controllerFade.a -= fadeRate * Time.deltaTime;
				controllerInteract.renderer.material.color = controllerFade;
			}
			if(mouseFade.a > 0f)
			{
				mouseFade.a -= fadeRate * Time.deltaTime;
				mouseInteract.renderer.material.color = mouseFade;
			}
		}


		//When Dragging prompt instructions
		if(usingController && PlayerPrefs.GetInt("Checkpoint") == 0 && PlayerDragRigidbody.click)
		{
			mouseInteract.gameObject.SetActive (false);
			controllerInteract.gameObject.SetActive (false);
			controllerDragPrompt.gameObject.SetActive (true);
			mouseDragPrompt.gameObject.SetActive (false);
		}

		if(!usingController && PlayerPrefs.GetInt("Checkpoint") == 0 && PlayerDragRigidbody.click)
		{
			mouseInteract.gameObject.SetActive (false);
			controllerInteract.gameObject.SetActive (false);
			controllerDragPrompt.gameObject.SetActive (false);
			mouseDragPrompt.gameObject.SetActive (true);
		}

		if(!PlayerDragRigidbody.click && PlayerPrefs.GetInt("Checkpoint") == 0)
		{
			mouseInteract.gameObject.SetActive (true);
			controllerInteract.gameObject.SetActive (true);
			controllerDragPrompt.gameObject.SetActive (false);
			mouseDragPrompt.gameObject.SetActive (false);
		}
		


		//---------------------If the player is busy then hide pointer--------------------
		if (busy)
		{
			pointer.gameObject.SetActive (false);
		}
		else
		{
			pointer.gameObject.SetActive (true);
		}
	}
}
