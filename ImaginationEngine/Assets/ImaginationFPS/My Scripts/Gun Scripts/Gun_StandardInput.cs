using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

/*
 * 
 * Check for Input
 * 
 */ 

namespace ImaginationEngine {
	public class Gun_StandardInput : MonoBehaviour {

		private Gun_Master gunMaster;
		private float nextAttack;
		public float attackRate = 0.5f;
		private Transform myTransform;
		public bool isAutomatic;
		public bool hasBurstFire;
		private bool isBurstFireActive;
		public string attackButtonName;
		public string reloadButtonName;
		public string burstFireButtonName;

		// Use this for initialization
		void Start () {
			SetInitialReferences();
		}
	
		// Update is called once per frame
		void Update () {
			CheckIfWeaponShouldAttack ();
			CheckForBurstFireToggle ();
			CheckForReloadRequest ();
		}

		void SetInitialReferences() {
			gunMaster = GetComponent<Gun_Master> ();
			myTransform = transform;
			gunMaster.isGunLoaded = true; //Player can shoot right away
		}

		void CheckIfWeaponShouldAttack() {
			if (Time.time > nextAttack && Time.timeScale > 0 && myTransform.root.CompareTag (GameManager_References._playerTag)) { //check if weapon is on player 
				if (isAutomatic && !isBurstFireActive) { //check if automatic, burst fire not enabled
#if !MOBILE_INPUT
                    if (Input.GetButton (attackButtonName)) { //can hold button down to keep shooting
						//Debug.Log ("Full Auto");
						AttemptAttack ();
					}
#endif
                    if (CrossPlatformInputManager.GetButtonDown("Shoot"))
                    { //can hold button down to keep shooting
                      //Debug.Log ("Full Auto");
                        AttemptAttack();
                    }
                } else if (isAutomatic && isBurstFireActive) { //check if automatic, and burst fire enabled
#if !MOBILE_INPUT
                    if (Input.GetButtonDown (attackButtonName)) { // must press button each time to shoot
						//Debug.Log ("Burst");
						StartCoroutine (RunBurstFire ());
					}
#endif
                    if (CrossPlatformInputManager.GetButtonDown("Shoot"))
                    { // must press button each time to shoot
                      //Debug.Log ("Burst");
                        StartCoroutine(RunBurstFire());
                    }
                } else if (!isAutomatic) { //check if not automatic
#if !MOBILE_INPUT
                    if (Input.GetButtonDown (attackButtonName)) { //must press button each time to shoot
						AttemptAttack ();
					}
#endif
                    if (CrossPlatformInputManager.GetButtonDown("Shoot"))
                    { //must press button each time to shoot
                        AttemptAttack();
                    }
                }
			}
		}

		//Check attack rate, ammo amount, and if loaded
		void AttemptAttack(){
			nextAttack = Time.time + attackRate;

			if (gunMaster.isGunLoaded) {
				//Debug.Log ("Shooting");
				gunMaster.CallEventPlayerInput (); //gun passes checks
			} else {
				gunMaster.CallEventGunNotUsable (); //if gun is not loaded, can add clicking sound effect here
			}
		}

		//Check for reload button press
        //For Mobile We will just reload when we are out of ammo <- will look at implementing this for the beta
		void CheckForReloadRequest(){
			if (Input.GetButtonDown (reloadButtonName) && Time.timeScale > 0 && myTransform.root.CompareTag (GameManager_References._playerTag)) {
				gunMaster.CallEventRequestReload ();
			}
		}

        //Check if burst fire button is pressed
        //For Mobile We will just have it so we do not need to switch burst <- will look at implementing this for the beta
        void CheckForBurstFireToggle(){
			if (Input.GetButtonDown (burstFireButtonName) && Time.timeScale > 0 && myTransform.root.CompareTag (GameManager_References._playerTag)) {
				//Debug.Log ("Burst Fire Toggled");
				isBurstFireActive = !isBurstFireActive; 
				gunMaster.CallEventToggleBurstFire ();
			}
		}

		//Shoot burst of three shots after one click
		IEnumerator RunBurstFire(){
			AttemptAttack ();
			yield return new WaitForSeconds (attackRate);
			AttemptAttack ();
			yield return new WaitForSeconds (attackRate);
			AttemptAttack ();
		}
	}
}


