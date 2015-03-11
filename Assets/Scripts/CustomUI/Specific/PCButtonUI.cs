using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// UI specific for representation a PC
/// </summary>
public class PCButtonUI : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public Text				namePJ;

	private PCController	pcController;
	private float			timeLastClick = -1;
	#endregion
	
	#region ACCESSORS
	public PCController	PCController{
		set{
			pcController = value;

			namePJ.text = pcController.Name;
		}
	}
	#endregion
	
	#region METHODS_UNITY
	#endregion
	
	#region METHODS_CUSTOM
	#endregion
	
	#region UI_EVENTS
	/// <summary>
	/// Raises the PJ button action event. Detect if player make double click for reset of camera offset
	/// </summary>
	public void OnPCButtonAction(){
		float currentTimeClick = Time.time;

		BoardManager.SelectedPC(pcController, ((currentTimeClick - timeLastClick) < InputController.TIME_DOUBLE_CLICK));
		timeLastClick = currentTimeClick;
	}
	#endregion
}
