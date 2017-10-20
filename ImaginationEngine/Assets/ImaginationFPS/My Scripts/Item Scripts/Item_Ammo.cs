using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Create ammo for weapons
 * includes option to make item picked up automatically or by pressing the pick up button
 * 
 */

namespace ImaginationEngine {
	public class Item_Ammo : MonoBehaviour {

		private Item_Master itemMaster;
		private GameObject playerGo;
		public string ammoName; //Can create duplicates of this script to add different ammo types and quantities
		public int quantity;
		public bool isTriggerPickup; //Can press E to pick up or walk up to ammo

		void OnEnable() {
			SetInitialReferences ();
			itemMaster.EventObjectPickup += TakeAmmo; //if E is pressed
		}

		void OnDisable() {
			itemMaster.EventObjectPickup -= TakeAmmo; //if E is pressed

		}
		
		// Use this for initialization
		void Start () {
			SetInitialReferences ();
		
		}

		void OnTriggerEnter(Collider other) { //if item is a trigger pickup and player is within range item will be automatically picked up
			if (other.CompareTag (GameManager_References._playerTag) && isTriggerPickup) {
				TakeAmmo ();
			}
		}

		void SetInitialReferences() {
			itemMaster = GetComponent<Item_Master> ();
			playerGo = GameManager_References._player;

			if (isTriggerPickup) {
				if (GetComponent<Collider> ()!=null) {
					GetComponent<Collider> ().isTrigger = true;
				}

				if (GetComponent<Rigidbody> ()!=null) {
					GetComponent<Rigidbody> ().isKinematic = true;
				}
			}
		}

		// Increase ammo in player
		void TakeAmmo() {
			playerGo.GetComponent<Player_Master> ().CallEventPickedUpAmmo (ammoName, quantity);
			Destroy (gameObject); //Once picked up, item will disappear
		}
	}
}


