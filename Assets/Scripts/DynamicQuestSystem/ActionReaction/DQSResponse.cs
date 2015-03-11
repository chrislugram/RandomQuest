using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DQSResponse {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public string			responseMessage = "";
	public DQSDialogQuery[]	responseDialogQuery;
	public Action			responseAction;
	#endregion
	
	#region ACCESSORS
	public bool HasDialog{
		get{ return (responseMessage != ""); }
	}
	#endregion
	
	#region METHODS_CONSTRUCTOR
	public DQSResponse(){
		responseDialogQuery = new DQSDialogQuery[0];
	}

	public DQSResponse(string message){
		responseDialogQuery = new DQSDialogQuery[0];
		responseMessage = message;
	}

	public DQSResponse(string message, DQSDialogQuery[] dialogQueries){
		responseDialogQuery = dialogQueries;
		responseMessage = message;
	}

	public DQSResponse(Action action){
		responseDialogQuery = new DQSDialogQuery[0];
		responseAction = action;
	}

	public DQSResponse(string message, Action action){
		responseDialogQuery = new DQSDialogQuery[0];
		responseMessage = message;
		responseAction = action;
	}
	#endregion
	
	#region METHODS_CUSTOM
	public void ExecuteResponse(){
		if (responseAction != null){
			responseAction();
		}
	}
	#endregion
}
