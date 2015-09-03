using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class PlayerData
{
		int level;

		public int Level {
				set;
				get;
		}

		long hp;

		public long Hp {
				set;
				get;
		}

		long atk;

		public long Atk {
				set;
				get;
		}

		long def;

		public long Def {
				set;
				get;
		}

		long agi;

		public long Agi {
				set;
				get;
		}

		long luk;

		public long Luk {
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

		int bonusPoint;

		public int BonusPoint {
				set;
				get;
		}
}

public class MonsterBean
{
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

		long hp;

		public long Hp {
				get;
				set;
		}

		long atk;

		public long Atk {
				get;
				set;
		}

		long def;

		public long Def {
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

public class KanjiBean
{
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

		string onSound;

		public string OnSound{ get; set; }

		string kunSound;

		public string KunSound{ get; set; }
	
}

public class QuestionManager : MonoBehaviour
{

		public Text visualDamage;
		private GameObject[] answerText;
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
//		public Text test;
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
		private PlayerData player;
		private MonsterBean monster;
		private int monsterExp;
		public GameObject slashGO;
		public GameObject thunderGO;
		public GameObject chargeGO;
		ParticleSystem chargeParticle;
		ParticleSystem thunderParticle;
		ParticleSystem particle;
		string textClickedButton;
		Vector3 thunderParticlePos;
		public GameObject deadGO;
		ParticleSystem deadParticle;
		GameObject monsterImg;
		AttackedEffect attackedEffectScript;
		iTween itweenScript;
		Image imageScript;
		public GameObject soundManager;
		List<WordInfo> listWordInfo = new List<WordInfo> ();
		AudioSource audioSource;
	public GameObject monsterManager;
	MonsterManager monsterManagerScript;
	public bool isMonsterMoveOut = false;
	FadeAwayVisualDamage fadeAwayVisualDamageScript;
	public Text prefabTextUI;
	Text charGO;

		void Start ()
		{
		monsterManagerScript = monsterManager.GetComponent<MonsterManager>();
		fadeAwayVisualDamageScript = visualDamage.GetComponent<FadeAwayVisualDamage> ();
		monsterManagerScript = monsterManager.GetComponent<MonsterManager> ();
		visualDamage.gameObject.SetActive (false);
				audioSource = soundManager.GetComponent<AudioSource> ();
				monsterImg = GameObject.FindGameObjectWithTag ("Monster");
				imageScript = monsterImg.GetComponent<Image> ();
				attackedEffectScript = monsterImg.GetComponent<AttackedEffect> ();
				answerText = GameObject.FindGameObjectsWithTag ("AnswerText");
				deadParticle = deadGO.GetComponent<ParticleSystem> ();
				chargeParticle = chargeGO.GetComponent<ParticleSystem> ();
				thunderParticle = thunderGO.GetComponent<ParticleSystem> ();
				particle = slashGO.GetComponent<ParticleSystem> ();
				Screen.orientation = ScreenOrientation.Portrait;
				buttonASelectable = buttonA.GetComponent<Selectable> ();
				buttonBSelectable = buttonB.GetComponent<Selectable> ();
				buttonCSelectable = buttonC.GetComponent<Selectable> ();
				buttonDSelectable = buttonD.GetComponent<Selectable> ();
				statusDialog.SetActive (false);
				questionCanvasGroup = questionPanel.GetComponent<CanvasGroup> ();
				monsterCanvasGroup = monsterPanel.GetComponent<CanvasGroup> ();
				statusDialogScript = statusDialog.GetComponent<StatusDialogScript> ();
				questionFadeScript = questionPanel.GetComponent<QuestionFadeScript> ();
				monsterImage = monsterGameObject.GetComponent<Image> ();
				monsterFadeScript = monsterPanel.GetComponent<MonsterFadeScript> ();
				playerHealthBarScript = playerHealthBar.GetComponent<PlayerHealthBar> ();
				enemyHealthBarScript = enemyHealthBar.GetComponent<EnemyHealthBar> ();
				timeBarScript = timeBar.GetComponent<TimeBar> ();
				Read ();
				playerHealthBarScript.maxHealth = player.Hp;
				SetMonster ();
				SetQuestion ();
				if (!isFileExisted ("PlayerInfo")) {
						File.WriteAllText (Application.persistentDataPath + "/" + "PlayerInfo", "");
				}
		
		
		
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (!questionFadeScript.onCD && !monsterFadeScript.onCD) {
			
						buttonASelectable.interactable = true;
						buttonBSelectable.interactable = true;
						buttonCSelectable.interactable = true;
						buttonDSelectable.interactable = true;
				} else if (questionFadeScript.onCD || monsterFadeScript.onCD) {
			
						buttonASelectable.interactable = false;
						buttonBSelectable.interactable = false;
						buttonCSelectable.interactable = false;
						buttonDSelectable.interactable = false;
				}
		}
		
		bool isFileExisted (string name)
		{
				string str = "{";
				return File.Exists (Application.persistentDataPath + "/" + name);

		}

		public void Read ()
		{
		
				if (Application.platform == RuntimePlatform.Android) {
						string jsonPlayerDataFile = "";
						Debug.Log ("DANG O TRONG ANDROID NE");
						while (!File.Exists(Application.persistentDataPath + "/" + "PlayerData.json")) {
								Debug.Log ("File khong ton tai!");
								string str = "{\n\t\"player\":[\n\t\t{\n\t\t\t\"level\":1,\n\t\t\t\"hp\":100,\n\t\t\t\"atk\":500,\n\t\t\t\"def\":100,\n\t\t\t\"agi\":100,\n\t\t\t\"luk\":100,\n\t\t\t\"exp\":0,\n\t\t\t\"nextLevelExp\":100,\n\t\t\t\"bonusPoint\":0\n\t\t}\n\t]\n}";
								Debug.Log (str);

								File.WriteAllText (Application.persistentDataPath + "/" + "PlayerData.json", str);
			
								
						}
						jsonPlayerDataFile = File.ReadAllText (Application.persistentDataPath + "/" + "PlayerData.json");
						JsonData jsonPlayerData = JsonMapper.ToObject (jsonPlayerDataFile);

//						test.text = jsonPlayerData ["player"] [0] ["hp"].ToString ();
						TextAsset jsonFile = Resources.Load ("data2") as TextAsset;
						TextAsset jsonMonsterFile = Resources.Load ("monster") as TextAsset;
						JsonData jsonKanjis = JsonMapper.ToObject (jsonFile.text);
						JsonData jsonMonster = JsonMapper.ToObject (jsonMonsterFile.text);

						KanjiBean kanji;


						kanjiList = new List<KanjiBean> ();
						monsterList = new List<MonsterBean> ();

						player = new PlayerData ();
						Debug.Log ("Cai nay chay truoc hay sau656756765!");
						player.Hp = System.Convert.ToInt64 (jsonPlayerData ["player"] [0] ["hp"].ToString ());
						player.Level = System.Convert.ToInt16 (jsonPlayerData ["player"] [0] ["level"].ToString ());
						player.Atk = System.Convert.ToInt64 (jsonPlayerData ["player"] [0] ["atk"].ToString ());
						player.Def = System.Convert.ToInt64 (jsonPlayerData ["player"] [0] ["def"].ToString ());
						player.Agi = System.Convert.ToInt64 (jsonPlayerData ["player"] [0] ["agi"].ToString ());
						player.Luk = System.Convert.ToInt64 (jsonPlayerData ["player"] [0] ["luk"].ToString ());
						player.CurrentExp = System.Convert.ToInt16 (jsonPlayerData ["player"] [0] ["exp"].ToString ());
						player.NextLevelExp = System.Convert.ToInt16 (jsonPlayerData ["player"] [0] ["nextLevelExp"].ToString ());
						player.BonusPoint = System.Convert.ToInt16 (jsonPlayerData ["player"] [0] ["bonusPoint"].ToString ());

			
						for (int i = 0; i < jsonMonster["monsters"].Count; i++) {
								monster = new MonsterBean ();
								monster.ID = System.Convert.ToInt16 (jsonMonster ["monsters"] [i] ["id"].ToString ());
								monster.Name = jsonMonster ["monsters"] [i] ["name"].ToString ();
								monster.Level = System.Convert.ToInt16 (jsonMonster ["monsters"] [i] ["level"].ToString ());
								monster.Hp = System.Convert.ToInt64 (jsonMonster ["monsters"] [i] ["hp"].ToString ());
								monster.Atk = System.Convert.ToInt64 (jsonMonster ["monsters"] [i] ["atk"].ToString ());
								monster.Def = System.Convert.ToInt64 (jsonMonster ["monsters"] [i] ["def"].ToString ());
								monster.Exp = System.Convert.ToInt16 (jsonMonster ["monsters"] [i] ["exp"].ToString ());
								monster.Image = jsonMonster ["monsters"] [i] ["image"].ToString ();
				
								monsterList.Add (monster);
						}
			
			
						for (int i = 0; i<jsonKanjis["kanjis"].Count; i++) {
				
								kanji = new KanjiBean ();
								kanji.ID = System.Convert.ToInt16 (jsonKanjis ["kanjis"] [i] ["id"].ToString ());
								kanji.Writing = jsonKanjis ["kanjis"] [i] ["writing"].ToString ();
								kanji.Meaning = jsonKanjis ["kanjis"] [i] ["meaning"].ToString ();
								kanji.OnReading = jsonKanjis ["kanjis"] [i] ["on"].ToString ();
								kanji.KunReading = jsonKanjis ["kanjis"] [i] ["kun"].ToString ();
								kanji.OnSound = jsonKanjis ["kanjis"] [i] ["onsound"].ToString ();
								kanji.KunSound = jsonKanjis ["kanjis"] [i] ["kunsound"].ToString ();
				
				
								kanjiList.Add (kanji);
						}
			
			
						for (int i = 0; i < kanjiList.Count; i++) {
								QuestionBean qb = new QuestionBean ();
								qb.question = kanjiList [i].Writing;
								qb.rightAnswer = kanjiList [i].Meaning;
								qb.answerA = kanjiList [i].Meaning;
								qb.onSound = kanjiList [i].OnSound;
								qb.kunSound = kanjiList [i].KunSound;
								KanjiBean kanjiWord = new KanjiBean ();
								kanjiWord = GetRandomKanji ();
								while (kanjiWord.Meaning == qb.answerA) {
										kanjiWord = GetRandomKanji ();
								}
								qb.answerB = kanjiWord.Meaning;
								kanjiWord = GetRandomKanji ();
								while (kanjiWord.Meaning == qb.answerA || kanjiWord.Meaning == qb.answerB) {
										kanjiWord = GetRandomKanji ();
								}
								qb.answerC = kanjiWord.Meaning;
								kanjiWord = GetRandomKanji ();
								while (kanjiWord.Meaning == qb.answerA || kanjiWord.Meaning == qb.answerB || kanjiWord.Meaning == qb.answerC) {
										kanjiWord = GetRandomKanji ();
								}
								qb.answerD = kanjiWord.Meaning;
								textToRead.Add (qb);
						}


				} else {


						TextAsset jsonFile = Resources.Load ("data2") as TextAsset;
						TextAsset jsonMonsterFile = Resources.Load ("monster") as TextAsset;
						TextAsset jsonPlayerDataFile = Resources.Load ("PlayerData") as TextAsset;


						JsonData jsonKanjis = JsonMapper.ToObject (jsonFile.text);
						JsonData jsonMonster = JsonMapper.ToObject (jsonMonsterFile.text);
						JsonData jsonPlayerData = JsonMapper.ToObject (jsonPlayerDataFile.text);

						KanjiBean kanji;


						player = new PlayerData ();
//						Debug.Log ("Cai nay chay truoc hay sau!");
						player.Hp = System.Convert.ToInt64 (jsonPlayerData ["player"] [0] ["hp"].ToString ());
						player.Level = System.Convert.ToInt16 (jsonPlayerData ["player"] [0] ["level"].ToString ());
						player.Atk = System.Convert.ToInt64 (jsonPlayerData ["player"] [0] ["atk"].ToString ());
						player.Def = System.Convert.ToInt64 (jsonPlayerData ["player"] [0] ["def"].ToString ());
						player.Agi = System.Convert.ToInt64 (jsonPlayerData ["player"] [0] ["agi"].ToString ());
						player.Luk = System.Convert.ToInt64 (jsonPlayerData ["player"] [0] ["luk"].ToString ());
						player.CurrentExp = System.Convert.ToInt16 (jsonPlayerData ["player"] [0] ["exp"].ToString ());
						player.NextLevelExp = System.Convert.ToInt16 (jsonPlayerData ["player"] [0] ["nextLevelExp"].ToString ());
						player.BonusPoint = System.Convert.ToInt16 (jsonPlayerData ["player"] [0] ["bonusPoint"].ToString ());

						kanjiList = new List<KanjiBean> ();
						monsterList = new List<MonsterBean> ();

						for (int i = 0; i < jsonMonster["monsters"].Count; i++) {
								monster = new MonsterBean ();
								monster.ID = System.Convert.ToInt16 (jsonMonster ["monsters"] [i] ["id"].ToString ());
								monster.Name = jsonMonster ["monsters"] [i] ["name"].ToString ();
								monster.Level = System.Convert.ToInt16 (jsonMonster ["monsters"] [i] ["level"].ToString ());
								monster.Hp = System.Convert.ToInt64 (jsonMonster ["monsters"] [i] ["hp"].ToString ());
								monster.Atk = System.Convert.ToInt64 (jsonMonster ["monsters"] [i] ["atk"].ToString ());
								monster.Def = System.Convert.ToInt64 (jsonMonster ["monsters"] [i] ["def"].ToString ());
								monster.Exp = System.Convert.ToInt16 (jsonMonster ["monsters"] [i] ["exp"].ToString ());
								monster.Image = jsonMonster ["monsters"] [i] ["image"].ToString ();

								monsterList.Add (monster);
						}


						for (int i = 0; i<jsonKanjis["kanjis"].Count; i++) {

								kanji = new KanjiBean ();
								kanji.ID = System.Convert.ToInt16 (jsonKanjis ["kanjis"] [i] ["id"].ToString ());
								kanji.Writing = jsonKanjis ["kanjis"] [i] ["writing"].ToString ();
								kanji.Meaning = jsonKanjis ["kanjis"] [i] ["meaning"].ToString ();
								kanji.OnReading = jsonKanjis ["kanjis"] [i] ["on"].ToString ();
								kanji.KunReading = jsonKanjis ["kanjis"] [i] ["kun"].ToString ();
								kanji.OnSound = jsonKanjis ["kanjis"] [i] ["onsound"].ToString ();
								kanji.KunSound = jsonKanjis ["kanjis"] [i] ["kunsound"].ToString ();

				
								kanjiList.Add (kanji);
						}


						for (int i = 0; i < kanjiList.Count; i++) {
								QuestionBean qb = new QuestionBean ();
								qb.question = kanjiList [i].Writing;
								qb.rightAnswer = kanjiList [i].Meaning;
								qb.answerA = kanjiList [i].Meaning;
								qb.onSound = kanjiList [i].OnSound;
								qb.kunSound = kanjiList [i].KunSound;


								KanjiBean kanjiWord = new KanjiBean ();
								kanjiWord = GetRandomKanji ();
								while (kanjiWord.Meaning == qb.answerA) {
										kanjiWord = GetRandomKanji ();
								}
								qb.answerB = kanjiWord.Meaning;
								kanjiWord = GetRandomKanji ();
								while (kanjiWord.Meaning == qb.answerA || kanjiWord.Meaning == qb.answerB) {
										kanjiWord = GetRandomKanji ();
								}
								qb.answerC = kanjiWord.Meaning;
								kanjiWord = GetRandomKanji ();
								while (kanjiWord.Meaning == qb.answerA || kanjiWord.Meaning == qb.answerB || kanjiWord.Meaning == qb.answerC) {
										kanjiWord = GetRandomKanji ();
								}
								qb.answerD = kanjiWord.Meaning;
								textToRead.Add (qb);
						}

				}

		}
		
		public void ShowOnlyRightAnswerButton ()
		{
				for (int i = 0; i < answerText.Length; i++) {
						if (answerText [i].GetComponent<Text> ().text != qb.rightAnswer) {
//								answerText [i].transform.parent.gameObject.SetActive (false);
								answerText [i].transform.parent.gameObject.GetComponent<CanvasGroup>().alpha = 0.3f;
						}
				}
		}

		public void ShowAllAnswerButton ()
		{
				for (int i = 0; i < answerText.Length; i++) {

//						answerText [i].transform.parent.gameObject.SetActive (true);
						answerText [i].transform.parent.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
				}
		}

		public PlayerData GetPlayerData ()
		{
//				Debug.Log ("Gui nhan vat");
				return player;
		}
		
		public MonsterBean GetMonsterData ()
		{
				return monster;
		}
		
		public class QuestionBean
		{
				public string question;
				public string answerA;
				public string answerB;
				public string answerC;
				public string answerD;
				public string rightAnswer;
				public string onSound;
				public string kunSound;
		}

		public class WordInfo
		{
				public string word;
				public int right = 0;
				public int wrong = 0;
		}
		
		public List<KanjiBean> kanjiList;
		public List<MonsterBean> monsterList;
		public List<QuestionBean> textToRead = new List<QuestionBean> ();
		
		public QuestionBean GetRandomQuestion ()
		{
				int q = Random.Range (0, textToRead.Count);
				return textToRead [q];
		}
		
		public KanjiBean GetRandomKanji ()
		{
				int q = Random.Range (0, kanjiList.Count);
				return kanjiList [q];
		}
		
		private void Shuffle<T> (List<T> list)
		{
				int count = list.Count;
				for (int i = count - 1; i > 0; i--) {
						int randIndex = Random.Range (0, i);
						T temp = list [i];
						list [i] = list [randIndex];
						list [randIndex] = temp;
				
				}
		}
		
		public void GainExp ()
		{
				player.CurrentExp += monsterExp;
				if (player.CurrentExp >= player.NextLevelExp) {
						player.Level += 1;
						player.BonusPoint += 5;
						Debug.Log ("Cong level mot lan!");
						statusDialogScript.ShowStatusDialog ();
				
				}
		}
		
		public void SetMonster ()
		{
				monster = monsterList [monsterID];
				enemyHealthBarScript.maxHealth = monster.Hp;
				monsterExp = monster.Exp;
				monsterImage.sprite = Resources.Load<Sprite> (monster.Image);
				monsterCanvasGroup.alpha = 1f;
				StartCoroutine (monsterFadeScript.FadeToBlack (0.5f));
				monsterID++;
		}
		
		public void SetQuestion ()
		{		
				qb = GetRandomQuestion ();
				question.text = qb.question;
				List<string> list = new List<string> ();
				list.Add (qb.answerA);
				list.Add (qb.answerB);
				list.Add (qb.answerC);
				list.Add (qb.answerD);
				audioSource.clip = Resources.Load (qb.onSound) as AudioClip;
				Debug.Log ("TAI SAO SO 8 KHONG CO AM TNANH: " + qb.onSound);
				Shuffle (list);
			
				answerA.text = list [0];
				answerB.text = list [1];
				answerC.text = list [2];
				answerD.text = list [3];
			
		}
		
		IEnumerator AudioCoolDown ()
		{
				while (audioSource.isPlaying) {
						yield return null;
				}
		monsterManagerScript.MoveMonsterIn ();
		isMonsterMoveOut = false;
		StartCoroutine (MovingMonsterCoolDown ());
				
		}

	IEnumerator MovingMonsterCoolDown(){
		while(!isMonsterMoveOut){
			yield return null;
		}
		ShowParticleWhenNotAnswer (chargeGO);
		InvokeFunctionsRightAnswer ();
		Debug.Log ("CHAY HIEU UNG!");
	}
		
	void FadeAwayDamage(){
		fadeAwayVisualDamageScript.FadeAwayDamage ();
	}

	void CreateTextForVisualDamage(){

	}

	void DisplayDamage(){
		visualDamage.gameObject.SetActive (true);
		Vector3 tempVector3 = GameObject.FindGameObjectWithTag ("Monster").transform.position;
		visualDamage.gameObject.transform.position = tempVector3;
//		visualDamage.GetComponent<RectTransform> ().offsetMax = new Vector2 (visualDamage.GetComponent<RectTransform> ().offsetMax.x + monsterGameObject.GetComponent<RectTransform> ().rect.width / 2, visualDamage.GetComponent<RectTransform> ().offsetMax.y);
		visualDamage.text = "DISPLAYED DAMAGE!!!";
		char[] copiedChar = visualDamage.text.ToCharArray ();
		if(copiedChar != null){
			for(int i = 0; i<copiedChar.Length; i++){
				charGO = Instantiate (prefabTextUI, visualDamage.transform.position, visualDamage.transform.rotation) as Text;
				charGO.text = copiedChar[i].ToString();
				charGO.transform.SetParent(visualDamage.transform);
				charGO.transform.localScale = new Vector3(1f,1f,1f);

//				charGO.GetComponent<RectTransform>().position

				charGO.GetComponent<RectTransform>().position = new Vector3(chargeGO.transform.position.x + i*0.1f,chargeGO.transform.position.y,chargeGO.transform.position.z);
				Debug.Log(chargeGO.transform.position);
			}
		}

		fadeAwayVisualDamageScript.ShowDamage ();
//		ScaleBigger ();
//		iTween.ScaleFrom (visualDamage.gameObject, iTween.Hash ("x",1f,"y",1f,"time",1f,"easetype","easeOutBack","oncomplete","FadeAwayDamage"));
		iTween.MoveTo (visualDamage.gameObject, iTween.Hash ("x",visualDamage.transform.position.x,"y",visualDamage.transform.position.y + 2f,"z",visualDamage.transform.position.z,"time",1f,"easetype","easeOutBack","oncomplete","FadeAwayDamage"));
//		iTween.ColorTo (visualDamage.GetComponent<Text>().gameObject, iTween.Hash ("a",0f,"time",2f));
	}

	void ScaleBigger(){
		iTween.ValueTo (visualDamage.gameObject, iTween.Hash ("from",new Vector2(0.4f,0.4f),"to",new Vector2(0.8f,0.8f),"easetype","easeOutBack","time",0.2f,"onupdate","ScaleVisualDamage"));
	}
	
	void ScaleNormal(){
		
	}
	
	void ScaleVisualDamage(Vector2 scaleV2){
		visualDamage.GetComponent<RectTransform>().localScale = new Vector3 (scaleV2.x, scaleV2.y, visualDamage.transform.localScale.z);
	}

		public void CheckAnswerA ()
		{
				textClickedButton = answerA.text;
				ShowOnlyRightAnswerButton ();
				if (answerA.text == qb.rightAnswer) {
						audioSource.Play ();
						StartCoroutine (AudioCoolDown ());
			monsterManagerScript.LoadMonster(answerA.transform.parent.FindChild("Image").GetComponent<Image>().sprite.name);
			Debug.Log(answerA.transform.parent.FindChild("Image").GetComponent<Image>().sprite.name);
			Debug.Log(answerA.transform.parent.name);
//			DisplayDamage();
						questionFadeScript.SetButtonInteractiableFalse ();
//						ShowParticleWhenNotAnswer (chargeGO);
//						InvokeFunctionsRightAnswer ();
						//						enemyHealthBarScript.Damage ();
//						ResetTimeBar ();
//						ShowAllAnswerButton ();
				} else {
						questionFadeScript.SetButtonInteractiableFalse ();
						playerHealthBarScript.Damage ();
						ShowWrongAnswerParticle ();
						
						
				}
		}

		void EnableAttackedEffect ()
		{
				attackedEffectScript.enabled = true;
				imageScript.color = new Color32 (203, 165, 165, 255);
				if (monsterImg.GetComponent<iTween> () != null) {
						monsterImg.GetComponent<iTween> ().enabled = true;
				}

		}

		void DisableAttackedEffect ()
		{
				attackedEffectScript.enabled = false;
				imageScript.color = new Color32 (255, 255, 255, 255);
				monsterImg.GetComponent<iTween> ().enabled = false;
		}

		void InvokeFunctionsRightAnswer ()
		{
				Invoke ("HideQuestion", chargeParticle.duration);
				Invoke ("EnableAttackedEffect", chargeParticle.duration);
				Invoke ("DisplayDamage", chargeParticle.duration);
				Invoke ("ShowAttackParticle", chargeParticle.duration);
				Invoke ("DamageEnemyHealthbar", chargeParticle.duration);
				Invoke ("InvokeFunctionsAfterAttackParticle", chargeParticle.duration);
		}
		
		void DamageEnemyHealthbar ()
		{
				enemyHealthBarScript.Damage ();
		}

		void InvokeFunctionsAfterAttackParticle ()
		{
				Invoke ("SetChargeParticleFalse", particle.duration);
				Invoke ("DisableAttackedEffect", particle.duration);
				Invoke ("SetParticleFalse", particle.duration);
				Invoke ("ResetTimeBar", particle.duration);
				Invoke ("ShowAllAnswerButton", particle.duration);
				Invoke ("ShowQuestion", particle.duration);
		}

		void SetChargeParticleFalse ()
		{
				chargeGO.SetActive (false);
		}

		public void CheckAnswerB ()
		{
				textClickedButton = answerB.text;
				ShowOnlyRightAnswerButton ();
				if (answerB.text == qb.rightAnswer) {
						audioSource.Play ();
						questionFadeScript.SetButtonInteractiableFalse ();
			monsterManagerScript.LoadMonster(answerB.transform.parent.FindChild("Image").GetComponent<Image>().sprite.name);
			Debug.Log(answerB.transform.parent.FindChild("Image").GetComponent<Image>().sprite.name);
			Debug.Log(answerB.transform.parent.name);
//			DisplayDamage();
						StartCoroutine (AudioCoolDown ());
//						ShowParticleWhenNotAnswer (chargeGO);
//						InvokeFunctionsRightAnswer ();
//						enemyHealthBarScript.Damage ();
//						ResetTimeBar ();
//						ShowAllAnswerButton ();
				} else {
						questionFadeScript.SetButtonInteractiableFalse ();
						playerHealthBarScript.Damage ();
						ShowWrongAnswerParticle ();
						
						
				}
		}
		
		public void CheckAnswerC ()
		{
				textClickedButton = answerC.text;
				ShowOnlyRightAnswerButton ();
				if (answerC.text == qb.rightAnswer) {
						audioSource.Play ();
						questionFadeScript.SetButtonInteractiableFalse ();
			monsterManagerScript.LoadMonster(answerC.transform.parent.FindChild("Image").GetComponent<Image>().sprite.name);
			Debug.Log(answerC.transform.parent.FindChild("Image").GetComponent<Image>().sprite.name);
			Debug.Log(answerC.transform.parent.name);
//			DisplayDamage();
						StartCoroutine (AudioCoolDown ());
//						ShowParticleWhenNotAnswer (chargeGO);
//						InvokeFunctionsRightAnswer ();
//						enemyHealthBarScript.Damage ();
//						ResetTimeBar ();
//						ShowAllAnswerButton ();
				} else {
						questionFadeScript.SetButtonInteractiableFalse ();
						playerHealthBarScript.Damage ();
						ShowWrongAnswerParticle ();
						
						
				}
		}
		
		public void CheckAnswerD ()
		{
				textClickedButton = answerD.text;
				ShowOnlyRightAnswerButton ();
				if (answerD.text == qb.rightAnswer) {
						audioSource.Play ();
						questionFadeScript.SetButtonInteractiableFalse ();
			monsterManagerScript.LoadMonster(answerD.transform.parent.FindChild("Image").GetComponent<Image>().sprite.name);
			Debug.Log(answerD.transform.parent.FindChild("Image").GetComponent<Image>().sprite.name);
			Debug.Log(answerD.transform.parent.name);
//			DisplayDamage();
						StartCoroutine (AudioCoolDown ());
//						ShowParticleWhenNotAnswer (chargeGO);
//						InvokeFunctionsRightAnswer ();
//						enemyHealthBarScript.Damage ();
				
//						ResetTimeBar ();
//						ShowAllAnswerButton ();
				} else {
						questionFadeScript.SetButtonInteractiableFalse ();
						playerHealthBarScript.Damage ();
						ShowWrongAnswerParticle ();
						

						
				}
		}
		
		void ResetTimeBar ()
		{
				timeBarScript.ResetTimeBar ();
		}

		void SetParticleFalse ()
		{
				slashGO.SetActive (false);
		}

		void ShowWrongAnswerParticle ()
		{
				for (int i = 0; i < answerText.Length; i++) {
						if (textClickedButton == answerText [i].GetComponent<Text> ().text) {
								thunderParticlePos = answerText [i].transform.parent.transform.position;
								thunderParticle.transform.position = thunderParticlePos;
						}

				}
				thunderGO.SetActive (true);
				Invoke ("ShowQuestion", CompareParticleDuration ());
				
				Invoke ("ResetTimeBar", CompareParticleDuration ());
				Invoke ("ShowAllAnswerButton", CompareParticleDuration ());
				Invoke ("SetThunderParticleFalse", CompareParticleDuration ());
		}

		public void ShowParticleWhenNotAnswer (GameObject particleGO)
		{
				for (int i = 0; i < answerText.Length; i++) {
						if (qb.rightAnswer == answerText [i].GetComponent<Text> ().text) {
								thunderParticlePos = answerText [i].transform.parent.transform.position;
								particleGO.transform.position = thunderParticlePos;
						}
			
				}
				particleGO.SetActive (true);
		}

		public float CompareParticleDuration ()
		{
				if (particle.duration > thunderParticle.duration) {
						return particle.duration;
				} else {
						return thunderParticle.duration;		
				}
		}

		public void SetThunderParticleFalse ()
		{
				thunderGO.SetActive (false);
		}
		
		void ShowQuestion ()
		{
				questionFadeScript.ShowQuestion ();
		}

		void ShowAttackParticle ()
		{
				slashGO.SetActive (true);
				
		}

		void HideQuestion ()
		{
				questionFadeScript.HideQuestion ();
		}
		
		
	



	

	

		// Use this for initialization
	
}
