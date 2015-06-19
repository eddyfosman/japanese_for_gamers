using UnityEngine;
using System.Collections;

public class WordKeeper : MonoBehaviour {

	public GameObject canvas;
	Vector3 canvasPos;
	GameObject canvasWord;

	// Use this for initialization
	void Start () {
		canvasPos = gameObject.transform.position;
		canvasWord = Instantiate (canvas, canvasPos, Quaternion.Euler(0f,0f,0f)) as GameObject;
		Invoke ("Destroyer", 2f);
	}

	void Destroyer(){
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {

		canvasPos = gameObject.transform.position;
		canvasWord.transform.position = canvasPos;
	}
}
