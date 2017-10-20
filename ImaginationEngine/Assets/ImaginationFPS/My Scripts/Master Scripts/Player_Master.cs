using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {
	public class Player_Master : MonoBehaviour {

		//Set up event handlers
		//Other scripts will call this script during certain events

		public delegate void GeneralEventHandler();
		public event GeneralEventHandler EventInventoryChanged;
		public event GeneralEventHandler EventHandsEmpty;
		public event GeneralEventHandler EventAmmoChanged;

		public delegate void AmmoPickupEventHandler(string ammoName, int quantity);
		public event AmmoPickupEventHandler EventPickedUpAmmo;

		public delegate void PlayerHealthEventHandler(int healthChange);
		public event PlayerHealthEventHandler EventPlayerHealthDeduction;
		public event PlayerHealthEventHandler EventPlayerHealthIncrease;

		//Set up methods
		public void CallEventInventoryChanged() {
			if (EventInventoryChanged != null) {
				EventInventoryChanged ();
			}
		}

		public void CallEventHandsEmpty() {
			if (EventHandsEmpty != null) {
				EventHandsEmpty ();
			}
		}

		public void CallEventAmmoChanged() {
			if (EventAmmoChanged != null) {
				EventAmmoChanged ();
			}
		}

		public void CallEventPickedUpAmmo(string ammoName, int quantity) {
			if (EventPickedUpAmmo != null) {
				EventPickedUpAmmo (ammoName, quantity); //checks for values and makes immediate use from them
			}
		}

		public void CallEventPlayerHealthDeduction(int dmg) {
			if (EventPlayerHealthDeduction != null) {
				EventPlayerHealthDeduction (dmg);
			}
		}

		//For health pickups, currently does not get used
		public void CallEventPlayerHealthIncrease(int increase) {
			if (EventPlayerHealthIncrease != null) {
				EventPlayerHealthIncrease (increase);
			}
		}
	}
}


