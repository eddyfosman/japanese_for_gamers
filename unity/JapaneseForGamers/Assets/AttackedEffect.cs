using UnityEngine;
using System.Collections;

public class AttackedEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.ShakePosition(gameObject, iTween.Hash("x", 0.5, "easeType", "linearTween", "loopType", "pingPong", "delay", .01));
	}
	

}
