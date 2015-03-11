using UnityEngine;
using System;
using System.Collections;

public class MovementController : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public event Action<float>	onMove = delegate{};
	public Vector3				posDestination;
	public float				distanceFollow = 1.5f;

	private NavMeshAgent		navMeshAgent;
	private Transform			movementTransform;
	private MovementController	followMovementController;
	#endregion
	
	#region ACCESSORS
//The Anchors are point around the PC where NPC or other PC can go
	public Vector3 FrontAnchor{
		get{ return movementTransform.position + movementTransform.forward.normalized; }
	}

	public Vector3 BackAnchor{
		get{ return movementTransform.position - movementTransform.forward.normalized; }
	}

	public Vector3 RightAnchor{
		get{ return movementTransform.position + movementTransform.right.normalized; }
	}

	public Vector3 LeftAnchor{
		get{ return movementTransform.position - movementTransform.right.normalized; }
	}
	#endregion
	
	#region METHODS_UNITY
	void Start(){
		navMeshAgent = GetComponent<NavMeshAgent> ();
		posDestination = this.transform.position;
		movementTransform = this.transform;
	}

	void Update(){
		if (!AppGameFlag.PAUSE){
			Move();
		}
	}
	#endregion
	
	#region METHODS_CUSTOM
	public void Follow(MovementController followMovementController){
		this.followMovementController = followMovementController;
	}

	private void Move(){
		//Follow the other PJ
		if (BoardManager.FollowPCFlag && (followMovementController != null) && (followMovementController != this)){
			posDestination = followMovementController.BackAnchor;
		}

		navMeshAgent.SetDestination (posDestination);

		onMove (navMeshAgent.velocity.magnitude);
	}
	#endregion
}
