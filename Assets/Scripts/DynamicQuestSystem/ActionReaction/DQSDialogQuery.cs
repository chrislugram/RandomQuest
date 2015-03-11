using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// DQS dialog query. This class is ONE diaglo from PC
/// </summary>

public class DQSDialogQuery{
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public string 	message;
	public DQSQuery	query;
	#endregion
	
	#region ACCESSORS
	#endregion

	#region METHODS_CONSTRUCTOR
	public DQSDialogQuery(string m, DQSQuery q){
		message = m;
		query = q;
	}
	#endregion

	#region METHODS_CUSTOM
	#endregion
	
	#region UI_EVENTS
	#endregion
}
