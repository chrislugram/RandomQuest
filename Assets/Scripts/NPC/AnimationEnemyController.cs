using UnityEngine;
using System.Collections;

/// <summary>
/// AnimationController. Manage the animations parameters of PC
/// </summary>
public class AnimationEnemyController : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	public static readonly string	PARAMETER_VELOCITY = "velocity";
	public static readonly string	PARAMETER_DANGER = "danger";
	public static readonly string	PARAMETER_ATTACK = "attack";
	#endregion
	
	#region FIELDS
	public Animator				animator;

	private MovementController 	movementController;
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
	void Start(){
		movementController = GetComponent<MovementController> ();

		movementController.onMove += DetectVelocity;
	}

	void OnDestroy(){
		movementController.onMove -= DetectVelocity;
	}
	#endregion
	
	#region METHODS_CUSTOM
	public void Attack(bool stateAttack){
		animator.SetBool (PARAMETER_ATTACK, stateAttack);
	}

	public void Alert(bool stateAlert){
		animator.SetBool (PARAMETER_DANGER, stateAlert);
	}
	#endregion
	
	#region METHODS_EVENTS
	private void DetectVelocity(float speed){
		animator.SetFloat (PARAMETER_VELOCITY, speed);
	}
	#endregion
}
