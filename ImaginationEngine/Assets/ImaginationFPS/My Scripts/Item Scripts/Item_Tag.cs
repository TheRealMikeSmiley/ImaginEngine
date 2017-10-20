using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Sets an object to Item automatically if the object is untagged
 * Items need to be tagged Item for it to work correctly
 * 
 */

namespace ImaginationEngine {
	public class Item_Tag : MonoBehaviour {

		public string itemTag;

		void OnEnable() {
			SetTag ();
		}

		void SetTag() {
			if (itemTag == "") {
				itemTag = "Untagged";
			}

			transform.tag = itemTag;
		}
	}
}


