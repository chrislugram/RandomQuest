using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SceneLoaderInstance : MonoBehaviour{
	
	#region STATIC_ENUM_CONSTANTS
	public event Action<float>	OnProgressChange = delegate{};
	public event Action			OnFinishLoad = delegate{};
	#endregion
	
	#region FIELDS
	private float			progressBar = 0;
	private AsyncOperation	asyncLoad = null;
	#endregion
	
	#region ACCESSORS
	#endregion

	#region METHODS_UNITY
	void Awake(){
		DontDestroyOnLoad(this.gameObject);
	}
	#endregion

	#region METHODS_CUSTOM	
	public void LoadScene(string nameScene){
		StartCoroutine(InternalLoad(nameScene));
	}

	private IEnumerator InternalLoad(string s){
		asyncLoad = Application.LoadLevelAsync(s);
		
		while(!asyncLoad.isDone){
			progressBar = asyncLoad.progress;
			OnProgressChange(progressBar);

			yield return null;
		}

		progressBar = asyncLoad.progress;
		OnProgressChange(progressBar);
		OnFinishLoad();
	}
	#endregion
}
