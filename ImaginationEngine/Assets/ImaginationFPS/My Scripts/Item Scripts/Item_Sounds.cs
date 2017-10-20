using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Plays sound when item picked up and thrown
 * 
 */ 

namespace ImaginationEngine {
	public class Item_Sounds : MonoBehaviour {

		private Item_Master itemMaster;
		public float defaultVolume;
		public AudioClip throwSound;
		public AudioClip pickupSound;

		void OnEnable() {
			SetInitialReferences ();
			itemMaster.EventObjectThrow += PlayThrowSound;
			itemMaster.EventObjectPickup += PlayPickupSound;
		}

		void OnDisable() {
			itemMaster.EventObjectThrow -= PlayThrowSound;
			itemMaster.EventObjectPickup -= PlayPickupSound;
		}

		void SetInitialReferences() {
			itemMaster = GetComponent<Item_Master> ();
		}

		//if sound is playing, play the sound at the players position at a user specified volume
		void PlayThrowSound() {
			if (throwSound != null) {
				AudioSource.PlayClipAtPoint (throwSound, transform.position, defaultVolume);
			}
		}

		void PlayPickupSound() {
			if (pickupSound != null) {
				AudioSource.PlayClipAtPoint (pickupSound, transform.position, defaultVolume);
			}
		}
			
	}
}


