using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Enables the NPC to receive damage 
 * Can add this to various body parts and change the multiplier 
 * 
 */ 

namespace ImaginationEngine {

	public class NPC_TakeDamage : MonoBehaviour {

		private NPC_Master npcMaster;
		public int damageMultiplier = 1; //May increase if attached to head or decreased if attached to arm, foot
		public bool shouldRemoveCollider;

		void OnEnable() {
			SetInitialReferences ();
			npcMaster.EventNpcDie += RemoveThis;
		}

		void OnDisable() {
			npcMaster.EventNpcDie -= RemoveThis;

		}

		void SetInitialReferences() {
			npcMaster = transform.root.GetComponent<NPC_Master> ();
		}

		public void ProcessDamage(int damage) {
			int damageToApply = damage * damageMultiplier;
			npcMaster.CallEventNpcDeductHealth (damageToApply);
		}

		//Remove after NPC dies so other NPC's do not attack it anymore
		void RemoveThis() {
			if (shouldRemoveCollider) {
				if (GetComponent<Collider> () != null) {
					Destroy (GetComponent<Collider> ());
				}

				if (GetComponent<Rigidbody> () != null) {
					Destroy (GetComponent<Rigidbody> ());
				}
			}

			gameObject.layer = LayerMask.NameToLayer ("Default"); //So AI does not continue to detect

			Destroy (this);
		}
	}
}
