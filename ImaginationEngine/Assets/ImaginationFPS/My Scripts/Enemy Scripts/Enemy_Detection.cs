using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Use an overlap sphere on the enemy to detect the player
 * 
 */ 

namespace ImaginationEngine {
	public class Enemy_Detection : MonoBehaviour {

		private Enemy_Master enemyMaster;
		private Transform myTransform;
		public Transform head;
		public LayerMask playerLayer;
		public LayerMask sightLayer;

		private float checkRate;
		private float nextCheck;
		public float detectRadius = 80; //Set radius to 80, can be changed in the inspector
		private RaycastHit hit;

		void OnEnable() {
			SetInitialReferences ();
			enemyMaster.EventEnemyDie += DisableThis;
		}

		void OnDisable() {
			enemyMaster.EventEnemyDie -= DisableThis;
		}
	
		// Update is called once per frame
		void Update () {
			CarryOutDetection ();
		}

		void SetInitialReferences() {
			enemyMaster = GetComponent<Enemy_Master> ();
			myTransform = transform;

			//Check if the user does not set the head
			if (head == null) {
				head = myTransform;
			}

			checkRate = Random.Range (0.8f, 1.2f); //Each enemy has a staggered check rate
		}

		// Check if detectRadius hit Player layer
		void CarryOutDetection() {
			if (Time.time > nextCheck) {
				nextCheck = Time.time + checkRate;

				Collider[] colliders = Physics.OverlapSphere (myTransform.position, detectRadius, playerLayer);

				if (colliders.Length > 0) {
					foreach (Collider potentialTargetCollider in colliders) {
						if (potentialTargetCollider.CompareTag (GameManager_References._playerTag)) { //Player layer is in the detection radius
							if (CanPotentialTargetBeSeen (potentialTargetCollider.transform)) { //Check if Player can be seen
								break; //end the loop
							}
						}
					}
				} else {

					enemyMaster.CallEventEnemyLostTarget (); //if nothing is found, do somthing else

				}
			}
		}

		// Check head position relative to player
		bool CanPotentialTargetBeSeen(Transform potentialTarget) {
			if (Physics.Linecast (head.position, potentialTarget.position, out hit, sightLayer)) {
				if (hit.transform == potentialTarget) {
					enemyMaster.CallEventEnemySetNavTarget (potentialTarget);
					return true;
				} else {
					enemyMaster.CallEventEnemyLostTarget ();
					return false;
				}
			} else {
				enemyMaster.CallEventEnemyLostTarget ();
				return false;
			}
		}

		void DisableThis() {
			this.enabled = false;
		}
	}
}


