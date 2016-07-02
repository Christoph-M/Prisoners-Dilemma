using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;


struct Board {
	public GameObject fieldElement;
	public GameObject fieldAction;
	public UInt16 action;
}

struct ScreenSpace {
	public const float width = 8.892f * 2.0f;
	public const float height = 5.0f * 2.0f;
	public const float left = -8.892f;
	public const float right = 8.892f;
	public const float top = 5.0f;
	public const float bottom = -5.0f;
}


public class Game : MonoBehaviour {
	public Interface interfaceScript;

	public GameObject fieldElement;
	public GameObject coopAction;
	public GameObject cheatAction;

	public uint boardSize = 10;


	private List<Board[]> board;

	private int round = 0;
	private int scoreP1 = 0;
	private int scoreP2 = 0;

	void Start () {
		board = new List<Board[]>();

		for (int i = 0; i < boardSize; ++i) {
			board.Add (new Board[2]);
			board [i] [0].action = 0;
			board [i] [1].action = 0;

			board [i] [0].fieldElement = Instantiate(fieldElement, new Vector3(1.5f * i - ScreenSpace.width / 2.0f + 2.0f, 1.0f, 0.0f), Quaternion.identity) as GameObject;
			board [i] [1].fieldElement = Instantiate(fieldElement, new Vector3(1.5f * i - ScreenSpace.width / 2.0f + 2.0f, -1.0f, 0.0f), Quaternion.identity) as GameObject;

			board [i] [0].fieldElement.transform.parent = GameObject.Find ("Board").transform;
			board [i] [1].fieldElement.transform.parent = GameObject.Find ("Board").transform;

			board [i] [0].fieldElement.name = "FieldElement_" + i + "_0";
			board [i] [1].fieldElement.name = "FieldElement_" + i + "_1";
		}
	}

	public void SetAction(int p, UInt16 action) {
		board [round] [p - 1].action = action;

		this.UpdateBoard ();

		if (board [round] [0].action != 0 && board [round] [1].action != 0) {
			if (board [round] [0].action == 1) {
				if (board [round] [1].action == 1) {
					scoreP1 += 3;
					scoreP2 += 3;
				} else {
					scoreP2 += 5;
				}
			} else {
				if (board [round] [1].action == 2) {
					++scoreP1;
					++scoreP2;
				} else {
					scoreP1 += 5;
				}
			}

			++round;
			GameObject.Find ("Round").GetComponent<Text> ().text = "" + (round + 1);
			GameObject.Find ("ScoreP1").GetComponent<Text> ().text = "" + scoreP1;
			GameObject.Find ("ScoreP2").GetComponent<Text> ().text = "" + scoreP2;
		}
	}

	private void UpdateBoard () {
		for (int i = 0; i < 2; ++i) {
			switch (board [round] [i].action) {
				case 1: 
					Destroy (board [round] [i].fieldAction);
					board [round] [i].fieldAction = Instantiate (coopAction, board [round] [i].fieldElement.transform.position, Quaternion.identity) as GameObject;
					board [round] [i].fieldAction.transform.parent = board [round] [i].fieldElement.transform;
					board [round] [i].fieldAction.name = "Coop_" + round + "_" + i;
					break;
				case 2: 
					Destroy (board [round] [i].fieldAction);
					board [round] [i].fieldAction = Instantiate (cheatAction, board [round] [i].fieldElement.transform.position, Quaternion.identity) as GameObject;
					board [round] [i].fieldAction.transform.parent = board [round] [i].fieldElement.transform;
					board [round] [i].fieldAction.name = "Cheat_" + round + "_" + i;
					break;
				default: break;
			}
		}
	}
}
