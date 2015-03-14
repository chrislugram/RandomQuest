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
	public event Action<float>	onTurnStep = delegate{};
	public event Action			onTurnEnd = delegate{};

	public decimal	turnDuration = 3;
	public decimal	turnStepDuration = 0.5m;

	private decimal	timeAccumulated = 0;
	#endregion
	
	#region ACCESSORS
	#endregion

	#region METHODS_UNITY
	void Awake(){
		DontDestroyOnLoad (this.gameObject);

		timeAccumulated = 0;
		StartCoroutine ("UpdateTurn");
	}
	#endregion

	#region METHODS_CUSTOM
	private IEnumerator UpdateTurn(){
		while(true){
			yield return new WaitForSeconds((float)turnStepDuration);

			timeAccumulated += turnStepDuration;
			decimal percentage = timeAccumulated/turnDuration;
			percentage = Decimal.Round(percentage,2);
			onTurnStep((float)percentage);

			if (percentage >= 1){
				timeAccumulated = 0;
				onTurnEnd();
				Debug.Log("======= turn end =======");
			}
		}
	}
	#endregion
}
