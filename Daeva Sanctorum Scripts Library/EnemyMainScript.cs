using UnityEngine;
using System.Collections;

public class EnemyMainScript : MonoBehaviour 
{
	//Booboo's first appearance
	public bool intro;
	private float introTimer;
	public float introMaxTimer = 10f;
	public float disappearTime = 2f; 
	

	// Use this for initialization
	void Start () 
	{
		introTimer = introMaxTimer;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//If first appearance and not paused
		if(intro && !PauseScript.isPaused)
		{
			//Count down
			if(!PlayerSight.enemyInSight)
			{
				introTimer -= Time.deltaTime;
			}
			else
			{
				introTimer -= disappearTime * Time.deltaTime;
			}
		}

		//If Intro timer runs out, disappear
		if(introTimer < 0f)
		{
			transform.gameObject.SetActive (false);
		}
	}

	void OnCollisionEnter (Collision other)
	{
		if(other.gameObject.CompareTag ("Player") && intro)
		{
			transform.gameObject.SetActive (false);
		}
	}
}
