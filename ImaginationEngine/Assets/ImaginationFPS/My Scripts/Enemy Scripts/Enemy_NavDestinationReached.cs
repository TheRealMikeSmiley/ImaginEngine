using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * 
 * Ensures that once the destination is reached by the enemy, it stops playing the walk animation
 * 
 */ 

namespace ImaginationEngine {
	public class Enemy_NavDestinationReached : MonoBehaviour {

		private Enemy_Master enemyMaster;
		private NavMeshAgent myNavMeshAgent;
		private float checkRate;
		private float nextCheck;

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
				CheckIfDestinationReached ();
			}
		}

		void SetInitialReferences() {
			enemyMaster = GetComponent<Enemy_Master> ();
			if (GetComponent<NavMeshAgent> () != null) {
				myNavMeshAgent = GetComponent<NavMeshAgent> ();
			}

			checkRate = Random.Range (0.3f, 0.4f);
		}

		void CheckIfDestinationReached() {
			if (enemyMaster.isOnRoute) { //Only performs check if enemy is moving somewhere
				if (myNavMeshAgent.remainingDistance < myNavMeshAgent.stoppingDistance) {
					enemyMaster.isOnRoute = false;
					enemyMaster.CallEventEnemyReachedNavTarget ();
				}
			}
		}

		void DisableThis() {
			this.enabled = false;
		}
	}
}


