using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * This will instantiate the default or hit particle effect
 * Default if an object with the default tag is hit
 * Hit if an object with the enemy tag is hit
 * 
 */ 

namespace ImaginationEngine {
	public class Gun_HitEffects : MonoBehaviour {

		private Gun_Master gunMaster;
		public GameObject defaultHitEffect;
		public GameObject enemyHitEffect;

		void OnEnable() {
			SetInitialReferences ();
			gunMaster.EventShotDefault += SpawnDefaultHitEffect;
			gunMaster.EventShotEnemy += SpawnEnemyHitEffect;
		}

		void OnDisable() {
			gunMaster.EventShotDefault -= SpawnDefaultHitEffect;
			gunMaster.EventShotEnemy -= SpawnEnemyHitEffect;
		}

		void SetInitialReferences() {
			gunMaster = GetComponent<Gun_Master> ();
		}

		void SpawnDefaultHitEffect(RaycastHit hitPosition, Transform hitTransform){
			if (defaultHitEffect != null) {
				Quaternion quatAngle = Quaternion.LookRotation (hitPosition.normal);
				Instantiate (defaultHitEffect, hitPosition.point, quatAngle);
			}
		}

		void SpawnEnemyHitEffect(RaycastHit hitPosition, Transform hitTransform){
			if (enemyHitEffect != null) {
				Quaternion quatAngle = Quaternion.LookRotation (hitPosition.normal);
				Instantiate (enemyHitEffect, hitPosition.point, quatAngle);			}
		}
	}
}


