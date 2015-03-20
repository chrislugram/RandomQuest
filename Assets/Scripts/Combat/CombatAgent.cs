using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Combat agent. This class evaluates all combat actions
/// </summary>
public class CombatAgent : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public event Action<int>	onReciveDamage = delegate{};

	[SerializeField]
	public CombatInfo			combatInfo;

	public GameObject			combatTargetSelector;
	#endregion
	
	#region ACCESSORS
	public int Defense{
		get{ return combatInfo.defense; }
	}

	public DiceShuffle.DICE Damage{
		get{ return combatInfo.damageDice; }
	}
	#endregion
	
	#region METHODS_UNITY
	#endregion
	
	#region METHODS_CUSTOM
	public void AttackTo(CombatAgent vsCombatAgent){
		int attackResult = DiceShuffle.Next (DiceShuffle.DICE.D20, combatInfo.attack);
		int defenseResult = DiceShuffle.Next (DiceShuffle.DICE.D20, combatInfo.defense);

		if (attackResult >= defenseResult){
			CombatLog.Add(this.name+" HIT attack to "+vsCombatAgent.name+" "+attackResult+"("+defenseResult+")");
			int damageResult = DiceShuffle.Next(combatInfo.damageDice);

			CombatLog.Add(vsCombatAgent.name+" recive "+damageResult);
			vsCombatAgent.ReciveDamage(damageResult);
		}else{
			CombatLog.Add(this.name+" miss attack to "+vsCombatAgent.name+" "+attackResult+"("+defenseResult+")");
		}
	}

	public void ReciveDamage(int damage){
		Debug.Log ("soy " + this.name + ", recibo " + damage);
		onReciveDamage (damage);
	}

	public void SetTargetSelector(bool targetSelectorFlag){
		combatTargetSelector.SetActive (targetSelectorFlag);
	}
	#endregion
	
	#region METHODS_EVENTS
	#endregion
}
