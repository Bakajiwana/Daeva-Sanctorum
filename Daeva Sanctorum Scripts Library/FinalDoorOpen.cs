using UnityEngine;
using System.Collections;

public class FinalDoorOpen : MonoBehaviour 
{
	private bool startDelay;
	public float delayTimer = 5f;

	public Transform doorMaterial;
	public Transform doorMaterial2;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(startDelay)
		{
			delayTimer -= Time.deltaTime;
		}

		if(delayTimer <= 0f)
		{
			animation.Play ("finalDoorOpen");
			this.enabled = false;
		}
	}

	public void FinalOpen()
	{
		startDelay = true;
		doorMaterial.renderer.material.SetColor ("_Color", Color.white);
		doorMaterial2.renderer.material.SetColor ("_Color", Color.white);
	}
}
