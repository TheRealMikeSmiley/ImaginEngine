using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Keep track of object references, such as enemy and player global tags

This consolidates the references into one script so other scripts
do not need to have reference checks

Global tags are accessible by all scripts and cannot be changed within
the inspector

*/

namespace ImaginationEngine {
	public class GameManager_References : MonoBehaviour {

		//Set global variables - can only have one 	

		public string playerTag;
		public static string _playerTag;

		public string enemyTag;
		public static string _enemyTag;

		public static GameObject _player;

		void OnEnable() {	//Set on enable so this script runs immediatly
			
			//check for empty references
			if (playerTag == "") {
				Debug.LogWarning ("Please type in the name of the player tag in the GameManagerReferences inspector slot");
			}

			if (enemyTag == "") {
				Debug.LogWarning ("Please type in the name of the enemy tag in the GameManagerReferences inspector slot");
			}

			//Set object references

			_playerTag = playerTag;
			_enemyTag = enemyTag;

			_player = GameObject.FindGameObjectWithTag (_playerTag);
		}
			
	}
}


