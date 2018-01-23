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
			gunMaster.EventShotDefault += ApplyDamage; //Allows other objects to receive damage
		}

		void OnDisable() {
			gunMaster.EventShotEnemy -= ApplyDamage;
			gunMaster.EventShotDefault -= ApplyDamage;

		}

		void SetInitialReferences() {
			gunMaster = GetComponent<Gun_Master> ();
		}

		//Checks location of raycast, applies damage - in case of damage multipliers
		void ApplyDamage(RaycastHit hitPosition, Transform hitTransform){
			//Changed apply damage, this way is more efficient and allows more than just enemies to receive damage
			hitTransform.SendMessage ("ProcessDamage", damage, SendMessageOptions.DontRequireReceiver);
			hitTransform.SendMessage ("CallEventPlayerHealthDeduction", damage, SendMessageOptions.DontRequireReceiver);
			hitTransform.SendMessage ("SetMyAttacker", transform.root, SendMessageOptions.DontRequireReceiver); //Tell the NPC whose attacking
		}
	}
}


