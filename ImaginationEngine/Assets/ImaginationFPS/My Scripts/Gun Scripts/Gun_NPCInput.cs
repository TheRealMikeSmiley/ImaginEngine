using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * This script is required if the NPC uses a ranged weapon, it will handle the input methods added to the Gun scripts
 * 
 */ 

namespace ImaginationEngine {

	public class Gun_NPCInput : MonoBehaviour {

		private Gun_Master gunMaster;
		private Transform myTransform;
		private RaycastHit hit;
		public LayerMask layerToAvoid; //Can set layers not to hit

		void OnEnable() {
			SetInitialReferences ();
			gunMaster.EventNpcInput += NpcFireGun;
		}

		void OnDisable() {
			gunMaster.EventNpcInput -= NpcFireGun;
		}


		void SetInitialReferences() {
			gunMaster = GetComponent<Gun_Master> ();
			myTransform = transform;
		}

		void NpcFireGun(float randomness) {
			Vector3 startPosition = new Vector3 (Random.Range (-randomness, randomness), Random.Range (-randomness, randomness), 0.5f);

			if (Physics.Raycast (myTransform.TransformPoint (startPosition), myTransform.forward, out hit, GetComponent<Gun_Shoot> ().range)) {
				if (hit.transform.gameObject.layer == layerToAvoid) {
					return;
				} else if (hit.transform.GetComponent<NPC_TakeDamage> () != null || hit.transform == GameManager_References._player.transform) {
					gunMaster.CallEventShotEnemy (hit, hit.transform);
				} else {
					gunMaster.CallEventShotDefault (hit, hit.transform);
				}
			}
		}
	}
}
