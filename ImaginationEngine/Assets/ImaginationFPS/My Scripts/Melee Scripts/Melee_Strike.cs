using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * This makes the melee weapon cause damage when swinging
 * 
 */ 

namespace ImaginationEngine {
	public class Melee_Strike : MonoBehaviour {

		private Melee_Master meleeMaster;
		private float nextSwingTime;
		public int damage = 25;
		
		// Use this for initialization
		void Start () {
			SetInitialReferences ();
		}

		//If weapon collides with enemy, while in use, cause damage
		void OnCollisionEnter(Collision collision) {
			if (collision.gameObject != GameManager_References._player && meleeMaster.isInUse && Time.time > nextSwingTime) {
				nextSwingTime = Time.time + meleeMaster.swingRate; //limits collision to once per swing, otherwise it could deal damage more than once per swing
				collision.transform.SendMessage ("ProcessDamage", damage, SendMessageOptions.DontRequireReceiver);
				collision.transform.root.SendMessage ("SetMyAttacker", transform.root, SendMessageOptions.DontRequireReceiver);
				meleeMaster.CallEventHit (collision, collision.transform);
			}
		}

		void SetInitialReferences() {
			meleeMaster = GetComponent<Melee_Master> ();
		}
	}
}


