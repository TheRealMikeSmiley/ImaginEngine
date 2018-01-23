using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImaginationEngine {
	public class NPC_Master : MonoBehaviour {

		public delegate void GeneralEventHandler();
		public event GeneralEventHandler EventNpcDie; //If health falls below 0
		public event GeneralEventHandler EventNpcLowHealth; //Start low health behaviour - Fleeing
		public event GeneralEventHandler EventNpcHealthRecovered; //Fleeing ends
		public event GeneralEventHandler EventNpcWalkAnim; //Start Animations
		public event GeneralEventHandler EventNpcStruckAnim;
		public event GeneralEventHandler EventNpcAttackAnim;
		public event GeneralEventHandler EventNpcRecoveredAnim;
		public event GeneralEventHandler EventNpcIdleAnim;

		public delegate void HealthEventHandler(int health);
		public event HealthEventHandler EventNpcDeductHealth;
		public event HealthEventHandler EventNpcIncreaseHealth;

		//For animation
		public string animationBoolPersuing = "isPersuing";
		public string animationTriggerStruck = "Struck";
		public string animationTriggerMelee = "Attack";
		public string animationTriggerRecovered = "Recovered";


		public void CallEventNpcDie(){
			if (EventNpcDie != null) { //Check if this parameter exists before starting event
				EventNpcDie (); //Call event
			}
		}

		public void CallEventNpcLowHealth(){
			if (EventNpcLowHealth != null) {
				EventNpcLowHealth ();
			}
		}


		public void CallEventNpcHealthRecovered() {
			if (EventNpcHealthRecovered != null) {
				EventNpcHealthRecovered ();
			}
		}

		public void CallEventNpcWalkAnim() {
			if (EventNpcWalkAnim != null) {
				EventNpcWalkAnim ();
			}
		}

		public void CallEventNpcStruckAnim() {
			if (EventNpcStruckAnim != null) {
				EventNpcStruckAnim ();
			}
		}

		public void CallEventNpcAttackAnim() {
			if (EventNpcAttackAnim != null) {
				EventNpcAttackAnim ();
			}
		}
			

		public void CallEventNpcRecoveredAnim() {
			if (EventNpcRecoveredAnim != null) {
				EventNpcRecoveredAnim ();
			}
		}

		public void CallEventNpcIdleAnim() {
			if (EventNpcIdleAnim != null) {
				EventNpcIdleAnim ();
			}
		}

		public void CallEventNpcDeductHealth(int health) { //Takes in health parameter, otherwise similar to previous events
			if (EventNpcDeductHealth != null) {
				EventNpcDeductHealth (health);
			}
		}

		public void CallEventNpcIncreaseHealth(int health) {
			if (EventNpcIncreaseHealth != null) {
				EventNpcIncreaseHealth (health);
			}
		}
	}
}


