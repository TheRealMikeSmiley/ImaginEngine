using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


