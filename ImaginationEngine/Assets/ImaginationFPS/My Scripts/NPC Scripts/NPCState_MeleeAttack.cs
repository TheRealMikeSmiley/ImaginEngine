using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {
	public class NPCState_MeleeAttack : NPCState_Interface { // derive from NPCState_Interface script

		private readonly NPC_StatePattern npc;
		private float distanceToTarget;

		public NPCState_MeleeAttack(NPC_StatePattern npcStatePattern) {
			npc = npcStatePattern;
		}

		public void UpdateState() {

			Look ();
			TryToAttack ();

		}

		public void ToPatrolState() {

			KeepWalking ();
			npc.isMeleeAttacking = false;
			npc.currentState = npc.patrolState;

		}

		public void ToAlertState(){}

		public void ToPersueState(){

			KeepWalking ();
			npc.isMeleeAttacking = false;
			npc.currentState = npc.persueState;

		}

		public void ToMeleeAttackState(){}

		public void ToRangeAttackState(){}

		void Look() {
			if (npc.persueTarget == null) {
				ToPatrolState ();
				return;
			}

			Collider[] colliders = Physics.OverlapSphere (npc.transform.position, npc.meleeAttackRange, npc.myEnemyLayers);

			if (colliders.Length == 0) {
				ToPersueState ();
				return;
			}

			foreach (Collider col in colliders) {
				if (col.transform.root == npc.persueTarget) {
					return;
				}
			}

			ToPersueState ();
		}

		void TryToAttack() {
			if (npc.persueTarget != null) {
				npc.meshRendererFlag.material.color = Color.magenta;

				if (Time.time > npc.nextAttack && !npc.isMeleeAttacking) {
					npc.nextAttack = Time.time + npc.attackRate;

					if (Vector3.Distance (npc.transform.position, npc.persueTarget.position) <= npc.meleeAttackRange) {
						Vector3 newPos = new Vector3 (npc.persueTarget.position.x, npc.transform.position.y, npc.persueTarget.position.z);
						npc.transform.LookAt (newPos);
						npc.npcMaster.CallEventNpcAttackAnim ();
						npc.isMeleeAttacking = true;
					} else {
						ToPersueState (); //if out of range, NPC should try to move closer
					}
				}
			} else {
				ToPersueState (); //if out of range, NPC should try to move closer
			}
		}

		void KeepWalking() {
			npc.myNavMeshAgent.isStopped = false;
			npc.npcMaster.CallEventNpcWalkAnim ();
		}
	}
}


