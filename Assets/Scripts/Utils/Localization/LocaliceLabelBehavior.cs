using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class LocaliceLabelBehavior : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	#endregion

	#region FIELDS
	public string 	key;

	private Text	textUI;
	#endregion

	#region ACCESSORS
	#endregion

	#region CONSTRUCTORS
	#endregion

	#region METHODS_UNITY
	// Use this for initialization. 
	// This is called only once, as soon as the GameObject is created
	private void Awake() {
		LocalizationApp.onChangeLanguage += ChangeLanguageAction;
		
		UpdateKey(key);
	}

	private void OnDestroy(){
		LocalizationApp.onChangeLanguage -= ChangeLanguageAction;
	}
	#endregion

	#region METHODS_CUSTOM
	public void UpdateKey(string key){
		this.key = key;

		ChangeLanguageAction ();
	}

	//Create specific change in new Unity UI
	private void ChangeLanguageAction(){
		if (textUI == null){
			textUI = GetComponent<Text> ();
		}
		
		textUI.text = LocalizationApp.TextApp (key);
	}
	#endregion
}
