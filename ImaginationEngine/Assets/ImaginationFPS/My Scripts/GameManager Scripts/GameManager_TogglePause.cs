using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {

	public class GameManager_TogglePause : MonoBehaviour {

		private GameManager_Master gameManagerMaster; //reference GameManager Master script
		private bool isPaused;

		void OnEnable(){
			SetInitialReferences ();
			gameManagerMaster.MenuToggleEvent += TogglePause; //have method subscribed to gameManagerMaster
			gameManagerMaster.InventoryUIToggleEvent += TogglePause; 
			//By doing this, whenever the inventory or pause button is pressed, the game will be paused
		}

		void OnDisable(){
			gameManagerMaster.MenuToggleEvent -= TogglePause; //unsubscribed
			gameManagerMaster.InventoryUIToggleEvent -= TogglePause;
		}

		void SetInitialReferences() {
			gameManagerMaster = GetComponent<GameManager_Master> ();
		}

		void TogglePause() {
			if (isPaused) {
				Time.timeScale = 1; //used to measure time in game, value of 1 means normal speed
				isPaused = false;
			} else {
				Time.timeScale = 0; //Nothing moves
				isPaused = true;
			}
		}
	}
}
