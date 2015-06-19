using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class PositionBean{

//	int id;
//	public int ID {
//				get;
//				set;
//	}

	Dictionary<string,string> option;
	public Dictionary<string,string> Option {
				get{return option;}
			
	}

	string type;
	public string Type {
				get;
				set;
	}

	float xpos;
	public float XPos{ 
				get; 
				set;
	}

	float ypos;
	public float YPos {
				get;
				set;
	}

	public PositionBean(){
		option = new Dictionary<string, string> ();
	}

}



public class KanjiManager : MonoBehaviour {
	public GameObject wordItem;
	PositionBean posObject;
	public GameObject explosion;
	Rope ropeScript;
	public bool isKanjiGrabbed = false;
	GameObject[] wordList;
	public List<GameObject> wordList2 = new List<GameObject>();
	KanjiWord kanjiWordScript;
	List <PositionBean> itemWordMiner = new List<PositionBean>();

	void GetItemDataFromJson(){
		TextAsset wordMinerText = Resources.Load ("wordminerjson") as TextAsset;
		JsonData jsonItem = JsonMapper.ToObject (wordMinerText.text);
		for(int i = 0; i < jsonItem["items"].Count; i++){
			posObject = new PositionBean();
			posObject.XPos = float.Parse(jsonItem["items"][i]["xpos"].ToString());
			posObject.YPos = float.Parse(jsonItem["items"][i]["ypos"].ToString());
			posObject.Type = jsonItem["items"][i]["type"].ToString();
			posObject.Option["moving"] = jsonItem["items"][i]["options"][0]["moving"].ToString();
			posObject.Option["direction"] = jsonItem["items"][i]["options"][0]["direction"].ToString();
			posObject.Option["delay"] = jsonItem["items"][i]["options"][0]["delay"].ToString();
			posObject.Option["timeinvoke"] = jsonItem["items"][i]["options"][0]["timeinvoke"].ToString();
			posObject.Option["speed"] = jsonItem["items"][i]["options"][0]["speed"].ToString();
			itemWordMiner.Add(posObject);
		}

		InstantiateItemInMap ();
	}

	void InstantiateItemInMap(){
		foreach(PositionBean posObject in itemWordMiner){
			if(posObject.Type == "word"){
				GameObject newWordItem = Instantiate (wordItem) as GameObject;
				newWordItem.transform.position = new Vector3(posObject.XPos, posObject.YPos, 0f);
				kanjiWordScript = newWordItem.GetComponent<KanjiWord>();
				kanjiWordScript.isMoveable = bool.Parse(posObject.Option["moving"]);
				kanjiWordScript.SpeedMultiplier = float.Parse(posObject.Option["speed"]);
				kanjiWordScript.Direction = float.Parse(posObject.Option["direction"]);
				kanjiWordScript.TimeDelay = float.Parse(posObject.Option["delay"]);
				kanjiWordScript.InvokeTime = float.Parse(posObject.Option["timeinvoke"]);
			}
			else if(posObject.Type == "tnt"){
				GameObject tnt = Instantiate(explosion) as GameObject;
				tnt.transform.position = new Vector3(posObject.XPos, posObject.YPos, 0f);
			}
		}
	}

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
		GetItemDataFromJson ();
		ropeScript = GameObject.Find ("Hand_L").GetComponent<Rope>();
		wordList = GameObject.FindGameObjectsWithTag ("Kanji");
		foreach(GameObject go in wordList){
			wordList2.Add(go);
		}

		GetItemDataFromJson ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		CheckTouch ();
	}
}
