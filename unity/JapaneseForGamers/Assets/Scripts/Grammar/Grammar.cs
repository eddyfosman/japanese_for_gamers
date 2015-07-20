using UnityEngine;
using System.Collections;

public class Grammar : MonoBehaviour {

	public GameObject wordPanel;
	public GameObject prefabCell;
	GameObject cellGo;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 5; i++){
			cellGo = Instantiate(prefabCell) as GameObject;
			cellGo.transform.SetParent(wordPanel.transform, false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
