using UnityEngine;
using System.Collections;

public class OutofBoundsScript : MonoBehaviour 
{
	//Fix the problem of items going out of bounds

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag ("Out of Bounds"))
		{
			transform.localPosition = Vector3.zero;
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.CompareTag ("Out of Bounds"))
		{
			transform.localPosition = Vector3.zero;
		}
	}
}
