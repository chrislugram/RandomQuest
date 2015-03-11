using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// DQS influence trigger. This classes trigger influence action to NPC
/// </summary>
[Serializable]
public class DQSInfluenceTrigger {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	#endregion
	
	#region ACCESSORS
	public static event Action<DQSInfluenceEvaluator.INFLUENCE_ABOUT, int, DQSInfluenceEvaluator.INFLUENCE_VALUES>	onTriggerAction = delegate{};

	public DQSInfluenceEvaluator.INFLUENCE_ABOUT	about;
	public DQSInfluenceEvaluator.INFLUENCE_VALUES	value;
	[Range (1, 10)]
	public int										weight;
	#endregion
	
	#region METHODS_UNITY
	#endregion
	
	#region METHODS_CUSTOM
	public void OnTriggerAction(){
		onTriggerAction (about, weight, value);
	}
	#endregion
	
	#region UI_EVENTS
	#endregion
}
