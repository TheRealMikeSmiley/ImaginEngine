using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * This script will start the NPC in a particular state, then handle each state case during runtime
 */ 

namespace ImaginationEngine {
	public class NPC_StatePattern : MonoBehaviour {

		//Decision Making
		private float checkRate = 0.1f;
		private float nextCheck;

		//All public variables can be changed in the inspector to change the behavior of each NPC
		public float sightRange = 40; //How far the NPC can see to detect other NPC's or Player
		public float detectBehindRange = 5; //How close an NPC/Player can get behind an enemy before detection
		public float meleeAttackRange = 4; //How far for a melee attack
		public float meleeAttackDamage = 10;
		public float rangeAttackRange = 35; //How close to another NPC before attacking
		public float rangeAttackDamage = 5;
		public float rangeAttackSpread = 0.5f; //Add randomness to shooting, lowers accuracy
		public float attackRate = 0.4f; //How often attacking is done per second
		public float nextAttack;
		public float fleeRange = 25; //How far a distance from persuing NPC/Player 
		public float offset = 0.4f; //
		public int requiredDetectionCount = 15; //When NPC sees a target, set required number of times before attacking/persuing target

		public bool hasRangeAttack; //Does NPC have a ranged attack
		public bool hasMeleeAttack; //Does NPC have a melee attack
		public bool isMeleeAttacking; //Check if melee attacking

		public Transform myFollowTarget; //If you want an NPC to follow (usually for friendly NPC's)
		[HideInInspector]
		public Transform persueTarget; //Confirm enemy, start persue state
		[HideInInspector]
		public Vector3 locationOfInterest; //See a target, investigate location
		[HideInInspector]
		public Vector3 wanderTarget; //Wandering around area
		[HideInInspector]
		public Transform myAttacker; //If NPC is attacked, others will be alerted and check around that area

		//Used for Sight
		public LayerMask sightLayers; //Set layers that NPC's can see and not see, for example, a friendly NPC should not block view
		public LayerMask myEnemyLayers;
		public LayerMask myFriendlyLayers;
		public string[] myEnemyTags;
		public string[] myFriendlyTags;

		//References
		public Transform[] waypoints; //Set points for NPC to patrol, if there is nothing to persue or follow
		public Transform head; //NPC will look at target
		public MeshRenderer meshRendererFlag; //Show cubes above NPC - for debugging, can be used to know what state NPC is in
		public GameObject rangeWeapon; //Set the NPC's range weapon, if there is one
		public NPC_Master npcMaster; //Set a reference to the NPC_Master script
		[HideInInspector]
		public NavMeshAgent myNavMeshAgent;

		//Used for state AI
		//Allows for state behaviors, but restricts to one state at a time
		public NPCState_Interface currentState;
		public NPCState_Interface capturedState;
		public NPCState_Patrol patrolState;
		public NPCState_Alert alertState;
		public NPCState_Persue persueState;
		public NPCState_MeleeAttack meleeAttackState;
		public NPCState_RangeAttack rangeAttackState;
		public NPCState_Flee fleeState;
		public NPCState_Struck struckState;
		public NPCState_InvestigateHarm investigateHarmState;
		public NPCState_Follow followState;

		void Awake() {
			SetupUpStateReferences ();
			SetInitialReferences ();
		}

		private void OnEnable() {
			npcMaster.EventNpcLowHealth += ActivateFleeState;
			npcMaster.EventNpcHealthRecovered += ActivatePatrolState;
			npcMaster.EventNpcDeductHealth += ActivateStruckState;
		}

		void Start() {
			SetInitialReferences ();
		}

		void OnDisable() {
			npcMaster.EventNpcLowHealth -= ActivateFleeState;
			npcMaster.EventNpcHealthRecovered -= ActivatePatrolState;
			npcMaster.EventNpcDeductHealth -= ActivateStruckState;
			StopAllCoroutines (); //Stop any possible coroutines if NPC dies
		}

