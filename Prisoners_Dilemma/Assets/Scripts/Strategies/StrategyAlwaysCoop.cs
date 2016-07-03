using UnityEngine;
using System.Collections;

public class StrategyAlwaysCoop : Strategy {

	public override int GetAction(int p) {
		return 1;
	}
}
