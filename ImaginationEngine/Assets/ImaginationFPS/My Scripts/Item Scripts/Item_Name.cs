using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Connects to player detect script and will display the name of the item (from the script) 
 * rather than the item name in the inspector
 * 
 */

namespace ImaginationEngine {
	public class Item_Name : MonoBehaviour {

		public string itemName;

		void Start() {
			SetItemName ();
		}

		void SetItemName() {
			transform.name = itemName; //If player doesnt write anything, item name wil be blank
		}
	}
}


