using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class NPCController : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	#endregion
	
	#region ACCESSORS
	public DQSInfluence	influence;
	#endregion
	
	#region METHODS_UNITY
	void Awake(){
		influence = GetComponent<DQSInfluence> ();
	}

	void Start(){
		influence.InitInfluences ();
	}

	void OnDestroy(){
		influence = null;
	}
	#endregion
	
	#region METHODS_CUSTOM
	#endregion
	
	#region UI_EVENTS
	#endregion
}
