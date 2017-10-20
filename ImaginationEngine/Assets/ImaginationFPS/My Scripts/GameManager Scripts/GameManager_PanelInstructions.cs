using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script ensures that the game over and instruction panels 
//don't collide with each other

namespace ImaginationEngine {
	public class GameManager_PanelInstructions : MonoBehaviour {

		public GameObject panelInstructions;
		private GameManager_Master gameManagerMaster;

		void OnEnable() {
			SetInitialReferences ();
			gameManagerMaster.GameOverEvent += TurnOffPanelInstructions;
		}

		void OnDisable() {
			gameManagerMaster.GameOverEvent -= TurnOffPanelInstructions;
		}
		
		void SetInitialReferences() {
			gameManagerMaster = GetComponent<GameManager_Master> ();
		}

		void TurnOffPanelInstructions() {
			if (panelInstructions != null) { //check if exists first
				panelInstructions.SetActive (false);
			}
		}
	}
}


