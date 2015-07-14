using UnityEngine;
using System.Collections;

//TURN OFF GRAVITY IF COLLISIONED lol

public class CrystalScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other)
	{
		//If the magnitude of the collision is greater then specified velocity, then emit a collision sound
		if(other.relativeVelocity.magnitude > 1)
		{
			rigidbody.useGravity = false;
		}
	}
}
