using UnityEngine;
using System.Collections;

public class StrategyForgivingTitForTat : Strategy {

	public override int GetAction(int p) {
		int round = aiScript.gameScript.GetRound ();

		if (round <= 1)
			return 1;

		int action = aiScript.gameScript.GetBoard () [round - 2] [(p == 1) ? 1 : 0].action + aiScript.gameScript.GetBoard () [round - 1] [(p == 1) ? 1 : 0].action;

		if (action == 8) {
			return 2;
		} else {
			return 1;
		}
	}
}
