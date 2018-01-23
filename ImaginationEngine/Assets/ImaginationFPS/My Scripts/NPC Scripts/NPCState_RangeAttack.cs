using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {
	public class NPCState_RangeAttack : NPCState_Interface { // derive from NPCState_Interface script

		private readonly NPC_StatePattern npc;
		private RaycastHit hit;

		public NPCState_RangeAttack (NPC_StatePattern npcStatePattern) {
			npc = npcStatePattern;
		}

		public void UpdateState() {

			Look ();
			TryToAttack ();

		}

		public void ToPatrolState() {

			KeepWalking ();
			npc.persueTarget = null;
			npc.currentState = npc.patrolState;

		}

		public void ToAlertState(){

			KeepWalking ();
			npc.currentState = npc.alertState;

		}

		public void ToPersueState(){

			KeepWalking ();
			npc.currentState = npc.persueState;

		}

		public void ToMeleeAttackState(){

			npc.currentState = npc.meleeAttackState;

		}

		public void ToRangeAttackState(){}

		void TryToAttack() {
			if (npc.persueTarget != null) {
				npc.meshRendererFlag.material.color = Color.cyan;

				if (!IsTargetInSight ()) {
					ToPersueState ();
					return;
				}

				if (Time.time > npc.nextAttack) {
					npc.nextAttack = Time.time + npc.attackRate;

					float distanceToTarget = Vector3.Distance (npc.transform.position, npc.persueTarget.position);

					TurnTowardsTarget ();

					if (distanceToTarget <= npc.rangeAttackRange) {
						StopWalking ();

						if (npc.rangeWeapon.GetComponent<Gun_Master> () != null) {
							npc.rangeWeapon.GetComponent<Gun_Master> ().CallEventNpcInput (npc.rangeAttackSpread); //Add random attack spread
							return;
						}
					}

					//if NPC has both melee and ranged, if close, choose melee attack
					if (distanceToTarget <= npc.meleeAttackRange && npc.hasMeleeAttack) {
						ToMeleeAttackState ();
					}
				}
			} else {
				ToPatrolState ();
			}
		}

		void TurnTowardsTarget() {

			Vector3 newPos = new Vector3 (npc.persueTarget.position.x, npc.transform.position.y, npc.persueTarget.position.z);
			npc.transform.LookAt (newPos);
		}

		void Look() {
			if (npc.persueTarget == null) {
				ToPatrolState ();
				return;
			}

			Collider[] colliders = Physics.OverlapSphere (npc.transform.position, npc.sightRange, npc.myEnemyLayers);

			if (colliders.Length == 0) { //no target
				ToPatrolState ();
				return;
			}

			foreach (Collider col in colliders) {
				if (col.transform.root == npc.persueTarget) { //See target
					TurnTowardsTarget ();
					return;
				}
			}

			ToPatrolState ();
		}

		void KeepWalking() {
			if (npc.myNavMeshAgent.enabled) {
				npc.myNavMeshAgent.isStopped = false;
				npc.npcMaster.CallEventNpcWalkAnim ();
			}
		}

		void StopWalking() {
			if (npc.myNavMeshAgent.enabled) {
				npc.myNavMeshAgent.isStopped = true;
				npc.npcMaster.CallEventNpcIdleAnim ();
			}
		}

		//Make sure target is in the line of sight before attempting to shoot target
		bool IsTargetInSight() {

			RaycastHit hit;

			Vector3 weaponLookAtVector = new Vector3 (npc.persueTarget.position.x, npc.persueTarget.position.y + npc.offset, npc.persueTarget.position.z);
			npc.rangeWeapon.transform.LookAt (weaponLookAtVector); //update weapon to look at target

			if (Physics.Raycast (npc.rangeWeapon.transform.position, npc.rangeWeapon.transform.forward, out hit)) {
				foreach (string tag in npc.myEnemyTags) {
					if (hit.transform.root.CompareTag (tag)) {
						return true;
					}
				}
				return false;
			} else {
				return false;
			}
		}

	}
}


