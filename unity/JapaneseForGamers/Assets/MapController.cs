using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapController : MonoBehaviour {

	List<Transform> listStroke2 = new List<Transform>();
	Transform[] listStroke = new Transform[20];
	public int strokeOrder = 1;
	public bool isLastStroke = false;

	public void NextStroke(){
		strokeOrder += 1;
		if((strokeOrder - 1 ) < listStroke2.Count){
			listStroke2[strokeOrder - 1].gameObject.SetActive(true);
		}
		else{
			isLastStroke = true;
		}
	}

	public void CallNextStrokeAfter1Sec(){
		Invoke ("NextStroke", 1f);
	}

	// Use this for initialization
	void Start () {
		foreach(Transform child in transform){
			if(child != null){
				listStroke2.Add(child);
				if(child.name != "S1"){
					child.gameObject.SetActive(false);
				}

			}
		}
//		for(int i = 0; i < gameObject.transform.childCount; i++){
//			Transform child = gameObject.transform.GetChild(i);
//			if(child != null){
//				listStroke[i] = child;
//
//			}
//		}
//		for(int i = 0; i < listStroke.Length; i++){
//			if(listStroke[i] != null){
//				Debug.Log("TEN CUA NET CHU: " + listStroke[i].name);
//			}
//
//		}

		foreach(Transform child in listStroke2){
			if(child != null){

				
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
