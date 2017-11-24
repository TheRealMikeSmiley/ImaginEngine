using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Create a hit effect when melee weapon hits somthing, similar to gun hit effect
 * 
 */ 

namespace ImaginationEngine {
	public class Melee_HitEffects : MonoBehaviour {

		private Melee_Master meleeMaster;
		public GameObject defaultHitEffect;
		public GameObject enemyHitEffect;

		void OnEnable() {
			SetInitialReferences ();

			meleeMaster.EventHit += SpawnHitEffect;
		}

		void OnDisable() {
			meleeMaster.EventHit -= SpawnHitEffect;

		}

		void SetInitialReferences() {
			meleeMaster = GetComponent<Melee_Master> ();
		}

		void SpawnHitEffect(Collision hitCollision, Transform hitTransform) {
			Quaternion quatAngle = Quaternion.LookRotation (hitCollision.contacts [0].normal);

			if (hitTransform.GetComponent<Enemy_TakeDamage> () != null) {
				Instantiate (enemyHitEffect, hitCollision.contacts [0].point, quatAngle);
			} else {
				Instantiate (defaultHitEffect, hitCollision.contacts [0].point, quatAngle);
			}
		}
	}
}


