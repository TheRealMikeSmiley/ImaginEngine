using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * This script fixes the issue with the gun not reset if the player reloads and then throws the gun
 * If not attached, the gun won't reload since it is stuck in the reload state
 * 
 */ 

namespace ImaginationEngine {
	public class Gun_Reset : MonoBehaviour {

		private Gun_Master gunMaster;
		private Item_Master itemMaster;

		void OnEnable() {
			SetInitialReferences ();

			if (itemMaster != null) {
				itemMaster.EventObjectThrow += ResetGun;
			}
		}

		void OnDisable() {
			if (itemMaster != null) {
				itemMaster.EventObjectThrow -= ResetGun;
			}
		}

		void SetInitialReferences() {
			gunMaster = GetComponent<Gun_Master> ();

			if (GetComponent<Item_Master> () != null) {
				itemMaster = GetComponent<Item_Master> ();
			}
		}

		void ResetGun() {
			gunMaster.CallEventRequestGunReset ();
		}
	}
}


