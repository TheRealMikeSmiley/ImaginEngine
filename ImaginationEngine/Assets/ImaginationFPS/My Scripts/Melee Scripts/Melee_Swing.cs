using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Sets melee weapon swing animation
 * 
 */ 

namespace ImaginationEngine {
	public class Melee_Swing : MonoBehaviour {

		private Melee_Master meleeMaster;
		public Collider myCollider;
		public Rigidbody myRigidbody;
		public Animator myAnimator;

		void OnEnable() {
			SetInitialReferences ();
			meleeMaster.EventPlayerInput += MeleeAttackAction;
		}

		void OnDisable() {
			meleeMaster.EventPlayerInput -= MeleeAttackAction;
		}

		void SetInitialReferences() {
			meleeMaster = GetComponent<Melee_Master> ();
		}

		//Weapon performs hit detection with collider
		void MeleeAttackAction(){
			myCollider.enabled = true;
			myRigidbody.isKinematic = false;
			myAnimator.SetTrigger ("Attack");
		}

		//Disable collider once animation is complete
		void MeleeAttackComplete(){ //Called by animation
			myCollider.enabled = false;
			myRigidbody.isKinematic = true;
			meleeMaster.isInUse = false; //Melee weapon is once again ready to attack
		}
	}
}


