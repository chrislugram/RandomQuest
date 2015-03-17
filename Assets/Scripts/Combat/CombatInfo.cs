using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Combat info. This class has all properties necessary for a combat
/// </summary>
[Serializable]
public class CombatInfo {
	#region FIELDS
	public int				attack;
	public DiceShuffle.DICE	damageDice;
	public int				defense;
	#endregion
}
