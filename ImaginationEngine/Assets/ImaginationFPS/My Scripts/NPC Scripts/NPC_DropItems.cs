using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Enables NPC's to drop items when they die, set the item in the inspector
 * 
 */ 

namespace ImaginationEngine {

	public class NPC_DropItems : MonoBehaviour {

		private NPC_Master npcMaster;
		public GameObject[] itemsToDrop;

		void OnEnable() {
			SetInitialReferences ();
			npcMaster.EventNpcDie += DropItems; //Make an array so there can be any number of dropped items
		}

		void OnDisable() {
			npcMaster.EventNpcDie -= DropItems;

		}

		void SetInitialReferences() {
			npcMaster = GetComponent<NPC_Master> ();
		}

		void DropItems() {
			if (itemsToDrop.Length > 0) {
				foreach (GameObject item in itemsToDrop) {
					StartCoroutine (PauseBeforeDrop (item));
				}
			}
		}

		//Give time for script to be set up, prevents errors on the Item Master
		IEnumerator PauseBeforeDrop(GameObject itemToDrop) {
			yield return new WaitForSeconds (0.05f);
			itemToDrop.SetActive (true);
			itemToDrop.transform.parent = null;
			yield return new WaitForSeconds (0.05f);
			if (itemToDrop.GetComponent<Item_Master> () != null) { //Ensure dropped object is an item
				itemToDrop.GetComponent<Item_Master> ().CallEventObjectThrow (); //Make item drop upwards, looks better
			}
		}
	}
}
