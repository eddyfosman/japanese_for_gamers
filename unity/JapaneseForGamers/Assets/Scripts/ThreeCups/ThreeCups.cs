using UnityEngine;
using System.Collections;

public class ThreeCups : MonoBehaviour {
	public Vector3[] cupPos;
	public Vector3 cupPos1;
	public Vector3 cupPos2;
	public Vector3 cupPos3;
	public GameObject cupPrefab;
	GameObject cupClone;
	Cup cupScript;
	GameObject[] allCups;
	int firstCup;
	Vector3 firstPos;
	GameObject ball;
	bool once = false;
	GameObject tempCup;
	bool isChangingCup = false;
	bool ready = false;
	public int numberShuffle;
	public float timeShuffle;
	public Transform[] path1;
	public Transform[] path2;
	public Transform[] path3;
	public Transform[] path4;
	public Transform[] path5;
	public Transform[] path6;

	void ShuffleCups(float timeShuffle){
		int changePos = Random.Range (1, 4);
		if(numberShuffle > 0){
			numberShuffle--;
		}
		switch(changePos){
		case 1:
			ChangePosition1(timeShuffle);
			break;
		case 2:
			ChangePosition2(timeShuffle);
			break;
		case 3:
			ChangePosition3(timeShuffle);
			break;
		}
	}

	void Ready(){
		ready = true;
	}

	void CallThis(){
		Debug.Log ("CHAY HAM NAY ROI DAY");
	}
	
	void CupGoUp(){
		iTween.MoveTo (allCups [firstCup], iTween.Hash ("position", new Vector3(allCups [firstCup].transform.position.x, allCups [firstCup].transform.position.y + 2f, allCups [firstCup].transform.position.z), "time", 1));
	}

	void TweenPath1(int i, float time){

		iTween.MoveTo(allCups[i],iTween.Hash("path",path1,"time",time,"looktime",.6,"easetype","easeInOutSine",
		                                     "onstart","ChangingCups","onstarttarget", gameObject));


	}

	void TweenPath2(int i, float time){
		iTween.MoveTo(allCups[i],iTween.Hash("path",path2,"time",time,"looktime",.6,"easetype","easeInOutSine",
		                                   "oncomplete","DoneChangingCups","oncompletetarget", gameObject));
	}

	void TweenPath3(int i, float time){
		iTween.MoveTo(allCups[i],iTween.Hash("path",path3,"time",time,"looktime",.6,"easetype","easeInOutSine",
		                                     "onstart","ChangingCups","onstarttarget", gameObject));
	}

	void TweenPath4(int i, float time){
		iTween.MoveTo(allCups[i],iTween.Hash("path",path4,"time",time,"looktime",.6,"easetype","easeInOutSine",
		                                     "oncomplete","DoneChangingCups","oncompletetarget", gameObject));
	}

	void TweenPath5(int i, float time){
		iTween.MoveTo(allCups[i],iTween.Hash("path",path5,"time",time,"looktime",.6,"easetype","easeInOutSine",
		                                     "onstart","ChangingCups","onstarttarget", gameObject));
	}

	void TweenPath6(int i, float time){
		iTween.MoveTo(allCups[i],iTween.Hash("path",path6,"time",time,"looktime",.6,"easetype","easeInOutSine",
		                                   "oncomplete","DoneChangingCups","oncompletetarget", gameObject));
	}
	
	void OnDrawGizmos(){
		iTween.DrawPath(path1);
		iTween.DrawPath(path2);
		iTween.DrawPath(path3);
		iTween.DrawPath(path4);
		iTween.DrawPath(path5);
		iTween.DrawPath(path6);
	}

	void MoveBallToCup(){
		firstPos = cupPos [allCups [firstCup].GetComponent<Cup> ().currentPosId];
		iTween.MoveTo (ball, iTween.Hash ("position", new Vector3(firstPos.x, firstPos.y, 1f),"delay", 1.5, "time", 2));

		
	}

	void ChangingCups(){
		isChangingCup = true;
		Debug.Log ("Dang xao tron coc!!!");
	}

