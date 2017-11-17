using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * This script will handle the dynamic crosshairs
 * The variables have been set in the animator
 * Depending on the player speed will dictate if crosshairs become smaller or larger
 */ 

namespace ImaginationEngine {
	public class Gun_DynamicCrosshair : MonoBehaviour {

		private Gun_Master gunMaster;
		public Transform canvasDynamicCrosshair;
		private Transform playerTransform;
		private Transform weaponCamera;
		private float playerSpeed;
		private float nextCaptureTime;
		private float captureInterval = 0.5f;
		private Vector3 lastPosition;
		public Animator crosshairAnimator;
		public string weaponCameraName;
		
		// Use this for initialization
		void Start () {
			SetInitialReferences ();
		
		}
	
		// Update is called once per frame
		void Update () {
			CapturePlayerSpeed ();
			ApplySpeedToAnimation ();
		}

		void SetInitialReferences() {
			gunMaster = GetComponent<Gun_Master> ();
			playerTransform = GameManager_References._player.transform;
			FindWeaponCamera (playerTransform);
			SetCameraOnDynamicCrosshairCanvas ();
			SetPlaneDistanceOnDynamicCrosshairCanvas ();
		}

		//Check and save the players current speed
		void CapturePlayerSpeed(){
			if (Time.time > nextCaptureTime) {
				nextCaptureTime = Time.time + captureInterval; //set interval in script, change may not be required
				playerSpeed = (playerTransform.position - lastPosition).magnitude / captureInterval;
				lastPosition = playerTransform.position;
				gunMaster.CallEventSpeedCaptured (playerSpeed); //sets firing source at a difference location
			}
		}

		//Compare captured speed to animator
		void ApplySpeedToAnimation(){
			if (crosshairAnimator != null) {
				crosshairAnimator.SetFloat ("Speed", playerSpeed); //Speed named in animator
			}
		}
			
		void FindWeaponCamera(Transform transformToSearchThrough) {
			if (transformToSearchThrough != null) {
				if (transformToSearchThrough.name == weaponCameraName) {
					weaponCamera = transformToSearchThrough;
					return;
				}

				foreach (Transform child in transformToSearchThrough) {
					FindWeaponCamera (child);
				}
			}
		}
			
		void SetCameraOnDynamicCrosshairCanvas() {
			if (canvasDynamicCrosshair != null && weaponCamera != null) {
				canvasDynamicCrosshair.GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceCamera;
				canvasDynamicCrosshair.GetComponent<Canvas> ().worldCamera = weaponCamera.GetComponent<Camera> ();
			}
		}

		void SetPlaneDistanceOnDynamicCrosshairCanvas(){
			if (canvasDynamicCrosshair != null) {
				canvasDynamicCrosshair.GetComponent<Canvas> ().planeDistance = 1;
			}
		}
	}
}


