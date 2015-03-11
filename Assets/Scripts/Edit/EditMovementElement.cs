using UnityEngine;
using System.Collections;

/// <summary>
/// Edit movement element. This script help in creation of scene
/// </summary>
[ExecuteInEditMode]
public class EditMovementElement : MonoBehaviour {

	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public Vector3		offset = Vector3.zero;

	private Transform	transformElement;
	private Vector3		lastPosition;
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
	void Start(){
		transformElement = this.transform;
	}

	void Update(){
		if (transformElement.position != lastPosition){
			lastPosition = new Vector3 (Mathf.FloorToInt(transformElement.position.x) + offset.x, Mathf.FloorToInt(transformElement.position.y) + offset.y, Mathf.FloorToInt(transformElement.position.z) + offset.z);
			transformElement.position = lastPosition;
		}
	}
	#endregion
	
	#region METHODS_CUSTOM
	#endregion
	
	#region EVENTS
	#endregion
}
