using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * When NPC dies, turn off the StatePattern to save on resources
 * 
 */ 

namespace ImaginationEngine {

	public class NPC_TurnOffStatePattern : MonoBehaviour {

		private NPC_Master npcMaster;
		private NPC_StatePattern npcPattern;

		void OnEnable() {
			SetInitialReferences ();
			npcMaster.EventNpcDie += TurnOffStatePattern;
		}

		void OnDisable() {
			npcMaster.EventNpcDie -= TurnOffStatePattern;
		}

		void SetInitialReferences() {
			npcMaster = GetComponent<NPC_Master> ();

			if (GetComponent<NPC_StatePattern> () != null) {
				npcPattern = GetComponent<NPC_StatePattern> ();
			}
		}

		void TurnOffStatePattern() {
			if (npcPattern != null) {
				npcPattern.enabled = false;
			}
		}
	}
}
