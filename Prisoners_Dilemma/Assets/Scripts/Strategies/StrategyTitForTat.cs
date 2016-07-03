using UnityEngine;
using System.Collections;

public class StrategyTitForTat : Strategy {

	public override int GetAction(int p) {
		int round = aiScript.gameScript.GetRound ();

		if (round == 0)
			return 1;

		if (aiScript.gameScript.GetBoard () [round - 1] [(p == 1) ? 1 : 0].action == 2) {
			return 1;
		} else {
			return 2;
		}
	}
}
