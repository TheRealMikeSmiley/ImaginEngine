using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {
	public class NPCState_Follow : NPCState_Interface { // derive from NPCState_Interface script

		private readonly NPC_StatePattern npc;
		private Collider[] colliders;
		private Vector3 lookAtPoint;
		private Vector3 heading;
		private float dotProd; //dot product

		public NPCState_Follow(NPC_StatePattern npcStatePattern) {
			npc = npcStatePattern;
		}

		public void UpdateState() {

			Look ();
			FollowTarget ();

		}

		public void ToPatrolState() {

			npc.currentState = npc.patrolState;

		}

		public void ToAlertState(){

			npc.currentState = npc.alertState;

		}

		public void ToPersueState(){}

		public void ToMeleeAttackState(){}

		public void ToRangeAttackState(){}

		void Look() {
			//Check near range
			colliders = Physics.OverlapSphere(npc.transform.position, npc.sightRange / 3, npc.myEnemyLayers);

			if (colliders.Length > 0) {
				AlertStateActions (colliders [0].transform);
				return;
			}

			//Check medium range
			colliders = Physics.OverlapSphere(npc.transform.position, npc.sightRange / 2, npc.myEnemyLayers);

			if (colliders.Length > 0) {
				VisibilityCalculations (colliders [0].transform);

				if (dotProd > 0) {
					AlertStateActions (colliders [0].transform);
					return;
				}
			}

			//Check max range
			colliders = Physics.OverlapSphere(npc.transform.position, npc.sightRange, npc.myEnemyLayers);

			foreach (Collider col in colliders) {
				RaycastHit hit;

				VisibilityCalculations (col.transform);

				if (Physics.Linecast (npc.head.position, lookAtPoint, out hit, npc.sightLayers)) {
					foreach (string tags in npc.myEnemyTags) {
						if (hit.transform.CompareTag (tags)) {
							if (dotProd > 0) {
								AlertStateActions (col.transform);
								return;
							}
						}
					}
				}
			}
		}

		void FollowTarget() {
			npc.meshRendererFlag.material.color = Color.blue;

			if (!npc.myNavMeshAgent.enabled) { //check if there is a nav mesh agent set
				return;
			}

			if (npc.myFollowTarget != null) {
				npc.myNavMeshAgent.SetDestination (npc.myFollowTarget.position);
				KeepWalking ();

			} else {
				ToPatrolState ();
			}

			if (HaveIReachedDestination ()) {
				StopWalking ();
			}
		}

		void AlertStateActions(Transform target) {
			npc.locationOfInterest = target.position; //For check state
			ToAlertState();
		}

		void VisibilityCalculations(Transform target) {
			lookAtPoint = new Vector3 (target.position.x, target.position.y + npc.offset, target.position.z);
			heading = lookAtPoint - npc.transform.position;
			dotProd = Vector3.Dot (heading, npc.transform.forward);
		}

		bool HaveIReachedDestination() {
			if (npc.myNavMeshAgent.remainingDistance <= npc.myNavMeshAgent.stoppingDistance && !npc.myNavMeshAgent.pathPending) {
				StopWalking ();
				return true;
			} else {
				KeepWalking ();
				return false;
			}
		}

		void KeepWalking() {
			npc.myNavMeshAgent.isStopped = false;
			npc.npcMaster.CallEventNpcWalkAnim ();
		}

		void StopWalking() {
			npc.myNavMeshAgent.isStopped = true;
			npc.npcMaster.CallEventNpcIdleAnim();
		}

	}
}


