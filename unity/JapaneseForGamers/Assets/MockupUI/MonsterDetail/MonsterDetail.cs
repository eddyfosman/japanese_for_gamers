using UnityEngine;
using System.Collections.Generic;
using EnhancedUI;
using LitJson;
public class MonsterDetail : MonoBehaviour {

	public List<object> dataList = new List<object> ();
	public EnhancedScroller scroller;
	TextAsset jsonMonsterFile;
	JsonData jsonMonsters;

	TextAsset jsonWordFile;
	JsonData jsonWords;

	public UnityEngine.UI.Text monsterID;
	public UnityEngine.UI.Text monsterName;
	public UnityEngine.UI.Image monsterImage;
	public UnityEngine.UI.Button opBackButton;

	List<string> wordList = new List<string>();
	SceneTransitionData sceneData = new SceneTransitionData();


	// Use this for initialization
	void Start () {
		SetOpBackButton ();
		Reload ();
	}

	void SetOpBackButton(){
		if(!sceneData.IsFromMonsterListScene){
			opBackButton.gameObject.SetActive(true);
		}
		else{
			opBackButton.gameObject.SetActive(false);
		}
	}

	public void Reload(){
		jsonMonsterFile = Resources.Load ("monsters") as TextAsset;
		jsonMonsters = JsonMapper.ToObject (jsonMonsterFile.text);

		jsonWordFile = Resources.Load ("words") as TextAsset;
		jsonWords = JsonMapper.ToObject (jsonWordFile.text);

		MonsterData selectedMonster = new MonsterData ();
		monsterID.text = selectedMonster.SelectedID;
		monsterName.text = selectedMonster.SelectedMonsterName;
		monsterImage.sprite = Resources.Load ("Sprites/" + selectedMonster.SelectedMonsterName, typeof(Sprite)) as Sprite;
//		Debug.Log (selectedMonster.SelectedID);
//		Debug.Log (selectedMonster.SelectedMonsterName);

		for(int i = 0; i < jsonMonsters["monsters"].Count; i++){
			Debug.Log(jsonMonsters["monsters"][i]["id"].ToString());
			if(jsonMonsters["monsters"][i]["id"].ToString() == selectedMonster.SelectedID){
				for(int j = 0; j < jsonMonsters["monsters"][i]["words"].Count; j++){
					wordList.Add(jsonMonsters["monsters"][i]["words"][j]["id"].ToString());
					Debug.Log(jsonMonsters["monsters"][i]["words"][j]["id"].ToString());
				}
			}
		}

		dataList.Clear ();

		for(int i = 0; i < jsonWords["words"].Count; i++){
			for(int j = 0; j < wordList.Count; j++){
				if(jsonWords["words"][i]["id"].ToString() == wordList[j]){
					dataList.Add(new TextData(){
						wordID = wordList[j],
						wordWriting = jsonWords["words"][i]["writing"].ToString(),
						wordMeaning = jsonWords["words"][i]["meaning"].ToString(),
						wordReading = jsonWords["words"][i]["reading"].ToString()

					});
					Debug.Log("Chu duoc gan la: " + jsonWords["words"][i]["writing"].ToString());
					wordList.Remove(wordList[j]);
					break;
				}
			}
		}

		scroller.Reload (dataList, 100f);
	}
	
	public void BackToMonsterListScene(){
		sceneData.IsFromMonsterListScene = true;
		Application.LoadLevel (2);
	}

	public void BackToWordDetailScene(){
		Application.LoadLevel (1);
	}


}
