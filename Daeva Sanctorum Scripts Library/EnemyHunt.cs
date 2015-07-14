using UnityEngine;
using System.Collections;

//Script Objective: Booboo's hunting script. This script allows booboo to haunt the hallways and hunt down the player within the lobby and
//hallways.

/*
 * There are a number of behaviours Booboo will go through when summoned.
 * Each time he is summoned the EnemySummon Trigger activates the method that creates a random behaviour of Booboo
 * The random behaviour will be stored in case switches. 
 * 
 * Things he will do:
 * 1. There will be a trigger in Booboo that is tagged as enemy, when player spots booboo and then looks away he will teleport to a new location
 * 2. Booboo will be waiting behind a door and idle. When player looks at booboo, he will chase the player
 * 3. Booboo will spawn in the main hall in an idle state. When the player looks at booboo and then looks away booboo will disappear. But if player
 *    is too close to the booboo, he will chase him. 
 * 4. Chase Player, simple.
 * 5. Chase player, but when in range shoot at player. When player is hit once, then booboo will no longer have to shoot again and just chase player.
*/

//REFERENCE: TYSON FOSTER'S FPS SCRIPT FROM ASSIGNMENT 1 - the nearest spawn bit

public class EnemyHunt : MonoBehaviour 
{
	//Dynamic Behaviour Variables
	private int randomBehaviour;

	private bool isSpotted; 
	private bool teleport; 
	private bool isTeleported;
	public Transform teleportParticles;

	[System.NonSerialized]	// Don't want to see in inspector
	public bool isIdle;

	//Attack variables
	[System.NonSerialized]	// Don't want to see in inspector
	public bool isAttacking = false;
	public Transform player; //For player location
	public float maxAttackDistance = 2f; 
	public float maxMainLobbyDistance = 14f;
	public float maxMeleeDistance = 3f;
	public float maxShootDistance = 10f;
	private bool spellHit;
	private float attackDelayTimer;
	public float attackMaxDelayTimer = 1f;
	public float damping = 10f;

	public float damage = 50f;

	//Behaviour 6 melee time
	private float meleeTime; 
	public float maxMeleeTime = 5f;

	//Spell Casting Variables
	public float spellCastMaxDelay = 5f;
	private float spellCastDelay;
	[System.NonSerialized]	// Don't want to see in inspector
	public bool castingSpell;
	public float spellCastSpeed = 50f;
	public Transform spellCastNode;
	public Transform spellCastingEffect;
	public Transform spellBall;

	private float animCastDelay;
	public float animCastMaxDelay = 1f;

	//Hide Variables
	public float hideMaxTimer = 30f;
	private float hideTimer = 0f;

	public bool isHunting = true;

	//Mecanim variables
	private Animator anim;				//A variable reference to the animator of the character

	//Alert Player Variables
	public float alertMaxDistance = 200f;

