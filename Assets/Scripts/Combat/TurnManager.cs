using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Turn manager. This class controlled all turn in the game, execute events in concrete moments
/// </summary>
public class TurnManager : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public static event Action			onTurnBegin = delegate{};

	public decimal	turnDuration = 3;

	private decimal	timeAccumulated = 0;
	private bool	paused = false;
	#endregion
	
	#region ACCESSORS
	#endregion

	#region METHODS_UNITY
	void Awake(){
		DontDestroyOnLoad (this.gameObject);

		timeAccumulated = 0;
	}

	void Update(){
		if (!paused){
			UpdateTurn();
		}
	}

	void OnDestroy(){
		Pause ();
	}
	#endregion

	#region METHODS_CUSTOM
	public void Pause(){
		paused = !paused;
	}

	private void UpdateTurn(){
		timeAccumulated += (decimal)Time.deltaTime;
		
		if (timeAccumulated >= turnDuration){
			timeAccumulated = 0;
			onTurnBegin();
			//Debug.Log("======= turn begin ======= "+Time.time);
		}
	}
	#endregion
}
