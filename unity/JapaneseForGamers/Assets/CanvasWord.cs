using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasWord : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("VI TRI CUA CANVAS :" + transform.position);
		Invoke ("Destroyer", 2f);
		Text textCanvas = gameObject.gameObject.GetComponentInChildren<Text> ();
		int rnd = Random.Range (0, 10);

		if(0 == rnd){
			textCanvas.text = "十";
		}
		else if(1 == rnd){
			textCanvas.text = "一";
		}
		else if(2 == rnd){
			textCanvas.text = "二";		
		}
		else if(3 == rnd){
			textCanvas.text = "三";		
		}
		else if(4 == rnd){
			textCanvas.text = "四";		
		}
		else if(5 == rnd){
			textCanvas.text = "五";
		}
		else if(6 == rnd){
			textCanvas.text = "六";
		}
		else if(7 == rnd){
			textCanvas.text = "七";
		}
		else if(8 == rnd){
			textCanvas.text = "八";
		}
		else{
			textCanvas.text = "九";
		}
	}

	
	void Destroyer(){
		Destroy (gameObject);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
