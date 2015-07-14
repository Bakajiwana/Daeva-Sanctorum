using UnityEngine;
using System.Collections;

//Script Objective: When the door is unlocked, this script is to add effects to the door unlocking

public class KeyUnlock : MonoBehaviour 
{
	public Transform target;
	public float speed;

	public Transform keyAnimateLock;

	// Update is called once per frame
	void Update () 
	{	
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, target.position, step);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag ("Lock"))
		{
			target.gameObject.SetActive (false);
			keyAnimateLock.animation.Play ("unlock");
		}
	}
}
