using UnityEngine;
using System.Collections.Generic;
using LitJson;
using EnhancedUI;

public class Scene2 : MonoBehaviour {

	private List<object> dataList = new List<object>();
	public EnhancedScroller scroller;

	public UnityEngine.UI.Text kanjiText;
	public UnityEngine.UI.Text readingText;
	public UnityEngine.UI.Text meaningText;
//	public UnityEngine.UI.Text writingText;
	public UnityEngine.UI.Text wordID;

	TextAsset jsonWordsFile;
	TextAsset jsonMonsterFile;
	JsonData jsonMonsters;
	JsonData jsonWords;
	List<string> monsterIdList = new List<string>();


	// Use this for initialization
	void Start () {
		Reload ();
	}

	public void Reload(){

		jsonMonsterFile = Resources.Load ("monsters") as TextAsset;
		jsonWordsFile = Resources.Load ("words") as TextAsset;
		jsonMonsters = JsonMapper.ToObject (jsonMonsterFile.text);
		jsonWords = JsonMapper.ToObject (jsonWordsFile.text);

		WordData selectedWord = new WordData ();
		kanjiText.text = selectedWord.WritingText;
		readingText.text = selectedWord.ReadingText;
		meaningText.text = selectedWord.MeaningText;
//		writingText;
		wordID.text = selectedWord.IDSelected;

		for(int i = 0; i < jsonWords["words"].Count; i++){
			if(jsonWords["words"][i]["id"].ToString() == selectedWord.IDSelected){
				for(int j = 0; j < jsonWords["words"][i]["monsters"].Count; j++){
					monsterIdList.Add(jsonWords["words"][i]["monsters"][j]["id"].ToString());
				}
			}
		}


		dataList.Clear ();

		for(int i = 0; i < jsonMonsters["monsters"].Count; i++){
			for(int j = 0; j < monsterIdList.Count; j++){
				if(jsonMonsters["monsters"][i]["id"].ToString() == monsterIdList[j]){
					dataList.Add(new ImageData()
					             {
						monsterID = jsonMonsters["monsters"][i]["image"].ToString()
					});
					monsterIdList.Remove(monsterIdList[j]);
					break;
				}
			}


		}

		scroller.Reload (dataList, 100f);
	}

	public void LoadSceneWord(){
		Application.LoadLevel(0);
	}

}