		void Update() {
			CarryOutUpdateState ();
		}

		//As each script is set up, add references to those scripts here
		void SetupUpStateReferences() {
			patrolState = new NPCState_Patrol (this);
			alertState = new NPCState_Alert (this);
			persueState = new NPCState_Persue (this);
			fleeState = new NPCState_Flee (this);
			followState = new NPCState_Follow (this);
			meleeAttackState = new NPCState_MeleeAttack (this);
			rangeAttackState = new NPCState_RangeAttack (this);
			struckState = new NPCState_Struck (this);
			investigateHarmState = new NPCState_InvestigateHarm (this);
		}

		void SetInitialReferences() {
			myNavMeshAgent=GetComponent<NavMeshAgent>();
			ActivatePatrolState(); //Set initial state to patrol
		}
			
		void CarryOutUpdateState() {
			if (Time.time > nextCheck) {
				nextCheck = Time.time + checkRate;
				currentState.UpdateState ();
			}
		}

		//Method to make patrol state the default state
		void ActivatePatrolState() {
			currentState = patrolState;
		}
			
		void ActivateFleeState() {
			if (currentState == struckState) { //Sets NPC to flee state if struck state occurs twice
				capturedState = fleeState;	//This prevents an endless loop of the NPC in the struck state
				return;						//Endless loop can occur if enemy is struck, then struck again
			}

			currentState = fleeState;
		}

		void ActivateStruckState(int dummy) {
			StopAllCoroutines ();

			if (currentState != struckState) {
				capturedState = currentState;
			}

			if (rangeWeapon != null) { //Change or remove if you have proper gun holding animation
				rangeWeapon.SetActive(false); //Makes the gun disappear so the struck state looks less odd
			}

			if (myNavMeshAgent.enabled) {
				myNavMeshAgent.isStopped = true; //Stop NPC movement when struck
			}

			currentState = struckState;

			isMeleeAttacking = false;

			npcMaster.CallEventNpcStruckAnim ();

			StartCoroutine (RecoverFromStruckState()); //Reset coroutine if struck while struck to avoid NPC from ignoring struck state
		}

		IEnumerator RecoverFromStruckState() {
			yield return new WaitForSeconds (1.5f); //Can change this number depending on how long the NPC should remain struck

			npcMaster.CallEventNpcRecoveredAnim ();

			if (rangeWeapon != null) {
				rangeWeapon.SetActive (true); //Once recovered, reactivate gun
			}

			if (myNavMeshAgent.enabled) {
				myNavMeshAgent.isStopped = false; //reactivate NavMesh (so NPC can move again)
			}

			currentState = capturedState; //return NPC back to original state
		}

		public void OnEnemyAttack() { //Called by melee attack animation, remember this if adding a new melee animation and no damage occurs
			if (persueTarget != null) {
				if (Vector3.Distance (transform.position, persueTarget.position) <= meleeAttackRange) {
					Vector3 toOther = persueTarget.position - transform.position;
					if (Vector3.Dot (toOther, transform.forward) > 0.5f) {
						persueTarget.SendMessage ("CallEventPlayerHealthDeduction", meleeAttackDamage, SendMessageOptions.DontRequireReceiver);
						persueTarget.SendMessage ("ProcessDamage", meleeAttackDamage, SendMessageOptions.DontRequireReceiver);
						persueTarget.SendMessage ("SetMyAttacker", transform.root, SendMessageOptions.DontRequireReceiver);
					}
				}
			}

			isMeleeAttacking = false;
		}
			
		public void SetMyAttacker(Transform attacker) {
			myAttacker = attacker;
		}

		//Sets a new location of interest
		public void Distract(Vector3 distractionPos) {
			locationOfInterest = distractionPos;

			if (currentState == patrolState) {
				currentState = alertState; //result in NPC walking towards location of interest
			}
		}
	}
}


