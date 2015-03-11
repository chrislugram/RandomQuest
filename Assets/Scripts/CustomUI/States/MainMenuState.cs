using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuState : StateApp {
	#region STATIC_ENUM_CONSTANTS
	public static readonly string 	KEY_DQS_DESCRIPTION = "mainmenu_dqs_description";
	public static readonly string 	KEY_COMBAT_DESCRIPTION = "mainmenu_combat_description";
	#endregion
	
	#region FIELDS
	public Text			descriptionScene;
	public GameObject	buttonStartGO;

	private string		sceneToLoad = "";
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
	#endregion
	
	#region METHODS_CUSTOM
	public override void InitState (){ base.InitState (); }
	public override void Activate (){
		base.Activate ();

		buttonStartGO.SetActive (false);
		descriptionScene.text = "";
	}

	public override void Desactivate (){
		buttonStartGO.SetActive (false);
		descriptionScene.text = "";

		base.Desactivate ();
	}
	#endregion
	
	#region UI_EVENTS
	public void OnLanguageChange(int languageIndex){
		LocalizationApp.ChangeLanguageAppTo ((LocalizationApp.LANGUAGE)languageIndex);
	}

	public void OnCloseButtonAction(){
		Application.Quit ();
	}

	public void OnDQSButtonAction(){
		descriptionScene.text = LocalizationApp.TextApp (KEY_DQS_DESCRIPTION);
		sceneToLoad = AppScenes.SCENE_TAVERN;
		buttonStartGO.SetActive (true);
	}

	public void OnCombatRPGButton(){
		descriptionScene.text = LocalizationApp.TextApp (KEY_COMBAT_DESCRIPTION);
		sceneToLoad = AppScenes.SCENE_COMBAT;
		buttonStartGO.SetActive (true);
	}

	public void OnStartButtonAction(){
		rootApp.ChangeState (StateReferenceApp.TYPE_STATE.GAME, sceneToLoad);
	}

	public void OnEmailButtonAction(){
		Application.OpenURL("mailto:chrislugram@gmail.com");
	}

	public void OnLinkedinButtonAction(){
		Application.OpenURL("http://linkedin.com/in/chrislugram");
	}
	#endregion
}
