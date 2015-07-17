using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MemoryMatching : MonoBehaviour {
	Card cardScript;
	List<GameObject> twoCards = new List<GameObject>();
	List<string> twoWords = new List<string>();
	List<Card> twoCardScripts = new List<Card>();
	public GameObject cardPrefab;
	GameObject cardGO;
	public int cardNumber = 16;
	public GameObject canvasParent;
	RectTransform rectTrans;
	float count = 0;
	List<Transform> listCard = new List<Transform>();
	List<string> listWord = new List<string> ();
	static int cardFlippedNum = 0;
	public int CardFlippedNum{
		get{return cardFlippedNum;}
		set{cardFlippedNum = value;
			if(2 == cardFlippedNum ){
				Invoke("CheckTwoChoice", 2f);

				Debug.Log("Kiem tra xem co trung khong ???");
			}
		}
	}

	void CheckTwoChoice(){
		GameObject[] allCard = GameObject.FindGameObjectsWithTag("Card");
		for(int i = 0; i < allCard.Length; i++){
			cardScript = allCard[i].GetComponent<Card>();
			if(cardScript.isFlipped && !cardScript.isFinished){
				twoCards.Add(allCard[i]);
				twoWords.Add(cardScript.cardString);
				twoCardScripts.Add(cardScript);
				if(twoCards.Count == 2){
					break;
				}
			}
		}
		if(twoWords[0] == twoWords[1]){
			Debug.Log("Tra loi dung roi");
			twoCardScripts[0].isFinished = true;
			twoCardScripts[1].isFinished = true;
			CardFlippedNum = 0;
			twoCards = new List<GameObject>();
			twoCardScripts = new List<Card>();
			twoWords = new List<string>();
		}
		else{
			Debug.Log("Tra loi SAI ROI");
			twoCardScripts[0].gameObject.GetComponentInChildren<Text>().text = "New Text";
			twoCardScripts[1].gameObject.GetComponentInChildren<Text>().text = "New Text";
			twoCardScripts[0].isFlipped = false;
			twoCardScripts[1].isFlipped = false;
			CardFlippedNum = 0;
			twoCards = new List<GameObject>();
			twoCardScripts = new List<Card>();
			twoWords = new List<string>();

		}
	}

	void CreateAllCards(){
		for(int i = 0; i < 4; i++){
			for(int j = 0; j < 4; j++){
//				int round = (int)count;
				cardGO = Instantiate(cardPrefab) as GameObject;
				cardGO.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 4, Screen.width / 4);
				rectTrans = cardGO.GetComponent<RectTransform>();
//				Debug.Log("Round la: " + round);
//				Debug.Log("Chieu rong cua la bai la: " + rectTrans.rect.width);
				cardGO.transform.position = new Vector3(j*rectTrans.rect.width + rectTrans.rect.width/2, -(i+1)*rectTrans.rect.width + rectTrans.rect.width/2, 0f);
				cardGO.transform.SetParent(canvasParent.transform, false);
//				count += 0.25f;
			}

		}
	}

	void GetAllCards(){
		foreach (Transform child in canvasParent.transform) {
			listCard.Add(child);
				}
	}

	// Use this for initialization
	void Start () {
		listWord.Add ("1");
		listWord.Add ("2");
		listWord.Add ("3");
		listWord.Add ("4");
		listWord.Add ("5");
		listWord.Add ("6");
		listWord.Add ("7");
		listWord.Add ("8");
		Debug.Log (Screen.width);
		Debug.Log (Screen.height);
		CreateAllCards ();
		GetAllCards ();
		for(int i = 0; i < listWord.Count; i++){
			for(int j = 0; j < 2; j++){
				if(listCard.Count > 0){
					int random = Random.Range(1, listCard.Count);
					Debug.Log("So phan tu trong listcard: "  + listCard.Count);
					Debug.Log("So RANDOM la : " + random);
//					listCard[random].GetComponentInChildren<Text>().text = listWord[i];
					listCard[random].GetComponent<Card>().cardString = listWord[i];
					listCard.Remove(listCard[random]);

				}
			}

		}



	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
