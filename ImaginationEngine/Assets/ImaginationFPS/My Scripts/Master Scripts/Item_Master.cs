using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {
	public class Item_Master : MonoBehaviour {

		//Set up event handlers
		//Other scripts will call this script during certain events
		//Must attach this script to each item object

		private Player_Master playerMaster;

		public delegate void GeneralEventHandler();
		public event GeneralEventHandler EventObjectThrow;
		public event GeneralEventHandler EventObjectPickup;

		public delegate void PickupActionEventHandler (Transform item);
		public event PickupActionEventHandler EventPickupAction;
		
		// Use this for initialization
		void Start () {
			SetInitialReferences ();
		
		}

		public void CallEventObjectThrow() {
			if (EventObjectThrow != null) {
				EventObjectThrow ();
			}
			playerMaster.CallEventHandsEmpty (); //moved outside so inventory can update when item picked up
			playerMaster.CallEventInventoryChanged ();
		}

		public void CallEventObjectPickup() {
			if (EventObjectPickup != null) {
				EventObjectPickup ();
			}
			playerMaster.CallEventInventoryChanged (); //moved outside so inventory can update when item picked up
		}

		public void CallEventPickupAction(Transform item) {
			if (EventPickupAction != null) {
				EventPickupAction (item);
			}
		}

		void SetInitialReferences() {
			if (GameManager_References._player != null) {
				playerMaster = GameManager_References._player.GetComponent<Player_Master> ();
			}
		}
	}
}


