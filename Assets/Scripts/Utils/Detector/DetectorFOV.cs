using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Detector FOV. Detect element in FOV
/// </summary>
public class DetectorFOV : Detector {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	[Range(1.0f, 360f)]
	public int					fov = 10;
	
	private Vector3				direction = Vector3.forward;
	private Vector3 			leftLineFOV;
	private Vector3 			rightLineFOV;

	private bool				anyEnemyDetected = false;
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
	void Start(){
		StartFind ();
	}

	void Update(){
		anyEnemyDetected = false;
		direction = this.transform.forward;
		rightLineFOV = RotatePointAroundTransform(direction.normalized*detectRadious, -fov/2);
		leftLineFOV = RotatePointAroundTransform(direction.normalized*detectRadious, fov/2);
	}

	void OnDrawGizmos() {
		Gizmos.DrawRay(transform.position, rightLineFOV);
		Gizmos.DrawRay(transform.position, leftLineFOV);

		Vector3 p = rightLineFOV;
		for(int i = 1; i <= 20; i++) {
			float step = fov/20;
			Vector3 p1 = RotatePointAroundTransform(direction.normalized*detectRadious, -fov/2 + step*(i));
			Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z) + p, p1-p);
			p = p1;
		}
		Gizmos.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z) + p, leftLineFOV - p);
	}
	#endregion
	
	#region METHODS_CUSTOM
	public bool DetectElement(){
		return (currentListElementDetected.Count > 0);
	}
	
	private bool InsideFOV(Vector3 elementPos) {
		Vector3 directionElement = this.transform.position - elementPos;
		float angle = Vector3.Angle(this.transform.forward, directionElement);

		if ((180 - angle) <= (fov/2)){
			return true;
		}else{
			return false;
		}
	}

	private Vector3 RotatePointAroundTransform(Vector3 p, float angles) {
		return new Vector3(Mathf.Cos((angles)  * Mathf.Deg2Rad) * (p.x) - Mathf.Sin((angles) * Mathf.Deg2Rad) * (p.z),
		                   0,
		                   Mathf.Sin((angles)  * Mathf.Deg2Rad) * (p.x) + Mathf.Cos((angles) * Mathf.Deg2Rad) * (p.z));
	}
	#endregion
	
	#region UI_EVENTS
	protected override bool ExtraConditions (GameObject element){
		return InsideFOV (element.transform.position);
	}
	#endregion
}
