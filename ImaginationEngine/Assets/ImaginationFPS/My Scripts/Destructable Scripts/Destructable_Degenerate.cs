using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Destructible object will gradually degenerate when health is low
 * Works in tandem with fire particle effect - the fire indicates health is low, and it will explode soon
 * 
 */ 

namespace ImaginationEngine {
	public class Destructable_Degenerate : MonoBehaviour {

		private Destructable_Master destructableMaster;
		private bool isHealthLow = false;
		public float degenRate = 1;
		private float nextDegenTime;
		public int healthLoss = 5;

		void OnEnable() {
			SetInitialReferences ();
			destructableMaster.EventHealthLow += HealthLow;
		}

		void OnDisable() {
			destructableMaster.EventHealthLow -= HealthLow;
		}

		// Update is called once per frame
		void Update () {
			CheckIfHealthShouldDegenerate ();
		}

		void SetInitialReferences() {
			destructableMaster = GetComponent<Destructable_Master> ();
		}

		void HealthLow() {
			isHealthLow = true;
		}

		void CheckIfHealthShouldDegenerate() {
			if (isHealthLow) {
				if (Time.time > nextDegenTime) {
					nextDegenTime = Time.time + degenRate;
					destructableMaster.CallEventDeductHealth (healthLoss);
				}
			}
		}
	}
}


