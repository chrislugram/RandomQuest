using UnityEngine;
using System.Collections;

/// <summary>
/// PC controller. Read PC save information and control all PC stats. This PCcontroller not contain AI
/// </summary>
public class PCController : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	public static readonly string	KEY_NAME = "name";

	public enum PC_TYPE{
		BLACK = 0,
		WHITE = 1
	}
	#endregion
	
	#region FIELDS
	private string				name;
	private int					strength;
	private int 				dexterity;
	private int 				mind;
	private Weapon				rightWeapon;
	private Weapon				leftWeapon;
	private Armor				armor;

	private JSONObject			pjJson;

	private MovementController	movementPCController;
	private Health				healthPCController;
	private CombatAgent			combatAgentPCController;
	private AnimationController	animationPCController;

	private bool				newTurn;

	private SimpleAIEnemy		targetEnemyController;
	#endregion
	
	#region ACCESSORS
	public JSONObject PjJson{
		set{
			pjJson = value;

			name = pjJson[KEY_NAME].str;
		}
	}

	public string Name{
		get{
			return name;
		}
	}

	public int Health{
		get{ return healthPCController.health; }
	}

	public bool IsSurrounded{
		get{ return movementPCController.IsSurrounded; }
	}
	#endregion
	
	#region METHODS_UNITY
	void Awake(){
		movementPCController = GetComponent<MovementController> ();
		healthPCController = GetComponent<Health> ();
		healthPCController.onDeath += HandleonDeath;
		combatAgentPCController = GetComponent<CombatAgent> ();
		combatAgentPCController.onReciveDamage += HandleonReciveDamage;
		animationPCController = GetComponent<AnimationController> ();

		TurnManager.onTurnBegin += OnTurnBegin;
	}

	void Update(){
		if (targetEnemyController != null){
			animationPCController.Alert (true);

			if (!movementPCController.IsNearTo(targetEnemyController.GetComponent<MovementController>()) && !animationPCController.IsAttacking){
				animationPCController.Attack (false);
				movementPCController.LookAt(targetEnemyController.transform);
				movementPCController.GoTo (targetEnemyController.GetComponent<MovementController> ().GetAnchor (this.transform), true);
			}else{
				animationPCController.Attack (true);
				if (animationPCController.HasEmptyAction){
					animationPCController.EventAnimationAction = AttakToEnemyTarget;
				}

				if (newTurn){
					animationPCController.NewTurn();
					newTurn = false;
				}
			}
		}
	}

	void OnDestroy(){
		TurnManager.onTurnBegin -= OnTurnBegin;

		movementPCController = null;
		healthPCController.onDeath -= HandleonDeath;
		healthPCController = null;
		combatAgentPCController.onReciveDamage -= HandleonReciveDamage;
		combatAgentPCController = null;
		animationPCController = null;
	}
	#endregion
	
	#region METHODS_CUSTOM
	public void MoveTo(Vector3 posDestination){
		movementPCController.posDestination = posDestination;
	}

	public void SetEnemyTarget(SimpleAIEnemy aiEnemy){
		targetEnemyController = aiEnemy;
		targetEnemyController.GetComponent<CombatAgent> ().SetTargetSelector (true);

	}
	#endregion

	#region METHODS_EVENTS
	private void HandleonDeath (){
		CombatLog.Add ("PC derrotado");
		Destroy (this.gameObject);
	}

	private void HandleonReciveDamage (int damage){
		Debug.Log ("PC recibe: " + damage);
		healthPCController.Damage (damage);
	}

	private void AttakToEnemyTarget(){
		Debug.Log ("EnemyTarget: " + targetEnemyController);
		combatAgentPCController.AttackTo(targetEnemyController.GetComponent<CombatAgent>());
	}

	private void OnTurnBegin(){
		newTurn = true;
	}
	#endregion
}
