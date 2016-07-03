using UnityEngine;
using System.Collections;

public class StrategyTester : Strategy {
	private bool otherPlayerRetaliates = false;

	public override int GetAction(int p) {
		int round = aiScript.gameScript.GetRound ();

		if (round <= 1) {
			otherPlayerRetaliates = false;
			return 1;
		}
		
		if (round == 2)
			return 2;

		if (round == 4 && aiScript.gameScript.GetBoard () [round - 1] [(p == 1) ? 1 : 0].action == 4)
			otherPlayerRetaliates = true;

		if (otherPlayerRetaliates) {
			return 1;
		} else {
			if (round % 2 == 0)
				return 2;
			else
				return 1;
		}
	}
}
