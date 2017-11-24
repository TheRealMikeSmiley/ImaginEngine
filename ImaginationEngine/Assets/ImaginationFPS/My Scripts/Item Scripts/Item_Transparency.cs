using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Changes material of an item to transparent when picked up
 * 		This way an item can be carried, but the player can still see through the object
 * 
 */ 

namespace ImaginationEngine {
	public class Item_Transparency : MonoBehaviour {

		private Item_Master itemMaster;
		public Material transparentMat;
		private Material primaryMat;

		void OnEnable() {
			SetInitialReferences ();
			itemMaster.EventObjectPickup += SetToTransparentMaterial;
			itemMaster.EventObjectThrow += SetToPrimaryMaterial;
		}

		void OnDisable() {
			itemMaster.EventObjectPickup -= SetToTransparentMaterial;
			itemMaster.EventObjectThrow -= SetToPrimaryMaterial;
		}
		
		// Use this for initialization
		void Start () {
			CaptureStartingMaterial ();
		}

		void SetInitialReferences() {
			itemMaster = GetComponent<Item_Master> ();
		}
			
		void CaptureStartingMaterial() {
			primaryMat = GetComponent<Renderer> ().material;
		}

		void SetToPrimaryMaterial() {
			GetComponent<Renderer> ().material = primaryMat;
		}

		void SetToTransparentMaterial() {
			GetComponent<Renderer> ().material = transparentMat;
		}
	}
}