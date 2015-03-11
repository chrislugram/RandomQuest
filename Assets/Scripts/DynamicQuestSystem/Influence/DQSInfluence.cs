using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// DQS influence. This class have the general influence of NPC from PC
/// </summary>
public class DQSInfluence : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	public static readonly	int		MAX_INFLUENCE = 100;
	public static readonly	int		MIN_INFLUENCE = -100;
	public static readonly string	PLAYER_PREF_POST_KEY = "_INFLUENCE";
	#endregion
	
	#region FIELDS
	public string						playerPrefName;

	[SerializeField]
	private List<DQSInfluenceEvaluator>	npcInfluences;
	private int							totalInfluence = 0;
	#endregion
	
	#region ACCESSORS
	public string PlayerPrefInfluenceKey{
		get { return playerPrefName+PLAYER_PREF_POST_KEY; }
	}

	public int InfluenceValue{
		get { return totalInfluence; }
	}
	#endregion
	
	#region METHODS_UNITY
	void Awake(){
		if (PlayerPrefs.HasKey(PlayerPrefInfluenceKey)){
			totalInfluence = PlayerPrefs.GetInt(PlayerPrefInfluenceKey);
		}else{
			totalInfluence = 0;
			SaveInfluence();
		}
	}

	void OnDestroy(){
		foreach(DQSInfluenceEvaluator infEvaluator in npcInfluences){
			infEvaluator.Unsuscribe();
			infEvaluator.onChangeInfluence -= OnChangeInfluence;
		}

		npcInfluences.Clear ();
		npcInfluences = null;
	}
	#endregion
	
	#region METHODS_CUSTOM
	public void InitInfluences(){
		foreach(DQSInfluenceEvaluator infEvaluator in npcInfluences){
			infEvaluator.Subscribe();
			infEvaluator.onChangeInfluence += OnChangeInfluence;
		}
	}

	private void SaveInfluence(){
		if (playerPrefName != ""){
			PlayerPrefs.SetInt(PlayerPrefInfluenceKey, totalInfluence);
		}else{
			Console.Warning("GameObject "+this.GetInstanceID()+" not have playerPrefName");
		}
	}
	#endregion
	
	#region EVENTS
	public void EvaluateAllInfluences(DQSInfluenceEvaluator.INFLUENCE_ABOUT about, int weight, DQSInfluenceEvaluator.INFLUENCE_VALUES value){
		foreach(DQSInfluenceEvaluator infEvaluator in npcInfluences){
			infEvaluator.EvaluateInfluence(about, weight, value);
		}
	}

	public void OnChangeInfluence(int valueChangeInfluence){
		totalInfluence += valueChangeInfluence;

		if (totalInfluence > MAX_INFLUENCE){
			totalInfluence = MAX_INFLUENCE;
		}else if (totalInfluence < MIN_INFLUENCE){
			totalInfluence = MIN_INFLUENCE;
		}

		SaveInfluence ();
	}
	#endregion
}
