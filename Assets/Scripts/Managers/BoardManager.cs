using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Board manager. Control the npc in the scene
/// </summary>
public class BoardManager : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public static event Action<bool>	onSelectedPC = delegate{};

	private static PCController[]		pcArray;
	private static PCController			currentPCSelected;
	private static bool					followPCFlag = true;
	#endregion
	
	#region ACCESSORS
	public static PCController	PCSelected{
		get{ return currentPCSelected; }
	}

	public static bool	FollowPCFlag{
		get{ return followPCFlag; }
		set{ followPCFlag = value; }
	}

	public static int PCHealth{
		get{ return currentPCSelected.Health; }
	}
	#endregion
	
	#region METHODS_UNITY
	#endregion
	
	#region METHODS_CUSTOM
	public static void Init(PCController[]	pcsController){
		pcArray = pcsController;

		currentPCSelected = pcArray [0];

		onSelectedPC (true);
	}

	public static void MovePJTo(Vector3 posDestination){
		currentPCSelected.MoveTo (posDestination);
	}

	public static void SelectedPC(PCController pcSelected, bool resetCamera = false){
		bool found = false;

		Debug.Log ("pc: " + pcSelected.name);

		for (int i = 0; i < pcArray.Length && !found; i++) {
			if (pcArray[i].GetInstanceID() == pcSelected.GetInstanceID()){
				currentPCSelected = pcSelected;
				found = true;
			}
		}

		onSelectedPC (resetCamera);
	}
	#endregion
}
