using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * 
 * Allow enemy to wander (rather than stay stationary) if no player has been detected
 * 
 */ 

namespace ImaginationEngine {
	public class Enemy_NavWander : MonoBehaviour {

		private Enemy_Master enemyMaster;
		private NavMeshAgent myNavMeshAgent;
		private float checkRate;
		private float nextCheck;

		private Transform myTransform;
		private float wanderRange = 10;
		private NavMeshHit navHit;
		private Vector3 wanderTarget;

		void OnEnable() {
			SetInitialReferences ();
			enemyMaster.EventEnemyDie += DisableThis;
		}

		void OnDisable() {
			enemyMaster.EventEnemyDie -= DisableThis;

		}
	
		// Update is called once per frame
		void Update () {
			if (Time.time > nextCheck) {
				nextCheck = Time.time + checkRate;
				CheckIfIShouldWander ();
			}
		}

		void SetInitialReferences() {
			enemyMaster = GetComponent<Enemy_Master> ();
			if (GetComponent<NavMeshAgent> () != null) {
				myNavMeshAgent = GetComponent<NavMeshAgent> ();
			}

			checkRate = Random.Range (0.3f, 0.4f);
			myTransform = transform;
		}

		void CheckIfIShouldWander() {
			if (enemyMaster.myTarget == null && !enemyMaster.isOnRoute && !enemyMaster.isNavPaused) { // check if the enemy has a target, is on route or is paused
				if(RandomWanderTarget(myTransform.position, wanderRange, out wanderTarget)) { //Set parameters for new wander point
					myNavMeshAgent.SetDestination (wanderTarget);
					enemyMaster.isOnRoute = true;
					enemyMaster.CallEventEnemyWalking ();
				}
			}
		}

		//Get a random position within a sphere and choose a position on the navmesh(area enemy can walk on)
		bool RandomWanderTarget(Vector3 centre, float range, out Vector3 result) {
			Vector3 randomPoint = centre + Random.insideUnitSphere * wanderRange;
			if (NavMesh.SamplePosition (randomPoint, out navHit, 1.0f, NavMesh.AllAreas)) { //if this random point can be traversed, go there
				result = navHit.position;
				return true;
			} else { //Otherwise the enemy wont move until a new check is made
				result = centre;
				return false;
			}
		}

		void DisableThis() {
			this.enabled = false;
		}
	}
}


