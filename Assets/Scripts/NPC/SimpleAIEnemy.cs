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
	public Vector2						movementTimes;
	public Transform					pathParent;

	public PCController					pcController = null;
	public bool							isMoving = false;

	private int							indexPointsPath = 0;
	private MovementController			movementController;
	private DetectorFOV					detectorController;
	private AnimationEnemyController	animationController;
	#endregion
	
	#region ACCESSORS
	#endregion

	#region METHODS_UNITY
	void Start(){
		movementController = GetComponent<MovementController> ();
		detectorController = GetComponent<DetectorFOV> ();
		detectorController.onDetectElement += DetectPC;
		animationController = GetComponent<AnimationEnemyController> ();

		StartCoroutine ("ChangeMoving");
	}
	#endregion

	#region METHODS_CUSTOM
	public override void InitTree (){
		Node nodeApproach = new Node ((int)NODES.APPROACH);
		nodeApproach.nodeCondition = ApproachCondition;
		nodeApproach.nodeAction = ApproachAction;

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

		movementController = null;
		detectorController.onDetectElement -= DetectPC;
		detectorController = null;
		animationController = null;

		base.Destroy ();
	}

//Peacefull
	private bool PeacefulCondition(){
		return (pcController == null);
	}
	
	private void PeacefulAction(){
		animationController.Alert (false);
		movementController.LookAt(null);
	}

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

		movementController.GoTo(pathParent.GetChild(indexPointsPath));
	}

//Alert
	private bool AlertCondition(){
		return (pcController != null);
	}
	
	private void AlertAction(){
		animationController.Alert (true);
		movementController.LookAt(pcController.transform);
	}

//WaitAttack
	private bool WaitAttackCondition(){
		return pcController.IsSurrounded;
	}
	
	private void WaitAttackAction(){
		movementController.Stop ();
	}

//Attack
	private bool AttackCondition(){
		return (!pcController.IsSurrounded && movementController.IsNearTo(pcController.GetComponent<MovementController>()));
	}
	
	private void AttackAction(){
		animationController.Attack (true);
		movementController.Stop ();

		if (animationController.HasEmptyAction){
			animationController.EventAnimationAction = AttakToPC;
		}
	}

//Approach
	private bool ApproachCondition(){
		return (!pcController.IsSurrounded && !movementController.IsNearTo(pcController.GetComponent<MovementController>()));
	}
		
	private void ApproachAction(){
		animationController.Attack (false);
		movementController.GoTo (pcController.GetComponent<MovementController> ().GetAnchor (this.transform));
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

	#region METHODS_EVENT
	private void DetectPC(GameObject pcGO){
		this.pcController = pcGO.GetComponent<PCController>();
	}

	private void AttakToPC(){
		Debug.Log ("Ataco al PC ");
	}
	#endregion
}
