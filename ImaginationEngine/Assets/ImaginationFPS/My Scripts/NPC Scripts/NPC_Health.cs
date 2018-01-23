using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {

	public class NPC_Health : MonoBehaviour {

		private NPC_Master npcMaster;
		public int npcMaxHealth = 100;
		public int npcHealth = 100;
		private bool healthCritical;
		private float healthLow = 25;
		public GameObject heldWeapon;

		void OnEnable() {
			SetInitialReferences ();
			npcMaster.EventNpcDeductHealth += DeductHealth;
			npcMaster.EventNpcIncreaseHealth += IncreaseHealth;
		}

		void OnDisable() {
			npcMaster.EventNpcDeductHealth -= DeductHealth;
			npcMaster.EventNpcIncreaseHealth -= IncreaseHealth;
		}

		void SetInitialReferences() {
			npcMaster = GetComponent<NPC_Master> ();
		}

		void DeductHealth(int healthChange) {
			npcHealth -= healthChange;
			if (npcHealth <= 0) {
				npcHealth = 0;
				npcMaster.CallEventNpcDie ();

				Destroy (heldWeapon); //Destroy any held weapon, otherwise it'll just float
				Destroy (gameObject, Random.Range (10, 20)); //destroy dead NPC object anywhere from 10 to 20 seconds
			}
		}

		void IncreaseHealth(int healthChange) {
			npcHealth += healthChange;
			if (npcHealth > npcMaxHealth) {
				npcHealth = npcMaxHealth;
			}

			CheckHealthFraction ();
		}

		void CheckHealthFraction() {
			if (npcHealth <= healthLow && npcHealth > 0) {
				npcMaster.CallEventNpcLowHealth ();
				healthCritical = true; //NPC will flee
			} else if (npcHealth > healthLow && healthCritical) {
				npcMaster.CallEventNpcHealthRecovered ();
				healthCritical = false;
			}
		}
	}
}
