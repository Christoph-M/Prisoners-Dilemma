using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Interface : MonoBehaviour {
	public Game gameScript;
	public Canvas canvas;
	public Button restart;
	public Button coopP1;
	public Button coopP2;
	public Button chratP1;
	public Button chratP2;
	public Slider boardSizeSlider;
	public Text round;
	public Text scoreP1;
	public Text scoreP2;
	public Text boardSize;


	void Start() {
		boardSizeSlider.value = gameScript.boardSize;
		boardSize.text = "" + gameScript.boardSize;
	}

	public void Coop(int p) {
		gameScript.SetAction (p, (UInt16)2);
	}

	public void Cheat(int p) {
		gameScript.SetAction (p, (UInt16)4);
	}

	public void Restart() {
		gameScript.ResetGame ();
		this.SetButtonsEnabled (true);
	}

	public void ChangeBoardSize() {
		gameScript.boardSize = (uint)boardSizeSlider.value;
		boardSize.text = "" + gameScript.boardSize;
	}

	public void SetButtonsEnabled(bool b) {
		coopP1.enabled = b;
		coopP2.enabled = b;
		chratP1.enabled = b;
		chratP2.enabled = b;
	}
}
