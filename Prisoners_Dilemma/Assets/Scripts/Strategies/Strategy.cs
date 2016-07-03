using UnityEngine;
using System.Collections;

public class Strategy : MonoBehaviour {
	public virtual int GetAction()  { return -1; }
}
