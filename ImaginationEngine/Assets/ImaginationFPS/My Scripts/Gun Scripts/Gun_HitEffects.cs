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

		void SpawnDefaultHitEffect(Vector3 hitPosition, Transform hitTransform){
			if (defaultHitEffect != null) {
				GameObject go = (GameObject)Instantiate (defaultHitEffect, hitPosition, Quaternion.identity);
				if (hitTransform.gameObject.GetComponent <Renderer> () != null) {
					go.GetComponent<ParticleSystemRenderer> ().material = hitTransform.gameObject.GetComponent<Renderer> ().material;
				}
				//Instantiate (defaultHitEffect, hitPosition, Quaternion.identity);
			}
		}

		void SpawnEnemyHitEffect(Vector3 hitPosition, Transform hitTransform){
			if (enemyHitEffect != null) {
				Instantiate (enemyHitEffect, hitPosition, Quaternion.identity);
			}
		}
	}
}


