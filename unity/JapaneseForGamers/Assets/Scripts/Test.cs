using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	Renderer render;

	// Use this for initialization
	void Start () {
		render = GameObject.Find ("Staff").GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 screenPos = GetComponent<Camera>().WorldToScreenPoint(GameObject.Find("Staff").transform.position);
		//Vector3 screenPos2 = GetComponent<Camera>().WorldToScreenPoint(GameObject.FindGameObjectWithTag("Grabber").transform.position);
		//Vector3 renderSize = GetComponent<Camera>().WorldToScreenPoint(render.bounds.size);
//		Debug.Log ("Vi tri cua day2 pixel : " + screenPos);
//		Debug.Log ("Kich co hop quanh day pixel : " + renderSize);
//		Debug.Log ("Vi tri cua dau day pixel : " + screenPos2);
	}
}
