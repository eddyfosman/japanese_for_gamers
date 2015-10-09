using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterManager : MonoBehaviour {
	
	public Image monsterA;
	public Image monsterB;
	public Image monsterC;
	public Image monsterD;
	public Image topCurtain;
	public Image bottomCurtain;
	public Image visualMonster;
	public Image behindMonsterPanel;
	public GameObject coverGamePanel;
	public GameObject buttonA;
	Vector3 startPos;
	Vector3 initPos;
	Sprite imageA;
	public GameObject questionManager;
	QuestionManager questionMangerScript;
	
	// Use this for initialization
	void Start () {
		questionMangerScript = questionManager.GetComponent<QuestionManager>();
		coverGamePanel.SetActive (false);
		behindMonsterPanel.gameObject.SetActive (false);
		startPos = monsterA.gameObject.transform.position;
		Debug.Log (buttonA.transform.position);
		
		initPos = visualMonster.transform.position;
		//		MoveMonsterIn ();
	}
	
	public void LoadMonster(string monsterName){
		visualMonster.sprite = Resources.Load ("Sprites/" + monsterName,typeof(Sprite)) as Sprite;
		Debug.Log (monsterName);
	}

	public void LoadMonster(MonsterEquip mq){
		visualMonster.sprite = Resources.Load ("Sprites/" + mq.Image,typeof(Sprite)) as Sprite;
	}
	
	public void MoveMonsterIn(){
		behindMonsterPanel.gameObject.SetActive (true);
		iTween.MoveTo (visualMonster.gameObject,iTween.Hash("x",0f,"y",0f,"z",initPos.z,"time",2f,"oncomplete","MoveMonsterOut","oncompletetarget",gameObject));
		Debug.Log (visualMonster.transform.position);
	}

	public void MoveMonsterIn(MonsterEquip mq){
		behindMonsterPanel.gameObject.SetActive (true);
		visualMonster.transform.position = new Vector3 (initPos.x, initPos.y - mq.PosY/100, initPos.z);
		iTween.MoveTo (visualMonster.gameObject,iTween.Hash("x",0f,"y",0f,"z",initPos.z,"time",2f,"oncomplete","MoveMonsterOut","oncompletetarget",gameObject));
	}
	
	public void MoveMonsterOut(){
		iTween.MoveTo (visualMonster.gameObject,iTween.Hash("x",-initPos.x,"y",initPos.y,"z",initPos.z,"time",1f,"oncomplete","InformQuestionManager","oncompletetarget",gameObject));
	}
	
	void InformQuestionManager(){
		visualMonster.gameObject.transform.position =new Vector3( initPos.x,initPos.y,initPos.z);
		questionMangerScript.isMonsterMoveOut = true;
		behindMonsterPanel.gameObject.SetActive (false);
		Debug.Log ("QUAI VAT DA CHAY RA NGOAI");
	}
	
	// Update is called once per frame
	void Update () {
		monsterA.gameObject.transform.position = new Vector3 (startPos.x, startPos.y - 0.45f, startPos.z);

	}
}