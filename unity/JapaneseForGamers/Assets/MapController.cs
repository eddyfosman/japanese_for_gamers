using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapController : MonoBehaviour {

	List<Transform> listStroke2 = new List<Transform>();
	Transform[] listStroke = new Transform[20];

	// Use this for initialization
	void Start () {
//		foreach(Transform child in transform){
//			if(child != null){
//				listStroke2.Add(child);
//			}
//		}
		for(int i = 0; i < gameObject.transform.childCount; i++){
			Transform child = gameObject.transform.GetChild(i);
			if(child != null){
				listStroke[i] = child;
//				listStroke.SetValue(child, i);
			}
		}
		for(int i = 0; i < listStroke.Length; i++){
			if(listStroke[i] != null){
				Debug.Log("TEN CUA NET CHU: " + listStroke[i].name);
			}

		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
