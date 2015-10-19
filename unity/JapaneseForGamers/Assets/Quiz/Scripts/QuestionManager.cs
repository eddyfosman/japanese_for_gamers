using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using LitJson;


public class QuestionManager : MonoBehaviour
{

    public SimpleSQL.SimpleSQLManager dbManager;


	public Text visualDamage;
	public Button buttonA;
	public Button buttonB;
	public Button buttonC;
	public Button buttonD;
	public GameObject statusDialog;
	public GameObject questionPanel;
	public GameObject monsterPanel;
	public Text question;
	public Text answerA;
	public Text answerB;
	public Text answerC;
	public Text answerD;
    public GameObject effectPrefab;
    public GameObject visualEffectContainer;
    public GameObject visualEffectMonsterContainer;
	public GameObject monsterGameObject;
	public GameObject playerHealthBar;
	public GameObject timeBar;
	public GameObject enemyHealthBar;
	public GameObject slashGO;
	public GameObject thunderGO;
	public GameObject chargeGO;
	public GameObject regenGO;
	public GameObject deadGO;
	public GameObject atkBuffGO;
	public GameObject poisonGO;
	public GameObject soundManager;
	public GameObject monsterManager;
	public bool isMonsterMoveOut = false;
	public Text prefabTextUI;
	public GameObject effectManagerGO;
	public GameObject visualEffectInform;

    private GameObject effectGO;
	private GameObject[] answerText;
	private Selectable buttonASelectable;
	private Selectable buttonBSelectable;
	private Selectable buttonCSelectable;
	private Selectable buttonDSelectable;
	private StatusDialogScript statusDialogScript;
	private CanvasGroup questionCanvasGroup;
	private QuestionFadeScript questionFadeScript;
	private CanvasGroup monsterCanvasGroup;
	private MonsterFadeScript monsterFadeScript;

	private QuestionBean qb ;
	private Image monsterImage;
	private PlayerHealthBar playerHealthBarScript;
	private TimeBar timeBarScript;
	private EnemyHealthBar enemyHealthBarScript;
	private int monsterID = 0;
	private PlayerData player;
	private MonsterBean monster;
	private int monsterExp;
		
	public ParticleSystem atkBuffParticle;
    public ParticleSystem poisonParticle;
    public ParticleSystem regenParticle;
    public ParticleSystem chargeParticle;
    public ParticleSystem thunderParticle;
    public ParticleSystem particle;

	private string textClickedButton;
	private Vector3 thunderParticlePos;
	private ParticleSystem deadParticle;
	private GameObject monsterImg;
	private AttackedEffect attackedEffectScript;
	private iTween itweenScript;
	private Image imageScript;
	private List<WordInfo> listWordInfo = new List<WordInfo> ();
	private AudioSource audioSource;
	private MonsterManager monsterManagerScript;
	private FadeAwayVisualDamage fadeAwayVisualDamageScript;
	private Text charGO;
	private EffectManager effectManagerScript;
	private VisualEffectInform visualEffectInformScript;
	public bool isPause = false;

	private bool isPlayerPhase = false;
	public bool IsPlayerPhase
	{
		set { isPlayerPhase = value; }
		get { return isPlayerPhase; }
	}

	private TextAsset jsonFile;
	private TextAsset jsonFileMonsterKanji;

	public List<KanjiBean> kanjiList;
	public List<MonsterBean> monsterList;
	public List<QuestionBean> textToReadPlayer= new List<QuestionBean> ();
	public List<QuestionBean> textToReadMonster = new List<QuestionBean>();
	public List<KanjiBean> kanjiListMonster;

		

	void Start ()
	{
		visualEffectInformScript = visualEffectInform.GetComponent<VisualEffectInform> ();
		effectManagerScript = effectManagerGO.GetComponent<EffectManager> ();
		monsterManagerScript = monsterManager.GetComponent<MonsterManager> ();
		fadeAwayVisualDamageScript = visualDamage.GetComponent<FadeAwayVisualDamage> ();
		monsterManagerScript = monsterManager.GetComponent<MonsterManager> ();
		visualDamage.gameObject.SetActive (false);
		audioSource = soundManager.GetComponent<AudioSource> ();
		monsterImg = GameObject.FindGameObjectWithTag ("Monster");
		imageScript = monsterImg.GetComponent<Image> ();
		attackedEffectScript = monsterImg.GetComponent<AttackedEffect> ();
		answerText = GameObject.FindGameObjectsWithTag ("AnswerText");
		poisonParticle = poisonGO.GetComponent<ParticleSystem> ();
		deadParticle = deadGO.GetComponent<ParticleSystem> ();
		chargeParticle = chargeGO.GetComponent<ParticleSystem> ();
		thunderParticle = thunderGO.GetComponent<ParticleSystem> ();
		regenParticle = regenGO.GetComponent<ParticleSystem> ();
		atkBuffParticle = atkBuffGO.GetComponent<ParticleSystem>();
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
		if (!isFileExisted ("PlayerInfo")) 
		{
				File.WriteAllText (Application.persistentDataPath + "/" + "PlayerInfo", "");
		}
	
	
	
	}
	
		// Update is called once per frame
	private void Update ()
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
		
	

	private bool isFileExisted (string name)
		{
				string str = "{";
				return File.Exists (Application.persistentDataPath + "/" + name);

		}
		
	public void SetParticleGO (GameObject particleGO, bool val)
		{
				particleGO.SetActive (val);
		}

	private void GetMonsterDataFromFile(){
		jsonFile = Resources.Load ("data2") as TextAsset;
		jsonFileMonsterKanji = Resources.Load ("monsterKanji") as TextAsset;
	}

