using UnityEngine;
using System.Collections;

//Script Objective: Maintain the players health

public class PlayerHealth : MonoBehaviour 
{
	//Health Variables
	public float playerHealth;
	public float playerMaxHealth = 100f;
	public float playerHeal = 0.5f;

	//Effect Variables
	public MotionBlur motionBlur;

	//Game Over Variables
	public static bool gameOver;
	public Transform gameOverScreen;
	public Transform metaHealthScreen;
	public float oilReplenish = 100f;
	
	public Transform leftArm;
	public MouseLook mouseLook1;
	public MouseLook mouseLook2;
	public CharacterMotorC move;

	//Re use the pause function to stop enemy and player, but use a one shot variable prevent the pause function
	//being sent every frame
	private bool gameOverOneShot = false;


	// Use this for initialization
	void Start () 
	{
		//Initialise Variables 
		playerHealth = playerMaxHealth; 

		Retry();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Adjust player health values to prevent nagatives and/ or extra health being calculated
		if (playerHealth <= 0f)
		{
			playerHealth = 0f;
		}
		else
		{
			if(playerHealth  >= playerMaxHealth)
			{
				playerHealth = playerMaxHealth;

				motionBlur.enabled = false;	//Disable the motion blur effect
			}
			else
			{
				//Heal over time
				playerHealth += playerHeal * Time.deltaTime;

				motionBlur.enabled = true;	//Enable the motion blur effect
			}
		}




		//----------------------Display the Meta Health Screen and maintain transparency-----------
		Color healthScreen = metaHealthScreen.renderer.material.color;
		healthScreen.a = (playerMaxHealth - playerHealth) / playerMaxHealth; 
		metaHealthScreen.renderer.material.color = healthScreen;
		//-----------------------------------------------------------------------------------------


		//If Game over
		if(playerHealth == 0f && gameOverOneShot == false)
		{
			gameOver = true;
			gameOverOneShot = true;

			//Stop player from moving
			mouseLook1.enabled = false; //Disable Mouse look scripts
			mouseLook2.enabled = false;
			move.enabled = false;
			leftArm.gameObject.SetActive (false);
			//GameObject.FindGameObjectWithTag("Level Manager").SendMessage ("Pause");
		}

		if(gameOver)
		{
			gameOverScreen.gameObject.SetActive (true);
		}
		else
		{
			gameOverScreen.gameObject.SetActive (false);
		}
	}

	//This function is called when the player takes damage
	public void ApplyPlayerDamage(float _damage)
	{
		//Player loses health according to the amound of damage specified
		playerHealth -= _damage; 
	}

	//This function is called when the player restores health
	public void ApplyPlayerRestore(float _restore)
	{
		playerHealth += _restore; 
	}

	//This function allows the player to start again from last checkpoint
	public void Retry()
	{
		playerHealth = playerMaxHealth; 
		gameOver = false;
		gameOverOneShot = false;
		//Send PauseScript a message to unpause
		GameObject.FindGameObjectWithTag("Level Manager").SendMessage ("Unpause");
		GameObject.FindGameObjectWithTag("Player Light").SendMessage ("addLight", oilReplenish, SendMessageOptions.DontRequireReceiver); //Replenish light
	}
}
