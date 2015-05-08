using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KanjiManager : MonoBehaviour {
	public GameObject explosion;
	Rope ropeScript;
	public bool isKanjiGrabbed = false;
	GameObject[] wordList;
	public List<GameObject> wordList2 = new List<GameObject>();
	KanjiWord kanjiWordScript;

	Transform GetGrabbedWordPos(){
		foreach(GameObject go in wordList2){
			if(go != null){
				kanjiWordScript = go.GetComponent<KanjiWord>();
				if(kanjiWordScript.grabbed){
					return kanjiWordScript.gameObject.transform;
				}

			}

		}
		return null;
	}

	void DestroyObject(){
		GameObject exploGO = Instantiate (explosion) as GameObject;
		exploGO.transform.position = GetGrabbedWordPos ().position;
		Destroy (GetGrabbedWordPos ().gameObject);
		Destroy (exploGO, 1f);
	}

	void CheckTouch(){
		if(Input.GetMouseButton(0)){
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if(hit && hit.collider.gameObject.tag == "Dynamite"){
				if(ropeScript.isPulling){
					ropeScript.isPulling = false;
					ropeScript.isMissed = true;
					ropeScript.d = true;
					Debug.Log("BOOOOOM");
					DestroyObject();
					ropeScript.grabberRigid2d.velocity *= 10f;
//					ropeScript.FaceTheOtherWay();
//					Debug.Log("MA SO 00000000000000005");
				}

			}
		}

	}

	// Use this for initialization
	void Start () {
		ropeScript = GameObject.Find ("Hand_L").GetComponent<Rope>();
		wordList = GameObject.FindGameObjectsWithTag ("Kanji");
		foreach(GameObject go in wordList){
			wordList2.Add(go);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		CheckTouch ();
	}
}
