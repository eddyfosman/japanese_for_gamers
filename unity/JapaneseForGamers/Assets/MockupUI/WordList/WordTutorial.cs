using UnityEngine;
using System.Collections.Generic;
using EnhancedUI;
using LitJson;

public class WordTutorial : MonoBehaviour {

	private List<object> dataList = new List<object>();
	public EnhancedScroller scroller;

	TextAsset jsonMonsterFile;
	TextAsset jsonWordFile;
	JsonData jsonMonsters;
	JsonData jsonWords;

	// Use this for initialization
	void Start () {
		Reload ();
	}

	public void Reload(){
		jsonMonsterFile = Resources.Load ("monsters") as TextAsset;
		jsonWordFile = Resources.Load ("words") as TextAsset;
		
		jsonMonsters = JsonMapper.ToObject (jsonMonsterFile.text);
		jsonWords = JsonMapper.ToObject (jsonWordFile.text);

		dataList.Clear ();
		
		for (int i = 0; i < jsonWords["words"].Count; i++)
		{
			dataList.Add(new WordData() 
			         { 
				wordId = jsonWords["words"][i]["id"].ToString(), 
				wordWriting = jsonWords["words"][i]["writing"].ToString(), 
				wordReading = jsonWords["words"][i]["reading"].ToString(), 
				wordMeaning = jsonWords["words"][i]["meaning"].ToString()
			});
		}

		for (int i = 0; i < jsonWords["words"].Count; i++)
		{
			dataList.Add(new WordData() 
			             { 
				wordId = jsonWords["words"][i]["id"].ToString(), 
				wordWriting = jsonWords["words"][i]["writing"].ToString(), 
				wordReading = jsonWords["words"][i]["reading"].ToString(), 
				wordMeaning = jsonWords["words"][i]["meaning"].ToString()
			});
		}

		for (int i = 0; i < jsonWords["words"].Count; i++)
		{
			dataList.Add(new WordData() 
			             { 
				wordId = jsonWords["words"][i]["id"].ToString(), 
				wordWriting = jsonWords["words"][i]["writing"].ToString(), 
				wordReading = jsonWords["words"][i]["reading"].ToString(), 
				wordMeaning = jsonWords["words"][i]["meaning"].ToString()
			});
		}

		scroller.Reload (dataList, 200f);

	}
	

}
