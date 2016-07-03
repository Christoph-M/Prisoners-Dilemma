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

	private uint rounds;
	private int round = 0;
	private int scoreP1 = 0;
	private int scoreP2 = 0;

	private bool gameRunning = true;

	void Start () {
		this.ResetGame ();
	}

	public void SetAction(int p, UInt16 action) {
		board [round] [p - 1].action = action;

		this.UpdateBoard (p - 1);

		if (board [round] [0].action != 0 && board [round] [1].action != 0) {
			int actions = board [round] [0].action + board [round] [1].action;

			switch (actions) {
				case 4: scoreP1 += 3; scoreP2 += 3; break;
				case 6:
					if (board [round] [0].action > board [round] [1].action)
						scoreP1 += 5;
					else
						scoreP2 += 5;
					break;
				case 8: ++scoreP1; ++scoreP2; break;
				default: break;
			}

			++round;

			if (round == rounds) {
				--round;

				gameRunning = false;
				interfaceScript.restart.gameObject.SetActive (true);
				interfaceScript.SetButtonsEnabled (false);
			}

			interfaceScript.round.text = "" + (round + 1);
			interfaceScript.scoreP1.text = "" + scoreP1;
			interfaceScript.scoreP2.text = "" + scoreP2;
		}
	}

	public void ResetGame() {
		if (board != null) {
			for (int i = 0; i < rounds; ++i) {
				for (int f = 0; f < 2; ++f) {
					Destroy (board [i] [f].fieldAction);
					Destroy (board [i] [f].fieldElement);
				}
			}
		}

		rounds = boardSize;
		round = 0;
		scoreP1 = 0;
		scoreP2 = 0;

		interfaceScript.round.text = "1";
		interfaceScript.scoreP1.text = "0";
		interfaceScript.scoreP2.text = "0";

		board = new List<Board[]>();

		float fieldSize = interfaceScript.canvas.pixelRect.width / boardSize;
		float fieldBorder = Mathf.Clamp(fieldSize / 10.0f, 0.0f, 20.0f);
		float fieldSizeWithoutBorder = fieldSize - fieldBorder;
		float halfWidth = interfaceScript.canvas.pixelRect.width / 2.0f;

		for (int i = 0; i < rounds; ++i) {
			board.Add (new Board[2]);

			for (int f = 0; f < 2; ++f) {
				board [i] [f].action = 0;

				board [i] [f].fieldElement = Instantiate(fieldElement) as GameObject;
				board [i] [f].fieldElement.transform.SetParent(GameObject.Find ("Board").transform, false);
				board [i] [f].fieldElement.name = "FieldElement_" + i + "_" + f;
			}

			RectTransform curBoard1 = board [i] [0].fieldElement.GetComponent<RectTransform> ();
			RectTransform curBoard2 = board [i] [1].fieldElement.GetComponent<RectTransform> ();

			curBoard1.sizeDelta = new Vector2 (fieldSizeWithoutBorder, Mathf.Clamp(fieldSizeWithoutBorder, 50.0f, 125.0f));
			curBoard2.sizeDelta = new Vector2 (fieldSizeWithoutBorder, Mathf.Clamp(fieldSizeWithoutBorder, 50.0f, 125.0f));
			curBoard1.anchoredPosition = new Vector2 (fieldSize * i - (halfWidth - fieldSize / 2.0f), curBoard1.rect.height / 2.0f + fieldBorder / 2.0f);
			curBoard2.anchoredPosition = new Vector2 (fieldSize * i - (halfWidth - fieldSize / 2.0f), -(curBoard2.rect.height / 2.0f + fieldBorder / 2.0f));
		}

		gameRunning = true;
		interfaceScript.restart.gameObject.SetActive (false);
	}

	public bool GameRunning() {
		return gameRunning;
	}

	private void UpdateBoard (int p) {
		Destroy (board [round] [p].fieldAction);
		board [round] [p].fieldAction = Instantiate ((board [round] [p].action == 2) ? coopAction : cheatAction) as GameObject;
		board [round] [p].fieldAction.transform.SetParent (board [round] [p].fieldElement.transform, false);
		board [round] [p].fieldAction.name = "Coop_" + round + "_" + p;
			
		RectTransform curAction = board [round] [p].fieldAction.GetComponent<RectTransform> ();
		RectTransform curField = board [round] [p].fieldElement.GetComponent<RectTransform> ();
			
		curAction.sizeDelta = curField.sizeDelta;
		curAction.anchoredPosition = new Vector2(0.0f, 0.0f);
	}
}
