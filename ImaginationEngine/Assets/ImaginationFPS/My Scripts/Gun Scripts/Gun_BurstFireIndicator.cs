using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * This script adds an image to the ammo canvas if the secondary fire mode is on
 * 
 */ 

namespace ImaginationEngine {
	public class Gun_BurstFireIndicator : MonoBehaviour {

		private Gun_Master gunMaster;
		public GameObject burstFireIndicator;

		void OnEnable() {
			SetInitialReferences ();
			gunMaster.EventToggleBurstFire += ToggleIndicator;
		}

		void OnDisable() {
			gunMaster.EventToggleBurstFire -= ToggleIndicator;
		}

		void SetInitialReferences() {
			gunMaster = GetComponent<Gun_Master> ();
		}

		void ToggleIndicator() {
			if (burstFireIndicator != null) {
				burstFireIndicator.SetActive (!burstFireIndicator.activeSelf);
			}
		}
	}
}


