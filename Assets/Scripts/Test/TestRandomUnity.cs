using UnityEngine;
using System.Collections;

public class TestRandomUnity : MonoBehaviour {

	void Start(){
		DiceShuffle.Init ();
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.A)){
			string unityS = "";
			string systemS = "";
			string shuffleS = "";

			System.Random randomSystem = new System.Random();

			for(int i=0; i<1000; i++){
				unityS += UnityEngine.Random.Range(1, 7)+"; ";
				systemS += randomSystem.Next(1, 7)+"; ";
				shuffleS += DiceShuffle.Next(DiceShuffle.DICE.D6)+"; ";
			}

			Debug.Log(unityS);
			Debug.Log(systemS);
			Debug.Log(shuffleS);
		}
	}
}
