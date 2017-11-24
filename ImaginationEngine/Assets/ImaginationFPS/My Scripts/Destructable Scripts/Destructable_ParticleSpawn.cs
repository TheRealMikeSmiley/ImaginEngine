using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creates the explosion effect

namespace ImaginationEngine {
	public class Destructable_ParticleSpawn : MonoBehaviour {

		private Destructable_Master destructableMaster;
		public GameObject explosionEffect;

		void OnEnable() {
			SetInitialReferences ();
			destructableMaster.EventDestroyMe += SpawnExplosion;
		}

		void OnDisable() {
			destructableMaster.EventDestroyMe -= SpawnExplosion;
		}

		void SetInitialReferences() {
			destructableMaster = GetComponent<Destructable_Master> ();
		}


		//Instantiate the explosion particle effect
		void SpawnExplosion() {
			if(explosionEffect!=null) {
				Instantiate(explosionEffect, transform.position, Quaternion.identity);
			}
		}
	}
}