	void Awake ()
	{
		//Initialise player animator
		anim = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () 
	{
		//While spawned re initialise behaviour variables
		isSpotted = false;
		teleport = false;
		isTeleported = false;
		
		if(randomBehaviour == 2 || randomBehaviour == 3)
		{
			isIdle = true;
		}
		else
		{
			isIdle = false;
		}

		attackDelayTimer = attackMaxDelayTimer;
		isAttacking = false;

		spellHit = false;
		spellCastDelay = spellCastMaxDelay; 
		castingSpell = false;

		//While being summoned, might just start the timer to when booboo must hide again
		hideTimer = hideMaxTimer;

		animCastDelay = animCastMaxDelay;

		meleeTime = maxMeleeTime;

		//Summon Animation
		anim.SetTrigger("Summon");

		//Emit Audio
		audio.Play ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!PlayerHealth.gameOver && !PauseScript.isPaused)
		{
			//Create a case switch for booboo's behaviour 
			switch (randomBehaviour) 
			{
			case 1: //-----------------Behaviour 1: Teleport when seen ----------------------
				//If player looks at enemy then mark isSpotted
				if(PlayerSight.enemyInSight == true)
				{
					isSpotted = true;
				}

				//If Player has looked away, activate teleport boolean but turn on isTeleported to prevent extra calls
				if(PlayerSight.enemyInSight == false && isSpotted == true && isTeleported == false)
				{
					teleport = true;
				}

				//Teleport booboo
				if(teleport && !isTeleported)
				{
					isTeleported = true;
					teleport = false;

					//FROM TYSON FOSTER'S SCRIPT: Spawn nearest teleport
					GameObject trans = GameObject.FindGameObjectWithTag ("Player");
					
					// find closest teleport, respawn there
					float backOffset = trans.transform.localPosition.z + 20f;
					Vector3 backSpawn = new Vector3 (trans.transform.localPosition.x, 
					                                 trans.transform.localPosition.y, 
					                                 backOffset);
					GameObject spawn = FindClosestTeleport(backSpawn);
					
					// do the spawn
					if (spawn) 
					{
						Vector3 newPos = spawn.transform.position;
						Instantiate (teleportParticles, transform.position, transform.rotation); 	//Emit Teleport particles
						transform.position = newPos;
					}
				}

				//when in range attack player
				if (player && isTeleported)
				{
					float dist = Vector3.Distance (player.position, transform.position);
					if(dist < maxAttackDistance)
					{
						isAttacking = true;
					}
				}
				break;

			case 2: //-----------------Behaviour 2: Wait behind door ------------------------
				//If player sees booboo chase after player
				if(PlayerSight.enemyInSight)
				{
					isIdle = false;
				}

				//when in range attack player
				if (player && isIdle == false)
				{
					float dist = Vector3.Distance (player.position, transform.position);
					if(dist < maxAttackDistance)
					{
						isAttacking = true;
					}
				}
				break;

			case 3: //-----------------Behaviour 3: Main Hall Haunt -------------------------
				//Attack player if within a certain range
				if (player)
				{
					float dist = Vector3.Distance (player.position, transform.position);
					if(dist < maxMainLobbyDistance)
					{
						isIdle = false;
						randomBehaviour = 4;
					}
				}
				break;

			case 4: //-----------------Behaviour 4: Chase Player ----------------------------
				//Automatically chases player when idle is false

				//But when in range attack player
				if (player)
				{
					float dist = Vector3.Distance (player.position, transform.position);
					if(dist < maxAttackDistance)
					{
						isAttacking = true;
					}
				}
				break;

			case 5://------------------Behaviour 5: Shoot Once then attack Range ------------
				//Chase player but also shoots at player when in shoot range

				//But when in range attack player
				if (player)
				{
					float dist = Vector3.Distance (player.position, transform.position);
					if(dist < maxAttackDistance && !spellHit && !castingSpell)
					{
						isIdle = false;
						isAttacking = true;
					}
					else if(dist < maxShootDistance && !spellHit && !castingSpell)
					{
						isIdle = true; 
						castingSpell = true;
						//Instantiate a particle charge
						foreach (Transform child in spellCastNode)
						{
							Transform castEffect = Instantiate (spellCastingEffect, child.position, child.rotation) as Transform;
							castEffect.transform.parent = transform; 
						}
						anim.SetTrigger ("Casting");	//Play spell casting animations
					}
					else if (dist > maxShootDistance && !spellHit && !castingSpell)
					{
						isIdle = false;
					}

					if(dist < maxAttackDistance && spellHit)
					{
						isAttacking = true;
					}
				}
				break;

			case 6://------------------Behaviour 6: Keep shooting at the player -------------
				if (player)
				{
					float dist = Vector3.Distance (player.position, transform.position);
					if(meleeTime > 0f)
					{
						if(dist < maxAttackDistance)
						{
							isIdle = false;
							isAttacking = true;
						}
						meleeTime -= Time.deltaTime;
					}
					else
					{
						isIdle = true;
						if(dist < maxShootDistance && !castingSpell && animCastDelay <= 0f)
						{
							castingSpell = true;
							//Instantiate a particle charge
							foreach (Transform child in spellCastNode)
							{
								Transform castEffect = Instantiate (spellCastingEffect, child.position, child.rotation) as Transform;
								castEffect.transform.parent = transform; 
							}
							anim.SetTrigger ("Casting");	//Play spell casting animations
						}
					}

					if(animCastDelay > 0f)
					{
						animCastDelay -= Time.deltaTime;
					}
				}
				break;
			}


			//There is a timer when booboo must hide again
			if(hideTimer > 0f && isHunting)
			{
				hideTimer -= Time.deltaTime;
			}
			if(hideTimer <= 0f)
			{
				Instantiate (teleportParticles, transform.position, transform.rotation); 	//Emit Teleport particles
				transform.gameObject.SetActive (false);
			}


			//If attacking
			if(isAttacking)
			{
				Attack ();
				anim.SetBool ("Melee", true); 	//Play melee animation
			}
			else
			{
				anim.SetBool ("Melee", false);	//Stop Playing melee animation
			}


			//If casting Spell
			if(castingSpell)
			{
				SpellCast ();
			}
		}
	}

