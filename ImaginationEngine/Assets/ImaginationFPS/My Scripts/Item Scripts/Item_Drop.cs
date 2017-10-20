using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {
	public class Item_Drop : MonoBehaviour {

		private Item_Master itemMaster;
		public string dropButtonName;
		private Transform myTransform;

		// Use this for initialization
		void Start () {
			SetInitialReferences ();
		}
	
		// Update is called once per frame
		void Update () {
			CheckForDropInput ();
		}

		void SetInitialReferences() {
			itemMaster = GetComponent<Item_Master> ();
			myTransform = transform;
		}

		void CheckForDropInput() {
			if (Input.GetButtonDown (dropButtonName) && Time.timeScale > 0 && myTransform.root.CompareTag (GameManager_References._playerTag)) {
				myTransform.parent = null;
				itemMaster.CallEventObjectThrow ();
			}
		}
	}
}


