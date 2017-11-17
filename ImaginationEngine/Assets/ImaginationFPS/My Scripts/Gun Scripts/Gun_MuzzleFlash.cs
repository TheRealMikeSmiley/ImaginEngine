using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Plays muzzle flash particle system when gun fires
 * 
 */ 

namespace ImaginationEngine {
	public class Gun_MuzzleFlash : MonoBehaviour {

		public ParticleSystem muzzleFlash;
		private Gun_Master gunMaster;

		void OnEnable() {
			SetInitialReferences ();
			gunMaster.EventPlayerInput += PlayMuzzleFlash;
		}

		void OnDisable() {
			gunMaster.EventPlayerInput -= PlayMuzzleFlash;
		}

		void SetInitialReferences() {
			gunMaster = GetComponent<Gun_Master> ();
		}

		//check if muzzle is already flashing, if not - play
		void PlayMuzzleFlash(){
			if (muzzleFlash != null) {
				muzzleFlash.Play ();
			}
		}
	}
}


