using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {
	public class GameManager_ToggleCursor : MonoBehaviour {

		private GameManager_Master gameManagerMaster;
		private bool isCursorLocked = true;

		void OnEnable() {
			SetInitialReferences ();
			gameManagerMaster.MenuToggleEvent += ToggleCursorState;
			gameManagerMaster.InventoryUIToggleEvent += ToggleCursorState;
		}

		void OnDisable() {
			gameManagerMaster.MenuToggleEvent -= ToggleCursorState;
			gameManagerMaster.InventoryUIToggleEvent -= ToggleCursorState;
		}

		// Update is called once per frame
		void Update () {
			CheckIfCursorShouldBeLocked ();
		}

		void SetInitialReferences(){
			gameManagerMaster = GetComponent<GameManager_Master> ();
		}

		void ToggleCursorState() { //toggle cursor lock
			isCursorLocked = !isCursorLocked;
		}

		void CheckIfCursorShouldBeLocked(){
			if (isCursorLocked) { //if locked, lock and hide cursor
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			} else { //otherwise unlock and show it
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}
	}
}


