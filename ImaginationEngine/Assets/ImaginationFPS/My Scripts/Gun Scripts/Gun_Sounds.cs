using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Plays audio clips attached to a weapn
 * 
 */ 

namespace ImaginationEngine {
	public class Gun_Sounds : MonoBehaviour {

		private Gun_Master gunMaster;
		private Transform myTransform;
		public float shootVolume = 0.4f;
		public float reloadVolume = 0.5f;
		public AudioClip[] shootSound; //can add any number of shoot sounds to an object
		public AudioClip reloadSound;

		void OnEnable() {
			SetInitialReferences ();
			gunMaster.EventPlayerInput += PlayShootSound;
		}

		void OnDisable() {
			gunMaster.EventPlayerInput += PlayShootSound;
		}

		void SetInitialReferences() {
			gunMaster = GetComponent<Gun_Master> ();
			myTransform = transform;
		}

		void PlayShootSound() {
			if (shootSound.Length > 0) { //check for shoot sounds
				int index = Random.Range (0, shootSound.Length);
				//Set index to add several different shoot sounds
				AudioSource.PlayClipAtPoint (shootSound [index], myTransform.position, shootVolume);
			}
		}

		public void PlayReloadSound() { //Called by reload animation event
			if (reloadSound != null) {
				AudioSource.PlayClipAtPoint (reloadSound, myTransform.position, reloadVolume);
			}
		}
	}
}


