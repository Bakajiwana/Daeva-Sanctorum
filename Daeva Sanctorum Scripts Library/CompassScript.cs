using UnityEngine;
using System.Collections;

public class CompassScript : MonoBehaviour 
{
	//Script Objective: The compass should guide the player towards its respective door
	//Using Vector3.RotateTowards to help: http://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html

	//Objective location variable
	public Transform objective;
	public Transform player;
	public float speed;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 targetDir = objective.position - player.position;
		float step = speed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
		transform.rotation = Quaternion.LookRotation (newDir);
	}
}