	private void LoadAllKanji(JsonData jsonData, List<KanjiBean> list){
		for (int i = 0; i<jsonData["kanjis"].Count; i++) {
			KanjiBean kanji;
			kanji = new KanjiBean ();
			kanji.ID = System.Convert.ToInt16 (jsonData ["kanjis"] [i] ["id"].ToString ());
			kanji.Writing = jsonData ["kanjis"] [i] ["writing"].ToString ();
			kanji.Meaning = jsonData ["kanjis"] [i] ["meaning"].ToString ();
			kanji.OnReading = jsonData ["kanjis"] [i] ["on"].ToString ();
			kanji.KunReading = jsonData ["kanjis"] [i] ["kun"].ToString ();
			kanji.OnSound = jsonData ["kanjis"] [i] ["onsound"].ToString ();
			kanji.KunSound = jsonData ["kanjis"] [i] ["kunsound"].ToString ();
			
			
			list.Add (kanji);
		}
	}

	private void LoadMonsterData(JsonData jsonData, List<MonsterBean> monsterList){
		for (int i = 0; i < jsonData["monsters"].Count; i++) {
			monster = new MonsterBean ();
			monster.ID = System.Convert.ToInt16 (jsonData ["monsters"] [i] ["id"].ToString ());
			monster.Name = jsonData ["monsters"] [i] ["name"].ToString ();
			monster.Level = System.Convert.ToInt16 (jsonData ["monsters"] [i] ["level"].ToString ());
			monster.Hp = System.Convert.ToInt64 (jsonData ["monsters"] [i] ["hp"].ToString ());
			monster.Atk = System.Convert.ToInt64 (jsonData ["monsters"] [i] ["atk"].ToString ());
			monster.Def = System.Convert.ToInt64 (jsonData ["monsters"] [i] ["def"].ToString ());
			monster.Exp = System.Convert.ToInt16 (jsonData ["monsters"] [i] ["exp"].ToString ());
			monster.Image = jsonData ["monsters"] [i] ["image"].ToString ();
			
			monsterList.Add (monster);
		}
	}

	private void LoadQuestionIntoText(List<KanjiBean> list, List<QuestionBean> list2, bool val){
		for (int i = 0; i < list.Count; i++) {
			QuestionBean qb = new QuestionBean ();
			qb.question = list [i].Writing;
			qb.rightAnswer = list [i].Meaning;
			qb.answerA = list [i].Meaning;
			qb.onSound = list [i].OnSound;
			qb.kunSound = list [i].KunSound;
			KanjiBean kanjiWord = new KanjiBean ();
			kanjiWord = GetRandomKanji (val);
			while (kanjiWord.Meaning == qb.answerA) {
				kanjiWord = GetRandomKanji (val);
			}
			qb.answerB = kanjiWord.Meaning;
			kanjiWord = GetRandomKanji (val);
			while (kanjiWord.Meaning == qb.answerA || kanjiWord.Meaning == qb.answerB) {
				kanjiWord = GetRandomKanji (val);
			}
			qb.answerC = kanjiWord.Meaning;
			kanjiWord = GetRandomKanji (val);
			while (kanjiWord.Meaning == qb.answerA || kanjiWord.Meaning == qb.answerB || kanjiWord.Meaning == qb.answerC) {
				kanjiWord = GetRandomKanji (val);
			}
			qb.answerD = kanjiWord.Meaning;
			list2.Add (qb);
		}
	}
	
	public void Read ()
	{
		
		if (Application.platform == RuntimePlatform.Android) 
		{
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
			GetMonsterDataFromFile();
	//						TextAsset jsonFile = Resources.Load ("data2") as TextAsset;
			TextAsset jsonMonsterFile = Resources.Load ("monster") as TextAsset;
			JsonData jsonKanjis = JsonMapper.ToObject (jsonFile.text);
			JsonData jsonMonsterKanjis = JsonMapper.ToObject(jsonFileMonsterKanji.text);
			JsonData jsonMonster = JsonMapper.ToObject (jsonMonsterFile.text);


			kanjiListMonster = new List<KanjiBean>();
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

			

			LoadMonsterData(jsonMonster, monsterList);
			
			LoadAllKanji(jsonKanjis, kanjiList);

			LoadAllKanji(jsonMonsterKanjis, kanjiListMonster);

			LoadQuestionIntoText(kanjiList, textToReadPlayer, true);

			LoadQuestionIntoText(kanjiListMonster, textToReadMonster, false);
		



		} 
		else 
		{

			GetMonsterDataFromFile();
//						TextAsset jsonFile = Resources.Load ("data2") as TextAsset;
			TextAsset jsonMonsterFile = Resources.Load ("monster") as TextAsset;
			TextAsset jsonPlayerDataFile = Resources.Load ("PlayerData") as TextAsset;

			JsonData jsonMonsterKanjis = JsonMapper.ToObject(jsonFileMonsterKanji.text);

			JsonData jsonKanjis = JsonMapper.ToObject (jsonFile.text);
			JsonData jsonMonster = JsonMapper.ToObject (jsonMonsterFile.text);
			JsonData jsonPlayerData = JsonMapper.ToObject (jsonPlayerDataFile.text);



			player = new PlayerData ();
			player.Hp = System.Convert.ToInt64 (jsonPlayerData ["player"] [0] ["hp"].ToString ());
			player.Level = System.Convert.ToInt16 (jsonPlayerData ["player"] [0] ["level"].ToString ());
			player.Atk = System.Convert.ToInt64 (jsonPlayerData ["player"] [0] ["atk"].ToString ());
			player.Def = System.Convert.ToInt64 (jsonPlayerData ["player"] [0] ["def"].ToString ());
			player.Agi = System.Convert.ToInt64 (jsonPlayerData ["player"] [0] ["agi"].ToString ());
			player.Luk = System.Convert.ToInt64 (jsonPlayerData ["player"] [0] ["luk"].ToString ());
			player.CurrentExp = System.Convert.ToInt16 (jsonPlayerData ["player"] [0] ["exp"].ToString ());
			player.NextLevelExp = System.Convert.ToInt16 (jsonPlayerData ["player"] [0] ["nextLevelExp"].ToString ());
			player.BonusPoint = System.Convert.ToInt16 (jsonPlayerData ["player"] [0] ["bonusPoint"].ToString ());

			kanjiListMonster = new List<KanjiBean>();

			kanjiList = new List<KanjiBean> ();
			monsterList = new List<MonsterBean> ();


			LoadMonsterData(jsonMonster, monsterList);

			LoadAllKanji(jsonKanjis, kanjiList);

			LoadAllKanji(jsonMonsterKanjis, kanjiListMonster);

			LoadQuestionIntoText(kanjiList, textToReadPlayer, true);

			LoadQuestionIntoText(kanjiListMonster, textToReadMonster, false);


            string sql = "SELECT * FROM Monsters";

            
            List<MonsterBean> tempMonster = dbManager.Query<MonsterBean>(sql);
            Debug.Log("SO LUONG QUAI VAT GOI RA DUOC LA: " + tempMonster.Count);
            foreach (MonsterBean m in tempMonster)
            {
                Debug.Log("ID CUA QUAI VAT LA: ____" + m.ID);
                Debug.Log("IMAGE CUA QUAI VAT LA: ____" + m.Image);
                Debug.Log("Hp CUA QUAI VAT LA: ____" + m.Hp);
            }

		}

	}
		
