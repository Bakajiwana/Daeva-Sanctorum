using UnityEngine;
using System.Collections;

//Script Objective: Booboo's final boss fight script.

public class BossFightScript : MonoBehaviour 
{	
	private float startDelay;
	public float startMaxDelay = 3f;

	[System.NonSerialized]	// Don't want to see in inspector
	public bool isIdle;
	
	//Attack variables
	[System.NonSerialized]	// Don't want to see in inspector
	public bool isAttacking = false;
	public Transform player; //For player location
	public float maxAttackDistance = 2f; 
	public float maxMeleeDistance = 3f;
	public float maxShootDistance = 10f;
	private float attackDelayTimer;
	public float attackMaxDelayTimer = 1f;
	public float damping = 10f;
	
	public float damage = 50f;
	
	//Spell Casting Variables
	public float spellCastMaxDelay = 5f;
	private float spellCastDelay;
	[System.NonSerialized]	// Don't want to see in inspector
	public bool castingSpell;
	public float spellCastSpeed = 50f;
	public Transform spellCastNode;
	public Transform spellCastingEffect;
	public Transform spellBall;
	public Transform homingSpellBall;
	public Transform homingCarpetBomb;
	public Transform homingSuperCarpetBomb;
	
	private float animCastDelay;
	public float animCastMaxDelay = 1f;

	//BOSS VARIABLES
	[System.NonSerialized]	// Don't want to see in inspector
	public int attackPhase = 1;
	public static bool shieldStruck; //When player activates a pillar, the pillar damages shield and forces booboo to take action
	public Transform[] randomTeleportPoints;
	public Transform magicArc;
	public Transform meleeSwipe;
	public Transform crystals;
	public Transform mainCrystal;
	public Transform inCrystal;
	public Transform shield;

	public Transform teleportParticles;

	//Music Variables
	public Transform bossFightSong;
	public Transform postFightSong;

	public Transform[] beams;
	
	//Mecanim variables
	private Animator anim;				//A variable reference to the animator of the character
	
	void Awake ()
	{
		//Initialise player animator
		anim = GetComponent<Animator>();
	}
	
