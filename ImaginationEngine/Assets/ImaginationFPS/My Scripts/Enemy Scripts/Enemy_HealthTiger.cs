using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Gives the enemy health
 * 
 */ 

namespace ImaginationEngine {
	public class Enemy_HealthTiger : MonoBehaviour {

		private Enemy_Master enemyMaster;
		public int enemyHealth = 100; //allow user to edit health in the inspector
		public float healthLow = 25; 

		void OnEnable() {
			SetInitialReferences ();
			enemyMaster.EventEnemyDeductHealth += DeductHealth;
			enemyMaster.EventEnemyIncreaseHealth += IncreaseHealth;
		}

		void OnDisable() {
			enemyMaster.EventEnemyDeductHealth -= DeductHealth;
			enemyMaster.EventEnemyIncreaseHealth -= IncreaseHealth;

		}

		void Update() {
			if (Input.GetKeyUp (KeyCode.Period)) {
				enemyMaster.CallEventEnemyIncreaseHealth (75);
			}

			if (Input.GetKeyUp (KeyCode.L)) {
				enemyMaster.CallEventEnemyDeductHealth (75);
			}
		}

		void SetInitialReferences() {
			enemyMaster = GetComponent<Enemy_Master> ();
		}

		void DeductHealth(int healthChange) {
			enemyHealth -= healthChange;
			if (enemyHealth <= 0) {
				enemyHealth = 1;
				//enemyMaster.CallEventEnemyDie ();
				//Destroy(gameObject, Random.Range(10,20)); //Removes enemy in a staggered amount of time - needed to free up resources
			}

			CheckHealthFraction ();
		}

		void CheckHealthFraction() {
			if (enemyHealth <= healthLow && enemyHealth > 0) {
				enemyMaster.CallEventEnemyHealthLow ();
			} else if (enemyHealth > healthLow) { 
				enemyMaster.CallEventEnemyHealthRecovered ();
			}
		}

		void IncreaseHealth(int healthChange) {
			enemyHealth += healthChange;
			if (enemyHealth > 100) {
				enemyHealth = 100;
			}

			CheckHealthFraction ();
		}
	}
}


