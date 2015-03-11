using UnityEngine;
using System.Collections;

/// <summary>
/// Ground controller. InputElement for ground touch detection
/// </summary>
public class GroundController : InputElement {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	private float 	timeTouchDown;
	#endregion
	
	#region ACCESSORS
	#endregion

	#region METHODS_UNITY
	#endregion

	#region METHODS_CUSTOM	
	public override void OnDown (){
		timeTouchDown = Time.time;
	}

	public override void OnMove (){

	}

	public override void OnUp (){
		float currentTimeUp = Time.time;

		if ((currentTimeUp - timeTouchDown) < InputController.TIME_DRAG){
			BoardManager.MovePJTo(pointTouched);
		}
	}
	#endregion
}
