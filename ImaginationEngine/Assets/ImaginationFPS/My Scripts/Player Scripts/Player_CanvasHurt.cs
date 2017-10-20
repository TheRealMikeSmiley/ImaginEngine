using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {
	public class Player_CanvasHurt : MonoBehaviour {

		public GameObject hurtCanvas;
		private Player_Master playerMaster;
		private float secondsTillHide = 2; // how long hurt image will stay - currently 2 seconds

		void OnEnable() {
			SetInitialReferences ();
			playerMaster.EventPlayerHealthDeduction += TurnOnHurtEffect;
		}

		void OnDisable() {
			playerMaster.EventPlayerHealthDeduction -= TurnOnHurtEffect;
		}

		void SetInitialReferences() {
			playerMaster = GetComponent<Player_Master> ();
		}

		void TurnOnHurtEffect(int dummy) {
			if (hurtCanvas != null) {
				StopAllCoroutines ();
				hurtCanvas.SetActive (true);
				StartCoroutine (ResetHurtCanvas ());
			}
		}

		IEnumerator ResetHurtCanvas() { // Coroutine to disable canvas
			yield return new WaitForSeconds (secondsTillHide);
			hurtCanvas.SetActive (false);
		}
	}
}


