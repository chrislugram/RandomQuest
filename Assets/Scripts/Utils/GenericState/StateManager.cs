using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class StateManager : MonoBehaviour {

	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public GameObject[]	statesGO;
	public GameObject[] popupStatesGO;
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
	void Awake(){
		if (RootApp.Instance != null){
			//Add states
			for(int i=0; i<statesGO.Length; i++){
				RootApp.Instance.AddStateGO(statesGO[i]);
			}

			//Add popup states
			for (int i=0; i<popupStatesGO.Length; i++){
				RootApp.Instance.AddPopupStateGO(popupStatesGO[i]);
			}
		}else{
			Console.Error(" Root null");
		}
	}
	#endregion
	
	#region METHODS_CUSTOM
	#endregion
}