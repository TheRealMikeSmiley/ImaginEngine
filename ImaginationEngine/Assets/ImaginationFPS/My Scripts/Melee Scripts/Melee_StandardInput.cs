using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Handles Player Input with a melee weapon equipped
 * 
 */ 

namespace ImaginationEngine {
	public class Melee_StandardInput : MonoBehaviour {

		private Melee_Master meleeMaster;
		private Transform myTransform;
		private float nextSwing;
		public string attackButtonName;

		
		// Use this for initialization
		void Start () {
			SetInitialReferences ();
		}
	
		// Update is called once per frame
		void Update () {
			CheckIfWeaponShouldAttack ();
		}

		void SetInitialReferences() {
			myTransform = transform;
			meleeMaster = GetComponent<Melee_Master> ();
		}

		void CheckIfWeaponShouldAttack() {
			if (Time.timeScale > 0 && myTransform.root.CompareTag (GameManager_References._playerTag) && !meleeMaster.isInUse) {
				if (Input.GetButton (attackButtonName) && Time.time > nextSwing) {
					nextSwing = Time.time + meleeMaster.swingRate; //Check Melee Master for swing rate
					meleeMaster.isInUse = true; //Prevent another swing before animation is over
					meleeMaster.CallEventPlayerInput ();
				}
			}
		}
	}
}


