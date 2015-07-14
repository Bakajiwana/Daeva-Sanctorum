using UnityEngine;
using System.Collections;

public class LanturnStabilize : MonoBehaviour 
{
	//keep the lanturn facing forward because gravity
	//So I created a empty gameobject in from of the lanturn so it will always look at it

	public Transform target;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.LookAt (target);
	}
}