	//This method is called when Booboo is summoned by a trigger
	public void RandomBehaviour(int _random)
	{
		randomBehaviour = _random;

		//Re initialise
		Start ();
	}

	//This procedure is used to find the closest booboo spawnpoint 
	GameObject FindClosestTeleport (Vector3 pos)
	{
		// Find all game objects with tag Respawn
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Enemy Teleport Point"); 
		
		// now iterate through them to find the closest one
		GameObject closestObject = null;
		float closestDistance = Mathf.Infinity; 
		foreach (GameObject go in gos)  
		{ 
			float distance = Vector3.Distance(go.transform.position, pos);
			if (distance < closestDistance) 
			{	 
				closestObject = go; 
				closestDistance = distance; 
			}
		}		
		return closestObject;
	}

	//This function is called to inflict damage onto the player when called
	void Attack()
	{
		//If attack delay timer is greater than 0 then look at player and play animation
		if(attackDelayTimer > 0f)
		{
			attackDelayTimer -= Time.deltaTime; //Countdown

			//Look at player
			Vector3 lookPos = player.position - transform.position;
			lookPos.y = 0;
			
			Quaternion rotation = Quaternion.LookRotation (lookPos);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * damping);
		}
		else
		{
			//Damage player if still in melee range
			float dist = Vector3.Distance (player.position, transform.position);
			if(dist < maxMeleeDistance)
			{
				GameObject.FindGameObjectWithTag ("Player").SendMessage ("ApplyPlayerDamage", damage);
				attackDelayTimer = attackMaxDelayTimer;
				isAttacking = false;
			}
			else
			{
				attackDelayTimer = attackMaxDelayTimer;
				isAttacking = false;
			}
		}
	}


	//This function is called when casting a spell at the player
	void SpellCast()
	{
		//Look at player
		Vector3 lookPos = player.position - transform.position;
		lookPos.y = 0;
		
		Quaternion rotation = Quaternion.LookRotation (lookPos);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * damping);

		//Spell Cast Delay Countdown
		spellCastDelay -= Time.deltaTime;

		//When Spall Cast Delay reaches 0 spell has been cast
		if(spellCastDelay <= 0f)
		{
			foreach (Transform child in spellCastNode)
			{
				Transform castSpell = Instantiate(spellBall, child.position, child.rotation) as Transform;
				castSpell.rigidbody.AddForce (transform.forward * spellCastSpeed);
			}			

			spellCastDelay = spellCastMaxDelay;
			animCastDelay = animCastMaxDelay;
			castingSpell = false;
			spellHit = true;
			isIdle = false;
		}
	}


	//This function is called when player moves into enemy
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.CompareTag ("Player"))
		{
			GameObject.FindGameObjectWithTag ("Player").SendMessage ("ApplyPlayerDamage", damage);
		}

		if(other.gameObject.CompareTag ("Explosive Potion"))
		{
			//If in the basement intro then stagger but if hunting the player in the hallways then disappear
			if(!isHunting)
			{
				anim.SetTrigger ("Stagger");
			}
			else
			{
				Instantiate (teleportParticles, transform.position, transform.rotation); 	//Emit Teleport particles
				transform.gameObject.SetActive (false);
			}
		}
	}
}
