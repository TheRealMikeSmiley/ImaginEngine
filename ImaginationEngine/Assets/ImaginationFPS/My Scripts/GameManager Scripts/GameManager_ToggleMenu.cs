using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {

	public class GameManager_ToggleMenu : MonoBehaviour {

		private GameManager_Master gameManagerMaster;
		public GameObject menu;

		// Use this for initialization
		void Start () {
			ToggleMenu (); //Turns on menu when game starts, may remove
		}
	
		// Update is called once per frame
		void Update () {
			CheckForMenuToggleRequest ();
		}

		void OnEnable() {
			SetInitialReferences ();
			gameManagerMaster.GameOverEvent += ToggleMenu; //if player is destroyed, bring up menu
		}

		void OnDisable() {

		}

		void SetInitialReferences() {
			gameManagerMaster = GetComponent<GameManager_Master> ();
		}

		void CheckForMenuToggleRequest() {
			//Check for button press, and make sure gameover and inventory screens are both off
			if (Input.GetKeyUp (KeyCode.Escape) && !gameManagerMaster.isGameOver && !gameManagerMaster.isInventoryUIOn) {
				ToggleMenu ();
			}
		}

		void ToggleMenu() {
			if (menu != null) { //make sure menu exists
				menu.SetActive(!menu.activeSelf); //if deactivated it will be activated, and vice versa
				gameManagerMaster.isMenuOn = !gameManagerMaster.isMenuOn;
				gameManagerMaster.CallEventMenuToggle ();
			} else {
				Debug.LogWarning("You need to assign a UI GameObject to the ToggleMenu script in the inspector");
			}
		}
	}
}
