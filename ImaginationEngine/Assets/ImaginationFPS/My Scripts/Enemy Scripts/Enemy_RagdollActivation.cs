using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Simple script to activate enemy ragdoll
 * 
 */ 

namespace ImaginationEngine {
	public class Enemy_RagdollActivation : MonoBehaviour {

		private Enemy_Master enemyMaster;
		private Collider myCollider;
		private Rigidbody myRigidbody;

		void OnEnable() {
			SetInitialReferences ();
			enemyMaster.EventEnemyDie += ActivateRagdoll;
		}

		void OnDisable() {
			enemyMaster.EventEnemyDie -= ActivateRagdoll;

		}

		void SetInitialReferences() {
			enemyMaster = transform.root.GetComponent<Enemy_Master> (); //attach to each collider of the body for the ragdoll effect

			if (GetComponent<Collider> () != null) {
				myCollider = GetComponent<Collider> ();
			}

			if (GetComponent<Rigidbody> () != null) {
				myRigidbody = GetComponent<Rigidbody> ();
			}
		}

		void ActivateRagdoll() {
			if (myRigidbody != null) {
				myRigidbody.isKinematic = false;
				myRigidbody.useGravity = true;
			}

			if (myCollider != null) {
				myCollider.isTrigger = false;
				myCollider.enabled = true;
			}
		}
	}
}


