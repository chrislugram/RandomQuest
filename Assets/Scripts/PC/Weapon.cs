using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	#region STATIC_ENUM_CONSTANTS
	public enum TYPE_WEAPON{
		NONE = -1,
		ONE_HAND = 0,
		TWO_HAND = 1,
		DISTANCE = 2,
		SHIELD = 3
	}
	#endregion
	
	#region FIELDS
	private TYPE_WEAPON	typeWeapon;
	private int			magicAttack;
	private int 		magicDamage;
	private int 		magicDefense;
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
	#endregion
	
	#region METHODS_CUSTOM
	#endregion
}
