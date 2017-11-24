using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Creates the explosion effect, once the barrel health drops to zero
 * 
 */ 

namespace ImaginationEngine {
	public class Destructable_Explode : MonoBehaviour {

		private Destructable_Master destructableMaster;
		private float distance;
		private int damageToApply;
		private Collider[] struckColliders;
		private Transform myTransform;
		private RaycastHit hit;

		public float explosionRange;
		public float explosionForce;
		public int rawDamage;

		void OnEnable() {
			SetInitialReferences ();
			destructableMaster.EventDestroyMe += ExplosionSphere;
		}

		void OnDisable() {
			destructableMaster.EventDestroyMe -= ExplosionSphere;

		}

		void SetInitialReferences() {
			destructableMaster = GetComponent<Destructable_Master> ();
			myTransform = transform;
		}

		//Sphere will decrease in damage over distance
		void ExplosionSphere() {
			myTransform.parent = null;
			GetComponent<Collider> ().enabled = false;

			struckColliders = Physics.OverlapSphere (myTransform.position, explosionRange);

			foreach (Collider col in struckColliders) {
				distance = Vector3.Distance (myTransform.position, col.transform.position);
				damageToApply = (int)Mathf.Abs ((1 - (distance / explosionRange)) * rawDamage); //further away, less damage taken

				//Line of site check, if barrel explodes and player is behind a wall, no damage inflicted
				if (Physics.Linecast (myTransform.position, col.transform.position, out hit)) { //Enemies will not block damage
					if (hit.transform == col.transform || col.GetComponent<Enemy_TakeDamage> () != null) {
						col.SendMessage ("ProcessDamage", damageToApply, SendMessageOptions.DontRequireReceiver);
						col.SendMessage ("CallEventPlayerHealthDeduction", damageToApply, SendMessageOptions.DontRequireReceiver); //Enemy not protected in this case
					}
				}

				//Check for rigidbody
				if (col.GetComponent<Rigidbody> () != null) {
					col.GetComponent<Rigidbody> ().AddExplosionForce (explosionForce, myTransform.position, explosionRange, 1, ForceMode.Impulse);
				}
			}

			Destroy (gameObject, 0.05f);
		}
	}
}


