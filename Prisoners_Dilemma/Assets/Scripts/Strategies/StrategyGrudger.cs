using UnityEngine;
using System.Collections;

public class StrategyGrudger : Strategy {
	private bool otherPlayerCheated = false;

	public override int GetAction(int p) {
		int round = aiScript.gameScript.GetRound ();

		if (round == 0) {
			otherPlayerCheated = false;
			return 1;
		}

		if (aiScript.gameScript.GetBoard () [round - 1] [(p == 1) ? 1 : 0].action == 4) {
			otherPlayerCheated = true;
		}

		if (otherPlayerCheated) {
			return 2;
		} else {
			return 1;
		}
	}
}
