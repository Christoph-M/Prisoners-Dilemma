using UnityEngine;
using System.Collections;

public class StrategyJoss : Strategy {

	public override int GetAction(int p) {
		int round = aiScript.gameScript.GetRound ();

		if (round == 0)
			return 1;
		
		if (Random.Range (0, 5) == 0) {
			return 2;
		} else {
			if (aiScript.gameScript.GetBoard () [round - 1] [(p == 1) ? 1 : 0].action == 2) {
				return 1;
			} else {
				return 2;
			}
		}
	}
}
