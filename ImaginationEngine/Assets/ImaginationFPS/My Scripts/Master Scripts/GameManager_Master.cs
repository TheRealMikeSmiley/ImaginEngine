using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {
	
	public class GameManager_Master : MonoBehaviour {

		//Set up event handlers
		//Other scripts will call this script during certain events

		public delegate void GameManagerEventHandler();

		//each event will use this event handler
		public event GameManagerEventHandler MenuToggleEvent;
		public event GameManagerEventHandler InventoryUIToggleEvent;
		public event GameManagerEventHandler RestartLevelEvent;
		public event GameManagerEventHandler GoToMenuSceneEvent;
		public event GameManagerEventHandler GameOverEvent;

		public bool isGameOver;
		public bool isInventoryUIOn;
		public bool isMenuOn;

		public void CallEventMenuToggle() {
			if (MenuToggleEvent != null) { //check if this event exists
				MenuToggleEvent(); //MenuToggleEvent will happen
			}
		}

		public void CallEventInventoryUIToggle() {
			if (InventoryUIToggleEvent != null) {
				InventoryUIToggleEvent ();
			}
		}

		public void CallEventRestartLevel() {
			if (RestartLevelEvent != null) {
				RestartLevelEvent ();
			}
		}

		public void CallEventGoToMenuScene() {
			if (GoToMenuSceneEvent != null) {
				GoToMenuSceneEvent ();
			}
		}

		public void CallEventGameOver() {
			if (GameOverEvent != null) {
				isGameOver = true;
				GameOverEvent ();
			}
		}
	}
}
