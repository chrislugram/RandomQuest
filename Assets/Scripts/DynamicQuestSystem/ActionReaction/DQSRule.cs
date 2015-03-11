using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// DQS rule. This class is a rule with a response associated
/// </summary>
public class DQSRule {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public Func<int[,], int>[]	criterions;
	public DQSResponse			response;
	#endregion
	
	#region ACCESSORS
	public int MaxScore{
		get{ return criterions.Length; }
	}
	#endregion
	
	#region METHODS_CUSTOM
	public int Evaluate(DQSQuery query){
		int total = 0;

		for(int i=0; i<criterions.Length; i++){
			int result = criterions[i](query.preparedQuery);
			total += result;
		}

		return total;
	}
	#endregion
	
	#region UI_EVENTS
	#endregion
}
