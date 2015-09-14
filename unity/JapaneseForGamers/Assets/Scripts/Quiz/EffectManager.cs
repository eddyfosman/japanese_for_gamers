using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectManager : MonoBehaviour {

	public delegate void OnRegenEnd (string turn);
	public static event OnRegenEnd onRegenEnd;

	public delegate void OnStunEnd (string turn);
	public static event OnStunEnd onStunEnd;

	public GameObject playerHealthBarGO;
	private PlayerHealthBar playerHealthBarScript;

	private List<Effect> effectList;

	private bool isRegenAdded = false;
	private bool isStunAdded = false;
	public bool IsEffectAdded(){

			return (isRegenAdded || isStunAdded);

	}

	public void ExecuteOnStunEvent(Effect effect){
		if(onStunEnd != null){
			onStunEnd(effect.Turn.ToString());
		}
	}

	public void AddEffectIntoList(Effect effect){
		if(effect != null){
			if(effect.Type == "regen" && !isRegenAdded){
				effectList.Add(effect);
				isRegenAdded = true;
			}
			if(effect.Type == "stun" && !isStunAdded){
				effectList.Add(effect);
				isStunAdded = true;
			}
			if(effect.Type == "regen" && isRegenAdded){
				for(int i = 0; i < effectList.Count; i++){
					if(effectList[i].Type == "regen"){
						effectList[i].Turn = 3;
					}
				}
			}
			if(effect.Type == "stun" && isStunAdded){
				for(int i = 0; i < effectList.Count; i++){
					if(effectList[i].Type == "stun"){
						effectList[i].Turn = 1;
					}
				}
			}

		}
	}

	public void ApplyEffect(string effectType){
		if(effectType == "regen"){
			for(int i = 0; i < effectList.Count; i++){
				if(effectList[i].Type == "regen"){
					if(effectList[i].Turn > 0){
						effectList[i].Turn--;
						if(onRegenEnd != null){
							onRegenEnd(effectList[i].Turn.ToString());
						}
						playerHealthBarScript.Heal(effectList[i].Value);
					}
					if(effectList[i].Turn == 0){
						effectList.Remove(effectList[i]);
						isRegenAdded = false;
					}
				}
			}
		}
		if(effectType == "stun"){
			for(int i = 0; i < effectList.Count; i++){
				if(effectList[i].Type == "stun"){
					if(effectList[i].Turn > 0){
						effectList[i].Turn--;
						if(onStunEnd != null){
							onStunEnd(effectList[i].Turn.ToString());
						}
					}
					if(effectList[i].Turn == 0){
						effectList.Remove(effectList[i]);
						isStunAdded = false;
					}
				}
			}
		}
	}
	
	void Start(){
		effectList = new List<Effect> ();
		playerHealthBarScript = playerHealthBarGO.GetComponent<PlayerHealthBar>();
	}
}