using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// User manager. Save the PC information for save the game status
/// </summary>
public class UserManager : MonoBehaviour {

	#region STATIC_ENUM_CONSTANTS
	public static readonly string	RESOURCES_PC_PATH = "Prefabs/PCs/PC";
	public static readonly string	RESOURCES_PCCOMBAT_PATH = "Prefabs/PCs/PCCombat";

	public static readonly string	KEY_PC0 = "pc_0";
	public static readonly string	KEY_PC1 = "pc_1";
	public static readonly string	KEY_POTIONS = "potions";
	public static readonly string	KEY_GENERAL_INVENTORY = "general_inventory";

	public static readonly string	NAME_PREFAB_PC = "PC";
	public static readonly string	NAME_GO_PC_1 = "PC_1";
	#endregion
	
	#region FIELDS
	private static JSONObject	pcJson;
	//private static JSONObject	pc1json;
	private static int			potions;
	private static Inventory	generalInventory;

	private static GameObject	pcGO;
	//private static GameObject	pc1GO;
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
	#endregion
	
	#region METHODS_CUSTOM
	//Read or copy the default save game
	public static void Init(){
		string configuration = "";
		if (!File.Exists(AppFiles.FILE_USER)){
			configuration = Resources.Load(AppFiles.RESOURCES_FILE_USER).ToString();
			File.WriteAllText(AppFiles.FILE_USER, configuration);
		}else{
			configuration = File.ReadAllText(AppFiles.FILE_USER);
		}

		ReadConfiguration (configuration);
	}

	//TODO: Search (or instantiante in future) the PCs prefabs
	public static PCController[] InitPCs(){
		if (pcGO == null){
			pcGO = CreatePJ (pcJson);
		}

		/*if (pc1GO == null){
			pc1GO = SearchPJ (NAME_GO_PC_1, pc1json);
		}*/


		PCController[] result = new PCController[1];
		result [0] = pcGO.GetComponent<PCController> ();
		//result [1] = pc1GO.GetComponent<PCController> ();

		return result;
	}

	private static GameObject  CreatePJ(JSONObject pjJson){
		GameObject pcPrefab = null;
		Debug.Log ("Scene: " + SceneLoader.CurrentScene);
		if (SceneLoader.CurrentScene == AppScenes.SCENE_COMBAT){
			pcPrefab = Resources.Load<GameObject> (RESOURCES_PCCOMBAT_PATH);
		}else{
			pcPrefab = Resources.Load<GameObject> (RESOURCES_PC_PATH);
		}
		GameObject inPoint = GameObject.Find ("InPoint");

		if (pcPrefab == null){
			Console.Error("Prefab PC not found");
			return null;
		}

		if (inPoint == null){
			Console.Error("InPoint not found");
			return null;
		}

		GameObject instantPC = (GameObject)GameObject.Instantiate (pcPrefab, inPoint.transform.position, Quaternion.identity);

		instantPC.GetComponent<PCController> ().PjJson = pjJson;

		return instantPC;
	}

	private static void ReadConfiguration (string configuration){
		JSONObject jsonConfiguration = new JSONObject (configuration);

		pcJson = jsonConfiguration [KEY_PC0];
		//pc1json = jsonConfiguration [KEY_PC1];
		potions = (int)jsonConfiguration [KEY_POTIONS].f;
		generalInventory = new Inventory(jsonConfiguration [KEY_GENERAL_INVENTORY]);
	}
	#endregion
}
