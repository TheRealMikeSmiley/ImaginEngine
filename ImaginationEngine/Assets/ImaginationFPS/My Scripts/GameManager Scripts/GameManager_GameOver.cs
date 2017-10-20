using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Keeps track of gameover event, game will pause and turn on Gameover panel

namespace ImaginationEngine {
	public class GameManager_GameOver : MonoBehaviour {

		private GameManager_Master gameManagerMaster;
		public GameObject panelGameOver;

		void OnEnable(){
			SetInitialReferences ();
			gameManagerMaster.GameOverEvent += TurnOnGameOverPanel;
		}

		void OnDisable(){
			gameManagerMaster.GameOverEvent -= TurnOnGameOverPanel;
		}

		void SetInitialReferences(){
			gameManagerMaster = GetComponent<GameManager_Master> ();
		}

		void TurnOnGameOverPanel(){
			if (panelGameOver != null) {
				panelGameOver.SetActive (true);
			}
		}
	}
}


