using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	public uint boardSize = 10;


	private List<UInt16[]> board;

	private int round = 0;

	private bool boardNeedUpdate = true;

	// Use this for initialization
	void Start () {
		board = new List<UInt16[]>();

		for (int i = 0; i < boardSize; ++i) {
			board.Add (new UInt16[2]);
			board [i] [0] = 0;
			board [i] [1] = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (boardNeedUpdate) {
			// Update board

			boardNeedUpdate = false;
		}
	}

	public void SetAction(int p, UInt16 action) {
		board [round] [p - 1] = action;

		if (board [round] [0] != 0 && board [round] [1] != 0) {
			++round;
		}

		boardNeedUpdate = true;
	}
}
