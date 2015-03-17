using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Combat log. This class save log of combat
/// </summary>
public class CombatLog {
	#region STATIC_ENUM_CONSTANTS
	public static readonly int	MAX_LOG = 100;
	#endregion

	#region FIELDS
	public static event Action<string[]>	onCombatLogChange = delegate {};

	private static Queue<string>			queueLog;
	#endregion

	#region METHODS_CUSTOM
	public static void Add(string log){
		Debug.Log (log);

		if (queueLog == null){
			queueLog = new Queue<string>(MAX_LOG);
		}

		if (queueLog.Count == MAX_LOG){
			queueLog.Dequeue();
		}

		queueLog.Enqueue (log);

		onCombatLogChange(queueLog.ToArray());
	}
	#endregion
}
