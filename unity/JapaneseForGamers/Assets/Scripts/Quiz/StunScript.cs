using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StunScript : MonoBehaviour {
	
	
	
	
	public Text turnCount;
	
	//	void Awake(){
	//		effectManager = GameObject.FindObjectOfType<EffectManager>();
	//
	//		if(effectManager != null){
	//			effectManager.onRegenEnd += DisplayTurn;
	//		}
	//
	//	}
	
	
	
	public void DisplayTurn(string turn){
		turnCount.text = turn;
		//		EffectManager.onRegenEnd -= DisplayTurn;
		
	}
	
	// Use this for initialization
	void Start () {
		EffectManager.onStunEnd += DisplayTurn;
	}
	
	void OnDisable(){
		EffectManager.onStunEnd -= DisplayTurn;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
