using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// DQS query trigger. This class trigger query
/// </summary>

public class DQSQueryTrigger : MonoBehaviour {
	#region STATIC_ENUM_CONSTANTS
	#endregion
	
	#region FIELDS
	public float				timeDisabled = 5f;
	public float				percentage = 1f;
	public DQSInfluence			dqsInfluence;

	[SerializeField]
	public List<DQSParVarValue>	interfaceQuery;

	private Collider			triggerCollider;
	private bool				triggerEnable = true;
	#endregion
	
	#region ACCESSORS
	#endregion
	
	#region METHODS_UNITY
	void Awake(){
		triggerCollider = this.GetComponent<Collider>();
	}

	void OnTriggerEnter(Collider otherCollider) {
		if (otherCollider.gameObject.layer == AppLayers.LAYER_PC && triggerEnable){
			if (percentage < 1){
				float random = UnityEngine.Random.Range(0, 1);

				if (random <= percentage){
					TriggerQuery();
				}
			}else{
				TriggerQuery();
			}
		}
	}
	#endregion
	
	#region METHODS_CUSTOM
	private void TriggerQuery(){
		DQSQuery query = new DQSQuery (interfaceQuery);

		if (dqsInfluence != null){
			query.AddGeneric((int)DQSParVarValue.DQS_KEYS_QUERY.INFLUENCE, dqsInfluence.InfluenceValue);
		}

		DQSManager.Launch (query, dqsInfluence);

		triggerEnable = false;
		Invoke ("Enable", timeDisabled);
	}

	private void Enable(){
		triggerEnable = true;
	}
	#endregion
	
	#region UI_EVENTS
	#endregion
}
