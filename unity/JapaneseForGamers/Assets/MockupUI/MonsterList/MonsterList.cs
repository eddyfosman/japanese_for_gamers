using UnityEngine;
using System.Collections.Generic;
using EnhancedUI;
using LitJson;

public class MonsterList : MonoBehaviour {
	public List<object> dataList = new List<object>();
	public EnhancedScroller scroller;

	TextAsset jsonMonsterFile;
	JsonData jsonMonsters;

	// Use this for initialization
	void Start () {
		Reload ();
	}

	public void Reload(){
		jsonMonsterFile = Resources.Load ("monsters") as TextAsset;
		jsonMonsters = JsonMapper.ToObject (jsonMonsterFile.text);

		dataList.Clear ();

		for(int i = 0; i < jsonMonsters["monsters"].Count; i++){
			dataList.Add(new MonsterData(){
				monsterID = jsonMonsters["monsters"][i]["id"].ToString(),
				monsterName = jsonMonsters["monsters"][i]["image"].ToString()

			});
		}

		scroller.Reload (dataList, 132f);

	}
	

}
