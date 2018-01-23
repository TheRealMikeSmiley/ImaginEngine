using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * 
 * When NPC dies, turn off the NavMeshAgent to save on resources
 * 
 */ 

namespace ImaginationEngine {

	public class NPC_TurnOffNavMeshAgent : MonoBehaviour {

		private NPC_Master npcMaster;
		private NavMeshAgent myNavMeshAgent;

		void OnEnable() {
			SetInitialReferences ();
			npcMaster.EventNpcDie += TurnOffNavMeshAgent;
		}

		void OnDisable() {
			npcMaster.EventNpcDie -= TurnOffNavMeshAgent;
		}

		void SetInitialReferences() {
			npcMaster = GetComponent<NPC_Master> ();

			if (GetComponent<NavMeshAgent> () != null) {
				myNavMeshAgent = GetComponent<NavMeshAgent> ();
			}
		}

		void TurnOffNavMeshAgent() {
			if (myNavMeshAgent != null) {
				myNavMeshAgent.enabled = false;
			}
		}
	}
}
