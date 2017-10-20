using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * This script will contain a list to define weapons and the respective ammo
 * 
 * This will ensure that if a weapon is reloaded, it will use a specific type of ammo 
 * 
 */

namespace ImaginationEngine {
	public class Player_AmmoBox : MonoBehaviour {

		private Player_Master playerMaster;

		//define a class, and make it a variable
		[System.Serializable]
		public class AmmoTypes
		{
			public string ammoName;
			public int ammoCurrentCarried;
			public int ammoMaxQuantity;

			//pass values so a new class will be made
			public AmmoTypes(string aName, int aMaxQuantity, int aCurrentCarried) {
				ammoName = aName;
				ammoMaxQuantity = aMaxQuantity;
				ammoCurrentCarried = aCurrentCarried;
			}
		}

		//Create a new list with user defined values, this will ensure that a game can be 
		//as simple or complex as the user wants
		public List<AmmoTypes> typesOfAmmunition = new List<AmmoTypes>();

		void OnEnable() {
			SetInitialReferences ();
			playerMaster.EventPickedUpAmmo += PickedUpAmmo;
		}

		void OnDisable() {
			playerMaster.EventPickedUpAmmo -= PickedUpAmmo;
		}

		void SetInitialReferences() {
			playerMaster = GetComponent<Player_Master> ();
		}

		void PickedUpAmmo(string ammoName, int quantity) {
			for(int i=0; i<typesOfAmmunition.Count; i++) { //how to iterate through the ammo types list
				if(typesOfAmmunition[i].ammoName==ammoName) { //if a match is found
					typesOfAmmunition [i].ammoCurrentCarried += quantity; // add the quantity to the ammo type

					//Check ammo amount during pickup, if higher than max quantity, make max quantity
					if (typesOfAmmunition [i].ammoCurrentCarried > typesOfAmmunition [i].ammoMaxQuantity) { 
						typesOfAmmunition [i].ammoCurrentCarried = typesOfAmmunition [i].ammoMaxQuantity;
					}

					//this will check the gun script and show the correct ammo, depending on the gun object
					playerMaster.CallEventAmmoChanged (); 

					break; //get out of for loop, no need to iterate again
				}
			}
		}
	}
}


