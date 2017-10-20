using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace ImaginationEngine {
	public class GameManager_TogglePlayer : MonoBehaviour {

		public FirstPersonController playerController;
		private GameManager_Master gameManagerMaster;

		void OnEnable() {
			SetInitialReferences ();
			gameManagerMaster.MenuToggleEvent += TogglePlayerController;
			gameManagerMaster.InventoryUIToggleEvent += TogglePlayerController;
		}

		void OnDisable() {
			gameManagerMaster.MenuToggleEvent -= TogglePlayerController;
			gameManagerMaster.InventoryUIToggleEvent -= TogglePlayerController;
		}

		void SetInitialReferences() {
			gameManagerMaster = GetComponent<GameManager_Master> ();
		}

		void TogglePlayerController() {
			if (playerController != null) { //check for player controller
				playerController.enabled = !playerController.enabled;
			}
		}
	}
}


