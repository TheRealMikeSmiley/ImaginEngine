using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Applies damage to an enemy depending on the raycast hit position
 * 
 */ 

namespace ImaginationEngine {
	public class Gun_ApplyDamage : MonoBehaviour {

		private Gun_Master gunMaster;
		public int damage = 10; //can alter in unity editor

		void OnEnable() {
			SetInitialReferences ();
			gunMaster.EventShotEnemy += ApplyDamage;
			gunMaster.EventShotDefault += ApplyDamage;
		}

		void OnDisable() {
			gunMaster.EventShotEnemy -= ApplyDamage;
			gunMaster.EventShotDefault -= ApplyDamage;

		}

		void SetInitialReferences() {
			gunMaster = GetComponent<Gun_Master> ();
		}

		//Checks location of raycast, applies damage - in case of damage multipliers
		void ApplyDamage(Vector3 hitPosition, Transform hitTransform){
			hitTransform.SendMessage ("ProcessDamage", damage, SendMessageOptions.DontRequireReceiver);
		}
	}
}


