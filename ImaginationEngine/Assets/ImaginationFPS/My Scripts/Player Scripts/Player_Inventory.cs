using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 
 * 
 * Inventory script, will hold all items picked up by the player and present them in list form in CanvasInventory
 * 
 * All items will be held under the FirstPersonCharacter Object, all items in the inventory will be deactivated, 
 * and the item of interest will be the only one activated, ensuring that inventory management is clear and two or more objects
 * cannot be active by accident
 * 
 * 
 */

namespace ImaginationEngine {
	public class Player_Inventory : MonoBehaviour {

		public Transform inventoryPlayerParent;
		public Transform inventoryUIParent;
		public GameObject uiButton;

		private Player_Master playerMaster;
		private GameManager_ToggleInventoryUI inventoryUIScript;
		private float timeToPlaceInHands = 0.1f;
		private Transform currentlyHeldItem;
		private int counter;
		private string buttonText;
		private List<Transform> listInventory = new List<Transform> ();

		void OnEnable() {
			SetInitialReferences ();
			DeactivateAllInventoryItems ();
			UpdateInventoryListAndUI ();
			CheckIfHandsEmpty ();

			playerMaster.EventInventoryChanged += UpdateInventoryListAndUI;
			playerMaster.EventInventoryChanged += CheckIfHandsEmpty;
			playerMaster.EventHandsEmpty += ClearHands;
		}

		void OnDisable() {
			playerMaster.EventInventoryChanged -= UpdateInventoryListAndUI;
			playerMaster.EventInventoryChanged -= CheckIfHandsEmpty;
			playerMaster.EventHandsEmpty -= ClearHands;
		}

		void SetInitialReferences() {
			inventoryUIScript = GameObject.Find ("GameManager").GetComponent<GameManager_ToggleInventoryUI> ();
			playerMaster = GetComponent<Player_Master> ();
		}

		//updates inventory
		void UpdateInventoryListAndUI() {
			counter = 0;
			listInventory.Clear ();
			listInventory.TrimExcess (); //if any empty entries, they will be removed

			ClearInventoryUI ();

			//Iterate through what the player is carrying
			foreach (Transform child in inventoryPlayerParent) {
				if (child.CompareTag ("Item")) {
					listInventory.Add (child);
					GameObject go = Instantiate (uiButton) as GameObject;
					buttonText = child.name;
					go.GetComponentInChildren<Text> ().text = buttonText;
					int index = counter;
					go.GetComponent<Button> ().onClick.AddListener (delegate {
						ActivateInventoryItem (index);
					});
					go.GetComponent<Button> ().onClick.AddListener (inventoryUIScript.ToggleInventoryUI); //closes ToggleInventory, which is more intuitive than manually closing it
					go.transform.SetParent (inventoryUIParent, false);
					counter++;
				}
			}
		}

		void CheckIfHandsEmpty() { //if an object is thrown or dropped, this will check and provide the next item in the list
			if (currentlyHeldItem == null && listInventory.Count > 0) { //check if hands are empty, and there are items in the list
				StartCoroutine (PlaceItemInHands (listInventory [listInventory.Count - 1])); //place an item from the list - the last item
			}
		}

		void ClearHands() {
			currentlyHeldItem = null;
		}

		void ClearInventoryUI() {
			foreach (Transform child in inventoryUIParent) {
				Destroy (child.gameObject);
			}
		}

		public void ActivateInventoryItem(int inventoryIndex) { //pass index value
			DeactivateAllInventoryItems ();	//deactivate everything
			StartCoroutine(PlaceItemInHands(listInventory[inventoryIndex])); //then the index value will be activated and show in player
		}

		//Deactivate all items carried by the player
		void DeactivateAllInventoryItems() {
			foreach (Transform child in inventoryPlayerParent) {
				if (child.CompareTag ("Item")) {	//if item carried in inventory has Layer "Item"
					child.gameObject.SetActive (false);	//deactivate it
				}
			}
		}

		IEnumerator PlaceItemInHands(Transform itemTransform) {
			yield return new WaitForSeconds (timeToPlaceInHands);
			currentlyHeldItem = itemTransform;
			currentlyHeldItem.gameObject.SetActive (true);
		}
	}
}


