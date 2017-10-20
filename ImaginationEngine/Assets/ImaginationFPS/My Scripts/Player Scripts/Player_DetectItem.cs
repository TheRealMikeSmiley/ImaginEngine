using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {
	public class Player_DetectItem : MonoBehaviour {

		//Checks for the layer, keeps item object from colliding with other objects

		[Tooltip("What layer is being used for items.")] //Tooltip provides a label for each public item
		public LayerMask layerToDetect;

		[Tooltip("What transform will the ray be fired from?")] //the ray will fire from the players camera
		public Transform rayTransformPivot;	// In this case the FirstPersonCharacter

		[Tooltip("The editor input button that will be used for picking up items.")] //Leave public so the button is easy to change
		public string buttonPickup;

		private Transform itemAvailableForPickup;
		private RaycastHit hit; // receive information from where the sphere ray hits
		private float detectRange = 3; 
		private float detectRadius = 0.7f; // makes the item easier to pickup in sphere rather than pinpoint 
		private bool itemInRange; 

		// Show onscreen that item can be picked up
		private float labelWidth = 200;
		private float labelHeight = 50;

		// Update is called once per frame
		void Update () {
			CastRayForDetectingItems ();
			CheckForItemPickupAttempt ();
		}

		// if sphere (from player camera) hits an item (in item layer), get information 
		// if sphere doesn't hit anything, show nothing
		void CastRayForDetectingItems(){
			if (Physics.SphereCast (rayTransformPivot.position, detectRadius, rayTransformPivot.forward, out hit, detectRange, layerToDetect)) {
				itemAvailableForPickup = hit.transform;
				itemInRange = true;
			} else {
				itemInRange = false;
			}
		}

		//
		void CheckForItemPickupAttempt(){
			if (Input.GetButtonDown (buttonPickup) && Time.timeScale > 0 && itemInRange && itemAvailableForPickup.root.tag != GameManager_References._playerTag) {
				//Debug.Log ("Pickup Attempted"); //check for pickup, will add pickup event once item module complete
				itemAvailableForPickup.GetComponent<Item_Master>().CallEventPickupAction(rayTransformPivot); //pickup event completed Oct 15 2017
			}
		}

		// If an item is available to pick up, show label
		void OnGUI(){
			if (itemInRange && itemAvailableForPickup != null) {
				GUI.Label(new Rect(Screen.width/2-labelWidth/2, Screen.height/2, labelWidth,labelHeight), itemAvailableForPickup.name);
			}
		}

	}
}


