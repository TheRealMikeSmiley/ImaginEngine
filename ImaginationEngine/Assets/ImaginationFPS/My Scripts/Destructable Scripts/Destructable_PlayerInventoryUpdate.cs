using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Fixes bug where if the barrel blows up while in the players hands, it stays in players inventory
 * This script updates the inventory, and will ensure destructible is removed from inventory once it explodes
 * 
 */ 

namespace ImaginationEngine {
	public class Destructable_PlayerInventoryUpdate : MonoBehaviour {

		private Destructable_Master destructableMaster;
		private Player_Master playerMaster;

		void OnEnable() {
			SetInitialReferences ();
			destructableMaster.EventDestroyMe += ForcePlayerInventoryUpdate;
		}

		void OnDisable() {
			destructableMaster.EventDestroyMe -= ForcePlayerInventoryUpdate;
		}
		
		// Use this for initialization
		void Start () {
			SetInitialReferences ();
		}

		void SetInitialReferences() {
			if (GetComponent<Item_Master> () == null) {
				Destroy (this);
			}

			if (GameManager_References._player != null) {
				playerMaster = GameManager_References._player.GetComponent<Player_Master> ();
			}

			destructableMaster = GetComponent<Destructable_Master> ();
		}

		void ForcePlayerInventoryUpdate() {
			if (playerMaster != null) {
				playerMaster.CallEventInventoryChanged ();
			}
		}
	}
}


