using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PoisonScript : MonoBehaviour {

	public Text turnCount;

	public void DisplayTurn(string turn){
		turnCount.text = turn;

	}
	
	void Start () {
		EffectManager.onPoisonEnd += DisplayTurn;
	}
	
	void OnDisable(){
		EffectManager.onPoisonEnd -= DisplayTurn;
	}

}
