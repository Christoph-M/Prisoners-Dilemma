using UnityEngine;
using System.Collections;

public class Strategy : MonoBehaviour {
	protected AI aiScript;

	void Start() {
		aiScript = FindObjectOfType<AI> ();
	}

	public virtual int GetAction(int p)  { return -1; }
}
