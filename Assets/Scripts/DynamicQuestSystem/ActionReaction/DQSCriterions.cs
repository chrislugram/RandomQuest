using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Criterion. TODO/HACK class. This class is a criterion of rules
/// </summary>
public class DQSCriterions {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	//Tavern Talk
	public static int TavernTalk(int[,] query){
		//Debug.Log ("Evalueo TavernTalk");
		Dictionary<int, int> queryDictionary = GetQueryDictionary (query);

		if (!queryDictionary.ContainsKey((int)DQSParVarValue.DQS_KEYS_QUERY.WHO) ||
		    !queryDictionary.ContainsKey((int)DQSParVarValue.DQS_KEYS_QUERY.CONCEPT)){
			return 0;
		}

		if ((queryDictionary [(int)DQSParVarValue.DQS_KEYS_QUERY.WHO] == (int)DQSParVarValue.DQS_VALUES_QUERY.TAVERN_OWNER) &&
		    (queryDictionary [(int)DQSParVarValue.DQS_KEYS_QUERY.CONCEPT] == (int)DQSParVarValue.DQS_VALUES_QUERY.TALK)){
			return 1;
		}else{
			return 0;
		}
	}

	//First time of something
	public static int FirstTime(int[,] query){
		//Debug.Log ("Evalueo FirstTime");
		Dictionary<int, int> queryDictionary = GetQueryDictionary (query);
		
		if (!queryDictionary.ContainsKey((int)DQSParVarValue.DQS_KEYS_QUERY.FIRST_TIME)){
			return 0;
		}
		
		if ((queryDictionary [(int)DQSParVarValue.DQS_KEYS_QUERY.FIRST_TIME] == 0)){
			return 1;
		}else{
			return 0;
		}
	}

	//Talk to PC
	public static int TalkToPC(int[,] query){
		//Debug.Log ("Evalueo TalkPC");
		Dictionary<int, int> queryDictionary = GetQueryDictionary (query);
		
		if (!queryDictionary.ContainsKey((int)DQSParVarValue.DQS_KEYS_QUERY.CONCEPT) ||
		    !queryDictionary.ContainsKey((int)DQSParVarValue.DQS_KEYS_QUERY.TO_WHO)){
			return 0;
		}
		
		if ((queryDictionary [(int)DQSParVarValue.DQS_KEYS_QUERY.CONCEPT] == (int)DQSParVarValue.DQS_VALUES_QUERY.TALK) &&
		    (queryDictionary [(int)DQSParVarValue.DQS_KEYS_QUERY.TO_WHO] == (int)DQSParVarValue.DQS_VALUES_QUERY.PC)){
			return 1;
		}else{
			return 0;
		}
	}

	//Influence Negative
	public static int InfluenceNegative(int[,] query){
		//Debug.Log ("Evalueo InfluenceNegative");
		Dictionary<int, int> queryDictionary = GetQueryDictionary (query);
		
		if (!queryDictionary.ContainsKey((int)DQSParVarValue.DQS_KEYS_QUERY.INFLUENCE)){
			return 0;
		}
		
		if ((queryDictionary [(int)DQSParVarValue.DQS_KEYS_QUERY.INFLUENCE] < -50)){
			return 1;
		}else{
			return 0;
		}
	}

	//Influence Positive
	public static int InfluencePositive(int[,] query){
		//Debug.Log ("Evalueo InfluencePositive");
		Dictionary<int, int> queryDictionary = GetQueryDictionary (query);
		
		if (!queryDictionary.ContainsKey((int)DQSParVarValue.DQS_KEYS_QUERY.INFLUENCE)){
			return 0;
		}
		
		if ((queryDictionary [(int)DQSParVarValue.DQS_KEYS_QUERY.INFLUENCE] > 50)){
			return 1;
		}else{
			return 0;
		}
	}

	//PC Talk
	public static int PCTalk(int[,] query){
		//Debug.Log ("Evalueo PCTalkNice");
		Dictionary<int, int> queryDictionary = GetQueryDictionary (query);
		
		if (!queryDictionary.ContainsKey((int)DQSParVarValue.DQS_KEYS_QUERY.WHO) ||
		    !queryDictionary.ContainsKey((int)DQSParVarValue.DQS_KEYS_QUERY.CONCEPT)){
			return 0;
		}
		
		if ((queryDictionary [(int)DQSParVarValue.DQS_KEYS_QUERY.WHO] == (int)DQSParVarValue.DQS_VALUES_QUERY.PC) &&
		    (queryDictionary [(int)DQSParVarValue.DQS_KEYS_QUERY.CONCEPT] == (int)DQSParVarValue.DQS_VALUES_QUERY.TALK)){
			return 1;
		}else{
			return 0;
		}
	}

	//Tone of message Nice
	public static int ToneNice(int[,] query){
		//Debug.Log ("Evalueo InfluencePositive");
		Dictionary<int, int> queryDictionary = GetQueryDictionary (query);
		
		if (!queryDictionary.ContainsKey((int)DQSParVarValue.DQS_KEYS_QUERY.TONE)){
			return 0;
		}
		
		if ((queryDictionary [(int)DQSParVarValue.DQS_KEYS_QUERY.TONE] == (int)DQSParVarValue.DQS_VALUES_QUERY.NICE)){
			return 1;
		}else{
			return 0;
		}
	}

	//Tone of message Insult
	public static int ToneInsult(int[,] query){
		//Debug.Log ("Evalueo InfluencePositive");
		Dictionary<int, int> queryDictionary = GetQueryDictionary (query);
		
		if (!queryDictionary.ContainsKey((int)DQSParVarValue.DQS_KEYS_QUERY.TONE)){
			return 0;
		}
		
		if ((queryDictionary [(int)DQSParVarValue.DQS_KEYS_QUERY.TONE] == (int)DQSParVarValue.DQS_VALUES_QUERY.INSULT)){
			return 1;
		}else{
			return 0;
		}
	}


	private static Dictionary<int, int> GetQueryDictionary(int[,] query){
		Dictionary<int, int> queryDictionary = new Dictionary<int, int> ();
		
		for(int i=0; i < query.GetLength(1); i++){
			queryDictionary.Add(query[0, i], query[1, i]);
		}

		return queryDictionary;
	}
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_CUSTOM
	#endregion
	
	#region UI_EVENTS
	#endregion
}
