using UnityEngine;
using System;
using UnityEngine.EventSystems;
using System.Collections;

public class InputController : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	public static readonly float		TIME_DRAG = 0.2f;
	public static readonly float		TIME_DOUBLE_CLICK = 0.2f;
	#endregion
	
	#region FIELDS
	public static event Action			onOpenInventoryAction = delegate{};
	public static event Action			onLeftCameraOffsetAction = delegate{};
	public static event Action			onRightCameraOffsetAction = delegate{};
	public static event Action			onUpCameraOffsetAction = delegate{};
	public static event Action			onDownCameraOffsetAction = delegate{};
	public static event Action			onOrbitCamera = delegate{};
	public static event Action<float>	onWheelScroll = delegate{};

	private EventSystem		eventSystem;
	private InputElement	currentInputElement;
	private Vector3			inputPosition;
	#endregion
	
	#region METHODS_UNITY
	void Awake(){
		DontDestroyOnLoad (this.gameObject);
	}

	void Update(){
		if (eventSystem == null){
			SearchEventSystem();
		}

		//Detect if any UI element are blocking the input system
		if (eventSystem != null && !eventSystem.IsPointerOverGameObject()){
			//Register Mouse Inputs
			DetectInputInGame();

			//Register Keyboard Inputs
			DetectKeyboardInputs();
		}


	}
	#endregion
	
	#region METHODS_CUSTOM
	private void SearchEventSystem(){
		GameObject eventSystemGO = GameObject.Find ("EventSystem");


		if (eventSystemGO == null){
			Console.Error("EventSystemGO not found");
		}else{
			eventSystem = eventSystemGO.GetComponent<EventSystem>();
		}
	}

	//Detect kind of mouse input
	private void DetectInputInGame(){
		if (Input.GetMouseButtonDown(0)){
			SearchSelectedInputElement();
			
			if (currentInputElement != null){
				currentInputElement.OnDown();			
			}
		}else if (Input.GetMouseButtonUp(0)){
			SearchSelectedInputElement();			
			
			if (currentInputElement != null){
				currentInputElement.OnUp();
			}
		}else if (Input.GetMouseButton(0)){
			SearchSelectedInputElement();
			
			if (currentInputElement != null){
				currentInputElement.OnMove();
			}
		}else if (Input.GetMouseButton(1)){
			onOrbitCamera();
		}else if (Input.GetAxis("Mouse ScrollWheel") != 0){
			onWheelScroll(Input.GetAxis("Mouse ScrollWheel"));
		}
	}

	//Selecting by layer the InputElement
	private void SearchSelectedInputElement(){
		currentInputElement = null;
		int layerDeep = int.MaxValue;

		inputPosition = Input.mousePosition;

		Ray inputRay = Camera.main.ScreenPointToRay(inputPosition);
		RaycastHit[] raycastHits = Physics.RaycastAll(inputRay.origin, inputRay.direction);

		for(int i=0; i<raycastHits.Length; i++){
			InputElement inputElement = raycastHits[i].collider.GetComponent<InputElement>();
			
			if (inputElement != null){	
				inputElement.pointTouched = raycastHits[i].point;

				if (currentInputElement == null){
					currentInputElement = inputElement;
					layerDeep = inputElement.gameObject.layer;
				}else{
					if (layerDeep < inputElement.gameObject.layer){
						currentInputElement = inputElement;
						layerDeep = inputElement.gameObject.layer;
					}
				}
			}
		}
	}

	//Detect kind of keyboard input
	private void DetectKeyboardInputs(){
		if (Input.GetKeyDown(KeyCode.I)){
			onOpenInventoryAction();
		}else if (Input.GetKey(KeyCode.A)){
			onLeftCameraOffsetAction();
		}else if (Input.GetKey(KeyCode.D)){
			onRightCameraOffsetAction();
		}else if (Input.GetKey(KeyCode.W)){
			onUpCameraOffsetAction();
		}else if (Input.GetKey(KeyCode.S)){
			onDownCameraOffsetAction();
		}
	}
	#endregion
}
