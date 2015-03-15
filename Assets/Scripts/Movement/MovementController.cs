using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MovementController : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	public static readonly float				NEAR_LIMIT	= 0.2f;

	public enum ANCHOR_TYPE{
		FRONT	= 0,
		BACK	= 1,
		RIGHT	= 2,
		LEFT	= 3,
		NONE	= 4
	}
	#endregion
	
	#region FIELDS
	public event Action<float>					onMove = delegate{};
	public Vector3								posDestination;
	public float								distanceFollow = 1.5f;

	private NavMeshAgent						navMeshAgent;
	private Transform							movementTransform;
	private MovementController					followMovementController;
	private Dictionary<Transform, ANCHOR_TYPE>	transformInAnchors;		
	private Transform							lookAtTransform;
	private Transform							parentPath;
	private int									indexPath;
	private bool								stopped;
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

	public bool IsStill{
		get{ return (navMeshAgent.velocity.magnitude == 0); }
	}

	public bool IsSurrounded{
		get{ return (transformInAnchors.Count >= 4); } 
	}
	#endregion
	
	#region METHODS_UNITY
	void Start(){
		navMeshAgent = GetComponent<NavMeshAgent> ();
		posDestination = this.transform.position;
		movementTransform = this.transform;

		indexPath = 0;
		stopped = true;

		transformInAnchors = new Dictionary<Transform, ANCHOR_TYPE> ();
	}

	void Update(){
		if (!AppGameFlag.PAUSE){
			Move();
		}
	}
	#endregion
	
	#region METHODS_CUSTOM
	public void FollowPath(Transform targetParentPath){
		if ((parentPath == null) || (parentPath != targetParentPath)){
			parentPath = targetParentPath;
		}

		stopped = false;
	}

	public void GoTo(Vector3 targetPos, bool breakPath = false){
		if (breakPath){
			parentPath = null;
		}

		posDestination = targetPos;
		stopped = false;
	}

	public void Stop(){
		posDestination = movementTransform.position;
		stopped = true;
	}

	public bool IsNearTo(MovementController movController){
		//Debug.Log ("Distance to Anchor: " + Vector3.Distance (movementTransform.position, movController.GetAnchor (movementTransform)));
		return (Vector3.Distance(movementTransform.position, movController.GetAnchor(movementTransform)) <= NEAR_LIMIT);
	}

	public void LookAt(Transform target){
		lookAtTransform = target;
	}

	/// <summary>
	/// Gets the anchor. If target don't have an anchor, assign it one. If yes get anchor of the dictionary
	/// </summary>
	public Vector3 GetAnchor(Transform target){
		ANCHOR_TYPE anchor = ANCHOR_TYPE.NONE;
		if (transformInAnchors.ContainsKey(target)){
			anchor = transformInAnchors[target];
		}else{
			foreach(ANCHOR_TYPE aType in Enum.GetValues(typeof(ANCHOR_TYPE))){
				if (aType != ANCHOR_TYPE.NONE && anchor == ANCHOR_TYPE.NONE && !transformInAnchors.ContainsValue(aType)){
					anchor = aType;
					transformInAnchors.Add(target, anchor);
				}
			}
		}

		if (anchor == ANCHOR_TYPE.BACK){
			return BackAnchor;
		}else if (anchor == ANCHOR_TYPE.FRONT){
			return FrontAnchor;
		}else if (anchor == ANCHOR_TYPE.LEFT){
			return LeftAnchor;
		}else if (anchor == ANCHOR_TYPE.RIGHT){
			return RightAnchor;
		}else{
			Console.Warning("Anchor "+target.name+" is NONE");
			return Vector3.zero;
		}
	}

	private void Move(){
		if (!stopped){
			if (parentPath != null){
				CalculatePath();
			}

			if (lookAtTransform != null){
				Vector3 direction = (lookAtTransform.position - movementTransform.position).normalized;
				Quaternion lookRotation = Quaternion.LookRotation(direction);
				movementTransform.rotation = Quaternion.Slerp(movementTransform.rotation, lookRotation, Time.deltaTime * navMeshAgent.angularSpeed);
			}
		}

		navMeshAgent.SetDestination (posDestination);
		onMove (navMeshAgent.velocity.magnitude);
	}

	private void CalculatePath(){
		float distance = navMeshAgent.remainingDistance;

		if (distance != Mathf.Infinity && navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && distance == 0){
			indexPath++;
			if (indexPath >= parentPath.childCount){
				indexPath = 0;
			}
		}

		posDestination = parentPath.GetChild(indexPath).position;
	}
	#endregion
}
