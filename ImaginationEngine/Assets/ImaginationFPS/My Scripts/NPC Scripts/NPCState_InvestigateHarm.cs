using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {
	public class NPCState_InvestigateHarm : NPCState_Interface { // derive from NPCState_Interface script

		private readonly NPC_StatePattern npc;
		private float offset = 0.3f;
		private RaycastHit hit;
		private Vector3 lookAtTarget;

		public NPCState_InvestigateHarm(NPC_StatePattern npcStatePattern) {
			npc = npcStatePattern;
		}

		public void UpdateState() {

			Look ();

		}

		public void ToPatrolState() {

			npc.currentState = npc.patrolState;

		}

		public void ToAlertState(){

			npc.currentState = npc.alertState;

		}

		public void ToPersueState(){

			npc.currentState = npc.persueState;

		}

		public void ToMeleeAttackState(){}

		public void ToRangeAttackState(){}

		void Look() {
			if (npc.persueTarget == null) {
				ToPatrolState ();
				return;
			}

			CheckIfTargetIsInDirectSight ();
		}

		void CheckIfTargetIsInDirectSight() {
			lookAtTarget = new Vector3 (npc.persueTarget.position.x, npc.persueTarget.position.y + offset, npc.persueTarget.position.z);

			//While investigating, send line cast, if somthing is hit, check if it matches, go to that location
			if (Physics.Linecast (npc.head.position, lookAtTarget, out hit, npc.sightLayers)) {
				if (hit.transform.root == npc.persueTarget) {
					npc.locationOfInterest = npc.persueTarget.position;
					GoToLocationOfInterest ();

					if (Vector3.Distance (npc.transform.position, lookAtTarget) <= npc.sightRange) { //if target is close
						ToPersueState ();
					}
				} else { //if target is not the Player or enemy NPC, go to alert state
					ToAlertState ();
				}
			} else {
				ToAlertState ();
			}
		}

		void GoToLocationOfInterest() {
			npc.meshRendererFlag.material.color = Color.black;

			if (npc.myNavMeshAgent.enabled && npc.locationOfInterest != Vector3.zero) {
				npc.myNavMeshAgent.SetDestination (npc.locationOfInterest);
				npc.myNavMeshAgent.isStopped = false;
				npc.npcMaster.CallEventNpcWalkAnim ();

				if (npc.myNavMeshAgent.remainingDistance <= npc.myNavMeshAgent.stoppingDistance) {
					npc.locationOfInterest = Vector3.zero;
					ToPatrolState ();
				}
			} else {
				ToPatrolState ();
			}
		}

	}
}


