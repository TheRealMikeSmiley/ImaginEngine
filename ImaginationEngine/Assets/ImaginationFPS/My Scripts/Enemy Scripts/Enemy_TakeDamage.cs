using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {
	public class Enemy_TakeDamage : MonoBehaviour {

		private Enemy_Master enemyMaster;
		public int damageMultiplier = 1; //can change depending on hitbox location
		public bool shouldRemoveCollider;

		void OnEnable() {
			SetInitialReferences ();
			enemyMaster.EventEnemyDie += RemoveThis;
		}

		void OnDisable() {
			enemyMaster.EventEnemyDie -= RemoveThis;

		}

		//Because adding seperate boxes to head and body
		void SetInitialReferences() {
			enemyMaster = transform.root.GetComponent<Enemy_Master> ();
		}

		//This will be used by the gun script, and damage will be applied to the enemy
		public void ProcessDamage(int damage) {
			int damageToApply = damage * damageMultiplier; 
			enemyMaster.CallEventEnemyDeductHealth (damageToApply);
		}

		//Hitbox should disappear, otherwise bullets will still hit an invisible pillar
		void RemoveThis() {
			if (shouldRemoveCollider) { 
				if (GetComponent<Collider> () != null) {
					Destroy(GetComponent<Collider>());
				}

				if (GetComponent<Rigidbody> () != null) {
					Destroy(GetComponent<Rigidbody>());
				}
			}

			Destroy (this); //Remove script once enemy health = 0
		}
	}
}


