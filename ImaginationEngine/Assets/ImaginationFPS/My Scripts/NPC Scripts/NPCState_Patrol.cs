using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * 
 * Set the patrol state for the NPC
 * 
 */ 

namespace ImaginationEngine {
	public class NPCState_Patrol : NPCState_Interface { // derive from NPCState_Interface script

		private readonly NPC_StatePattern npc;
		private int nextWayPoint;
		private Collider[] colliders;
		private Vector3 lookAtPoint;
		private Vector3 heading;
		private float dotProd;

		public NPCState_Patrol(NPC_StatePattern npcStatePattern) {
			npc = npcStatePattern; //Makes reference to StatePattern script, now script can be initialized
		}

		public void UpdateState() {
			Look ();
			Patrol ();
		}

		public void ToPatrolState() {}

		public void ToAlertState(){

			npc.currentState = npc.alertState; //transition to alert state

		}

		public void ToPersueState(){}

		public void ToMeleeAttackState(){}

		public void ToRangeAttackState(){}

		void Look() {
			//Check medium range
			colliders = Physics.OverlapSphere (npc.transform.position, npc.sightRange / 3, npc.myEnemyLayers); //Only look for enemy NPC

			//If in front, move to alert state actions
			if (colliders.Length > 0) {
				VisibilityCalculations (colliders [0].transform);

				if (dotProd > 0) {
					AlertStateActions (colliders [0].transform);
					return; //Avoid moving to next method
				}
			}

			//Check max range
			colliders=Physics.OverlapSphere(npc.transform.position, npc.sightRange, npc.myEnemyLayers);

			foreach (Collider col in colliders) {
				RaycastHit hit;

				VisibilityCalculations (col.transform);

				if (Physics.Linecast (npc.head.position, lookAtPoint, out hit, npc.sightLayers)) {
					foreach (string tags in npc.myEnemyTags) { //Set any number of enemy tags
						if (hit.transform.CompareTag (tags)) { //if found an enemy tag
							if (dotProd > 0) {
								AlertStateActions (col.transform); //start alert state
								return;
							}
						}
					}
				}
			}
		}

		void Patrol() {
			npc.meshRendererFlag.material.color = Color.green; //set flag to green

			//If there is a follow target, transition to follow state
			if (npc.myFollowTarget != null) {
				npc.currentState = npc.followState;
			}

			if (!npc.myNavMeshAgent.enabled) {
				return;
			}

			//If there are waypoints
			if (npc.waypoints.Length > 0) {
				MoveTo (npc.waypoints [nextWayPoint].position); //Set waypoint position in the inspector

				if (HaveIReachedDestination ()) {
					nextWayPoint = (nextWayPoint + 1) % npc.waypoints.Length; //Quick way to ensure NPC follows path
					//If there are 4 waypoints, and the NPC hit the 4th location, the count will return to 0 and start over
				}
			} else { //Wander if there are no waypoints
				if (HaveIReachedDestination ()) {
					StopWalking ();

					if (RandomWanderTarget (npc.transform.position, npc.sightRange, out npc.wanderTarget)) {
						MoveTo (npc.wanderTarget);
					}
				}
			}
		}

		void AlertStateActions(Transform target) {
			npc.locationOfInterest = target.position; //For check state
			ToAlertState();
		}
			
		void VisibilityCalculations(Transform target) {
			lookAtPoint = new Vector3 (target.position.x, target.position.y + npc.offset, target.position.z); //Offset used so NPC doesnt look at ground
			heading = lookAtPoint - npc.transform.position; //Set line cast
			dotProd = Vector3.Dot (heading, npc.transform.forward); //Determine if target is in front of NPC
		}

		//if no waypoints are set, set wander target for NPC
		bool RandomWanderTarget(Vector3 centre, float range, out Vector3 result) {
			NavMeshHit navHit;

			//Pick random point
			Vector3 randomPoint = centre + Random.insideUnitSphere * npc.sightRange;
			if (NavMesh.SamplePosition (randomPoint, out navHit, 3.0f, NavMesh.AllAreas)) { //Find valid Navmesh area
				result = navHit.position;
				return true;
			} else {
				result = centre; //dont go anywhere
				return false;
			}
		}

		bool HaveIReachedDestination() {
			//If NPC has reached target and doesnt have a new target
			if (npc.myNavMeshAgent.remainingDistance <= npc.myNavMeshAgent.stoppingDistance && !npc.myNavMeshAgent.pathPending) {
				StopWalking ();
				return true;
			} else {
				KeepWalking ();
				return false;
			}
		}

		//Set a new destination
		void MoveTo(Vector3 targetPos) {
			if (Vector3.Distance (npc.transform.position, targetPos) > npc.myNavMeshAgent.stoppingDistance + 1) {
				npc.myNavMeshAgent.SetDestination (targetPos);
				KeepWalking ();
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


