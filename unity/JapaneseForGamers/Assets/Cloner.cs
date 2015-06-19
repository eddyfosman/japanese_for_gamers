using UnityEngine;
using System.Collections;

public class Cloner : MonoBehaviour {

	PaddleManager paddleManagerScript;

	// Use this for initialization
	void Start () {
		paddleManagerScript = GameObject.Find ("PaddleManager").GetComponent<PaddleManager> (); 
		Destroy (gameObject, 5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if(col.name == "Paddle"){
			paddleManagerScript.Cloner();
			Destroy(gameObject);
		}
	}

}
