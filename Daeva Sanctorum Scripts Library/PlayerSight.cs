using UnityEngine;
using System.Collections;

//Script Objective: Outputs player sight

public class PlayerSight : MonoBehaviour 
{
	//Main Camera Variable
	public Transform cam;

	//Player's sight
	public static bool enemyInSight;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		RaycastHit sight;
		if(Physics.Raycast (cam.position, cam.forward, out sight))
		{
			//If Player sees Booboo
			if(sight.transform.CompareTag ("Enemy"))
			{
				enemyInSight = true;
			}
			else
			{
				enemyInSight = false;
			}
		}
	}
}
