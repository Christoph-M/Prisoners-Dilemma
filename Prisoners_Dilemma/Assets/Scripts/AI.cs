using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {
	public Game gameScript;
	public Interface interfaceScript;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameScript.GameRunning ()) {
			switch (Random.Range (1, 3)) {
				case 1: interfaceScript.Coop (1); break;
				case 2: interfaceScript.Cheat (1); break;
			}

			switch (Random.Range (1, 3)) {
				case 1: interfaceScript.Coop (2); break;
				case 2: interfaceScript.Cheat (2); break;
			}
		}
	}
}
