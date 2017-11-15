using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * This script enables the animations set up in Unity
 * 
 */ 

namespace ImaginationEngine {
	public class Enemy_Animation : MonoBehaviour {

		private Enemy_Master enemyMaster;
		private Animator myAnimator;

		void OnEnable() {
			SetInitialReferences ();
			enemyMaster.EventEnemyDie += DisableAnimator;
			enemyMaster.EventEnemyWalking += SetAnimationToWalk;
			enemyMaster.EventEnemyReachedNavTarget += SetAnimationToIdle;
			enemyMaster.EventEnemyAttack += SetAnimationToAttack;
			enemyMaster.EventEnemyDeductHealth += SetAnimationToStruck;
		}

		void OnDisable() {
			enemyMaster.EventEnemyDie -= DisableAnimator;
			enemyMaster.EventEnemyWalking -= SetAnimationToWalk;
			enemyMaster.EventEnemyReachedNavTarget -= SetAnimationToIdle;
			enemyMaster.EventEnemyAttack -= SetAnimationToAttack;
			enemyMaster.EventEnemyDeductHealth -= SetAnimationToStruck;
		}

		void SetInitialReferences() {
			enemyMaster = GetComponent<Enemy_Master> ();

			if (GetComponent<Animator> () != null) {
				myAnimator = GetComponent<Animator> ();
			}
		}

		void SetAnimationToWalk() {
			if (myAnimator != null) { //check for animator
				if (myAnimator.enabled) {
					myAnimator.SetBool ("isPursuing", true); //checks for exact spelling in the animation controller
				}
			}
		}

		//Copy structure from previous animation
		void SetAnimationToIdle() {
			if (myAnimator != null) { //check for animator
				if (myAnimator.enabled) {
					myAnimator.SetBool ("isPursuing", false);
				}
			}
		}

		void SetAnimationToAttack() {
			if (myAnimator != null) { //check for animator
				if (myAnimator.enabled) {
					myAnimator.SetTrigger ("Attack");
				}
			}
		}

		void SetAnimationToStruck(int dummy) {
			if (myAnimator != null) { //check for animator
				if (myAnimator.enabled) {
					myAnimator.SetTrigger ("Struck");
				}
			}
		}

		//Disable animator - when enemy dies
		//Otherwise the animations will override the ragdoll set up
		void DisableAnimator() {
			if (myAnimator != null) { //check for animator
				myAnimator.enabled = false;
			}
		}
	}
}


