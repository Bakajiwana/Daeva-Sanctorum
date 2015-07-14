using UnityEngine;
using System.Collections;

public class CompassKeyTransition : MonoBehaviour 
{
	/*SCRIPT OBJECTIVE:
  	  When player picks up key
      Transition takes effect
      Pointer background which this object is parented to will appear
      when this appears the dummy key will move towards the compass transition trigger
      when both dummy key and compass triggers collide the compass pointer will appear
     */ 

	//I will use a Vector3.Lerp: http://docs.unity3d.com/ScriptReference/Vector3.Lerp.html

	//Lerp Variables
	private Vector3 startMarker;
	public Transform endMarker;
	public float speed = 1.0f;
	private float startTime;
	private float journeyLength;
	public float smooth = 5.0f;

	//Pointer Variables
	public Transform pointer;
	public Transform self;

	// Use this for initialization
	void Start () 
	{
		startMarker = transform.position;
		startTime = Time.time;
		journeyLength = Vector3.Distance (startMarker, endMarker.position);
	}
	
	// Update is called once per frame
	void Update () 
	{
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp (startMarker, endMarker.position, fracJourney);
	}

	void OnTriggerEnter(Collider other)
	{
		pointer.gameObject.SetActive (true);
		self.gameObject.SetActive (false);
	}
}
