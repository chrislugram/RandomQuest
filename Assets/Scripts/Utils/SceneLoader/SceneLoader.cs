using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SceneLoader{
	
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	private static string			currentScene = "";
	private static GameObject		sceneLoaderInstance;
	private static bool				isLoading;
	private static bool				isDone;
	private static AsyncOperation	asyncLoading;
	private static float			progress;
	#endregion
	
	#region ACCESSORS
	public static bool IsLoading{
		get{ return isLoading; }
	}

	public static float ProgressLoading{
		get{ return progress; }
	}

	public static bool IsDone{
		get{ return isDone; } 
	}

	public static string CurrentScene{
		get{ return currentScene; }
	}
	#endregion
	
	#region METHODS_CUSTOM	
	public static void LoadScene(string name){
		Debug.Log ("cargo: " + name);
		currentScene = name;

		if (isLoading){
			return;
		}

		sceneLoaderInstance = new GameObject("sceneLoaderInstanceGO", typeof(SceneLoaderInstance));

		progress = 0;
		isLoading = true;
		isDone = false;

		sceneLoaderInstance.GetComponent<SceneLoaderInstance>().OnProgressChange += OnProgressChangeAction;
		sceneLoaderInstance.GetComponent<SceneLoaderInstance>().OnFinishLoad += OnFinishLoadAction;

		sceneLoaderInstance.GetComponent<SceneLoaderInstance>().LoadScene(currentScene);
	}

	private static void OnProgressChangeAction(float p){
		progress = p;
	}

	private static void OnFinishLoadAction(){
		GameObject.Destroy(sceneLoaderInstance);
		isLoading = false;
		isDone = true;
	}
	#endregion
}