		public void ShowOnlyRightAnswerButton ()
		{
				for (int i = 0; i < answerText.Length; i++) {
						if (answerText [i].GetComponent<Text> ().text != qb.rightAnswer) {
//								answerText [i].transform.parent.gameObject.SetActive (false);
								answerText [i].transform.parent.gameObject.GetComponent<CanvasGroup> ().alpha = 0.3f;
						}
				}
		}

		public void ShowAllAnswerButton ()
		{
				for (int i = 0; i < answerText.Length; i++) {

						answerText [i].transform.parent.gameObject.GetComponent<CanvasGroup> ().alpha = 1f;

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
		
		
		
		public QuestionBean GetRandomQuestion (bool isPlayerPhase)
		{
			if (isPlayerPhase)
			{
				int q = Random.Range (0, textToReadPlayer.Count);
				return textToReadPlayer[q];
			}
			else
			{
				int q = Random.Range(0, textToReadMonster.Count);
				return textToReadMonster[q];
			}
				
		}
		
	public KanjiBean GetRandomKanji (bool val)
	{
		if (val)
		{
			int q = Random.Range (0, kanjiList.Count);
			return kanjiList [q];
		}
		else
		{
			int q = Random.Range (0, kanjiListMonster.Count);
			return kanjiListMonster [q];
		}
			
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
		if (player.CurrentExp >= player.NextLevelExp) 
		{
			player.Level += 1;
			player.BonusPoint += 5;
			Debug.Log ("Cong level mot lan!");
			statusDialogScript.ShowStatusDialog ();
		
		}
	}
		
	private string CalculateHideEffect()
	{
		int q = Random.Range (0, 100);
		Debug.Log ("GIA TRI RANDOM LA: " + q);
		if (q < 25)
        {
            return "evade";
        }
        else if (q >= 25 && q < 50)
        {
            return "poison";
        }
        else if (q >= 50 && q < 75)
        {
            return "atkBuff";
        }
        else if (q >= 75 && q < 100)
        {
            return "regen";
        }
        return "";
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
		qb = GetRandomQuestion (isPlayerPhase);
		question.text = qb.question;
		List<string> list = new List<string> ();
		list.Add (qb.answerA);
		list.Add (qb.answerB);
		list.Add (qb.answerC);
		list.Add (qb.answerD);
		audioSource.clip = Resources.Load (qb.onSound) as AudioClip;
		Shuffle (list);
	
		answerA.text = list [0];
		answerB.text = list [1];
		answerC.text = list [2];
		answerD.text = list [3];
		Debug.Log ("THIET LAP CAU HOI MOI NGAY LAP TUC NE !!!");
		if (effectManagerScript.IsAtkBuffAdded)
		{
			effectManagerScript.ApplyEffect ("atkBuff");
		}
//		isPlayerPhase = !isPlayerPhase;
		
	}
		
	private IEnumerator AudioCoolDown (MonsterEquip mq)
	{
		while (audioSource.isPlaying) 
		{
			yield return null;
		}
		monsterManagerScript.MoveMonsterIn (mq);
		isMonsterMoveOut = false;
		StartCoroutine (MovingMonsterCoolDown ());
			
	}

	private IEnumerator MovingMonsterCoolDown ()
	{
		while (!isMonsterMoveOut) 
		{
			yield return null;
		}
		ShowChargeParticleWhenAnswer (chargeGO);
		InvokeFunctionsRightAnswer ();
		Debug.Log ("CHAY HIEU UNG!");
	}
		
	private void FadeAwayDamage ()
	{
		fadeAwayVisualDamageScript.FadeAwayDamage ();
	}

	private void CreateTextForVisualDamage ()
	{

	}

	private void DisplayDamage ()
	{
		visualDamage.gameObject.SetActive (true);
		Vector3 tempVector3 = GameObject.FindGameObjectWithTag ("Monster").transform.position;
		visualDamage.gameObject.transform.position = tempVector3;
//		visualDamage.GetComponent<RectTransform> ().offsetMax = new Vector2 (visualDamage.GetComponent<RectTransform> ().offsetMax.x + monsterGameObject.GetComponent<RectTransform> ().rect.width / 2, visualDamage.GetComponent<RectTransform> ().offsetMax.y);
		visualDamage.text = "DISPLAYED DAMAGE!!!";

		// doan code phia duoi tao ra cac doi tuong chu de co the sap xep tao chu len xuong
//		char[] copiedChar = visualDamage.text.ToCharArray ();
//		if(copiedChar != null){
//			for(int i = 0; i<copiedChar.Length; i++){
//				charGO = Instantiate (prefabTextUI, visualDamage.transform.position, visualDamage.transform.rotation) as Text;
//				charGO.text = copiedChar[i].ToString();
//				charGO.transform.SetParent(visualDamage.transform);
//				charGO.transform.localScale = new Vector3(1f,1f,1f);
//
////				charGO.GetComponent<RectTransform>().position
//
//				charGO.GetComponent<RectTransform>().position = new Vector3(chargeGO.transform.position.x + i*0.1f,chargeGO.transform.position.y,chargeGO.transform.position.z);
//				Debug.Log(chargeGO.transform.position);
//			}
//		}

		fadeAwayVisualDamageScript.ShowDamage ();
		iTween.MoveTo (visualDamage.gameObject, iTween.Hash ("x", visualDamage.transform.position.x, "y", visualDamage.transform.position.y + 2f, "z", visualDamage.transform.position.z, "time", 1f, "easetype", "easeOutBack", "oncomplete", "FadeAwayDamage"));
	}

		
	
	private void ScaleVisualDamage (Vector2 scaleV2)
	{
		visualDamage.GetComponent<RectTransform> ().localScale = new Vector3 (scaleV2.x, scaleV2.y, visualDamage.transform.localScale.z);
	}

	private void EnableAttackedEffect ()
	{
		attackedEffectScript.enabled = true;
		imageScript.color = new Color32 (203, 165, 165, 255);
		if (monsterImg.GetComponent<iTween> () != null) {
			monsterImg.GetComponent<iTween> ().enabled = true;
		}

	}

	private void SetAtkBuffParticleFalse()
	{
		atkBuffGO.SetActive (false);
		isPause = false;
		visualEffectInformScript.FadeOutVisualEffect ();
	}

	private void SetRegenParticleFalse ()
	{
		regenGO.SetActive (false);
		visualEffectInformScript.FadeOutVisualEffect ();
	}

	private void DisableAttackedEffect ()
	{
		attackedEffectScript.enabled = false;
		imageScript.color = new Color32 (255, 255, 255, 255);
		monsterImg.GetComponent<iTween> ().enabled = false;
	}

	private void ExecutedFunctionsAfterEvadeEffect ()
	{

		HideQuestion ();
		ResetTimeBar ();
		ShowAllAnswerButton ();
		ShowQuestion ();

	}
		
	private void InvokeFunctionsRightAnswer ()
	{
		Invoke ("HideQuestion", chargeParticle.duration);
		Invoke ("EnableAttackedEffect", chargeParticle.duration);
		Invoke ("DisplayDamage", chargeParticle.duration);
		Invoke ("ShowAttackParticle", chargeParticle.duration);
		Invoke ("DamageEnemyHealthbar", chargeParticle.duration);
		Invoke ("InvokeFunctionsAfterAttackParticle", chargeParticle.duration);
	}
		
	private void DamageEnemyHealthbar ()
	{
		enemyHealthBarScript.Damage ();
	}

	private void InvokeFunctionsAfterAttackParticle ()
	{
		Invoke ("SetChargeParticleFalse", particle.duration);
		Invoke ("DisableAttackedEffect", particle.duration);
		Invoke ("SetParticleFalse", particle.duration);
		Invoke ("ResetTimeBar", particle.duration);
		Invoke ("ShowAllAnswerButton", particle.duration);
		Invoke ("ShowQuestion", particle.duration);
	}

	private void SetChargeParticleFalse ()
	{
		chargeGO.SetActive (false);
	}

	private void FadeOutVisualEffect2()
	{

			visualEffectInformScript.FadeOutVisualEffect ();
			isPause = false;


	}

	public void SetPoisonEffectOff()
	{
		poisonGO.SetActive (false);
	}

	private void FadeOutVisualEffect ()
	{
		visualEffectInformScript.FadeOutVisualEffect ();
		ExecutedFunctionsAfterEvadeEffect ();
	}

	public void CheckAnswerA ()
	{
		MonsterEquip monsterEquip = answerA.transform.parent.FindChild ("Image").GetComponent<MonsterEquip> ();
		textClickedButton = answerA.text;
		ShowOnlyRightAnswerButton ();
		if (answerA.text == qb.rightAnswer) {
			audioSource.Play ();
            if (isPlayerPhase)
            {
                if (monsterEquip.EffectProperty.Type == "poison")
                {
                    if (!effectManagerScript.IsPoisonAdded)
                    {
                        ShowVisualEffect(monsterEquip, "poison", "me");
                    }
                    effectManagerScript.AddEffectIntoList(CloneEffect(monsterEquip));
                    ShowPoisonEffect();

                }

                if (monsterEquip.EffectProperty.Type == "atkBuff")
                {
                    if (!effectManagerScript.IsAtkBuffAdded)
                    {
                        ShowVisualEffect(monsterEquip, "atkBuff", "me");
                    }
                    effectManagerScript.AddEffectIntoList(CloneEffect(monsterEquip));
                    ShowAtkBuffEffect();

                }

                if (monsterEquip.EffectProperty.Type == "regen")
                {
                    if (!effectManagerScript.IsRegenAdded)
                    {
                        ShowVisualEffect(monsterEquip, "regen", "me");
                    }
                    effectManagerScript.AddEffectIntoList(CloneEffect(monsterEquip));
                }

                if (monsterEquip.EffectProperty.Type == "evade")
                {
                    if (!effectManagerScript.IsEvadeAdded)
                    {
                        ShowVisualEffect(monsterEquip, "evade", "me");
                    }
                    effectManagerScript.AddEffectIntoList(CloneEffect(monsterEquip));
                }


                if (effectManagerScript.IsEnemyEvadeAdded)
                {
                    //effectManagerScript.ApplyEffect("evade", "enemy");
                    StartCoroutine(effectManagerScript.DelayApplyEffect("evade", "enemy"));
                    visualEffectInformScript.SetSprite("evade");
                    visualEffectInformScript.FadeInVisualEffect();
                    Invoke("FadeOutVisualEffect", 1f);
                }
                else
                {
                    StartCoroutine(AudioCoolDown(monsterEquip));

                }
                
            }
            else
            {
                Invoke("SetChargeParticleFalse", particle.duration);
                Invoke("SetParticleFalse", particle.duration);
                Invoke("ResetTimeBar", particle.duration);
                Invoke("ShowAllAnswerButton", particle.duration);
                Invoke("ShowQuestion", particle.duration);
            }


            monsterManagerScript.LoadMonster(answerA.transform.parent.FindChild("Image").GetComponent<Image>().sprite.name);
            questionFadeScript.SetButtonInteractiableFalse ();
		
		} 
		else 
		{
			if (isPlayerPhase)
			{
				questionFadeScript.SetButtonInteractiableFalse ();
				ShowParticleWhenNotAnswer (deadGO);
			}
			else
			{

                Effect enemyEffect = CreateEnemyEffect();
                WhichEffectCreated(enemyEffect);
                //if (CalculateHideEffect())
                //{
                //	if (monsterEquip.EffectProperty.Type == "evade")
                //	{
                //		effectManagerScript.AddEffectIntoList (monsterEquip.EffectProperty);
                //		effectManagerScript.ExecuteOnEvadeEvent (monsterEquip.EffectProperty);
                //	}
                //}
                questionFadeScript.SetButtonInteractiableFalse ();
                if (effectManagerScript.IsEvadeAdded)
                {
                    //effectManagerScript.ApplyEffect("evade", "me");
                    StartCoroutine(effectManagerScript.DelayApplyEffect("evade", "me"));
                    visualEffectInformScript.SetSprite("evade");
                    visualEffectInformScript.FadeInVisualEffect();
                    Invoke("FadeOutVisualEffect", 1f);
                }
                else
                {
                    playerHealthBarScript.Damage();
                    ShowWrongAnswerParticle();
                }
            }

		
		}
	}

	public void CheckAnswerB ()
	{
		MonsterEquip monsterEquip = answerB.transform.parent.FindChild ("Image").GetComponent<MonsterEquip> ();
		textClickedButton = answerB.text;
		ShowOnlyRightAnswerButton ();
		if (answerB.text == qb.rightAnswer) {
			audioSource.Play ();
			questionFadeScript.SetButtonInteractiableFalse ();
			monsterManagerScript.LoadMonster (answerB.transform.parent.FindChild ("Image").GetComponent<Image> ().sprite.name);


            if (isPlayerPhase)
            {
                if (monsterEquip.EffectProperty.Type == "poison")
                {
                    if (!effectManagerScript.IsPoisonAdded)
                    {
                        ShowVisualEffect(monsterEquip, "poison", "me");
                    }
                    effectManagerScript.AddEffectIntoList(CloneEffect(monsterEquip));
                    ShowPoisonEffect();

                }

                if (monsterEquip.EffectProperty.Type == "atkBuff")
                {
                    if (!effectManagerScript.IsAtkBuffAdded)
                    {
                        ShowVisualEffect(monsterEquip, "atkBuff", "me");
                    }
                    effectManagerScript.AddEffectIntoList(CloneEffect(monsterEquip));
                    ShowAtkBuffEffect();

                }

                if (monsterEquip.EffectProperty.Type == "regen")
                {
                    if (!effectManagerScript.IsRegenAdded)
                    {
                        ShowVisualEffect(monsterEquip, "regen", "me");
                    }
                    effectManagerScript.AddEffectIntoList(CloneEffect(monsterEquip));
                }

                if (monsterEquip.EffectProperty.Type == "evade")
                {
                    if (!effectManagerScript.IsEvadeAdded)
                    {
                        ShowVisualEffect(monsterEquip, "evade", "me");
                    }
                    effectManagerScript.AddEffectIntoList(CloneEffect(monsterEquip));
                }


                if (effectManagerScript.IsEnemyEvadeAdded)
                {
                    //effectManagerScript.ApplyEffect("evade", "enemy");
                    StartCoroutine(effectManagerScript.DelayApplyEffect("evade", "enemy"));
                    visualEffectInformScript.SetSprite("evade");
                    visualEffectInformScript.FadeInVisualEffect();
                    Invoke("FadeOutVisualEffect", 1f);
                }
                else
                {
                    StartCoroutine(AudioCoolDown(monsterEquip));

                }

            }
            else
            {
                Invoke("SetChargeParticleFalse", particle.duration);
                Invoke("SetParticleFalse", particle.duration);
                Invoke("ResetTimeBar", particle.duration);
                Invoke("ShowAllAnswerButton", particle.duration);
                Invoke("ShowQuestion", particle.duration);
            }
        } 
		else 
		{
			if (isPlayerPhase)
			{
				questionFadeScript.SetButtonInteractiableFalse ();
				ShowParticleWhenNotAnswer (deadGO);
			}
			else
			{

                Effect enemyEffect = CreateEnemyEffect();
                WhichEffectCreated(enemyEffect);
                //if (CalculateHideEffect())
                //{
                //	if (monsterEquip.EffectProperty.Type == "evade")
                //	{
                //		effectManagerScript.AddEffectIntoList (monsterEquip.EffectProperty);
                //		effectManagerScript.ExecuteOnEvadeEvent (monsterEquip.EffectProperty);
                //	}
                //}
                questionFadeScript.SetButtonInteractiableFalse ();
                if (effectManagerScript.IsEvadeAdded)
                {
                    //effectManagerScript.ApplyEffect("evade", "me");
                    StartCoroutine(effectManagerScript.DelayApplyEffect("evade", "me"));
                    visualEffectInformScript.SetSprite("evade");
                    visualEffectInformScript.FadeInVisualEffect();
                    Invoke("FadeOutVisualEffect", 1f);
                }
                else
                {
                    playerHealthBarScript.Damage();
                    ShowWrongAnswerParticle();
                }
            }

				
				
		}
	}
	
    private Effect CloneEffect(MonsterEquip mq)
    {
        Effect effect = new Effect();
        if (mq != null)
        {
            effect.Apply = mq.EffectProperty.Apply;
            effect.Turn = mq.EffectProperty.Turn;
            effect.Value = mq.EffectProperty.Value;
            effect.Type = mq.EffectProperty.Type;
        }
        return effect;
    }
    	
	public void CheckAnswerC ()
	{
		MonsterEquip monsterEquip = answerC.transform.parent.FindChild ("Image").GetComponent<MonsterEquip> ();
		textClickedButton = answerC.text;
		ShowOnlyRightAnswerButton ();
		if (answerC.text == qb.rightAnswer)
        {
			audioSource.Play ();
			questionFadeScript.SetButtonInteractiableFalse ();
			monsterManagerScript.LoadMonster (answerC.transform.parent.FindChild ("Image").GetComponent<Image> ().sprite.name);

            if (isPlayerPhase)
            {
                if (monsterEquip.EffectProperty.Type == "poison")
                {
                    if (!effectManagerScript.IsPoisonAdded)
                    {
                        ShowVisualEffect(monsterEquip, "poison", "me");
                    }
                    effectManagerScript.AddEffectIntoList(CloneEffect(monsterEquip));
                    ShowPoisonEffect();

                }

                if (monsterEquip.EffectProperty.Type == "atkBuff")
                {
                    if (!effectManagerScript.IsAtkBuffAdded)
                    {
                        ShowVisualEffect(monsterEquip, "atkBuff", "me");
                    }
                    effectManagerScript.AddEffectIntoList(CloneEffect(monsterEquip));
                    ShowAtkBuffEffect();

                }

                if (monsterEquip.EffectProperty.Type == "regen")
                {
                    if (!effectManagerScript.IsRegenAdded)
                    {
                        ShowVisualEffect(monsterEquip, "regen", "me");
                    }
                    effectManagerScript.AddEffectIntoList(CloneEffect(monsterEquip));
                }

                if (monsterEquip.EffectProperty.Type == "evade")
                {
                    if (!effectManagerScript.IsEvadeAdded)
                    {
                        ShowVisualEffect(monsterEquip, "evade", "me");
                    }
                    effectManagerScript.AddEffectIntoList(CloneEffect(monsterEquip));
                }


                if (effectManagerScript.IsEnemyEvadeAdded)
                {
                    //effectManagerScript.ApplyEffect("evade", "enemy");
                    StartCoroutine(effectManagerScript.DelayApplyEffect("evade", "enemy"));
                    visualEffectInformScript.SetSprite("evade");
                    visualEffectInformScript.FadeInVisualEffect();
                    Invoke("FadeOutVisualEffect", 1f);
                }
                else
                {
                    StartCoroutine(AudioCoolDown(monsterEquip));

                }

            }
            else
            {
                Invoke("SetChargeParticleFalse", particle.duration);
                Invoke("SetParticleFalse", particle.duration);
                Invoke("ResetTimeBar", particle.duration);
                Invoke("ShowAllAnswerButton", particle.duration);
                Invoke("ShowQuestion", particle.duration);
            }
        }
        else
        {
			if (isPlayerPhase)
			{
				questionFadeScript.SetButtonInteractiableFalse ();
				ShowParticleWhenNotAnswer (deadGO);
			}
			else
			{

                Effect enemyEffect = CreateEnemyEffect();
                WhichEffectCreated(enemyEffect);
                //if (CalculateHideEffect())
                //{
                //	if (monsterEquip.EffectProperty.Type == "evade")
                //	{
                //		effectManagerScript.AddEffectIntoList (monsterEquip.EffectProperty);
                //		effectManagerScript.ExecuteOnEvadeEvent (monsterEquip.EffectProperty);
                //	}
                //}
                questionFadeScript.SetButtonInteractiableFalse ();
                if (effectManagerScript.IsEvadeAdded)
                {
                    //effectManagerScript.ApplyEffect("evade", "me");
                    StartCoroutine(effectManagerScript.DelayApplyEffect("evade", "me"));
                    visualEffectInformScript.SetSprite("evade");
                    visualEffectInformScript.FadeInVisualEffect();
                    Invoke("FadeOutVisualEffect", 1f);
                }
                else
                {
                    playerHealthBarScript.Damage();
                    ShowWrongAnswerParticle();
                }
            }

				
				
		}
	}
		
	public void CheckAnswerD ()
	{
		MonsterEquip monsterEquip = answerD.transform.parent.FindChild ("Image").GetComponent<MonsterEquip> ();
		textClickedButton = answerD.text;
		ShowOnlyRightAnswerButton ();
		if (answerD.text == qb.rightAnswer) {
			audioSource.Play ();
			questionFadeScript.SetButtonInteractiableFalse ();
			monsterManagerScript.LoadMonster (answerD.transform.parent.FindChild ("Image").GetComponent<Image> ().sprite.name);


            if (isPlayerPhase)
            {
                if (monsterEquip.EffectProperty.Type == "poison")
                {
                    if (!effectManagerScript.IsPoisonAdded)
                    {
                        ShowVisualEffect(monsterEquip, "poison", "me");
                    }
                    effectManagerScript.AddEffectIntoList(CloneEffect(monsterEquip));
                    ShowPoisonEffect();

                }

                if (monsterEquip.EffectProperty.Type == "atkBuff")
                {
                    if (!effectManagerScript.IsAtkBuffAdded)
                    {
                        ShowVisualEffect(monsterEquip, "atkBuff", "me");
                    }
                    effectManagerScript.AddEffectIntoList(CloneEffect(monsterEquip));
                    ShowAtkBuffEffect();

                }

                if (monsterEquip.EffectProperty.Type == "regen")
                {
                    if (!effectManagerScript.IsRegenAdded)
                    {
                        ShowVisualEffect(monsterEquip, "regen", "me");
                    }
                    effectManagerScript.AddEffectIntoList(CloneEffect(monsterEquip));
                }

                if (monsterEquip.EffectProperty.Type == "evade")
                {
                    if (!effectManagerScript.IsEvadeAdded)
                    {
                        ShowVisualEffect(monsterEquip, "evade", "me");
                    }
                    effectManagerScript.AddEffectIntoList(CloneEffect(monsterEquip));
                }


                if (effectManagerScript.IsEnemyEvadeAdded)
                {
                    //effectManagerScript.ApplyEffect("evade", "enemy");
                    StartCoroutine(effectManagerScript.DelayApplyEffect("evade", "enemy"));
                    visualEffectInformScript.SetSprite("evade");
                    visualEffectInformScript.FadeInVisualEffect();
                    Invoke("FadeOutVisualEffect", 1f);
                }
                else
                {
                    StartCoroutine(AudioCoolDown(monsterEquip));

                }

            }
            else
            {
                Invoke("SetChargeParticleFalse", particle.duration);
                Invoke("SetParticleFalse", particle.duration);
                Invoke("ResetTimeBar", particle.duration);
                Invoke("ShowAllAnswerButton", particle.duration);
                Invoke("ShowQuestion", particle.duration);
            }
        } 
		else 
		{
			if (isPlayerPhase)
			{
				questionFadeScript.SetButtonInteractiableFalse ();
				ShowParticleWhenNotAnswer (deadGO);
			}
			else
			{
                Effect enemyEffect = CreateEnemyEffect();

                WhichEffectCreated(enemyEffect);
                //if (CalculateHideEffect())
                //{
                //	if (monsterEquip.EffectProperty.Type == "evade")
                //	{
                //		effectManagerScript.AddEffectIntoList (monsterEquip.EffectProperty);
                //		effectManagerScript.ExecuteOnEvadeEvent (monsterEquip.EffectProperty);
                //	}

                //}
                questionFadeScript.SetButtonInteractiableFalse();
                if (effectManagerScript.IsEvadeAdded)
                {
                    //effectManagerScript.ApplyEffect("evade", "me");
                    StartCoroutine(effectManagerScript.DelayApplyEffect("evade", "me"));
                    visualEffectInformScript.SetSprite("evade");
                    visualEffectInformScript.FadeInVisualEffect();
                    Invoke("FadeOutVisualEffect", 1f);
                }
                else
                {
                    playerHealthBarScript.Damage();
                    ShowWrongAnswerParticle();
                }
                
                
            }
		}
	}

    private void WhichEffectCreated(Effect effect)
    {
        switch (effect.Type)
        {
            case "evade":
                if (!effectManagerScript.IsEnemyEvadeAdded)
                {
                    ShowVisualEffect(effect, effect.Type, effect.Apply);
                    effectManagerScript.AddEffectIntoList(effect);
                }
                break;
            case "regen":
                if (!effectManagerScript.IsEnemyRegenAdded)
                {
                    ShowVisualEffect(effect, effect.Type, effect.Apply);
                    effectManagerScript.AddEffectIntoList(effect);
                }
                break;
            case "poison":
                if (!effectManagerScript.IsEnemyPoisonAdded)
                {
                    ShowVisualEffect(effect, effect.Type, effect.Apply);
                    effectManagerScript.AddEffectIntoList(effect);
                }
                break;
            case "atkBuff":
                if (!effectManagerScript.IsEnemyAtkBuffAdded)
                {
                    ShowVisualEffect(effect, effect.Type, effect.Apply);
                    effectManagerScript.AddEffectIntoList(effect);
                }
                break;
        }
    }

    private Effect CreateEnemyEffect()
    {
        string effect = CalculateHideEffect();
        Effect enemyMonsterEffect = new Effect();
        switch (effect)
        {
            case "evade":
                enemyMonsterEffect.Type = "evade";
                enemyMonsterEffect.Turn = 1;
                enemyMonsterEffect.Apply = "enemy";
                enemyMonsterEffect.Value = 0;
                break;
            case "poison":
                enemyMonsterEffect.Type = "poison";
                enemyMonsterEffect.Turn = 3;
                enemyMonsterEffect.Apply = "enemy";
                enemyMonsterEffect.Value = 30;
                break;
            case "atkBuff":
                enemyMonsterEffect.Type = "atkBuff";
                enemyMonsterEffect.Turn = 3;
                enemyMonsterEffect.Apply = "enemy";
                enemyMonsterEffect.Value = 20;
                break;
            case "regen":
                enemyMonsterEffect.Type = "regen";
                enemyMonsterEffect.Turn = 3;
                enemyMonsterEffect.Apply = "enemy";
                enemyMonsterEffect.Value = 2000;
                break;

        }
        return enemyMonsterEffect;
    }

    private void ShowVisualEffect(MonsterEquip monsterEquip, string effectType)
    {
        effectGO = Instantiate(effectPrefab) as GameObject;

        effectGO.GetComponent<EffectScript>().SetEffectType(effectType);
        if (monsterEquip.EffectProperty.Apply == "me")
        {
            foreach (Transform child in visualEffectContainer.transform)
            {
                if (child.transform.childCount == 0)
                {
                    effectGO.transform.SetParent(child.transform);
                    RectTransform effectGORect = effectGO.GetComponent<RectTransform>();
                    effectGORect.localPosition = Vector3.zero;
                    effectGORect.localScale = Vector3.one;
                }
            }
        }
    }

    private void ShowVisualEffect(MonsterEquip monsterEquip, string effectType, string source)
    {
        effectGO = Instantiate(effectPrefab) as GameObject;

        effectGO.GetComponent<EffectScript>().SetEffectType(effectType, source);
        if (monsterEquip.EffectProperty.Apply == "me")
        {
            foreach (Transform child in visualEffectContainer.transform)
            {
                if (child.transform.childCount == 0)
                {
                    effectGO.transform.SetParent(child.transform);
                    RectTransform effectGORect = effectGO.GetComponent<RectTransform>();
                    effectGORect.localPosition = Vector3.zero;
                    effectGORect.localScale = Vector3.one;
                }
            }
        }
    }

    private void ShowVisualEffect(Effect effect, string effectType)
    {
        effectGO = Instantiate(effectPrefab) as GameObject;

        effectGO.GetComponent<EffectScript>().SetEffectType(effectType);
        if (effect.Apply == "enemy")
        {
            foreach (Transform child in visualEffectMonsterContainer.transform)
            {
                if (child.transform.childCount == 0)
                {
                    effectGO.transform.SetParent(child.transform);
                    RectTransform effectGORect = effectGO.GetComponent<RectTransform>();
                    effectGORect.localPosition = Vector3.zero;
                    effectGORect.localScale = Vector3.one;
                }
            }
        }
    }

    private void ShowVisualEffect(Effect effect, string effectType, string source)
    {
        effectGO = Instantiate(effectPrefab) as GameObject;

        effectGO.GetComponent<EffectScript>().SetEffectType(effectType, source);
        if (effect.Apply == "enemy")
        {
            foreach (Transform child in visualEffectMonsterContainer.transform)
            {
                if (child.transform.childCount == 0)
                {
                    effectGO.transform.SetParent(child.transform);
                    RectTransform effectGORect = effectGO.GetComponent<RectTransform>();
                    effectGORect.localPosition = Vector3.zero;
                    effectGORect.localScale = Vector3.one;
                }
            }
        }
        else
        {
            
            foreach (Transform child in visualEffectContainer.transform)
            {
                if (child.transform.childCount == 0)
                {
                    effectGO.transform.SetParent(child.transform);
                    RectTransform effectGORect = effectGO.GetComponent<RectTransform>();
                    effectGORect.localPosition = Vector3.zero;
                    effectGORect.localScale = Vector3.one;
                }
            }
            
        }
    }

    private void ShowPoisonEffect()
	{
		if (effectManagerScript.IsPoisonAdded)
		{
//			effectManagerScript.ApplyEffect ("poison");
			visualEffectInformScript.SetSprite ("poison");
			visualEffectInformScript.FadeInVisualEffect ();
			SetParticleGO (poisonGO, true);
			isPause = true;
			Invoke("FadeOutVisualEffect2", 1f);
		}
	}

	private void ShowAtkBuffEffect()
	{
		Debug.Log("GIA TANG LUC TAN CONG !!!!!");
		if (effectManagerScript.IsAtkBuffAdded)
		{
			Debug.Log("GIA TANG LUC TAN CONG !!!!!");
			effectManagerScript.ApplyEffect ("atkBuff");
			visualEffectInformScript.SetSprite ("atkBuff");
			visualEffectInformScript.FadeInVisualEffect ();
			SetParticleGO (atkBuffGO, true);
			isPause = true;
			Invoke ("SetAtkBuffParticleFalse", atkBuffParticle.duration);
		}
	}



	private void ResetTimeBar ()
	{
		timeBarScript.ResetTimeBar ();
        //effectManagerScript.ApplyEffect(IsPlayerPhase);
        StartCoroutine(effectManagerScript.DelayApplyEffect(isPlayerPhase));
		//if (effectManagerScript.IsRegenAdded) {
		//	Debug.Log ("CONG MAU NE!!!");
		//	effectManagerScript.ApplyEffect ("regen");
		//	visualEffectInformScript.SetSprite ("regen");
		//	visualEffectInformScript.FadeInVisualEffect ();
		//	SetParticleGO (regenGO, true);
		//	Invoke ("SetRegenParticleFalse", regenParticle.duration);
		//}

		//if (effectManagerScript.IsPoisonAdded) {
		//	Debug.Log("CHAY DONG NAY MAY LAN THE HA CAC BAN !!!");
		//	effectManagerScript.ApplyEffect ("poison");
		//}
	}

	private void SetParticleFalse ()
	{
		slashGO.SetActive (false);
	}

	private void ShowWrongAnswerParticle ()
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


	
	public void ShowChargeParticleWhenAnswer (GameObject particleGO)
	{
		for (int i = 0; i < answerText.Length; i++) {
			if (qb.rightAnswer == answerText [i].GetComponent<Text> ().text) {
				thunderParticlePos = answerText [i].transform.parent.transform.position;
				particleGO.transform.position = thunderParticlePos;
			}
			
		}
		particleGO.SetActive (true);
		Invoke("SetDeadParticleFalse", CompareParticleDuration());
		Invoke ("ShowQuestion", CompareParticleDuration ());
		Invoke ("ShowAllAnswerButton", CompareParticleDuration ());
	}

	private void SetDeadParticleFalse()
	{
		deadGO.SetActive (false);
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
		Invoke("SetDeadParticleFalse", CompareParticleDuration());
		Invoke ("ShowQuestion", CompareParticleDuration ());
		Invoke ("ResetTimeBar", CompareParticleDuration ());
		Invoke ("ShowAllAnswerButton", CompareParticleDuration ());
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
		
	private void ShowQuestion ()
	{
		questionFadeScript.ShowQuestion ();
	}

	private void ShowAttackParticle ()
	{
		slashGO.SetActive (true);
			
	}

	private void HideQuestion ()
	{
		questionFadeScript.HideQuestion ();
	}
		
		
}
