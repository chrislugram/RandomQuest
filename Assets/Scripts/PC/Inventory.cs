using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory {
	#region STATIC_ENUM_CONSTANTS
	public static readonly string KEY_TYPE_ITEM = "type_item";
	#endregion
	
	#region FIELDS
	private List<InventoryItem> inventory;
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_CONSTRUCTORS
	public Inventory(JSONObject jsonInventory){
		foreach (JSONObject jsonItem in jsonInventory.list) {
			InventoryItem.TYPE_ITEM typeItem = (InventoryItem.TYPE_ITEM)jsonItem[KEY_TYPE_ITEM].f;
			inventory.Add(new InventoryItem(typeItem, jsonItem));
		}
	}
	#endregion
	
	#region METHODS_CUSTOM
	#endregion
}
