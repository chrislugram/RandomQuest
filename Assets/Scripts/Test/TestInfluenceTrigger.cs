using UnityEngine;
using System.Collections;

public class TestInfluenceTrigger : MonoBehaviour {

	[SerializeField]
	public DQSInfluenceTrigger trigger;

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Pasa por trigger " + this.name+", "+other.name);
		trigger.OnTriggerAction ();
	}
}
