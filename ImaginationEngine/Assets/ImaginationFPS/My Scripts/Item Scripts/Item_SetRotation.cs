using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Fixes issue with picking up items
 * Instead of the item held in the same rotation as it was picked up, it will be held the same way no matter how it was picked up
 * 
 */ 

namespace ImaginationEngine {
	public class Item_SetRotation : MonoBehaviour {

		private Item_Master itemMaster;
		public Vector3 itemLocalRotation;

		void OnEnable() {
			SetInitialReferences ();
			itemMaster.EventObjectPickup += SetRotationOnPlayer;
		}

		void OnDisable() {
			itemMaster.EventObjectPickup -= SetRotationOnPlayer;
		}
		
		// Use this for initialization
		void Start () {
			SetRotationOnPlayer ();
		}

		void SetInitialReferences() {
			itemMaster = GetComponent<Item_Master> ();
		}

		void SetRotationOnPlayer() {
			if (transform.root.CompareTag (GameManager_References._playerTag)) {
				transform.localEulerAngles = itemLocalRotation;
			}
		}
	}
}


