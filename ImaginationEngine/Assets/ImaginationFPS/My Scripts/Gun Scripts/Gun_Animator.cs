using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Animates the gun object when the shooting
 * 
 */ 

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

		//Animation set up in Animator, can change animation there
		void PlayShootAnimation(){
			if (myAnimator != null) {
				myAnimator.SetTrigger ("Shoot");
			}
		}
	}
}


