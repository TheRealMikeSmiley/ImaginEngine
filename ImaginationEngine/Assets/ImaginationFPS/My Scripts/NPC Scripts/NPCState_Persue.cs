using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {
	public class NPCState_Persue : NPCState_Interface { // derive from NPCState_Interface script

		private readonly NPC_StatePattern npc;
		private float capturedDistance;

		public NPCState_Persue(NPC_StatePattern npcStatePattern) {
			npc = npcStatePattern;
		}

		public void UpdateState() {
			Look ();
			Persue ();
		}

		public void ToPatrolState() {

			KeepWalking ();
			npc.currentState = npc.patrolState;

		}

		public void ToAlertState(){

			KeepWalking ();
			npc.currentState = npc.alertState;

		}

		public void ToPersueState(){}

		public void ToMeleeAttackState(){

			npc.currentState = npc.meleeAttackState;

		}

		public void ToRangeAttackState(){

			npc.currentState = npc.rangeAttackState;

		}

		void Look() {
			//Check if there is a persue target
			if (npc.persueTarget == null) {
				ToPatrolState ();
				return; //if persue target is not found, there is no need to move further in this method
			}

			Collider[] colliders = Physics.OverlapSphere (npc.transform.position, npc.sightRange, npc.myEnemyLayers);

			if (colliders.Length == 0) {
				npc.persueTarget = null;
				ToPatrolState ();
				return;
			}

			capturedDistance = npc.sightRange * 2;

			//Persue closest enemy target
			foreach (Collider col in colliders) {
				float distanceToTarg = Vector3.Distance (npc.transform.position, col.transform.position);

				if (distanceToTarg < capturedDistance) { //go through array of targets to find closest target
					capturedDistance = distanceToTarg;
					npc.persueTarget = col.transform.root;
				}
			}
		}

		void Persue() {
			npc.meshRendererFlag.material.color = Color.red;

			if (npc.myNavMeshAgent.enabled && npc.persueTarget != null) {
				npc.myNavMeshAgent.SetDestination (npc.persueTarget.position);
				npc.locationOfInterest = npc.persueTarget.position; //used by alert state in case NPC goes back to alert state
				KeepWalking ();

				float distanceToTarget = Vector3.Distance (npc.transform.position, npc.persueTarget.position);

				if (distanceToTarget <= npc.rangeAttackRange && distanceToTarget > npc.meleeAttackRange) { //Close enough to shoot, too far to melee attack
					if (npc.hasRangeAttack) {
						ToRangeAttackState ();
					}
				} else if (distanceToTarget <= npc.meleeAttackRange) { //Close enough for melee attack
					if (npc.hasMeleeAttack) {
						ToMeleeAttackState ();
					} else if (npc.hasRangeAttack) {
						ToRangeAttackState ();
					}
				}
			} else {
				ToAlertState ();
			}
		}

		void KeepWalking() {
			npc.myNavMeshAgent.isStopped = false;
			npc.npcMaster.CallEventNpcWalkAnim ();
		}
	}
}


