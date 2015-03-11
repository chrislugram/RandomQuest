using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Console{
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_CUSTOM	
	public static void Log(string s){
		if (AppDevelopFlag.DEVELOP){
			Debug.Log("[Log] "+s);
		}
	}

	public static void Warning(string s){
		if (AppDevelopFlag.DEVELOP){
			Debug.LogWarning("[Warning] "+s);
		}
	}

	public static void Error(string s){
		if (AppDevelopFlag.DEVELOP){
			Debug.LogError("[ERROR] "+s);
		}
	} 
	#endregion
}
