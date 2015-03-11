using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// DQSQuery. Manage one query run to BBDD of rules
/// </summary>
public class DQSQuery {
	#region STATIC_ENUM_CONSTANTS
	#endregion
		
	#region FIELDS
	public int[,]					preparedQuery;

	private Dictionary<int, int>	genericQuery;
	private Dictionary<int, int>	pcQuery;
	private Dictionary<int, int>	memoryQuery;
	private bool					builded = false;	
	#endregion

	#region METHODS_CONSTRUCTOR
	public DQSQuery(List<DQSParVarValue>	interfaceQuery){
		Init ();

		foreach (DQSParVarValue par in interfaceQuery){
			AddGeneric((int)par.key, (int)par.value);
		}
	}
	#endregion
		
	#region METHODS_CUSTOM
	public void AddGeneric(int key, int value){
		if (!genericQuery.ContainsKey(key)){
			genericQuery.Add (key, value);
		}else{
			Console.Warning("[DQS] duplicate generic key in query "+key);
		}
	}

	public void Build(){
		//PC
		PCController pcController = BoardManager.PCSelected;
		if (pcQuery.ContainsKey((int)DQSParVarValue.DQS_KEYS_QUERY.HEALTH)){
			pcQuery[(int)DQSParVarValue.DQS_KEYS_QUERY.HEALTH] = pcController.Health;
		}else{
			pcQuery.Add ((int)DQSParVarValue.DQS_KEYS_QUERY.HEALTH, pcController.Health);
		}
		
		//Memory

		//Convert to arrays
		preparedQuery = new int[2, genericQuery.Count + pcQuery.Count + memoryQuery.Count];

		List<int> keys = new List<int> ();
		keys.AddRange (genericQuery.Keys);
		keys.AddRange (pcQuery.Keys);
		keys.AddRange (memoryQuery.Keys);

		List<int> values = new List<int> ();
		values.AddRange (genericQuery.Values);
		values.AddRange (pcQuery.Values);
		values.AddRange (memoryQuery.Values);

		for(int i=0; i<2; i++){
			for (int j = 0; j < (genericQuery.Count + pcQuery.Count + memoryQuery.Count); j++) {
				if (i==0){
					preparedQuery[i, j] = keys[j];
				}else{
					preparedQuery[i, j] = values[j];
				}
			}
		}
	}

	private void Init(){
		genericQuery = new Dictionary<int, int> ();
		pcQuery = new Dictionary<int, int> ();
		memoryQuery = new Dictionary<int, int> ();
	}
	#endregion
		
	#region UI_EVENTS
	#endregion
}
