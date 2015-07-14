using UnityEngine;
using System.Collections;

//Move an object in a certain direction

public class TranslateScript : MonoBehaviour 
{
	public float x;
	public float y;
	public float z;
	public float speed;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(x * Time.deltaTime * speed, y * Time.deltaTime * speed,z * Time.deltaTime * speed, Space.World);
	}
}
