using UnityEngine;
using System.Collections;

public class StrategyRandom : Strategy {

	public override int GetAction() {
		return Random.Range (1, 3);
	}
}