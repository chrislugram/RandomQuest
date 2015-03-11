using UnityEngine;
using System.Collections;

/// <summary>
/// PC controller. Read PC save information and control all PC stats
/// </summary>
public class PCController : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	public static readonly string	KEY_NAME = "name";

	public enum PC_TYPE{
		BLACK = 0,
		WHITE = 1
	}
	#endregion
	
	#region FIELDS
	private string				name;
	private int					strength;
	private int 				dexterity;
	private int 				mind;
	private Weapon				rightWeapon;
	private Weapon				leftWeapon;
	private Armor				armor;

	private JSONObject			pjJson;

	private MovementController	movementPCController;
	private Health				healthPCController;
	#endregion
	
	#region ACCESSORS
	public JSONObject PjJson{
		set{
			pjJson = value;

			name = pjJson[KEY_NAME].str;
		}
	}

	public string Name{
		get{
			return name;
		}
	}

	public int Health{
		get{ return healthPCController.health; }
	}
	#endregion
	
	#region METHODS_UNITY
	void Awake(){
		//BoardManager.onSelectedPC += FollowOtherPC;

		movementPCController = GetComponent<MovementController> ();
		healthPCController = GetComponent<Health> ();
	}

	void OnDestroy(){
		movementPCController = null;
		healthPCController = null;
		//BoardManager.onSelectedPC -= FollowOtherPC;
	}
	#endregion
	
	#region METHODS_CUSTOM
	public void MoveTo(Vector3 posDestination){
		movementPCController.posDestination = posDestination;
	}
	#endregion

	#region METHODS_CUSTOM
	private void FollowOtherPC(bool b){
		movementPCController.Follow (BoardManager.PCSelected.GetComponent<MovementController>());
	}
	#endregion
}
