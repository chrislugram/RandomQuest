using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SpawnElement : MonoBehaviour {

	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public event Action	onInitialized = delegate {};
	public event Action	onDesactive = delegate {};

	protected Spawner	spawner;
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
	#endregion
	
	#region METHODS_CUSTOM
	public virtual void Initialized(Spawner spawner){
		this.spawner = spawner;

		onInitialized ();
	}

	public virtual void Desactive(){
		onDesactive ();

		if (spawner != null){
			spawner.AddSpawnElementToCache (this.gameObject);
			this.gameObject.SetActive(false);
		}else{
			Destroy (this.gameObject);
		}
	}
	#endregion
}