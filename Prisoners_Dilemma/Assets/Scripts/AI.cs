using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI : MonoBehaviour {
	public Game gameScript;
	public Interface interfaceScript;

	public List<Strategy> strategies;


	private int p1AiStrategy = 0;
	private int p2AiStrategy = 0;

	private bool p1AiEnabled = false;
	private bool p2AiEnabled = false;

	private bool p1AiPlayed = false;
	private bool p2AiPlayed = false;

	private int lastRound;
	private int currentRound;
	
	// Update is called once per frame
	void Update () {
		currentRound = gameScript.GetRound ();

		if (currentRound != lastRound) {
			p1AiPlayed = false;
			p2AiPlayed = false;
		}

		if (gameScript.GameRunning ()) {
			if (p1AiEnabled && !p1AiPlayed) {
				switch (strategies[p1AiStrategy].GetAction()) {
					case 1: interfaceScript.Coop (1); break;
					case 2: interfaceScript.Cheat (1); break;
					default: Debug.Log ("No Action/Strategy selected"); break;
				}

				p1AiPlayed = true;
			}

			if (p2AiEnabled && !p2AiPlayed) {
				switch (strategies[p2AiStrategy].GetAction()) {
					case 1: interfaceScript.Coop (2); break;
					case 2: interfaceScript.Cheat (2); break;
					default: Debug.Log ("No Action/Strategy selected"); break;
				}

				p2AiPlayed = true;
			}
		} else if (p1AiPlayed || p2AiPlayed) {
			p1AiPlayed = false;
			p2AiPlayed = false;
		}

		lastRound = currentRound;
	}

	public void SetAiEnabled(int p, bool enabled) {
		switch (p) {
			case 1: p1AiEnabled = enabled; break;
			case 2: p2AiEnabled = enabled; break;
		}
	}

	public void SetAiStrategy(int p, int strategy) {
		switch (p) {
			case 1: p1AiStrategy = strategy; break;
			case 2: p1AiStrategy = strategy; break;
		}
	}
}
