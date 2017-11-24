using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Cause damage to destructable object when thrown 
 * 		this will cause barrel to explode if thrown into enemy
 * 
 */ 

namespace ImaginationEngine {
	public class Destructable_CollisionDetection : MonoBehaviour {

		private Destructable_Master destructableMaster;
		private Collider[] hitColliders;
		private Rigidbody myRigidbody;

		public float thresholdMass = 50;
		public float thresholdSpeed = 6;

		// Use this for initialization
		void Start () {
			SetInitialReferences ();
		}

		void OnCollisionEnter(Collision col) {
			if (col.contacts.Length > 0) {
				if (col.contacts [0].otherCollider.GetComponent<Rigidbody> () != null) { //check collision with other item
					CollisionCheck (col.contacts [0].otherCollider.GetComponent<Rigidbody> ());
				} else {
					SelfSpeedCheck ();
				}
			}
		}

		void SetInitialReferences() {
			destructableMaster = GetComponent<Destructable_Master> ();

			if (GetComponent<Rigidbody> () != null) {
				myRigidbody = GetComponent<Rigidbody> ();
			}
		}

		//Check mass of other object
		void CollisionCheck(Rigidbody otherRigidbody) {
			if (otherRigidbody.mass > thresholdMass && otherRigidbody.velocity.sqrMagnitude > (thresholdSpeed * thresholdSpeed)) {
				int damage = (int)otherRigidbody.mass;
				destructableMaster.CallEventDeductHealth (damage);
			} else {
				SelfSpeedCheck ();
			}
		}

		//Object is thrown, and hits somthing, damage will be applied to object
		void SelfSpeedCheck() {
			if (myRigidbody.velocity.sqrMagnitude > (thresholdSpeed * thresholdSpeed)) {
				int damage = (int)myRigidbody.mass;
				destructableMaster.CallEventDeductHealth (damage);
			}
		}
	}
}


