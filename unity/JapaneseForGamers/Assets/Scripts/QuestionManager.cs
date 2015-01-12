﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using LitJson;


public class QuestionManager : MonoBehaviour {
	public Text question;
	public Text answerA;
	public Text answerB;
	public Text answerC;
	public Text answerD;
	private QuestionBean qb ;
	public GameObject monsterGameObject;
	private Image monsterImage;
	public GameObject playerHealthBar;
	private PlayerHealthBar playerHealthBarScript;
	public GameObject timeBar;
	private TimeBar timeBarScript;
	public GameObject enemyHealthBar;
	private EnemyHealthBar enemyHealthBarScript;
	int monsterID = 0;


	public class MonsterBean{
		int id;
		public int ID {
			get;
			set;
		}
		string name;
		public string Name {
			get;
			set;
		}
		int level;
		public int Level {
			get;
			set;
		}
		int hp;
		public int Hp {
			get;
			set;
		}
		int atk;
		public int Atk {
			get;
			set;
		}
		int def;
		public int Def {
			get;
			set;
		}
		string image;
		public string Image {
			get;
			set;
		}


	}

	public class KanjiBean{
		int id;
		public int ID {
						get;
						set;
		}
		string writing;
		public string Writing {
						get;
						set;
		}
		string meaning;
		public string Meaning {
						get;
						set;
		}
		string on;
		public string OnReading {
						get;
						set;
		}
		string kun;
		public string KunReading {
						get;
						set;
		}

	}

	public class QuestionBean{
		public string question;
		public string answerA;
		public string answerB;
		public string answerC;
		public string answerD;
		public string rightAnswer;
	}

	public List<KanjiBean> kanjiList;
	public List<MonsterBean> monsterList;
	public List<QuestionBean> textToRead = new List<QuestionBean>();


	
	public void Read(){

		string curline; //current line
		System.IO.StreamReader file;

		StringReader reader;
		if(Application.platform == RuntimePlatform.Android){
			TextAsset bindata= Resources.Load("data") as TextAsset;
			reader = new StringReader(bindata.text);
			while((curline = reader.ReadLine()) != null)
			{
				string[] ans = curline.Split(";"[0]);
				QuestionBean qb = new QuestionBean();
				qb.question = ans[0];
				qb.answerA = ans[1];
				qb.answerB = ans[2];
				qb.answerC = ans[3];
				qb.answerD = ans[4];
				qb.rightAnswer = ans[5];
				textToRead.Add(qb);
			}

		}
		else{

			file = new System.IO.StreamReader ("Assets/Resources/data.txt");
			TextAsset jsonFile = Resources.Load("data2") as TextAsset;
			TextAsset jsonMonsterFile = Resources.Load("monster") as TextAsset;
			JsonData jsonKanjis = JsonMapper.ToObject(jsonFile.text);
			JsonData jsonMonster = JsonMapper.ToObject(jsonMonsterFile.text);
			KanjiBean kanji;
			MonsterBean monster;
			kanjiList = new List<KanjiBean>();
			monsterList = new List<MonsterBean>();

			for(int i = 0; i < jsonMonster["monsters"].Count; i++){
				monster = new MonsterBean();
				monster.ID = System.Convert.ToInt16(jsonMonster["monsters"][i]["id"].ToString());
				monster.Name = jsonMonster["monsters"][i]["name"].ToString();
				monster.Level = System.Convert.ToInt16(jsonMonster["monsters"][i]["level"].ToString());
				monster.Hp = System.Convert.ToInt16(jsonMonster["monsters"][i]["hp"].ToString());
				monster.Atk = System.Convert.ToInt16(jsonMonster["monsters"][i]["atk"].ToString());
				monster.Def = System.Convert.ToInt16(jsonMonster["monsters"][i]["def"].ToString());
				monster.Image = jsonMonster["monsters"][i]["image"].ToString();

				monsterList.Add(monster);
			}
			for(int i = 0; i < monsterList.Count; i++){
				Debug.Log(monsterList[i].ID);
			}

			for(int i = 0; i<jsonKanjis["kanjis"].Count; i++)
			{

				kanji = new KanjiBean();
				kanji.ID = System.Convert.ToInt16(jsonKanjis["kanjis"][i]["id"].ToString());
				kanji.Writing = jsonKanjis["kanjis"][i]["writing"].ToString();
				kanji.Meaning = jsonKanjis["kanjis"][i]["meaning"].ToString();
				kanji.OnReading = jsonKanjis["kanjis"][i]["on"].ToString();
				kanji.KunReading = jsonKanjis["kanjis"][i]["kun"].ToString();
				

				
				kanjiList.Add(kanji);
			}


			for(int i = 0; i < kanjiList.Count; i++){
				QuestionBean qb = new QuestionBean();
				qb.question = kanjiList[i].Writing;
				qb.rightAnswer = kanjiList[i].Meaning;
				qb.answerA = kanjiList[i].Meaning;
				KanjiBean kanjiWord = new KanjiBean();
				kanjiWord = GetRandomKanji();
				while(kanjiWord.Meaning == qb.answerA){
					kanjiWord = GetRandomKanji();
				}
				qb.answerB = kanjiWord.Meaning;
				kanjiWord = GetRandomKanji();
				while(kanjiWord.Meaning == qb.answerA || kanjiWord.Meaning == qb.answerB){
					kanjiWord = GetRandomKanji();
				}
				qb.answerC = kanjiWord.Meaning;
				kanjiWord = GetRandomKanji();
				while(kanjiWord.Meaning == qb.answerA || kanjiWord.Meaning == qb.answerB || kanjiWord.Meaning == qb.answerC){
					kanjiWord = GetRandomKanji();
				}
				qb.answerD = kanjiWord.Meaning;
				textToRead.Add(qb);
			}

//			while((curline = file.ReadLine()) != null)
//			{
//				string[] ans = curline.Split(";"[0]);
//				QuestionBean qb = new QuestionBean();
////				for(int i = 0; i < ans.Length; i++){
////					Debug.Log(ans[i]);
////				}
//				qb.question = ans[0];
//				qb.answerA = ans[1];
//				qb.answerB = ans[2];
//				qb.answerC = ans[3];
//				qb.answerD = ans[4];
//				qb.rightAnswer = ans[5];
//				textToRead.Add(qb);
//			}
		}



	}



