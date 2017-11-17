using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Contains all important information regarding gun ammo
 * This includes deducting ammo when shooting, checking ammo status, handling gun reloading
 * 
 */ 

namespace ImaginationEngine {
	public class Gun_Ammo : MonoBehaviour {

		private Player_Master playerMaster;
		private Gun_Master gunMaster;
		private Player_AmmoBox ammoBox;
		private Animator myAnimator;

		public int clipSize;
		public int currentAmmo;
		public string ammoName;
		public float reloadTime;

		void OnEnable() {
			SetInitialReferences ();
			StartingSanityCheck ();
			CheckAmmoStatus ();

			gunMaster.EventPlayerInput += DeductAmmo;
			gunMaster.EventPlayerInput += CheckAmmoStatus;
			gunMaster.EventRequestReload += TryToReload;
			gunMaster.EventGunNotUsable += TryToReload;
			gunMaster.EventRequestGunReset += ResetGunReloading;

			if (playerMaster != null) { //Needs to be here
				playerMaster.EventAmmoChanged += UIAmmoUpdateRequest;
			}

			if (ammoBox != null) {
				StartCoroutine (UpdateAmmoUIWhenEnabling ());
			}
		}

		void OnDisable() {
			gunMaster.EventPlayerInput -= DeductAmmo;
			gunMaster.EventPlayerInput -= CheckAmmoStatus;
			gunMaster.EventRequestReload -= TryToReload;
			gunMaster.EventGunNotUsable -= TryToReload;
			gunMaster.EventRequestGunReset -= ResetGunReloading;

			if (playerMaster != null) {
				playerMaster.EventAmmoChanged -= UIAmmoUpdateRequest;
			}
		}
		
		// Use this for initialization
		void Start () {
			SetInitialReferences ();
			StartCoroutine (UpdateAmmoUIWhenEnabling ());

			if (playerMaster != null) { //And here to avoid showing as null during initial runtime
				playerMaster.EventAmmoChanged += UIAmmoUpdateRequest;
			}
		}

		void SetInitialReferences() {
			gunMaster = GetComponent<Gun_Master> ();

			if (GetComponent<Animator> () != null) {
				myAnimator = GetComponent<Animator> ();
			}

			if (GameManager_References._player != null) {
				playerMaster = GameManager_References._player.GetComponent<Player_Master> ();
				ammoBox = GameManager_References._player.GetComponent<Player_AmmoBox> ();
			}
		}

		//Remove ammo, update UI
		void DeductAmmo(){
			currentAmmo--;
			UIAmmoUpdateRequest ();
		}

		//Starts reload animation, the animation will complete reload action
		void TryToReload(){
			for (int i = 0; i < ammoBox.typesOfAmmunition.Count; i++) {
				if (ammoBox.typesOfAmmunition [i].ammoName == ammoName) {
					//check if there is ammo available for reload, if current clip is full, and if already reloading
					if (ammoBox.typesOfAmmunition [i].ammoCurrentCarried > 0 && currentAmmo != clipSize && !gunMaster.isReloading) {
						gunMaster.isReloading = true;
						gunMaster.isGunLoaded = false;

						if (myAnimator != null) {
							myAnimator.SetTrigger ("Reload"); //Trigger set in animator
						} else {
							StartCoroutine (ReloadWithoutAnimation ()); //Perform animation-less reload
						}
					}
					break;
				}
			}
		}

		void CheckAmmoStatus(){
			if (currentAmmo <= 0) {
				currentAmmo = 0;
				gunMaster.isGunLoaded = false; //checks ammo amount, will not fire if at zero
			} else if (currentAmmo > 0) {
				gunMaster.isGunLoaded = true; //gun can continue to fire
			}
		}

		//If ammo is set higher than clip size in the inspector, adjust clip size to correct amount at runtime
		void StartingSanityCheck(){
			if (currentAmmo > clipSize) {
				currentAmmo = clipSize;
			}
		}

		//This will be called by another script
		void UIAmmoUpdateRequest(){
			for (int i = 0; i < ammoBox.typesOfAmmunition.Count; i++) {
				if (ammoBox.typesOfAmmunition [i].ammoName == ammoName) {
					gunMaster.CallEventAmmoChanged (currentAmmo, ammoBox.typesOfAmmunition [i].ammoCurrentCarried);
					break;
				}
			}
		}

		//Once reloading has completed, gun is no longer considered reloading (and can be reloaded again)
		void ResetGunReloading(){
			gunMaster.isReloading = false;
			CheckAmmoStatus ();
			UIAmmoUpdateRequest ();
		}

		public void OnReloadComplete() { //This event is called by reload animation
			//Attempt to add ammo to current ammo
			for (int i = 0; i < ammoBox.typesOfAmmunition.Count; i++) {
				if (ammoBox.typesOfAmmunition [i].ammoName == ammoName) {
					int ammoTopUp = clipSize - currentAmmo;

					//check ammobox, then add ammo to clip to max it out
					if (ammoBox.typesOfAmmunition [i].ammoCurrentCarried >= ammoTopUp) {
						currentAmmo += ammoTopUp;
						ammoBox.typesOfAmmunition [i].ammoCurrentCarried -= ammoTopUp; //subtract amount taken from ammo box
					}

					//assign the rest of the ammo to the clip, set the ammobox to zero
					else if (ammoBox.typesOfAmmunition [i].ammoCurrentCarried < ammoTopUp && ammoBox.typesOfAmmunition [i].ammoCurrentCarried != 0) {
						currentAmmo += ammoBox.typesOfAmmunition [i].ammoCurrentCarried;
						ammoBox.typesOfAmmunition [i].ammoCurrentCarried = 0;
					}

					break; //jump out of for-loop
				}
			}

			ResetGunReloading ();
		}

		IEnumerator ReloadWithoutAnimation(){ //In case there is no animation attached
			yield return new WaitForSeconds (reloadTime);
			OnReloadComplete ();
		}

		IEnumerator UpdateAmmoUIWhenEnabling() {
			yield return new WaitForSeconds (0.05f); //fudge factor to ensure UI is updated when changing weapons
			UIAmmoUpdateRequest();
		}
	}
}


