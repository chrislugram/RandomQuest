using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// DQS manager. Manage query petitions and responses, static class.
/// </summary>
public class DQSManager {
	#region STATIC_ENUM_CONSTANTS
	public enum WORLD {
		NONE 	= 0,
		TAVERN 	= 1,
		FOREST	= 2
	}
	#endregion
	
	#region FIELDS
	public static event Action<DQSResponse>	onResponseHasDialog;
	public static event Action				onFinishDialog;

	private static WORLD					currentWorld;
	private static DQSRule[]				currentRules;
	private static DQSInfluence				currentDialogInfluence;
	#endregion
	
	#region ACCESSORS
	public static DQSInfluence	DialogInfluence{
		get{ return currentDialogInfluence; }
		set{ 
			currentDialogInfluence = value;
			if (currentDialogInfluence != null){
				Console.Log("Current dialog with "+currentDialogInfluence.gameObject.name);
			}
		}
	}
	#endregion
	
	#region METHODS_CUSTOM
	public static void Launch(DQSQuery query, DQSInfluence influence = null){
		if (query != null){
			if(influence != null){
				currentDialogInfluence = influence;
			}
			
			DQSRule bestRule = null;
			int bestScoreRule = int.MinValue;
			int indexRule = -1;
			
			query.Build ();
			
			for(int i=0; i<currentRules.Length; i++){
				if (bestScoreRule <= currentRules[i].MaxScore){
					int score = currentRules[i].Evaluate(query);
					
					if (score > bestScoreRule){
						bestScoreRule = score;
						bestRule = currentRules[i];
						indexRule = i;
					}
				}
			}

			if (bestScoreRule > 0){
				GenerateResponse (bestRule);
			}else{
				Console.Warning("score 0 for all rules");
			}
		}else{
			FinishDialog();
		}
	}

	public static void LoadMap(WORLD world){
		currentWorld = world;

		DQSRuleCollections.Init ();

		if (currentWorld == WORLD.TAVERN){
			currentRules = DQSRuleCollections.tavernRules;
		}else if (currentWorld == WORLD.FOREST){
			currentRules = DQSRuleCollections.islandRules;
		}
	}

	public static void FinishDialog(float delay = 0){
		currentDialogInfluence = null;

		TaskManager.Launch (CloseDialog (delay));
	}

	private static IEnumerator CloseDialog(float delay){
		yield return new WaitForSeconds (delay);
		
		onFinishDialog ();
	}

	private static void GenerateResponse(DQSRule rule){
		Debug.Log ("response: " + rule.response.responseMessage);
		if (rule.response.HasDialog) {
			onResponseHasDialog(rule.response);
		}

		rule.response.ExecuteResponse ();
	}
	#endregion
	
	#region UI_EVENTS
	#endregion
}
