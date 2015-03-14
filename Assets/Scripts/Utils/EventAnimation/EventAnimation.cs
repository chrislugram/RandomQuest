using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Event animation. Generic class to add a actions to animation events. When was executed, the action will set to null
/// </summary>
public class EventAnimation : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public Action	eventAnimationAction = null;
	#endregion
	
	#region METHODS_EVENTS
	public void EventAnimationAction(){
		if (eventAnimationAction != null){
			eventAnimationAction();
		}

		eventAnimationAction = null;
	}
	#endregion
}
