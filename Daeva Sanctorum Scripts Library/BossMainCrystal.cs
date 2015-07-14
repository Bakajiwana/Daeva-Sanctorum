using UnityEngine;
using System.Collections;

//Script Objective: When booboo is defeated he is trapped in a crystal.

public class BossMainCrystal : MonoBehaviour 
{
	public Transform booboo;
	public Transform finalLog;
	public Transform shatterParticles;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.CompareTag ("Explosive Potion") && !booboo.gameObject.activeSelf)
		{
			if(finalLog)
			{
				finalLog.gameObject.SetActive (true);
			}
			Instantiate (shatterParticles,transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}

	public void Interact()
	{
		if(!booboo.gameObject.activeSelf)
		{
			if(finalLog)
			{
				finalLog.gameObject.SetActive (true);
			}
			Instantiate (shatterParticles,transform.position, transform.rotation);
			PlayerDragRigidbody.click = false;
			Destroy (gameObject);
		}
	}
}
