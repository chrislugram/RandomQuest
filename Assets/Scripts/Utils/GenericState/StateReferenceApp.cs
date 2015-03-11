using UnityEngine;
using System.Collections;

/// <summary>
/// State reference app. This class must be redefined for each project
/// </summary>
public class StateReferenceApp {

	#region STATIC_ENUM_CONSTANTS
	public static readonly string	GAME						= "GameState";
	public static readonly string	MAIN_MENU					= "MainMenuState";

	public static readonly string	POPUP_ALERT					= "PopupErrorState";
	
	public enum TYPE_STATE{
		GAME						= 0,
		MAIN_MENU					= 1
	}

	public enum POPUP_TYPE_STATE{
		POPUP_ALERT					= 0,
	}
	#endregion
	
	#region FIELDS
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
	#endregion
	
	#region METHODS_CUSTOM
	#endregion
}
