using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Set health of destructable object
 * 
 */ 

namespace ImaginationEngine {
	public class Destructable_Health : MonoBehaviour {

		private Destructable_Master destructableMaster;
		public int health; //change in the inspector
		private int startingHealth;
		private bool isExploding = false;

		void OnEnable() {
			SetInitialReferences ();
			destructableMaster.EventDeductHealth += DeductHealth;
		}

		void OnDisable() {
			destructableMaster.EventDeductHealth -= DeductHealth;
		}

		void SetInitialReferences() {
			destructableMaster = GetComponent<Destructable_Master> ();
			startingHealth = health;
		}

		//Health deduction
		void DeductHealth(int healthToDeduct) {
			health -= healthToDeduct;

			CheckIfHealthLow (); //check for low health each time deduct health method called

			//check if health is below 0, and not exploding
			if (health <= 0 && !isExploding) {
				isExploding = true; //if below zero and not exploding, explode
				destructableMaster.CallEventDestroyMe (); //destroy object once explosion occurs
			}
		}

		//Set low health to half of starting health
		void CheckIfHealthLow(){
			if (health <= startingHealth / 2) {
				destructableMaster.CallEventHealthLow ();
			}
		}
	}
}


