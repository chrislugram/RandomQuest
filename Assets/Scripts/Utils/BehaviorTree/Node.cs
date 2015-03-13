using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Node. It's a generic node of Behaviour Tree
/// </summary>
public class Node {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public int			id;
	public int			depth;
	public Func<bool>	nodeCondition;
	public Action		nodeAction;
	public List<Node>	nodeChilds;
	#endregion
	
	#region ACCESSORS
	#endregion

	#region METHODS_CONSTRUCTOR
	public Node(int id){
		this.id = id;
		depth = 0;
		nodeChilds = new List<Node> ();
	}

	public Node(bool isRoot){
		this.id = -1;
		depth = 0;
		nodeChilds = new List<Node> ();
		nodeCondition = () => {
			return true;
		};
	}
	#endregion

	#region METHODS_CUSTOM
	public void AddChild(Node node){
		node.UpdateDepth ();
		nodeChilds.Add (node);
	}

	public void UpdateDepth(){
		depth++;
		foreach (Node node in nodeChilds) {
			node.UpdateDepth();
		}
	}

	public void EvaluateNode(){
		if (nodeCondition == null){
			Console.Error("Node "+id+" without condition");
			return;
		}

		if (nodeCondition()){
			if (nodeAction != null){
				nodeAction();
			}

			foreach (Node node in nodeChilds) {
				node.EvaluateNode();
			}
		}
	}

	public void DestroyNode(){
		nodeCondition = null;
		nodeAction = null;

		foreach(Node node in nodeChilds){
			node.DestroyNode();
		}

		nodeChilds.Clear ();
		nodeChilds = null;
	}
	#endregion
}
