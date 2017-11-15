using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * This script takes care of attacking state by the enemy
 * 
 */ 

namespace ImaginationEngine {
	public class Enemy_Attack : MonoBehaviour {

		//Public variables make it easier to change values that will affect behavior, private vars can be left alone
		private Enemy_Master enemyMaster;
		private Transform attackTarget;
		private Transform myTransform;
		public float attackRate = 1;
		private float nextAttack;
		public float attackRange=3.5f; //based off of stopping distance
		public int attackDamage = 10;

		void OnEnable() {
			SetInitialReferences ();
			enemyMaster.EventEnemyDie += DisableThis;
			enemyMaster.EventEnemySetNavTarget += SetAttackTarget;
		}

		void OnDisable() {
			enemyMaster.EventEnemyDie -= DisableThis;
			enemyMaster.EventEnemySetNavTarget -= SetAttackTarget;
		}
	
		// Update is called once per frame
		void Update () {
			TryToAttack ();
		}

		void SetInitialReferences() {
			enemyMaster = GetComponent<Enemy_Master> ();
			myTransform = transform;
		}

		void SetAttackTarget(Transform targetTransform) {
			attackTarget = targetTransform; //target will be the player
		}

		void TryToAttack() {
			if (attackTarget != null) {
				if (Time.time > nextAttack) {
					nextAttack = Time.time + attackRate;
					if (Vector3.Distance (myTransform.position, attackTarget.position) <= attackRange) {
						Vector3 lookAtVector = new Vector3 (attackTarget.position.x, myTransform.position.y, attackTarget.position.z); //Enemy will face the player when attacking
						myTransform.LookAt (lookAtVector); //Will turn the enemy to face player
						enemyMaster.CallEventEnemyAttack (); //Enemy attack animation will play
						enemyMaster.isOnRoute = false; //stops enemy movement while attacking
					}
				}
			}
		}

		public void OnEnemyAttack() { //Called by hpunch animation (Attack animation), this will apply the damage to the player
			if (attackTarget != null) {
				//If enemy is close enough and player is in front of the enemy
				if (Vector3.Distance (myTransform.position, attackTarget.position) <= attackRange && attackTarget.GetComponent<Player_Master> () != null) {
					Vector3 toOther = attackTarget.position - myTransform.position;
					//Debug.Log (Vector3.Dot (toOther, myTransform.forward).ToString ()); //Check values when moving around enemy
					//Used this to find best values for damage radius

					//This will check to make sure that player is in front of the enemy so damage doesnt occur behind or to the side of the enemy
					if (Vector3.Dot (toOther, myTransform.forward) > 0.5f) {
						attackTarget.GetComponent<Player_Master> ().CallEventPlayerHealthDeduction (attackDamage); //hurt the player
					}
				}
			}
		}

		void DisableThis(){
			this.enabled = false;
		}
	}
}


