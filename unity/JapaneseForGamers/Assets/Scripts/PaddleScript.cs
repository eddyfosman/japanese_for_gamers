using UnityEngine;
using System.Collections;

public class PaddleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Horizontal") < 0){
			transform.Translate(-10f * Time.deltaTime, 0f, 0f);
		}

		if(Input.GetAxis("Horizontal") > 0){
			transform.Translate(10f * Time.deltaTime, 0f, 0f);
		}
	}
}
