using UnityEngine;
using System.Collections;

public class TestMaterialInfluence : MonoBehaviour {

	private static readonly float	center = 0.5f;

	public Color					initColor;

	private Renderer				thisRenderer;
	private DQSInfluence			influence;

	// Use this for initialization
	void Start () {
		thisRenderer = this.GetComponent<Renderer>();
		influence = GetComponent<DQSInfluence> ();
	}
	
	// Update is called once per frame
	void Update () {
		float finalValue = (influence.InfluenceValue / 200f);
		thisRenderer.material.color = new Color (initColor.r, initColor.g + finalValue, initColor.b);
	}
}
