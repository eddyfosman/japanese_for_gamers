using UnityEngine;
using System.Collections;

public class Effect{
	public string Type {
		get;
		set;
	}
	
	public string Apply {
		get;
		set;
	}
	
	public int Value {
		get;
		set;
	}
	
	public int Turn {
		set;
		get;
	}
}

public class MonsterEquip : MonoBehaviour {
	
	public string ID {
		get;
		set;
	}
	
	public string Image {
		get;
		set;
	}

	public float PosX {
				get;
				set;

	}

	public float PosY {
				get;
				set;
	}
	
	private Hashtable bonus = new Hashtable();
	public void AddValue(string key, string value){
		if(key != null && value != null){
			bonus.Add(key, value);
		}
	}
	
	private Effect effect = new Effect ();
	
	public Effect EffectProperty{
		get{
			return effect;
		}
	}
	
	
}