	void DoneChangingCups(){
		isChangingCup = false;

		Debug.Log ("Xao trong coc xong!!!");
	}

	void CupGoDown(){
		iTween.MoveTo (allCups [firstCup], iTween.Hash ("position", new Vector3(allCups [firstCup].transform.position.x, allCups [firstCup].transform.position.y, allCups [firstCup].transform.position.z),"delay", 3,
		                                                "time", 1,"oncomplete", "Ready", "oncompletetarget", gameObject));
	}

	public void ChangePosition1(float timeShuffle){
		for(int i = 0; i < allCups.Length; i++){
			if(allCups[i].GetComponent<Cup>().currentPosId == 0){
				allCups[i].GetComponent<Cup>().currentPosId = 1;
//				allCups[i].transform.position = cupPos[1];
				TweenPath1(i,timeShuffle);
				tempCup = allCups[i];
			}
		}
		for(int i = 0; i < allCups.Length; i++){
			if(allCups[i].GetComponent<Cup>().currentPosId == 1 && allCups[i] != tempCup){
				allCups[i].GetComponent<Cup>().currentPosId = 0;
//				allCups[i].transform.position = cupPos[0];
				TweenPath2(i,timeShuffle);
			}
		}
	}

	public void ChangePosition2(float timeShuffle){
		for(int i = 0; i < allCups.Length; i++){
			if(allCups[i].GetComponent<Cup>().currentPosId == 1){
				allCups[i].GetComponent<Cup>().currentPosId = 2;
//				allCups[i].transform.position = cupPos[2];
				TweenPath5(i,timeShuffle);
				tempCup = allCups[i];
			}
		}
		for(int i = 0; i < allCups.Length; i++){
			if(allCups[i].GetComponent<Cup>().currentPosId == 2 && allCups[i] != tempCup){
				allCups[i].GetComponent<Cup>().currentPosId = 1;
//				allCups[i].transform.position = cupPos[1];
				TweenPath6(i,timeShuffle);
			}
		}

	}

	public void ChangePosition3(float timeShuffle){
		for(int i = 0; i < allCups.Length; i++){
			if(allCups[i].GetComponent<Cup>().currentPosId == 0){
				allCups[i].GetComponent<Cup>().currentPosId = 2;
//				allCups[i].transform.position = cupPos[2];
				TweenPath3(i,timeShuffle);
				tempCup = allCups[i];
			}
		}
		for(int i = 0; i < allCups.Length; i++){
			if(allCups[i].GetComponent<Cup>().currentPosId == 2 && allCups[i] != tempCup){
				allCups[i].GetComponent<Cup>().currentPosId = 0;
//				allCups[i].transform.position = cupPos[0];
				TweenPath4(i,timeShuffle);
			}
		}
//		
	}

	// Use this for initialization
	void Start () {
		ball = GameObject.FindGameObjectWithTag("Ball");

		for(int i = 0; i < cupPos.Length; i++){
			cupClone = Instantiate(cupPrefab) as GameObject;
			cupClone.GetComponent<Cup> ().currentPosId = i;
			cupClone.transform.position = cupPos[i];
		}
//		for(int i = 0; i < allCups.Length; i++){
//			allCups [i].GetComponent<Cup> ().currentPosId = i;
//		}
		allCups = GameObject.FindGameObjectsWithTag("Cup");
		firstCup = Random.Range (0, 3);
		allCups [firstCup].GetComponent<Cup> ().keepBall = true;
		cupScript = allCups[firstCup].GetComponent<Cup> ();
		CupGoUp ();
		MoveBallToCup ();
		CupGoDown ();
	}





	
	// Update is called once per frame
	void FixedUpdate () {
		if(numberShuffle > 0 && !isChangingCup && ready){
			Debug.Log("So lan tron coc con lai la: "  + numberShuffle);
			ShuffleCups(timeShuffle);
		}
		if(!once){
			once = true;
			CupGoUp ();
		}
		ball.transform.position = new Vector3(cupScript.gameObject.transform.position.x, cupScript.gameObject.transform.position.y, ball.transform.position.z);
	}



}
