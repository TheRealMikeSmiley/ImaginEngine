using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * This script allows for the NPC to look like it's holding a weapon
 * This allows the user to have an NPC hold a weapon without the need for animations
 * A user can purchase animations for specific weapon types, but these animations will not be included
 * The user must place the right hand and left hand locations in the inspector
 * 
 */ 

namespace ImaginationEngine {

	public class NPC_HoldRangeWeapon : MonoBehaviour {

		private NPC_StatePattern npcStatePattern;
		private Animator myAnimator;
		public Transform rightHandTarget;
		public Transform leftHandTarget;

		// Use this for initialization
		void Start () {
			SetInitialReferences ();
		}

		void SetInitialReferences() {
			npcStatePattern = GetComponent<NPC_StatePattern> ();
			myAnimator = GetComponent<Animator> ();
		}

		void OnAnimatorIK() { //Uses the Inverse Kinematics method specific to Unity
			if (npcStatePattern.rangeWeapon == null) { //check if NPC has a ranged weapon
				return;
			}

			if (myAnimator.enabled) { //If NPC has a ranged weapon
				if (npcStatePattern.rangeWeapon.activeSelf) {
					if (rightHandTarget != null) { //For Right hand
						myAnimator.SetIKPositionWeight (AvatarIKGoal.RightHand, 1);
						myAnimator.SetIKRotationWeight (AvatarIKGoal.RightHand, 1);
						myAnimator.SetIKPosition (AvatarIKGoal.RightHand, rightHandTarget.position);
						myAnimator.SetIKRotation (AvatarIKGoal.RightHand, rightHandTarget.rotation);
					}

					if (leftHandTarget != null) { //For Left Hand
						myAnimator.SetIKPositionWeight (AvatarIKGoal.LeftHand, 1);
						myAnimator.SetIKRotationWeight (AvatarIKGoal.LeftHand, 1);
						myAnimator.SetIKPosition (AvatarIKGoal.LeftHand, leftHandTarget.position);
						myAnimator.SetIKRotation (AvatarIKGoal.LeftHand, leftHandTarget.rotation);
					}
				}
			}
		}
	}
}
