using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// DQSParVarValue. This class have all posibles states keys and values in a Query
/// </summary>
[Serializable]
public class DQSParVarValue {
	#region KEYS
	public enum DQS_KEYS_QUERY{
		//GENERIC
		WHO 			= 000,
		CONCEPT 		= 001,
		TO_WHO			= 002,
		WHAT			= 003,
		INFLUENCE		= 004,
		TONE			= 005,

		//PC
		HEALTH 			= 100,
		LEVEL 			= 101,

		//MEMORY
		TIMES_IN_MAP	= 200,
		FIRST_TIME		= 201
	}
	#endregion
	
	#region VALUES
	public enum DQS_VALUES_QUERY{
		//Generic
		PC 				= 0,
		TAVERN_OWNER 	= 1,
		ENTER_TO 		= 2,
		MEET_TO 		= 3,
		SEE_FIRST_TIME	= 4,
		TALK 			= 5,
		NICE			= 6,
		INSULT			= 7
	}
	#endregion

	#region FIELD
	public DQS_KEYS_QUERY		key;
	public DQS_VALUES_QUERY 	value;
	#endregion

	#region METHODS_CONSTRUCTOR
	public DQSParVarValue(){}

	public DQSParVarValue(DQS_KEYS_QUERY key, DQS_VALUES_QUERY value){
		this.key = key;
		this.value = value;
	}

	public static List<DQSParVarValue> CreateListParValues(DQS_KEYS_QUERY[] keys, DQS_VALUES_QUERY[] values){
		List<DQSParVarValue> result = new List<DQSParVarValue>();

		for(int i=0; i<keys.Length; i++){
			result.Add(new DQSParVarValue(keys[i], values[i]));
		}

		return result;	          
	}
	#endregion
}
