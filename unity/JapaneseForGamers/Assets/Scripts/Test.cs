﻿using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	Renderer render;

	// Use this for initialization
	void Start () {
		render = GameObject.Find ("Rope").GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 screenPos = camera.WorldToScreenPoint(GameObject.Find("Rope").transform.position);
		Vector3 screenPos2 = camera.WorldToScreenPoint(GameObject.Find("TipPoint").transform.position);
		Vector3 renderSize = camera.WorldToScreenPoint(render.bounds.size);
		Debug.Log ("Vi tri cua day2 pixel : " + screenPos);
		Debug.Log ("Kich co hop quanh day pixel : " + renderSize);
		Debug.Log ("Vi tri cua dau day pixel : " + screenPos2);
	}
}