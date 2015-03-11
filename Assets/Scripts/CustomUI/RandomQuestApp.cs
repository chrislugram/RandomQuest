using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RandomQuestApp : RootApp {

	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
	#endregion
	
	#region METHODS_CUSTOM
	/// <summary>
	/// Inits the root app. Create diccionaries for UI states and initialized game subsystems
	/// </summary>
	protected override void InitRootApp (){
		PlayerPrefs.DeleteAll ();

		//Init Subsystems
		UserManager.Init ();
		TaskManager.Init ();
		LocalizationApp.Init ();
		
		//Init UI states
		states = new Dictionary<StateReferenceApp.TYPE_STATE, string> ();
		popupStates = new Dictionary<StateReferenceApp.POPUP_TYPE_STATE, string> ();
		
		//Add UI states
		states.Add (StateReferenceApp.TYPE_STATE.GAME, StateReferenceApp.GAME);
		states.Add (StateReferenceApp.TYPE_STATE.MAIN_MENU, StateReferenceApp.MAIN_MENU);
		
		//Load first UI state and scene
		ChangeState (currentTypeState, AppScenes.SCENE_MAIN_MENU);
	}
	#endregion
}
