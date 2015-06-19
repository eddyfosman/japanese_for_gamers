using UnityEngine;
using System.Collections;

public class PaddleManager : MonoBehaviour {

	public GameObject ball;
	GameObject[] brickList;
	public bool reachedLeft = false;
	public bool reachedRight = false;
	public int paddleCount = 2;
	int paddleTime = 9;
	public int PaddleTime{
		set{
			paddleTime = value;
//			dong nay de bat lai dem clone
//			paddleCount = paddleTime/5;
			if(paddleCount > 4){
				paddleCount = 4;
			}

		}
		get{
			return paddleTime;
		}
	}

	float coolDown = 1f;
	bool onCD = false;
	bool onDelay = false;
	public bool onCDBall = false;

	public GameObject paddle2;
	public GameObject paddle3;
	public GameObject paddle4;
	// Use this for initialization
	void Start () {
		GameObject Ball = Instantiate (ball) as GameObject;
		if(paddleCount < 2){
			paddle2.SetActive(false);
			paddle3.SetActive(false);
			paddle4.SetActive(false);
		}
		brickList = GameObject.FindGameObjectsWithTag("Brick");

	}

	public void BallThrough(){
		for(int i = 0; i < brickList.Length; i++){
			if(brickList[i] != null){
				BoxCollider boxCol = brickList[i].GetComponent<BoxCollider>();
				boxCol.isTrigger = true;
			}

		}
		StartCoroutine (SuperBallCoolDown());
	}

	IEnumerator SuperBallCoolDown(){
		onCDBall = true;
		yield return new WaitForSeconds (4f);
		onCDBall = false;
		NoBallThrough ();
	}

	public void NoBallThrough(){
		for(int i = 0; i < brickList.Length; i++){
			if(brickList[i] != null){
				BoxCollider boxCol = brickList[i].GetComponent<BoxCollider>();
				boxCol.isTrigger = false;
			}
			
		}
	}


	IEnumerator CoolDownTime(){
		onCD = true;
		yield return new WaitForSeconds (coolDown);
		onCD = false;
	}

	IEnumerator DelayClone(){
		onDelay = true;
		yield return new WaitForSeconds (0.2f);
		onDelay = false;
	}

	public void Cloner(){
//		if(paddleCount < 4){
		if(!onDelay){
			PaddleTime += 5;
		}
		StartCoroutine (DelayClone());
//		}

//		Invoke ("Decloner", 5f);
	}

	public void Decloner(){
		if (paddleCount > 1) {
//			dong nay de loai bo 1 paddle tren man hinh
//			paddleCount--;
		}

	}
	
	// Update is called once per frame
	void Update () {

		if(!onCD && paddleTime > 9){
			StartCoroutine(CoolDownTime());
			PaddleTime-=1;
		}

		if(paddleCount == 1){
			paddle2.SetActive(false);
			paddle3.SetActive(false);
			paddle4.SetActive(false);
		}
		if(paddleCount == 2){
			paddle2.SetActive(true);
			paddle3.SetActive(false);
			paddle4.SetActive(false);
		}
		if(paddleCount == 3){
			paddle2.SetActive(true);
			paddle3.SetActive(true);
			paddle4.SetActive(false);
		}
		if(paddleCount == 4){
			paddle2.SetActive(true);
			paddle3.SetActive(true);
			paddle4.SetActive(true);
		}
//		Debug.Log ("Tong so thoi gian dang co la: " + paddleTime);
//		Debug.Log ("So ninja tren man hinh la: " + paddleCount);
	}
}
