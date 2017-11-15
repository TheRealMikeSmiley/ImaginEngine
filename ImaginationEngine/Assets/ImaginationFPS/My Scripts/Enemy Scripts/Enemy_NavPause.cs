 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * 
 * If enemy is struck, it will cause them to pause (react to being hit)
 * 
 */ 

namespace ImaginationEngine {
	public class Enemy_NavPause : MonoBehaviour {

		private Enemy_Master enemyMaster;
		private NavMeshAgent myNavMeshAgent;
		private float pauseTime = 1;

		void OnEnable() {
			SetInitialReferences ();
			enemyMaster.EventEnemyDie += DisableThis;
			enemyMaster.EventEnemyDeductHealth += PauseNavMeshAgent;
		}

		void OnDisable() {
			enemyMaster.EventEnemyDie -= DisableThis;
			enemyMaster.EventEnemyDeductHealth -= PauseNavMeshAgent;
		}

		void SetInitialReferences() {
			enemyMaster = GetComponent<Enemy_Master> ();
			if (GetComponent<NavMeshAgent> () != null) {
				myNavMeshAgent = GetComponent<NavMeshAgent> ();
			}
		}

		void PauseNavMeshAgent(int dummy) { //subscribes to enemy health deduction script so it will subscribe to a dummy (subscribe but will not actually use)
			if (myNavMeshAgent != null) {
				if (myNavMeshAgent.enabled) {
					myNavMeshAgent.ResetPath ();
					enemyMaster.isNavPaused = true;
					StartCoroutine (RestartNavMeshAgent ());
				}
			}
		}

		IEnumerator RestartNavMeshAgent() {
			yield return new WaitForSeconds (pauseTime);
			enemyMaster.isNavPaused = false;
		}

		void DisableThis() {
			StopAllCoroutines (); //Stops if enemy is dead, do not want coroutines running when they don't need to
		}
	}
}


