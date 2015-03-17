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
	public Vector2						movingTimes;
	public Transform					pathParent;

	public PCController					pcController = null;
	public bool							isMoving = false;
	
	private MovementController			movementController;
	private DetectorFOV					detectorController;
	private AnimationEnemyController	animationController;
	private CombatAgent					combatAgent;
	private Health						health;
	private bool						newTurn;
	#endregion
	
	#region ACCESSORS
	#endregion

	#region METHODS_UNITY
	void Start(){
		movementController = GetComponent<MovementController> ();
		detectorController = GetComponent<DetectorFOV> ();
		detectorController.onDetectElement += DetectPC;
		animationController = GetComponent<AnimationEnemyController> ();
		combatAgent = GetComponent<CombatAgent> ();
		combatAgent.onReciveDamage += HandleonReciveDamage;
		health = GetComponent<Health> ();
		health.onDeath += HandleonDeath;

		TurnManager.onTurnBegin += OnTurnBegin;

		StartCoroutine("ChangeMoving");
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
		combatAgent = null;
		health.onDeath -= HandleonDeath;
		health = null;

		TurnManager.onTurnBegin -= OnTurnBegin;

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
		movementController.Stop();
	}

//Walk
	private bool WalkCondition(){
		return (isMoving);
	}
	
	private void WalkAction(){
		//Debug.Log("WalkAction");
		movementController.FollowPath(pathParent);
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

		if (newTurn){
			animationController.NewTurn();
			newTurn = false;
		}
	}

//Approach
	private bool ApproachCondition(){
		return (!pcController.IsSurrounded && !movementController.IsNearTo(pcController.GetComponent<MovementController>()) && !animationController.IsAttacking);
	}
		
	private void ApproachAction(){
		animationController.Attack (false);
		movementController.GoTo (pcController.GetComponent<MovementController> ().GetAnchor (this.transform), true);
	}
	#endregion

	#region METHODS_EVENT
	private void HandleonDeath (){
		CombatLog.Add ("Death: " + this.name);
		Destroy (this.gameObject);
	}

	private void HandleonReciveDamage (int damage){
		health.Damage (damage);
	}

	private IEnumerator ChangeMoving(){
		while(true){
			isMoving = true;
			yield return new WaitForSeconds(movingTimes.x);

			isMoving = false;
			yield return new WaitForSeconds(movingTimes.y);
		}
	}

	private void DetectPC(GameObject pcGO){
		this.pcController = pcGO.GetComponent<PCController>();
	}

	private void AttakToPC(){
		Debug.Log ("PC: " + pcController);
		combatAgent.AttackTo(pcController.GetComponent<CombatAgent>());
	}

	private void OnTurnBegin(){
		newTurn = true;
	}
	#endregion
}
