using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Detector. This class detects elements with a particular layer in a radius
/// </summary>
public class Detector : MonoBehaviour {

	#region FIELDS
	public float					costumUpdateDeltaTime = 0.25f;
	public LayerMask				layerMaskFind;

	[Range(0.1f, 100f)]
	public float					detectRadious;

	public event Action<GameObject>	onDetectElement = delegate {};
	public event Action				onBeginDetect = delegate {};

	public List<GameObject> 		currentListElementDetected = null;
	#endregion

	#region COSTUM_METHODS
	public void StartFind(){
		currentListElementDetected = new List<GameObject> ();

		StartCoroutine(CostumUpdate ());
	}

	protected IEnumerator CostumUpdate(){
		while(true){
			onBeginDetect();

			List<GameObject> newList = new List<GameObject>();;
			foreach(GameObject go in currentListElementDetected){
				if (go != null && go.layer != layerMaskFind.value){
					//currentListElementDetected.Remove(go);
				}else{
					newList.Add(go);
				}
			}

			currentListElementDetected.Clear();
			currentListElementDetected.AddRange(newList);

			currentListElementDetected.RemoveAll(delegate (GameObject o){
				return o == null;
			});

			if (currentListElementDetected.Count == 0){
				FindElement();
			}

			yield return new WaitForSeconds(costumUpdateDeltaTime);
		}
	}

	protected void FindElement(){
		int layerMask = 1 << layerMaskFind.value;

		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, detectRadious, layerMaskFind.value);

		for (int i=0; i<hitColliders.Length; i++){
			if (!currentListElementDetected.Contains(hitColliders[i].gameObject)){
				if (ExtraConditions(hitColliders[i].gameObject)){
					currentListElementDetected.Add(hitColliders[i].gameObject);
					onDetectElement(hitColliders[i].gameObject);
				}
			}
		}
	}

	protected virtual bool ExtraConditions(GameObject element){
		return true;
	}
	#endregion
}
