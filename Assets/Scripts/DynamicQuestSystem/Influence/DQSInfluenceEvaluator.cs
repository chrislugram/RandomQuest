using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// DQS influence evaluator. This class evalue the influence about one value
/// </summary>
[Serializable]
public class DQSInfluenceEvaluator {
	#region STATIC_ENUM_CONSTANTS
	public enum INFLUENCE_ABOUT{
		NONE = 0,
		TALK_NICE = 1,
		TALK_INSULT = 2
	}

	public enum INFLUENCE_VALUES{
		NONE = 0,
		AGREE = 1,
		DESAGREE = 2,
	}
	#endregion
	
	#region FIELDS
	public event Action<int>	onChangeInfluence = delegate{};

	public INFLUENCE_ABOUT		about;
	public INFLUENCE_VALUES 	value;
	[Range (0, 4)]
	public int					weight;			
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
	#endregion
	
	#region METHODS_CUSTOM
	public void Subscribe(){
		DQSInfluenceTrigger.onTriggerAction += EvaluateInfluence;
	}

	public void Unsuscribe(){
		DQSInfluenceTrigger.onTriggerAction -= EvaluateInfluence;
	}
	#endregion
	
	#region EVENTS
	public void EvaluateInfluence(INFLUENCE_ABOUT about, int weight, INFLUENCE_VALUES value){
		int resultWeight = 0;
		if (this.about == about){
			if (this.value == value){
				resultWeight = weight;
			}else{
				resultWeight = -weight;
			}
		}

		if (resultWeight != 0){
			onChangeInfluence(resultWeight*this.weight);
		}
	}
	#endregion
}
