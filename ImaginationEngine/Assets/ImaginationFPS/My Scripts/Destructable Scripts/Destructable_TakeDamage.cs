using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Set object so damage can be inflicted from player
 * 
 */ 

namespace ImaginationEngine {
	public class Destructable_TakeDamage : MonoBehaviour {

		private Destructable_Master destructableMaster;

		// Use this for initialization
		void Start () {
			SetInitialReferences ();
		}

		void SetInitialReferences() {
			destructableMaster = GetComponent<Destructable_Master> ();
		}

		public void ProcessDamage(int damage) {
			destructableMaster.CallEventDeductHealth (damage);
		}
	}
}


