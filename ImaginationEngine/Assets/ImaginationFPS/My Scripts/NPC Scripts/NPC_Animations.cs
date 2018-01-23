using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * This script will control animations for the NPC
 * 
 */ 

namespace ImaginationEngine {

public class NPC_Animations : MonoBehaviour {

		private NPC_Master npcMaster;
		private Animator myAnimator;

		void OnEnable() {
			SetInitialReferences ();
			npcMaster.EventNpcAttackAnim += ActivateAttackAnimation;
			npcMaster.EventNpcWalkAnim += ActivateWalkingAnimation;
			npcMaster.EventNpcIdleAnim += ActivateIdleAnimation;
			npcMaster.EventNpcRecoveredAnim += ActivateRecoveredAnimation;
			npcMaster.EventNpcStruckAnim += ActivateStruckAnimation;
		}

		void OnDisable() {
			npcMaster.EventNpcAttackAnim -= ActivateAttackAnimation;
			npcMaster.EventNpcWalkAnim -= ActivateWalkingAnimation;
			npcMaster.EventNpcIdleAnim -= ActivateIdleAnimation;
			npcMaster.EventNpcRecoveredAnim -= ActivateRecoveredAnimation;
			npcMaster.EventNpcStruckAnim -= ActivateStruckAnimation;
		}

		void SetInitialReferences() {
			npcMaster = GetComponent<NPC_Master> ();

			if (GetComponent<Animator> () != null) {
				myAnimator = GetComponent<Animator> ();
			}
		}

		void ActivateWalkingAnimation() {
			if (myAnimator != null) { //if animator exists
				if (myAnimator.enabled) { //if animator is enabled
					myAnimator.SetBool (npcMaster.animationBoolPersuing, true); //set isPersuing to true on Animation Controller
				}
			}
		}

		void ActivateIdleAnimation() {
			if (myAnimator != null) {
				if (myAnimator.enabled) {
					myAnimator.SetBool (npcMaster.animationBoolPersuing, false);
				}
			}
		}

		void ActivateAttackAnimation() {
			if (myAnimator != null) {
				if (myAnimator.enabled) {
					myAnimator.SetTrigger (npcMaster.animationTriggerMelee);
				}
			}
		}

		void ActivateRecoveredAnimation() {
			if (myAnimator != null) {
				if (myAnimator.enabled) {
					myAnimator.SetTrigger (npcMaster.animationTriggerRecovered);
				}
			}
		}

		void ActivateStruckAnimation() {
			if (myAnimator != null) {
				if (myAnimator.enabled) {
					myAnimator.SetTrigger (npcMaster.animationTriggerStruck);
				}
			}
		}
	}
}