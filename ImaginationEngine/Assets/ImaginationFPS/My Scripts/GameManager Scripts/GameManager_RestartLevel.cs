using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Restarts level, when restart button pressed in pause menu
//Attach to GameManager, then attach object to ButtonRestart
//Call event from GameManager master - CallEventRestartLevel

namespace ImaginationEngine {
	public class GameManager_RestartLevel : MonoBehaviour {

		private GameManager_Master gameManagerMaster;

		void OnEnable(){
			SetInitialReferences ();
			gameManagerMaster.RestartLevelEvent += RestartLevel;
		}

		void OnDisable(){
			gameManagerMaster.RestartLevelEvent -= RestartLevel;
		}

		void SetInitialReferences(){
			gameManagerMaster = GetComponent<GameManager_Master> ();
		}

		//This reads what level the user is currently in and restarts there
		void RestartLevel(){
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		}
	}
}


