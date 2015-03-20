using UnityEngine;
using System.Collections;

/// <summary>
/// Enemy input controller. This class receive all events related with InputController
/// </summary>
public class EnemyInputController : InputElement {

	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_CUSTOM	
	public override void 	OnDown (){
		BoardManager.PCSelected.SetEnemyTarget (this.GetComponent<SimpleAIEnemy> ());
	}
	#endregion
}
