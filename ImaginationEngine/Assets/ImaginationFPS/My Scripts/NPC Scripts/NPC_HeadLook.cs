using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * This script sets the head of an NPC to look at their target
 * 
 */ 

namespace ImaginationEngine {

	public class NPC_HeadLook : MonoBehaviour {

		private NPC_StatePattern npcStatePattern;
		private Animator myAnimator;

		// Use this for initialization
		void Start () {
			SetInitialReferences ();
		}

		void SetInitialReferences() {
			npcStatePattern = GetComponent<NPC_StatePattern> ();
			myAnimator = GetComponent<Animator> ();
		}

		//Set NPC head to look at target
		void OnAnimatorIK() { //Unity Method
			if (myAnimator.enabled) {
				if (npcStatePattern.persueTarget != null) {
					myAnimator.SetLookAtWeight (1, 0, 0.5f, 0.5f, 0.7f);
					myAnimator.SetLookAtPosition (npcStatePattern.persueTarget.position);
				} else {
					myAnimator.SetLookAtWeight (0);
				}
			}
		}
	}
}
