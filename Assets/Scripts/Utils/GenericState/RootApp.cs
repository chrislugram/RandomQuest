using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Root app. Generic Root class to control all UI states of the game
/// </summary>
public class RootApp : MonoBehaviour{

	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public static RootApp												Instance;
	
	public StateReferenceApp.TYPE_STATE									currentTypeState;
	public StateApp														currentState;
	public StateReferenceApp.POPUP_TYPE_STATE							currentPopupTypeState;
	public StateApp														currentPopupState;

	protected Dictionary<StateReferenceApp.TYPE_STATE, string>			states; 
	protected Dictionary<StateReferenceApp.POPUP_TYPE_STATE, string>	popupStates; 
	protected List<GameObject>											listStatesGO;
	protected List<GameObject>											listPopupStatesGO;
	protected Stack<StateApp>											stackPopupStatesGO;
	#endregion

	#region ACCESSORS
	#endregion

	#region METHODS_UNITY
	void Awake(){
		DontDestroyOnLoad (this.gameObject);


		listStatesGO = new List<GameObject> ();
		listPopupStatesGO = new List<GameObject>();
		stackPopupStatesGO = new Stack<StateApp> ();

		RootApp.Instance = this;

		InitRootApp();
	}

	void OnDestroy(){
		RootApp.Instance = null;
		states = null;
	}
	#endregion
	
	#region METHODS_CUSTOM
	public virtual void AddStateGO(GameObject go){
		if (!listStatesGO.Contains(go)){
			listStatesGO.Add(go);
		}
	}

	public virtual void AddPopupStateGO(GameObject go){
		if (!listPopupStatesGO.Contains(go)){
			listPopupStatesGO.Add(go);
		}
	}


	public virtual void ShowPopupState(StateReferenceApp.POPUP_TYPE_STATE typePopupState){

		if (popupStates.ContainsKey (typePopupState)) {
			 GameObject currentPopupStateGO = SearchPopupGO (popupStates [typePopupState]);

			if (currentPopupStateGO != null){
				currentPopupStateGO.SetActive(true);
				currentPopupState = currentPopupStateGO.GetComponent<StateApp>();
				currentPopupState.Activate();
				stackPopupStatesGO.Push (currentPopupState);		
			}else{
				Debug.LogWarning("Warning: PopupState not found in List");
			}
	
		}else{
			Debug.LogWarning("Warning: PopupState not found");
		}
	}

	public virtual void HidePopupState(){

		if (currentPopupState != null){
			currentPopupState.Desactivate();
			currentPopupState.gameObject.SetActive(false);
			stackPopupStatesGO.Pop();	

			if(stackPopupStatesGO.Count > 0){
				currentPopupState = stackPopupStatesGO.Peek();
			}
		}
	}

	public virtual void ChangeState(StateReferenceApp.TYPE_STATE typeState, string newScene = ""){

		Debug.Log("=============="+typeState);

		if (currentState != null){
			currentState.Desactivate ();
			currentState.gameObject.SetActive (false);
		}

		//Cargo nuevo estado UI de la misma escena
		if (newScene == ""){
			LoadStateInScene(typeState);
		//Cargo nuevo estado UI de OTRA escena
		}else{
			StartCoroutine(LoadStateInOtherScene(typeState, newScene));
		}
	}

	protected virtual void InitRootApp(){}

	protected virtual void LoadStateInScene(StateReferenceApp.TYPE_STATE typeState){
		if (states.ContainsKey(typeState)){

			GameObject currentStateGO = SearchGO(states[typeState]);

			if (currentStateGO != null){
				currentStateGO.SetActive(true);
				currentState = currentStateGO.GetComponent<StateApp>();
				currentState.Activate();
			}else{
				Debug.LogWarning("Warning: State not found in List "+typeState);
			}

		}else{
			Debug.LogWarning("Warning: State not found");
		}
	}

	protected virtual GameObject SearchGO(string stateName){
		listStatesGO.RemoveAll (item => item == null);

		foreach(GameObject go in listStatesGO){
			if (go.name == stateName){
				return go;
			}
		}
		return null;
	}

	protected virtual GameObject SearchPopupGO(string statePopupName){
		listPopupStatesGO.RemoveAll (item => item == null);
		
		foreach(GameObject go in listPopupStatesGO){
			if (go.name == statePopupName){
				return go;
			}
		}
		
		return null;
	}

	protected virtual IEnumerator LoadStateInOtherScene(StateReferenceApp.TYPE_STATE typeState, string newScene){

		SceneLoader.LoadScene (newScene);

		while(SceneLoader.IsLoading){
			yield return null;
		}

		LoadStateInScene (typeState);
	}
	#endregion
}