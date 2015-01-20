using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using LitJson;


public class PlayerData{
	int level;
	public int Level {
		set;
		get;
	}
	int hp;
	public int Hp {
		set;
		get;
	}
	int atk;
	public int Atk {
		set;
		get;
	}
	int def;
	public int Def {
		set;
		get;
	}
	int currentExp;
	public int CurrentExp {
		set;
		get;
	}
	int nextLevelExp;
	public int NextLevelExp {
		set;
		get;
	}
	
}

	

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
	int exp;
	public int Exp {
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

public class QuestionManager : MonoBehaviour {
	public Button buttonA;
	public Button buttonB;
	public Button buttonC;
	public Button buttonD;
	private Selectable buttonASelectable;
	private Selectable buttonBSelectable;
	private Selectable buttonCSelectable;
	private Selectable buttonDSelectable;


	public GameObject statusDialog;
	private StatusDialogScript statusDialogScript;

	public GameObject questionPanel;
	private CanvasGroup questionCanvasGroup;
	private QuestionFadeScript questionFadeScript;
	public GameObject monsterPanel;
	private CanvasGroup monsterCanvasGroup;
	private MonsterFadeScript monsterFadeScript;
	public Text test;
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
	PlayerData player;
	private int monsterExp;







	public PlayerData GetPlayerData(){
		Debug.Log ("Gui nhan vat");
		return player;
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


		if(Application.platform == RuntimePlatform.Android){
			string jsonPlayerDataFile = "";

			while(!File.Exists(Application.persistentDataPath + "/" + "PlayerData.json")){
				Debug.Log("File khong ton tai!");
				string str = "{\n\t\"player\":[\n\t\t{\n\t\t\t\"level\":1,\n\t\t\t\"hp\":100,\n\t\t\t\"atk\":5,\n\t\t\t\"def\":0,\n\t\t\t\"exp\":0,\n\t\t\t\"nextLevelExp\":100\n\t\t}\n\t]\n}";
				Debug.Log(str);

				File.WriteAllText(Application.persistentDataPath + "/" + "PlayerData.json", str);
			
			jsonPlayerDataFile = File.ReadAllText(Application.persistentDataPath + "/" + "PlayerData.json");
			}
			JsonData jsonPlayerData = JsonMapper.ToObject(jsonPlayerDataFile);

			test.text =jsonPlayerData["player"][0]["hp"].ToString();
			TextAsset jsonFile = Resources.Load("data2") as TextAsset;
			TextAsset jsonMonsterFile = Resources.Load("monster") as TextAsset;
//			TextAsset jsonPlayerDataFile = Resources.Load("PlayerData") as TextAsset;
			JsonData jsonKanjis = JsonMapper.ToObject(jsonFile.text);
			JsonData jsonMonster = JsonMapper.ToObject(jsonMonsterFile.text);
//			JsonData jsonPlayerData = JsonMapper.ToObject(jsonPlayerDataFile.text);
			KanjiBean kanji;
			MonsterBean monster;
//			PlayerData player;
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
				monster.Exp = System.Convert.ToInt16(jsonMonster["monsters"][i]["exp"].ToString());
				monster.Image = jsonMonster["monsters"][i]["image"].ToString();
				
				monsterList.Add(monster);
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

		}
		else{


			TextAsset jsonFile = Resources.Load("data2") as TextAsset;
			TextAsset jsonMonsterFile = Resources.Load("monster") as TextAsset;
			TextAsset jsonPlayerDataFile = Resources.Load("PlayerData") as TextAsset;

//			if(jsonPlayerDataFile == null){
//				Debug.Log("File khong ton tai!");
//				string str = "{\n\t\"player\":[\n\t\t{\n\t\t\t\"level\":1,\n\t\t\t\"hp\":100,\n\t\t\t\"atk\":5,\n\t\t\t\"def\":0,\n\t\t\t\"exp\":0,\n\t\t\t\"nextLevelExp\":100\n\t\t}\n\t]\n}";
//				Debug.Log(str);
//			}
			JsonData jsonKanjis = JsonMapper.ToObject(jsonFile.text);
			JsonData jsonMonster = JsonMapper.ToObject(jsonMonsterFile.text);
			JsonData jsonPlayerData = JsonMapper.ToObject(jsonPlayerDataFile.text);

			KanjiBean kanji;
			MonsterBean monster;

			player = new PlayerData();
			Debug.Log("Cai nay chay truoc hay sau!");
			player.Hp = System.Convert.ToInt16(jsonPlayerData["player"][0]["hp"].ToString());
			player.Level = System.Convert.ToInt16(jsonPlayerData["player"][0]["level"].ToString());
			player.Atk = System.Convert.ToInt16(jsonPlayerData["player"][0]["atk"].ToString());
			player.Def = System.Convert.ToInt16(jsonPlayerData["player"][0]["def"].ToString());
			player.CurrentExp = System.Convert.ToInt16(jsonPlayerData["player"][0]["exp"].ToString());
			player.NextLevelExp = System.Convert.ToInt16(jsonPlayerData["player"][0]["nextLevelExp"].ToString());

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
				monster.Exp = System.Convert.ToInt16(jsonMonster["monsters"][i]["exp"].ToString());
				monster.Image = jsonMonster["monsters"][i]["image"].ToString();

				monsterList.Add(monster);
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

	public void GainExp(){
		player.CurrentExp += monsterExp;
		if(player.CurrentExp >= player.NextLevelExp){
			player.Level += 1;
			Debug.Log("Cong level mot lan!");
			statusDialog.SetActive(true);

		}
	}

	public void SetMonster(){
		MonsterBean monster = monsterList [monsterID];
		enemyHealthBarScript.maxHealth = monster.Hp;
		monsterExp = monster.Exp;
		monsterImage.sprite = Resources.Load<Sprite> (monster.Image);
		monsterCanvasGroup.alpha = 1f;
		StartCoroutine (monsterFadeScript.FadeToBlack(0.5f));
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

		Shuffle (list);

		answerA.text = list[0];
		answerB.text = list[1];
		answerC.text = list[2];
		answerD.text = list[3];

	}

	public void CheckAnswerA(){
		if(answerA.text == qb.rightAnswer){

			enemyHealthBarScript.Damage();
			timeBarScript.ResetTimeBar();
		}
		else{
			playerHealthBarScript.Damage();

			timeBarScript.ResetTimeBar();
		}
	}

	public void CheckAnswerB(){
		if(answerB.text == qb.rightAnswer){

			enemyHealthBarScript.Damage();
			timeBarScript.ResetTimeBar();
		}
		else{
			playerHealthBarScript.Damage();

			timeBarScript.ResetTimeBar();
		}
	}

	public void CheckAnswerC(){
		if(answerC.text == qb.rightAnswer){

			enemyHealthBarScript.Damage();
			timeBarScript.ResetTimeBar();
		}
		else{
			playerHealthBarScript.Damage();

			timeBarScript.ResetTimeBar();
		}
	}

	public void CheckAnswerD(){
		if(answerD.text == qb.rightAnswer){

			enemyHealthBarScript.Damage();
			timeBarScript.ResetTimeBar();
		}
		else{
			playerHealthBarScript.Damage();

			timeBarScript.ResetTimeBar();
		}
	}

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.Portrait;
		buttonASelectable = buttonA.GetComponent<Selectable>();
		buttonBSelectable = buttonB.GetComponent<Selectable>();
		buttonCSelectable = buttonC.GetComponent<Selectable>();
		buttonDSelectable = buttonD.GetComponent<Selectable>();
		statusDialog.SetActive (false);
		questionCanvasGroup = questionPanel.GetComponent<CanvasGroup>();
		monsterCanvasGroup = monsterPanel.GetComponent<CanvasGroup>();
		statusDialogScript = statusDialog.GetComponent<StatusDialogScript> ();
		questionFadeScript = questionPanel.GetComponent<QuestionFadeScript>();
		monsterImage = monsterGameObject.GetComponent<Image> ();
		monsterFadeScript = monsterPanel.GetComponent<MonsterFadeScript>();
		playerHealthBarScript = playerHealthBar.GetComponent<PlayerHealthBar> ();
		enemyHealthBarScript = enemyHealthBar.GetComponent<EnemyHealthBar> ();
		timeBarScript = timeBar.GetComponent<TimeBar> ();
		Read ();
		SetMonster ();
		SetQuestion ();



	}
	
	// Update is called once per frame
	void Update () {
		if(!questionFadeScript.onCD && !monsterFadeScript.onCD){

			buttonASelectable.interactable = true;
			buttonBSelectable.interactable = true;
			buttonCSelectable.interactable = true;
			buttonDSelectable.interactable = true;
		}
		else if(questionFadeScript.onCD || monsterFadeScript.onCD){

			buttonASelectable.interactable = false;
			buttonBSelectable.interactable = false;
			buttonCSelectable.interactable = false;
			buttonDSelectable.interactable = false;
		}
	}
}
