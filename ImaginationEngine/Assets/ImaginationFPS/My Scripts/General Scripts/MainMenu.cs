using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script enables the buttons to load the next level

namespace ImaginationEngine {
	public class MainMenu : MonoBehaviour {
		
		public void PlayGame() {
			SceneManager.LoadScene (1); //Loads next scene 
		}

		public void ExitGame() {
			Application.Quit (); //Close game
		}
	}
}


