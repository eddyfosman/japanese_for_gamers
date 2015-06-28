using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapController : MonoBehaviour {

	List<Transform> listStroke2 = new List<Transform>();
	Transform[] listStroke = new Transform[20];
	public GameObject strokeParent;
	public int strokeOrder = 1;
	public bool isLastStroke = false;
	public List<Vector3> listPos = new List<Vector3>(); 
	public int currentStroke = 2;
	public void NextStroke(){
		strokeOrder += 1;
		if((strokeOrder - 1 ) < listStroke2.Count){
			listStroke2[strokeOrder - 1].gameObject.SetActive(true);
		}
		else{
			isLastStroke = true;
		}
	}
	bool finishPreparing = false;

	public void CallNextStrokeAfter1Sec(){
//		Invoke ("NextStroke", 1f);
		Invoke("GetNextStroke", 1f) ;
	}

	void PrepareWord(){
		foreach(Transform child in transform){
			if(child != null && child.name.Substring(0,1) != "_"){
				listStroke2.Add(child);
				
				Mesh mesh = child.gameObject.GetComponent<MeshFilter>().mesh;
				
				
				
				Vector3[] vertices = mesh.vertices;
				
				int i = 0;
				while (i < vertices.Length) {
					if(i == vertices.Length/2){
						Vector3 worldPt = transform.TransformPoint(vertices[i]);
						float y = worldPt.y;
						GameObject goParent = Instantiate (strokeParent, new Vector3(worldPt.x,  y,worldPt.z), Quaternion.Euler(0f,0f,0f)) as GameObject;
						goParent.name = "_" + child.name;
						child.parent = goParent.transform;
						goParent.transform.parent = transform;
//						Debug.Log("VI TRI CUA " + child.name + " LA : " + child.transform.TransformPoint(worldPt));
//						Debug.Log("SO VERTICES LA: " + vertices.Length);
						listPos.Add(worldPt);
						
					}
					
					
					
					i++;
				}
				mesh.vertices = vertices;
				mesh.RecalculateBounds();
				child.gameObject.SetActive(false);
			}
		}
	}

	void CheckPreparation(){
		foreach(Transform child in transform){
			if(child.name.Substring(0,1) != "_"){
				break;
			}
			else{
				finishPreparing = true;
				break;
			}
		}
	}

	// Use this for initialization
	void Start () {
//		foreach(Transform child in transform){
//			if(child != null){
//				listStroke2.Add(child);
//
//				Mesh mesh = child.gameObject.GetComponent<MeshFilter>().mesh;
//				Vector3[] vertices = mesh.vertices;
//
//				int i = 0;
//				while (i < vertices.Length) {
//					if(i == vertices.Length/2){
//						Vector3 worldPt = transform.TransformPoint(vertices[i]);
//						Debug.Log("VI TRI CUA " + child.name + " LA : " + child.transform.TransformPoint(worldPt));
//						listPos.Add(worldPt);
//					}
//
//
//
//					i++;
//				}
//				mesh.vertices = vertices;
//				mesh.RecalculateBounds();
//				if(child.name != "S1"){
//					child.gameObject.SetActive(false);
//				}
//
//			}
//		}
		while(!finishPreparing){
			PrepareWord ();
			CheckPreparation();
		}


		GetNextStroke ();

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



	}

	public void GetNextStroke(){
		if(listStroke2.Count == 0){
			return;
		}
		int number = Random.Range (0, listStroke2.Count);
		listStroke2 [number].gameObject.SetActive (true);
		listStroke2.Remove(listStroke2[number]);

//		Debug.Log ("SO NGAU NHIEN LA: " + number);
//		Debug.Log ("SO PHAN TU CON TRONG LIST LA: " + listStroke2.Count);
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
