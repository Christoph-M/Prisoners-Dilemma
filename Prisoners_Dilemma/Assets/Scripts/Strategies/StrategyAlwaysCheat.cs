using UnityEngine;
using System.Collections;

public class StrategyAlwaysCheat : Strategy {

	public override int GetAction(int p) {
		return 2;
	}
}
