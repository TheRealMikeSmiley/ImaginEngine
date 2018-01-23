using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {
	public class NPCState_Struck: NPCState_Interface { // derive from NPCState_Interface script

		private readonly NPC_StatePattern npc;
		private float informRate = 0.5f;
		private float nextInform;
		private Collider[] colliders;
		private Collider[] friendlyColliders;

		public NPCState_Struck(NPC_StatePattern npcStatePattern) {
			npc = npcStatePattern;
		}

		public void UpdateState() {

			InformNearbyAlliesThatIHaveBeenHurt ();

		}

		public void ToPatrolState() {}

		public void ToAlertState(){}

		public void ToPersueState(){}

		public void ToMeleeAttackState(){}

		public void ToRangeAttackState(){}


		void InformNearbyAlliesThatIHaveBeenHurt() {
			if (Time.time > nextInform) {
				nextInform = Time.time + informRate;
			} else {
				return;
			}

			if (npc.myAttacker != null) {
				friendlyColliders = Physics.OverlapSphere (npc.transform.position, npc.sightRange, npc.myFriendlyLayers);

				if (IsAttackerClose ()) {
					AlertNearbyAllies ();
					SetMyselfToInvestigate ();
				}
			}
		}

		bool IsAttackerClose() {
			if (Vector3.Distance (npc.transform.position, npc.myAttacker.position) <= npc.sightRange * 2) { //if attacker is within double sight range, then alert allies
				return true;
			} else {
				return false; //if attacker is too far, continue current state
			}
		}

		//If Ally is in range, they will be told of the NPC attacker and will investigate the location
		void AlertNearbyAllies() {
			foreach (Collider ally in friendlyColliders) {
				if (ally.transform.root.GetComponent<NPC_StatePattern> () != null) {
					NPC_StatePattern allyPattern = ally.transform.root.GetComponent<NPC_StatePattern> ();

					if (allyPattern.currentState == allyPattern.patrolState) {
						allyPattern.persueTarget = npc.myAttacker;
						allyPattern.locationOfInterest = npc.myAttacker.position;
						allyPattern.currentState = allyPattern.investigateHarmState;
						allyPattern.npcMaster.CallEventNpcWalkAnim ();
					}
				}
			}
		}

		void SetMyselfToInvestigate() {
			npc.persueTarget = npc.myAttacker;
			npc.locationOfInterest = npc.myAttacker.position; //if NPC is attacked and attacker position is within range, NPC will investigate

			if (npc.capturedState == npc.patrolState) {
				npc.capturedState = npc.investigateHarmState; //NPC goes to find what attacked them
			}
		}
	}
}