using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Input element. Class father to recive input from InputController
/// </summary>
public class InputElement : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public Vector3			pointTouched;
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_CUSTOM	
	public virtual void 	OnDown (){}
	public virtual void 	OnUp (){}
	public virtual void 	OnMove(){}
	#endregion
}
