using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Health. Generic class for manage health of PC
/// </summary>
public class Health : MonoBehaviour {

	#region STATIC_ENUM_CONSTANTS
	public event Action	onDeath = delegate {};
	#endregion

	#region FIELDS
	public int	health;

	private int	maxHealth;
	#endregion

	#region ACCESSORS
	#endregion


	#region METHODS_UNITY
	void Awake(){
		maxHealth = health;
	}
	#endregion

	#region METHODS_CUSTOM
	public void SetInitHealth(int n){
		health = n;
		maxHealth = n;
	}

	public void Damage(int n){
		health -= n;

		Debug.Log ("vida total que queda: " + health);

		if (health <= 0){
			onDeath();
		}
	}

	public void Regenerate(){
		if (health < maxHealth){
			health++;
		}
	}

	public void Restore(){
		health = maxHealth;
	}

	public void OnDestroy(){
		onDeath = delegate {};
	}
	#endregion


}
