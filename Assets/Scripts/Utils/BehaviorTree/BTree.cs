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
	protected Node	rootTree;
	protected bool	destroyedTree;
	#endregion
	
	#region ACCESSORS
	#endregion

	#region METHODS_CUSTOM
	void Awake(){
		InitTree ();
	}

	void Update(){
		UpdateTree();
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
	}

	public virtual void UpdateTree(){
		rootTree.EvaluateNode ();
	}

	public virtual void Destroy(){
		rootTree.DestroyNode ();
		destroyedTree = true;
	}
	#endregion
}
