using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Emit an explosion sound 
 * 
 */ 

namespace ImaginationEngine {
	public class Destructable_Sound : MonoBehaviour {

		private Destructable_Master destructableMaster;
		public float explosionVolume = 0.5f; //default at half volume
		public AudioClip explodingSound; //set the sound in the inspector

		void OnEnable() {
			SetInitialReferences ();
			destructableMaster.EventDestroyMe += PlayExplosionSound; //Play sound when destroy event is called
		}

		void OnDisable() {
			destructableMaster.EventDestroyMe -= PlayExplosionSound;
		}

		void SetInitialReferences() {
			destructableMaster = GetComponent<Destructable_Master> ();
		}

		void PlayExplosionSound() {
			if (explodingSound != null) {
				AudioSource.PlayClipAtPoint (explodingSound, transform.position, explosionVolume);
			}
		}
	}
}


