using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Card : MonoBehaviour {
	public bool isFinished = false;
	public bool isFlipped = false;
	public string cardString;
	MemoryMatching memoryMatchingScript;

	void FlipCardBack(){
		iTween.RotateTo (gameObject, new Vector3 (0, 0, 0), 1f);
		if(!isFlipped && memoryMatchingScript.CardFlippedNum < 2){
			isFlipped = true;
			GetComponentInChildren<Text> ().text = cardString;
			memoryMatchingScript.CardFlippedNum += 1;
		}
	}

	public void FlipCard(){
//		iTween.RotateTo (gameObject, new Vector3 (0, 90f, 0), 1f);
		if(!isFlipped && memoryMatchingScript.CardFlippedNum < 2){
			iTween.RotateTo (gameObject, iTween.Hash("rotation",new Vector3 (0, 90f, 0), "oncomplete", "FlipCardBack", "time", 1 ));

		}
	}

	// Use this for initialization
	void Start () {
		memoryMatchingScript = GameObject.Find ("MemoryManager").GetComponent<MemoryMatching> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
