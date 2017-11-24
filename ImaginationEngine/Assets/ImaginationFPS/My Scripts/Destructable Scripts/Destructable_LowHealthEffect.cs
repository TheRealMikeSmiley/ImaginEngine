using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Create a damage effect to destructible object
 * 
 */ 

namespace ImaginationEngine {
	public class Destructable_LowHealthEffect : MonoBehaviour {

		private Destructable_Master destructableMaster;
		public GameObject[] lowHealthEffectGO;

		void OnEnable() {
			SetInitialReferences ();
			destructableMaster.EventHealthLow += TurnOnLowHealthEffect;
		}

		void OnDisable() {
			destructableMaster.EventHealthLow -= TurnOnLowHealthEffect;
		}

		void SetInitialReferences() {
			destructableMaster = GetComponent<Destructable_Master> ();
		}
			
		void TurnOnLowHealthEffect() {
			if (lowHealthEffectGO.Length > 0) {
				for (int i = 0; i < lowHealthEffectGO.Length; i++) {
					lowHealthEffectGO [i].SetActive (true);
				}
			}
		}
	}
}