	// Use this for initialization
	void Start () 
	{				
		attackDelayTimer = attackMaxDelayTimer;
		isAttacking = false;
	
		spellCastDelay = spellCastMaxDelay; 
		castingSpell = false;
		
		animCastDelay = animCastMaxDelay;
		
		//Summon Animation
		anim.SetTrigger("Summon");

		transform.localPosition = Vector3.zero;

		if(PlayerPrefs.GetInt ("Checkpoint") == 8)
		{
			attackPhase = 1;
		}

		isIdle = true;

		startDelay = startMaxDelay;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//If start delay is greater than 0, do a countdown. There is a pause in Booboo at the start
		if(startDelay > 0f)
		{
			startDelay -= Time.deltaTime;
		}


		if(!PlayerHealth.gameOver && !PauseScript.isPaused && startDelay <= 0f)
		{
			//Create a case switch for booboo's behaviour 
			switch (attackPhase) 
			{
			case 1: //-----------------Attack Phase 1:  Shoot Magic Balls-----------------------
				magicArc.gameObject.SetActive (false);
				crystals.gameObject.SetActive (true);
				transform.localPosition = Vector3.zero;		//STAY IN MIDDLE
				Cast ();
				break;
			case 2: //-----------------Attack Phase 2: Melee Phase-------------------------------
				//Move into melee phase, slow speed initialised in BossDestination class
				MeleePhase ();
				magicArc.gameObject.SetActive (false);
				crystals.gameObject.SetActive (false);
				break; 
			case 3: //-----------------Attack Phase 3: Homing Magic Ball Phase-------------------
				Cast ();
				magicArc.gameObject.SetActive (false);
				crystals.gameObject.SetActive (true);
				transform.localPosition = Vector3.zero;		//STAY IN MIDDLE
				break;
			case 4: //-----------------Attack Phase 4: Melee Phase-------------------------------
				//Move into Melee Phase, med speed initialised in BossDestination class
				MeleePhase ();
				magicArc.gameObject.SetActive (false);
				crystals.gameObject.SetActive (false);
				break;
			case 5: //-----------------Attack Phase 5: Arc Magic Fire----------------------------
				magicArc.gameObject.SetActive (true);
				crystals.gameObject.SetActive (true);
				transform.localPosition = Vector3.zero;		//STAY IN MIDDLE
				Cast ();
				break;
			case 6: //-----------------Attack Phase 6: Melee Phase-------------------------------
				//Time to fuck shit up with fast moving booboo, fast speed initialised in BossDestination class
				magicArc.gameObject.SetActive (true);
				crystals.gameObject.SetActive (false);
				MeleePhase ();
				break;
			case 7: //-----------------Attack Phase 7: Curse Phase-------------------------------
				magicArc.gameObject.SetActive (true);
				crystals.gameObject.SetActive (true);
				transform.localPosition = Vector3.zero;		//STAY IN MIDDLE
				Cast ();
				break; 
			case 8: //-----------------Attack Phase 8: Final Melee Phase-------------------------
				magicArc.gameObject.SetActive (true);
				crystals.gameObject.SetActive (false);
				mainCrystal.gameObject.SetActive (true);
				shield.gameObject.SetActive (false); //Shield destroyed
				MeleePhase ();
				break;
			}

			//If shield struck by pillar blast from player
			if(shieldStruck)
			{
				attackPhase++; 			//Move into the next phase
				Teleport ();			//Teleport to a random location
				shieldStruck = false;	//Turn off boolean
			}


			//Fix animation gaps
			if(animCastDelay > 0f)
			{
				isIdle = true; 
				animCastDelay -= Time.deltaTime;
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


	//Function is called when casting a spell
	void Cast()
	{
		if(!castingSpell && animCastDelay <= 0f)
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
	}

	//Function is called to activate melee
	void MeleePhase()
	{
		//But when in range attack player
		if (player)
		{
			//Turn off idle, so Booboo chases player
			isIdle = false;

			float dist = Vector3.Distance (player.position, transform.position);
			if(dist < maxAttackDistance)
			{
				isAttacking = true;
			}
		}
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

			//During the final phase enhance Booboo's melee
			if(attackPhase == 8)
			{
				foreach (Transform child in spellCastNode)
				{
					Transform castSpell = Instantiate(meleeSwipe, child.position, child.rotation) as Transform;
					castSpell.rigidbody.AddForce (transform.forward * spellCastSpeed);
				}
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
				if(attackPhase == 1)
				{
					Transform castSpell = Instantiate(spellBall, child.position, child.rotation) as Transform;
					castSpell.rigidbody.AddForce (transform.forward * spellCastSpeed);
				}
				else if(attackPhase == 3)
				{
					Transform castSpell = Instantiate(homingSpellBall, child.position, child.rotation) as Transform;
					castSpell.rigidbody.AddForce (transform.forward * spellCastSpeed);
				}
				else if(attackPhase == 5)
				{
					Instantiate(homingCarpetBomb, child.position, child.rotation);
				}
				else if(attackPhase == 7)
				{
					Instantiate(homingSuperCarpetBomb, child.position, child.rotation);
				}
			}			
			
			spellCastDelay = spellCastMaxDelay;
			animCastDelay = animCastMaxDelay;
			castingSpell = false;
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
		
		if(other.gameObject.CompareTag ("Explosive Potion") && attackPhase != 8)
		{
			attackPhase++;
			transform.localPosition = Vector3.zero;
		}

		if(other.gameObject.CompareTag ("Explosive Potion") && attackPhase == 8)
		{
			inCrystal.gameObject.SetActive (true);
			bossFightSong.gameObject.SetActive (false);  //Turn off boss song
			for(int i = 0; i < beams.Length; i++)
			{
				beams[i].gameObject.SetActive (false);	//Turn off all blue beams
			}
			postFightSong.gameObject.SetActive (true);	 //Turn on post battle song
			transform.gameObject.SetActive(false);
		}
	}

	//Function is called when booboo needs to teleport
	public void Teleport()
	{
		Instantiate (teleportParticles, transform.position, transform.rotation); 	//Emit Teleport particles
		int randomPoint = Random.Range (0, randomTeleportPoints.Length); //Create random number

		//Teleport to random position
		transform.position = randomTeleportPoints [randomPoint].position;
	}
}
