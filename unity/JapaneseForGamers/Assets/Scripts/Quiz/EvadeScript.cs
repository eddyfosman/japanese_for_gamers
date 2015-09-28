using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EvadeScript : MonoBehaviour {
	
	
	
	
	public Text turnCount;
	
	
	public void DisplayTurn(string turn){
		turnCount.text = turn;
		//		EffectManager.onRegenEnd -= DisplayTurn;
		
	}
	
	// Use this for initialization
	void Start () {
		EffectManager.onEvadeEnd += DisplayTurn;
	}
	
	void OnDisable(){
		EffectManager.onEvadeEnd -= DisplayTurn;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
