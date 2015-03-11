using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class GizmoLookAt : MonoBehaviour {

	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
	void Update(){
		Debug.DrawRay (transform.position, transform.forward * 5, Color.cyan);
		Debug.DrawRay (transform.position, transform.up * 5, Color.red);
		Debug.DrawRay (transform.position, transform.right * 5, Color.green);
	}
	#endregion
	
	#region METHODS_CUSTOM
	#endregion
}