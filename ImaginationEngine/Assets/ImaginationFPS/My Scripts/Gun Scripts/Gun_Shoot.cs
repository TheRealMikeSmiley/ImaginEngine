using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {
	public class Gun_Shoot : MonoBehaviour {

		private Gun_Master gunMaster;
		private Transform myTransform;
		private Transform camTransform;
		private RaycastHit hit;
		public float range = 400; //set how far the gun can shoot
		private float offsetFactor = 7; //give random point origin of raycast (depending on the speed of the player)
		private Vector3 startPosition; //give random point origin of raycast (depending on the speed of the player)

		void OnEnable() {
			SetInitialReferences ();
			gunMaster.EventPlayerInput += OpenFire;
			gunMaster.EventSpeedCaptured += SetStartOfShootingPosition;
		}

		void OnDisable() {
			gunMaster.EventPlayerInput -= OpenFire;
			gunMaster.EventSpeedCaptured -= SetStartOfShootingPosition;
		}

		void SetInitialReferences() {
			gunMaster = GetComponent<Gun_Master> ();
			myTransform = transform;
			camTransform = myTransform.parent; //Set the camera location, disabled until the player equips object
		}

		//raycast shot from gun, raycast will either hit default object or enemy and return the location
		void OpenFire() {
			//Debug.Log ("OpenFire called");
			if (Physics.Raycast (camTransform.TransformPoint (startPosition), camTransform.forward, out hit, range)) {
				if (hit.transform.GetComponent<NPC_TakeDamage> () != null) {
					gunMaster.CallEventShotEnemy (hit, hit.transform);
				} else {
					gunMaster.CallEventShotDefault (hit, hit.transform);
				}
			}
		}

		//Sets the offset of the raycast depending on players speed
		void SetStartOfShootingPosition(float playerSpeed) {
			float offset = playerSpeed / offsetFactor;
			startPosition = new Vector3 (Random.Range (-offset, offset), Random.Range (-offset, offset), 1);
		}
	}
}


