using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AtkBuffScript : MonoBehaviour {

	public Text turnCount;

	public void DisplayTurn(string turn){
		turnCount.text = turn;

	}
	
	// Use this for initialization
	void Start () {
		EffectManager.onAtkBuffEnd += DisplayTurn;
	}
	
	void OnDisable(){
		EffectManager.onAtkBuffEnd -= DisplayTurn;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
