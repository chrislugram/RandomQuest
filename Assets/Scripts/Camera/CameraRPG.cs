using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CameraRPG : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public float 				distance = 10f;
	public float 				distanceMin = 5f;
	public float 				distanceMax = 20f;
	public float				distanceDamping = 3;
	public float				rotationXDamping = 250f;
	public float				rotationYDamping = 120f;
	public float				rotationYMinLimit = -20;
	public float				rotationYMaxLimit = 80;

	public Transform			followedTransform;

	public bool					resetOffset;

	private Transform			cameraTransform;

	private float				xAxis = 0;
	private	float 				yAxis = 0;
	private bool				orbitFlag = false;
	private Vector3				offset = Vector3.zero;
	private Vector3				lastFollowedTransformPos = Vector3.zero;

	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
	void Awake(){
		cameraTransform = this.transform;

		InputController.onRightCameraOffsetAction += OffsetRightCamera;
		InputController.onLeftCameraOffsetAction += OffsetLeftCamera;
		InputController.onUpCameraOffsetAction += OffsetUpCamera;
		InputController.onDownCameraOffsetAction += OffsetDownCamera;
		InputController.onOrbitCamera += DetectOrbitCamera;
		InputController.onWheelScroll += WheelScroll;
		
		BoardManager.onSelectedPC += SetSelectedTarget;
	}

	void Start(){
		xAxis = cameraTransform.eulerAngles.y;
		yAxis = cameraTransform.eulerAngles.x;

		yAxis = ClampYAngle (yAxis);
	}

	void LateUpdate(){
		if (followedTransform == null){
			return;
		}
			
		//Reset the camera offset if it's necesary
		if (resetOffset){
			offset = Vector3.zero;
			resetOffset = false;
		}

		Quaternion rotation = Quaternion.Euler (yAxis, xAxis, 0);

		Vector3 position = Vector3.zero;

		//If camera offset is distinct of zero, the camera not follow the target
		if (offset != Vector3.zero){
			position = rotation * new Vector3 (0, 0, -distance) + lastFollowedTransformPos + offset;
		}else{
			position = rotation * new Vector3 (0, 0, -distance) + followedTransform.position;
			lastFollowedTransformPos = followedTransform.position;
		}
			
		cameraTransform.rotation = rotation;
		cameraTransform.position = position;
	}

	void OnDestroy(){
		InputController.onRightCameraOffsetAction -= OffsetRightCamera;
		InputController.onLeftCameraOffsetAction -= OffsetLeftCamera;
		InputController.onUpCameraOffsetAction -= OffsetUpCamera;
		InputController.onDownCameraOffsetAction -= OffsetDownCamera;
		InputController.onOrbitCamera -= DetectOrbitCamera;
		InputController.onWheelScroll -= WheelScroll;

		BoardManager.onSelectedPC -= SetSelectedTarget;
	}
	#endregion
	
	#region METHODS_CUSTOM
	private float ClampYAngle(float angle){
		if (angle < -360){
			angle += 360;
		}
		if (angle > 360){
			angle -= 360;
		}
		
		return Mathf.Clamp(angle, rotationYMinLimit, rotationYMaxLimit);
	}
	#endregion

	#region EVENTS
	private void OffsetRightCamera(){
		Vector3 rightPlane = new Vector3 (cameraTransform.right.x, 0, cameraTransform.right.z);
		offset += rightPlane*0.1f;
	}

	private void OffsetLeftCamera(){
		Vector3 rightPlane = new Vector3 (cameraTransform.right.x, 0, cameraTransform.right.z);
		offset -= rightPlane*0.1f;
	}

	private void OffsetUpCamera(){
		Vector3 forwardPlane = new Vector3 (cameraTransform.forward.x, 0, cameraTransform.forward.z);
		offset += forwardPlane*0.1f;
	}
	
	private void OffsetDownCamera(){
		Vector3 forwardPlane = new Vector3 (cameraTransform.forward.x, 0, cameraTransform.forward.z);
		offset -= forwardPlane*0.1f;
	}

	private void DetectOrbitCamera(){
		xAxis += Input.GetAxis("Mouse X") * rotationXDamping * 0.02f;
		yAxis -= Input.GetAxis("Mouse Y") * rotationYDamping * 0.02f;
		
		yAxis = ClampYAngle (yAxis);
	}

	private void WheelScroll(float wheelScrollValue){
		distance -= wheelScrollValue*distanceDamping;

		distance = Mathf.Clamp (distance, distanceMin, distanceMax);
	}

	private void SetSelectedTarget(bool resetOffset){
		this.resetOffset = resetOffset;

		followedTransform = BoardManager.PCSelected.transform;
	}
	#endregion
}