	public QuestionBean GetRandomQuestion(){
		int q = Random.Range (0, textToRead.Count);
		return textToRead [q];
	}

	public KanjiBean GetRandomKanji(){
		int q = Random.Range (0, kanjiList.Count);
		return kanjiList [q];
	}

	private void Shuffle<T>(List<T> list){
		int count = list.Count;
		for(int i = count - 1; i > 0; i--){
			int randIndex = Random.Range(0, i);
			T temp = list[i];
			list[i] = list[randIndex];
			list[randIndex] = temp;

		}
	}

	public void SetMonster(){
		MonsterBean monster = monsterList [monsterID];
		enemyHealthBarScript.maxHealth = monster.Hp;
		monsterImage.sprite = Resources.Load<Sprite> (monster.Image);
		monsterID++;
	}

	public void SetQuestion(){
		qb = GetRandomQuestion ();
		question.text = qb.question;
		List<string> list = new List<string> ();
		list.Add (qb.answerA);
		list.Add (qb.answerB);
		list.Add (qb.answerC);
		list.Add (qb.answerD);
//		for(int i = 0; i < list.Count; i++){
//			Debug.Log(list[i]);
//		}
		Shuffle (list);
//		for(int i = 0; i < list.Count; i++){
//			Debug.Log(list[i]);
//		}
		answerA.text = list[0];
		answerB.text = list[1];
		answerC.text = list[2];
		answerD.text = list[3];

	}

	public void CheckAnswerA(){
		if(answerA.text == qb.rightAnswer){
			Debug.Log("Answer A is right!");
			enemyHealthBarScript.Damage();
			timeBarScript.ResetTimeBar();
		}
		else{
			playerHealthBarScript.Damage();
//			SetQuestion();
			timeBarScript.ResetTimeBar();
		}
	}

	public void CheckAnswerB(){
		if(answerB.text == qb.rightAnswer){
//			Debug.Log("Answer B is right!");
			enemyHealthBarScript.Damage();
			timeBarScript.ResetTimeBar();
		}
		else{
			playerHealthBarScript.Damage();
//			SetQuestion();
			timeBarScript.ResetTimeBar();
		}
	}

	public void CheckAnswerC(){
		if(answerC.text == qb.rightAnswer){
//			Debug.Log("Answer C is right!");
			enemyHealthBarScript.Damage();
			timeBarScript.ResetTimeBar();
		}
		else{
			playerHealthBarScript.Damage();
//			SetQuestion();
			timeBarScript.ResetTimeBar();
		}
	}

	public void CheckAnswerD(){
		if(answerD.text == qb.rightAnswer){
//			Debug.Log("Answer D is right!");
			enemyHealthBarScript.Damage();
			timeBarScript.ResetTimeBar();
		}
		else{
			playerHealthBarScript.Damage();
//			SetQuestion();
			timeBarScript.ResetTimeBar();
		}
	}

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.Portrait;
		monsterImage = monsterGameObject.GetComponent<Image> ();
		playerHealthBarScript = playerHealthBar.GetComponent<PlayerHealthBar> ();
		enemyHealthBarScript = enemyHealthBar.GetComponent<EnemyHealthBar> ();
		timeBarScript = timeBar.GetComponent<TimeBar> ();
		Read ();
		SetMonster ();
//		timeBarScript.ResetTimeBar ();
		SetQuestion ();
//		Debug.Log (textToRead.Count);


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
