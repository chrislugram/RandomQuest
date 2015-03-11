using UnityEngine;
using System.Collections;

public class InventoryItem {
	#region STATIC_ENUM_CONSTANTS
	public enum TYPE_ITEM{
		NONE = -1,
		WEAPON = 0,
		ARMOR = 1,
	}
	#endregion
	
	#region FIELDS
	public TYPE_ITEM typeItem;
	public JSONObject jsonItem;
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_CONSTRUCTORS
	public InventoryItem(TYPE_ITEM typeItem, JSONObject jsonItem){
		this.typeItem = typeItem;
		this.jsonItem = jsonItem;
	}
	#endregion
	
	#region METHODS_CUSTOM
	#endregion
}
