using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Simple ai enemy. Simple AI of a generic Enemy. It's a example to use a BehaviourTree
/// </summary>
public class SimpleAIEnemy : BTree {
	#region STATIC_ENUM_CONSTANTS
	public enum NODES{
		ALERT 		= 0,
		PEACEFUL 	= 1,
		STILL		= 2,
		WALK		= 3,
		ATTACK		= 4,
		APPROACH	= 5,
		WAIT_ATTACK	= 6,
		DIRECT		= 7,
		FLANK		= 8
	}
	#endregion
	
	#region FIELDS
	public Vector2				movementTimes;
	public Transform			pathParent;

	public GameObject			pcGO = null;
	public bool					isMoving = false;

	private int					indexPointsPath = 0;
	private MovementController	movementController;
	#endregion
	
	#region ACCESSORS
	#endregion

	#region METHODS_UNITY
	void Start(){
		movementController = GetComponent<MovementController> ();

		StartCoroutine ("ChangeMoving");
	}
	#endregion

	#region METHODS_CUSTOM
	public override void InitTree (){
		Node nodeFlank = new Node ((int)NODES.FLANK);
		nodeFlank.nodeCondition = FlankCondition;
		nodeFlank.nodeAction = FlankAction;

		Node nodeDirect = new Node ((int)NODES.DIRECT);
		nodeDirect.nodeCondition = DirectCondition;
		nodeDirect.nodeAction = DirectAction;

		Node nodeApproach = new Node ((int)NODES.APPROACH);
		nodeApproach.nodeCondition = ApproachCondition;
		nodeApproach.nodeAction = ApproachAction;
		nodeApproach.AddChild (nodeDirect);
		nodeApproach.AddChild (nodeFlank);

		Node nodeAttack = new Node ((int)NODES.ATTACK);
		nodeAttack.nodeCondition = AttackCondition;
		nodeAttack.nodeAction = AttackAction;

		Node nodeWaitAttack = new Node ((int)NODES.WAIT_ATTACK);
		nodeWaitAttack.nodeCondition = WaitAttackCondition;
		nodeWaitAttack.nodeAction = WaitAttackAction;

		Node nodeAlert = new Node ((int)NODES.ALERT);
		nodeAlert.nodeCondition = AlertCondition;
		nodeAlert.nodeAction = AlertAction;
		nodeAlert.AddChild (nodeAttack);
		nodeAlert.AddChild (nodeApproach);
		nodeAlert.AddChild (nodeWaitAttack);

		Node nodeWalk = new Node ((int)NODES.WALK);
		nodeWalk.nodeCondition = WalkCondition;
		nodeWalk.nodeAction = WalkAction;

		Node nodeStill = new Node ((int)NODES.STILL);
		nodeStill.nodeCondition = StillCondition;
		nodeStill.nodeAction = StillAction;

		Node nodePeaceful = new Node ((int)NODES.PEACEFUL);
		nodePeaceful.nodeCondition = PeacefulCondition;
		nodePeaceful.nodeAction = PeacefulAction;
		nodePeaceful.AddChild (nodeWalk);
		nodePeaceful.AddChild (nodeStill);

		rootTree = new Node (true);
		rootTree.AddChild (nodeAlert);
		rootTree.AddChild (nodePeaceful);

		base.InitTree ();
	}

	public override void Destroy (){
		StopCoroutine ("ChangeMoving");

		base.Destroy ();
	}

//Peacefull
	private bool PeacefulCondition(){
		return (pcGO == null);
	}
	
	private void PeacefulAction(){}

//Still
	private bool StillCondition(){
		return (!isMoving);
	}
	
	private void StillAction(){
		Debug.Log("StillAction");
		if (!movementController.IsStill){
			indexPointsPath--;
			if (indexPointsPath < 0){
				indexPointsPath = 0;
			}
		}
		movementController.Stop();
	}

//Walk
	private bool WalkCondition(){
		return (isMoving);
	}
	
	private void WalkAction(){
		Debug.Log("WalkAction");
		if (movementController.IsStill){
			indexPointsPath++;
			if (indexPointsPath >= pathParent.childCount){
				indexPointsPath = 0;
			}
		}

		movementController.GoToTransform(pathParent.GetChild(indexPointsPath));
	}

//Alert
	private bool AlertCondition(){
		return false;
	}
	
	private void AlertAction(){
		
	}

//WaitAttack
	private bool WaitAttackCondition(){
		return false;
	}
	
	private void WaitAttackAction(){
		
	}

//Attack
	private bool AttackCondition(){
		return false;
	}
	
	private void AttackAction(){
		
	}

//Approach
	private bool ApproachCondition(){
		return false;
	}
		
	private void ApproachAction(){
			
	}

//Direct
	private bool DirectCondition(){
		return false;
	}
	
	private void DirectAction(){
		
	}

//Flank
	private bool FlankCondition(){
		return false;
	}

	private void FlankAction(){

	}

	private IEnumerator ChangeMoving(){
		while(true){
			isMoving = true;
			yield return new WaitForSeconds(movementTimes.x);

			isMoving = false;
			yield return new WaitForSeconds(movementTimes.y);
		}
	}
	#endregion
}
