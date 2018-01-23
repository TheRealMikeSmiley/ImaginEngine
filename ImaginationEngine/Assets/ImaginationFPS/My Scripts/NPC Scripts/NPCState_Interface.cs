using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Use a state machine to handle each state

namespace ImaginationEngine {
	public interface NPCState_Interface { //Interface, does not inherit from monobehavior

		//Each script that derives from this interface and must have these methods inside
		//Each of these scripts enable a specific behavior, while one state runs, none of the others will run
		//More behaviors can be added, first here, then update each NPCState script

		void UpdateState();
		void ToPatrolState();
		void ToAlertState();
		void ToPersueState();
		void ToMeleeAttackState();
		void ToRangeAttackState();

	}
}


