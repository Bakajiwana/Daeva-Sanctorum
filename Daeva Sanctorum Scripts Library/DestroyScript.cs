using UnityEngine;
using System.Collections;

// A reusable script to destroy things like particle effects with a timer

public class DestroyScript : MonoBehaviour 
{
	public float destroyTimer;

	// Use this for initialization
	void Start () 
	{
		Destroy (gameObject, destroyTimer);
	}
}
