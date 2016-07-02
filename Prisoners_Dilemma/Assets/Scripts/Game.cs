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

		float fieldPixel = interfaceScript.canvas.pixelRect.width / boardSize;
		float fieldSize = fieldPixel - 10.0f;
		float halfWidth = interfaceScript.canvas.pixelRect.width / 2.0f;

		for (int i = 0; i < boardSize; ++i) {
			board.Add (new Board[2]);

			for (int f = 0; f < 2; ++f) {
				board [i] [f].action = 0;

				board [i] [f].fieldElement = Instantiate(fieldElement) as GameObject;
				board [i] [f].fieldElement.transform.SetParent(GameObject.Find ("Board").transform, false);
				board [i] [f].fieldElement.name = "FieldElement_" + i + "_" + f;
			}

			RectTransform curBoard1 = board [i] [0].fieldElement.GetComponent<RectTransform> ();
			RectTransform curBoard2 = board [i] [1].fieldElement.GetComponent<RectTransform> ();

			curBoard1.sizeDelta = new Vector2 (fieldSize, Mathf.Clamp(fieldSize, 50.0f, 125.0f));
			curBoard2.sizeDelta = new Vector2 (fieldSize, Mathf.Clamp(fieldSize, 50.0f, 125.0f));
			curBoard1.anchoredPosition = new Vector2 (fieldPixel * i - (halfWidth - fieldPixel / 2.0f), curBoard1.rect.height / 2.0f + 5.0f);
			curBoard2.anchoredPosition = new Vector2 (fieldPixel * i - (halfWidth - fieldPixel / 2.0f), -(curBoard2.rect.height / 2.0f + 5.0f));
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

			if (round == boardSize) --round; // To-Do: End game

			interfaceScript.round.text = "" + (round + 1);
			interfaceScript.scoreP1.text = "" + scoreP1;
			interfaceScript.scoreP2.text = "" + scoreP2;
		}
	}

	private void UpdateBoard () {
		for (int i = 0; i < 2; ++i) {
			switch (board [round] [i].action) {
				case 1: {
					Destroy (board [round] [i].fieldAction);
					board [round] [i].fieldAction = Instantiate (coopAction) as GameObject;
					board [round] [i].fieldAction.transform.SetParent (board [round] [i].fieldElement.transform, false);
					board [round] [i].fieldAction.name = "Coop_" + round + "_" + i;
						
					RectTransform curAction = board [round] [i].fieldAction.GetComponent<RectTransform> ();
					RectTransform curField = board [round] [i].fieldElement.GetComponent<RectTransform> ();
						
					curAction.sizeDelta = curField.sizeDelta;
					curAction.anchoredPosition = new Vector2(0.0f, 0.0f);
					break;}
				case 2: {
					Destroy (board [round] [i].fieldAction);
					board [round] [i].fieldAction = Instantiate (cheatAction) as GameObject;
					board [round] [i].fieldAction.transform.SetParent (board [round] [i].fieldElement.transform, false);
					board [round] [i].fieldAction.name = "Cheat_" + round + "_" + i;

					RectTransform curAction = board [round] [i].fieldAction.GetComponent<RectTransform> ();
					RectTransform curField = board [round] [i].fieldElement.GetComponent<RectTransform> ();

					curAction.sizeDelta = curField.sizeDelta;
					curAction.anchoredPosition = new Vector2(0.0f, 0.0f);
					break;}
				default: break;
			}
		}
	}
}
