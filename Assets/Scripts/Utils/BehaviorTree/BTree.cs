using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// B tree. Base class to create Behaviour tree
/// </summary>
public class BTree : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public float 	evaluateTimeStep = 0.2f;

	protected Node	rootTree;
	protected bool	destroyedTree;
	#endregion
	
	#region ACCESSORS
	#endregion

	#region METHODS_CUSTOM
	void Awake(){
		InitTree ();
	}

	void OnDestroy(){
		if (!destroyedTree){
			Destroy();
		}
	}
	#endregion

	#region METHODS_CUSTOM
	public virtual void InitTree(){
		destroyedTree = false;

		StartCoroutine ("Evaluate");
	}

	public virtual IEnumerator Evaluate(){
		while(true){
			yield return new WaitForSeconds(evaluateTimeStep);

			rootTree.EvaluateNode ();
		}
	}

	public virtual void Destroy(){
		StopCoroutine ("Evaluate");
		rootTree.DestroyNode ();
		destroyedTree = true;
	}
	#endregion
}
