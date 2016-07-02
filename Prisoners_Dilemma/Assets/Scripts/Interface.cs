using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Interface : MonoBehaviour {
	public Game gameScript;
	public Canvas canvas;

	public void Coop(int p) {
		gameScript.SetAction (p, (UInt16)1);
	}

	public void Cheat(int p) {
		gameScript.SetAction (p, (UInt16)2);
	}
}
