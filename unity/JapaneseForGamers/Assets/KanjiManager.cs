using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KanjiManager : MonoBehaviour {
	public bool isKanjiGrabbed = false;
	GameObject[] wordList;
	public List<GameObject> wordList2 = new List<GameObject>();

	// Use this for initialization
	void Start () {
		wordList = GameObject.FindGameObjectsWithTag ("Kanji");
		foreach(GameObject go in wordList){
			wordList2.Add(go);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
