using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Resets "in use" if melee weapon is thrown
 * Fixes glitch, if user throws melee weapon while attacking and picks it back up, it will stay "In Use"
 * This resets the item, in case that happens
 * 
 */ 

namespace ImaginationEngine {
	public class Melee_Reset : MonoBehaviour {

		private Melee_Master meleeMaster;
		private Item_Master itemMaster;

		void OnEnable() {
			SetInitialReferences ();

			if (itemMaster != null) {
				itemMaster.EventObjectThrow += ResetMelee;
			}
		}

		void OnDisable() {
			itemMaster.EventObjectThrow -= ResetMelee;
		}

		void SetInitialReferences() {
			meleeMaster = GetComponent<Melee_Master> ();

			if (GetComponent<Item_Master> () != null) {
				itemMaster = GetComponent<Item_Master> ();
			}
		}

		void ResetMelee() {
			meleeMaster.isInUse = false;
		}
	}
}


