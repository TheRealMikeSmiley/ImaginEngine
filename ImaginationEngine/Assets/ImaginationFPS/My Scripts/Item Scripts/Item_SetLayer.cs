using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * This script will set anything picked up by the player to the Weapon layer and
 * anything thrown will be set to Item layer
 * 
 */

namespace ImaginationEngine {
	public class Item_SetLayer : MonoBehaviour {

		private Item_Master itemMaster;
		public string itemThrowLayer;
		public string itemPickupLayer;

		void OnEnable() {
			SetInitialReferences ();
			itemMaster.EventObjectPickup += SetItemToPickupLayer;
			itemMaster.EventObjectThrow += SetItemToThrowLayer;
		}

		void OnDisable() {
			itemMaster.EventObjectPickup -= SetItemToPickupLayer;
			itemMaster.EventObjectThrow -= SetItemToThrowLayer;
		}

		void Start(){
			SetLayerOnEnable ();
		}

		void SetInitialReferences() {
			itemMaster = GetComponent<Item_Master> ();
		}

		void SetItemToThrowLayer() {
			SetLayer (transform, itemThrowLayer);
		}

		void SetItemToPickupLayer() {
			SetLayer (transform, itemPickupLayer);
		}

		//If a layer is not defined, automatically set a layer
		void SetLayerOnEnable() {
			if (itemPickupLayer == "") {
				itemPickupLayer = "Item";
			}

			if (itemThrowLayer == "") {
				itemThrowLayer = "Item";
			}

			//If the gameobject is currently on the player, set the layer to pickup
			if (transform.root.CompareTag (GameManager_References._playerTag)) {
				SetItemToPickupLayer ();
			} else {
				SetItemToThrowLayer ();
			}
		}

		void SetLayer(Transform tForm, string itemLayerName) {
			tForm.gameObject.layer = LayerMask.NameToLayer (itemLayerName); //set objects layer

			//set each child to same layer
			foreach (Transform child in tForm) {
				SetLayer (child, itemLayerName);
			}
		}
	}
}


