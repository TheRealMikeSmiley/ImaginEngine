using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {
	public class Item_Throw : MonoBehaviour {

		private Item_Master itemMaster;
		private Transform myTransform;
		private Rigidbody myRigidbody;
		private Vector3 throwDirection;

		public bool canBeThrown;
		public string throwButtonName;
		public float throwForce;
		
		// Use this for initialization
		void Start () {
			SetInitialReferences ();
		}
	
		// Update is called once per frame
		void Update () {
			CheckForThrowInput ();
		}

		void SetInitialReferences() {
			itemMaster = GetComponent<Item_Master> ();
			myTransform = transform;
			myRigidbody = GetComponent<Rigidbody> ();
		}
			
		void CheckForThrowInput() {
			if (throwButtonName != null) {
				//Perform checks to ensure the item can be thrown, in particular the item is within the Player
				if (Input.GetButtonDown (throwButtonName) && Time.timeScale > 0 && canBeThrown && myTransform.root.CompareTag (GameManager_References._playerTag)) {
					CarryOutThrowActions ();
				}
			}
		}

		void CarryOutThrowActions() {
			throwDirection = myTransform.parent.forward; //forware direction is the direction the player is facing
			myTransform.parent = null; //makes sure item doesnt turn if thrown if the player turns
			itemMaster.CallEventObjectThrow (); //Go from kinematic (if held), to not(if thrown) - ex add gravity, force etc.
			HurlItem ();
		}

		//Get force of throw value
		void HurlItem() {
			myRigidbody.AddForce (throwDirection * throwForce, ForceMode.Impulse);
		}
	}
}


