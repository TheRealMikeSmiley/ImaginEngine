using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ImaginationEngine {
	public class NPCState_Flee : NPCState_Interface { // derive from NPCState_Interface script

		private Vector3 directionToEnemy;
		private NavMeshHit navHit;

		private readonly NPC_StatePattern npc;

		public NPCState_Flee(NPC_StatePattern npcStatePattern) {
			npc = npcStatePattern;
		}

		public void UpdateState() {

			CheckIfIShouldFlee ();
			CheckIfIShouldFight ();

		}

		public void ToPatrolState() {

			KeepWalking ();
			npc.currentState = npc.patrolState;

		}

		public void ToAlertState(){}

		public void ToPersueState(){}

		public void ToMeleeAttackState(){

			KeepWalking ();
			npc.currentState = npc.meleeAttackState;

		}

		public void ToRangeAttackState(){}

		void CheckIfIShouldFlee() {
			npc.meshRendererFlag.material.color = Color.gray;

			Collider[] colliders = Physics.OverlapSphere (npc.transform.position, npc.sightRange, npc.myEnemyLayers);

			//If too far, return to patrol state
			if (colliders.Length == 0) {
				ToPatrolState ();
				return;
			}

			directionToEnemy = npc.transform.position - colliders [0].transform.position; //run in straight line, directly away from attacker
			Vector3 checkPos = npc.transform.position + directionToEnemy;

			//Verify fleeing target available in navmesh
			if (NavMesh.SamplePosition (checkPos, out navHit, 3.0f, NavMesh.AllAreas)) {
				npc.myNavMeshAgent.destination = navHit.position;
				KeepWalking ();
			} else {
				StopWalking ();
			}
		}

		void CheckIfIShouldFight() {
			if (npc.persueTarget == null) { //check if there is a target
				return; //if not don't run through anymore code
			}

			float distanceToTarget = Vector3.Distance (npc.transform.position, npc.persueTarget.position);

			if (npc.hasMeleeAttack && distanceToTarget <= npc.meleeAttackRange) { //if a target is close enough for a melee attack, it will stop fleeing and begin attacking
				ToMeleeAttackState (); //if there is no melee attack, it will just continue to flee
			}
		}

		void KeepWalking() {
			npc.myNavMeshAgent.isStopped = false;
			npc.npcMaster.CallEventNpcWalkAnim ();
		}

		void StopWalking() {
			npc.myNavMeshAgent.isStopped = true;
			npc.npcMaster.CallEventNpcIdleAnim ();
		}

	}
}


