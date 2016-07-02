﻿using UnityEngine;
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

	void Start () {
		this.ResetGame ();
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

			if (round == rounds) {
				--round;

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

		interfaceScript.restart.gameObject.SetActive (false);
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
