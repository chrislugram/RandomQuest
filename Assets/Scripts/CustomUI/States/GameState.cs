using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameState : StateApp {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	#endregion
	
	#region ACCESSORS
	public PCButtonUI[]	pcButtons;
	public GameObject	containerDialogGO;
	public Text[]		dialogTexts;
	public Text			responseText;
	public Text			previousDialogText;

	private string		previousDialog;
	private DQSResponse	response;
	#endregion
	
	#region METHODS_UNITY
	#endregion
	
	#region METHODS_CUSTOM
	public override void InitState (){
		base.InitState ();
	}

	public override void Activate (){
		base.Activate ();

		DQSManager.onResponseHasDialog += ReceiveDialog;
		DQSManager.onFinishDialog += CloseDialog;
		DQSManager.LoadMap (DQSManager.WORLD.TAVERN);

		//Get PJs from user saved information
		PCController[] pcArray = UserManager.InitPCs ();
		BoardManager.Init (pcArray);

		//HACK: Only 1 PC
		pcButtons [0].PCController = pcArray [0];
		BoardManager.SelectedPC (pcArray [0], true);
	}

	public override void Desactivate (){
		DQSManager.onResponseHasDialog -= ReceiveDialog;
		DQSManager.onFinishDialog -= CloseDialog;

		base.Desactivate ();
	}
	#endregion

	#region UI_EVENTS
	public void ReceiveDialog(DQSResponse resp){
		Debug.Log("....abriendo dialogo");
		response = resp;

		if (!containerDialogGO.activeSelf){
			previousDialogText.text = "";
		}

		containerDialogGO.SetActive (true);
		responseText.text = response.responseMessage;

		for (int i=0; i<dialogTexts.Length; i++){
			if (i < response.responseDialogQuery.Length){
				dialogTexts[i].gameObject.SetActive(true);
				dialogTexts[i].text = response.responseDialogQuery[i].message;
			}else{
				dialogTexts[i].gameObject.SetActive(false);
			}
		}
	}

	public void CloseDialog(){
		containerDialogGO.SetActive (false);
	}

	public void OnDialogResponseSelected(int index){
		DQSManager.Launch (response.responseDialogQuery [index].query);
	}

	/// <summary>
	/// Raises the change mode group event. Change in UI the mode of PC movement
	/// </summary>
	public void OnChangeModeGroup(bool mode){
		BoardManager.FollowPCFlag = mode;
	}
	#endregion
}
