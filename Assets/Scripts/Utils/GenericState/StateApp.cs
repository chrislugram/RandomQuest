using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class StateApp : MonoBehaviour {

	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	protected RootApp	rootApp;
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
	void Awake(){
		rootApp = RootApp.Instance;

		InitState ();
	}
	#endregion
	
	#region METHODS_CUSTOM
	public virtual void InitState(){}
	public virtual void Activate(){}
	public virtual void Desactivate(){
		this.gameObject.SetActive (false);
	}
	#endregion
}