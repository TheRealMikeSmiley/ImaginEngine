using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

namespace ImaginationEngine {

	public class GameManager_ToggleInventoryUI : MonoBehaviour {

		//Tooltip shows a message in the inspector
		[Tooltip("Does this game mode have an inventory? Set to true if yes")]
		public bool hasInventory;
		public GameObject inventoryUI;
		public string toggleInventoryButton;
		private GameManager_Master gameManagerMaster;

		// Use this for initialization
		void Start () {
			SetInitialReferences ();
		}

		// Update is called once per frame
		void Update () {
			CheckForInventoryUIToggleRequest ();
		}

		void SetInitialReferences(){
			gameManagerMaster = GetComponent<GameManager_Master> ();

			//Check if button has been set, show warning if not
			if (toggleInventoryButton == "") {
				Debug.LogWarning ("Please type name of button used to toggle inventory in GameManager_ToggleInventoryUI");
				this.enabled = false;
			}
		}

		//Checks for button press to toggle Inventory
		void CheckForInventoryUIToggleRequest() {
#if !MOBILE_INPUT         
            //Check for button, if menu is already showing, inventory exists and there is not a game over
            if (Input.GetButtonUp (toggleInventoryButton) && !gameManagerMaster.isMenuOn && !gameManagerMaster.isGameOver && hasInventory) {
				ToggleInventoryUI ();
            }
#endif
            if (CrossPlatformInputManager.GetButtonDown("Inventory") && !gameManagerMaster.isMenuOn && !gameManagerMaster.isGameOver && hasInventory)
            {
                ToggleInventoryUI();
            }
        }

        //Locks cursor, pauses game
        public void ToggleInventoryUI(){
			if (inventoryUI != null) { //checks if assigned
				inventoryUI.SetActive (!inventoryUI.activeSelf);
				gameManagerMaster.isInventoryUIOn = !gameManagerMaster.isInventoryUIOn;
				gameManagerMaster.CallEventInventoryUIToggle ();
			}
		}
	}
}
