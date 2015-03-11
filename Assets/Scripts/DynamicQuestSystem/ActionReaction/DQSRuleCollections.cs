using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// DQS rule collections. TODO/HACK class with all rules in the Demo
/// </summary>
public class DQSRuleCollections {
	#region STATIC_ENUM_CONSTANTS
	public enum RULES_FOR_TAVERN{
		TAVERN_OWNER_SAY_HELLO = 0,
		PC_TALK_NICE = 1,
		PC_TALK_INSULT = 2,
		TAVERN_OWNER_SAY_HELLO_ANGRY = 3,
		TAVERN_OWNER_SAY_HELLO_HAPPY = 4
	}
	#endregion
	
	#region FIELDS
	public static DQSRule[]		tavernRules;
	public static DQSRule[]		islandRules;

	private static bool			inicialized = false;
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_CUSTOM
	public static void Init(){
		if (!inicialized){
			//Rules for Tavern
			RulesForTavern ();
			
			//Rules for Fores
			RulesForIsland ();
		}
	}

	//TODO: This is only a Hack, the next step in the demo is to create a system who'll read this Rules/Queries/Dialog
	private static void RulesForTavern(){
		tavernRules = new DQSRule[5];

//Say Hello Tavern when see a PC
		int index = (int)RULES_FOR_TAVERN.TAVERN_OWNER_SAY_HELLO;
		tavernRules [index] = new DQSRule ();
		tavernRules [index].criterions = new Func<int[,], int>[2];
		tavernRules [index].criterions [0] = DQSCriterions.TavernTalk;
		tavernRules [index].criterions [1] = DQSCriterions.TalkToPC;

		//Create the dialog responses
		DQSDialogQuery[] dialogQueries = new DQSDialogQuery[2];

		DQSQuery dqsQuery01 = new DQSQuery (DQSParVarValue.CreateListParValues(
			new DQSParVarValue.DQS_KEYS_QUERY[] {DQSParVarValue.DQS_KEYS_QUERY.WHO, DQSParVarValue.DQS_KEYS_QUERY.CONCEPT, DQSParVarValue.DQS_KEYS_QUERY.TONE}, 
			new DQSParVarValue.DQS_VALUES_QUERY[] {DQSParVarValue.DQS_VALUES_QUERY.PC, DQSParVarValue.DQS_VALUES_QUERY.TALK, DQSParVarValue.DQS_VALUES_QUERY.NICE}
		));

		DQSQuery dqsQuery02 = new DQSQuery (DQSParVarValue.CreateListParValues(
			new DQSParVarValue.DQS_KEYS_QUERY[] {DQSParVarValue.DQS_KEYS_QUERY.WHO, DQSParVarValue.DQS_KEYS_QUERY.CONCEPT, DQSParVarValue.DQS_KEYS_QUERY.TONE}, 
			new DQSParVarValue.DQS_VALUES_QUERY[] {DQSParVarValue.DQS_VALUES_QUERY.PC, DQSParVarValue.DQS_VALUES_QUERY.TALK, DQSParVarValue.DQS_VALUES_QUERY.INSULT}
		));

		dialogQueries [0] = new DQSDialogQuery ("Hi Tavern-owner", dqsQuery01);
		dialogQueries [1] = new DQSDialogQuery ("Go To Hell!", dqsQuery02);

		tavernRules [index].response = new DQSResponse ("Hi PC!", dialogQueries);

//Tavern talk to PC, he is angry
		index = (int)RULES_FOR_TAVERN.TAVERN_OWNER_SAY_HELLO_ANGRY;
		tavernRules [index] = new DQSRule ();
		tavernRules [index].criterions = new Func<int[,], int>[3];
		tavernRules [index].criterions [0] = DQSCriterions.TavernTalk;
		tavernRules [index].criterions [1] = DQSCriterions.TalkToPC;
		tavernRules [index].criterions [2] = DQSCriterions.InfluenceNegative;
		
		tavernRules [index].response = new DQSResponse ("Get out of my tavern!", () =>{
			DQSManager.FinishDialog(4);
		});


//Tavern talk to PC, he is happy
		index = (int)RULES_FOR_TAVERN.TAVERN_OWNER_SAY_HELLO_HAPPY;
		tavernRules [index] = new DQSRule ();
		tavernRules [index].criterions = new Func<int[,], int>[3];
		tavernRules [index].criterions [0] = DQSCriterions.TavernTalk;
		tavernRules [index].criterions [1] = DQSCriterions.TalkToPC;
		tavernRules [index].criterions [2] = DQSCriterions.InfluencePositive;
		
		//Create the dialog responses
		DQSDialogQuery[] dialogQueries03 = new DQSDialogQuery[3];
		
		DQSQuery dqsQuery04 = new DQSQuery (DQSParVarValue.CreateListParValues(
			new DQSParVarValue.DQS_KEYS_QUERY[] {DQSParVarValue.DQS_KEYS_QUERY.WHO, DQSParVarValue.DQS_KEYS_QUERY.CONCEPT, DQSParVarValue.DQS_KEYS_QUERY.TONE}, 
			new DQSParVarValue.DQS_VALUES_QUERY[] {DQSParVarValue.DQS_VALUES_QUERY.PC, DQSParVarValue.DQS_VALUES_QUERY.TALK, DQSParVarValue.DQS_VALUES_QUERY.NICE}
		));

		DQSQuery dqsQuery05 = new DQSQuery (DQSParVarValue.CreateListParValues(
			new DQSParVarValue.DQS_KEYS_QUERY[] {DQSParVarValue.DQS_KEYS_QUERY.WHO, DQSParVarValue.DQS_KEYS_QUERY.CONCEPT, DQSParVarValue.DQS_KEYS_QUERY.TONE}, 
			new DQSParVarValue.DQS_VALUES_QUERY[] {DQSParVarValue.DQS_VALUES_QUERY.PC, DQSParVarValue.DQS_VALUES_QUERY.TALK, DQSParVarValue.DQS_VALUES_QUERY.INSULT}
		));
		
		dialogQueries03 [0] = new DQSDialogQuery ("Good to see you too!", dqsQuery04);
		dialogQueries03 [1] = new DQSDialogQuery ("It's a good day...", null);
		dialogQueries03 [2] = new DQSDialogQuery ("Please, shut up", dqsQuery05);
		
		tavernRules [index].response = new DQSResponse ("Good to see you!", dialogQueries03);

//Talk nice to Tavern
		index = (int)RULES_FOR_TAVERN.PC_TALK_NICE;
		tavernRules [index] = new DQSRule ();
		tavernRules [index].criterions = new Func<int[,], int>[2];
		tavernRules [index].criterions [0] = DQSCriterions.PCTalk;
		tavernRules [index].criterions [1] = DQSCriterions.ToneNice;
		tavernRules [index].response = new DQSResponse (() =>{
			DQSManager.DialogInfluence.EvaluateAllInfluences(DQSInfluenceEvaluator.INFLUENCE_ABOUT.TALK_NICE, 10, DQSInfluenceEvaluator.INFLUENCE_VALUES.AGREE);
			DQSManager.FinishDialog();
		});

//Talk insult to Tavern
		index = (int)RULES_FOR_TAVERN.PC_TALK_INSULT;
		tavernRules [index] = new DQSRule ();
		tavernRules [index].criterions = new Func<int[,], int>[2];
		tavernRules [index].criterions [0] = DQSCriterions.PCTalk;
		tavernRules [index].criterions [1] = DQSCriterions.ToneInsult;
		tavernRules [index].response = new DQSResponse (() =>{
			DQSManager.DialogInfluence.EvaluateAllInfluences(DQSInfluenceEvaluator.INFLUENCE_ABOUT.TALK_INSULT, 10, DQSInfluenceEvaluator.INFLUENCE_VALUES.AGREE);
			DQSManager.FinishDialog();
		});
	}

	private static void RulesForIsland(){}
	#endregion
	
	#region UI_EVENTS
	#endregion
}
