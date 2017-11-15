using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {
	public class Gun_Animator : MonoBehaviour {

		private Gun_Master gunMaster;
		private Animator myAnimator;

		void OnEnable() {
			SetInitialReferences ();
			gunMaster.EventPlayerInput += PlayShootAnimation;
		}

		void OnDisable() {
			gunMaster.EventPlayerInput -= PlayShootAnimation;
		}

		void SetInitialReferences() {
			gunMaster = GetComponent<Gun_Master> ();

			if (GetComponent<Animator> () != null) {
				myAnimator = GetComponent<Animator> ();
			}
		}

		void PlayShootAnimation(){
			if (myAnimator != null) {
				myAnimator.SetTrigger ("Shoot");
			}
		}
	}
}


