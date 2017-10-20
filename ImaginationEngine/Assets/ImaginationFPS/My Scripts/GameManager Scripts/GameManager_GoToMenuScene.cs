using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script allows us to go back to the first scene

namespace ImaginationEngine {
	public class GameManager_GoToMenuScene : MonoBehaviour {

		private GameManager_Master gameManagerMaster;

		void OnEnable() {
			SetInitialReferences ();
			gameManagerMaster.GoToMenuSceneEvent += GoToMenuScene;
		}

		void OnDisable() {
			gameManagerMaster.GoToMenuSceneEvent -= GoToMenuScene;
		}

		void SetInitialReferences() {
			gameManagerMaster = GetComponent<GameManager_Master> ();
		}

		void GoToMenuScene() {
			SceneManager.LoadScene (0); //Load very first scene, in this case menu
		}
	}
}
