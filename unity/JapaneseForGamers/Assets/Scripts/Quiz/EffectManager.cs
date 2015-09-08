using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectManager : MonoBehaviour {
	private List<Effect> effectList;

	public void AddEffectIntoList(Effect effect){
		if(effect != null){
			effectList.Add(effect);
		}
	}

	void Start(){
		effectList = new List<Effect> ();
	}
}
