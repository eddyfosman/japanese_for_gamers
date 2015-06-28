using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour {
	public int hitPoints = 1;
	public GameObject clone;
	public GameObject word;
	MapController mapControllerScript;
	public GameObject powerup;
	MeshCollider mesh;
	public static int numberDestroy = 0;
	Vector3 wordPos;


	// Use this for initialization
	void Start () {
//		Debug.Log ("DA CHAY CAI NAY CHUA THE NHI");
		mapControllerScript = GameObject.FindGameObjectWithTag("Map").GetComponent<MapController>();
		mesh = gameObject.GetComponent<MeshCollider>();


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CreateWord(){
		numberDestroy++;

		mapControllerScript.currentStroke = numberDestroy + 1;



//		Debug.Log("THU TU CUA NET VE TRONG SELFDESTROY LA : " + mapControllerScript.currentStroke);
		wordPos = new Vector3 (mapControllerScript.listPos[numberDestroy-1].x, mapControllerScript.listPos[numberDestroy-1].y, mapControllerScript.listPos[numberDestroy-1].z);
//		wordPos = new Vector3 (0.3f,  4.4f, 1f);

		GameObject wordBonus = Instantiate (word, wordPos, Quaternion.Euler(0f, 0f, 0f)) as GameObject;

		int rnd = Random.Range (5, 10);
		Sprite myFruit;
		if(rnd == 0){
			myFruit = Resources.Load("zero", typeof(Sprite)) as Sprite;
		}
		else if(1 == rnd){
			myFruit = Resources.Load("one", typeof(Sprite)) as Sprite;
		}
		else if(2 == rnd){
			myFruit = Resources.Load("two", typeof(Sprite)) as Sprite;
		}
		else if(3 == rnd){
			myFruit = Resources.Load("three", typeof(Sprite)) as Sprite;
		}
		else if(4 == rnd){
			myFruit = Resources.Load("four", typeof(Sprite)) as Sprite;
		}
		else if(5 == rnd){
			myFruit = Resources.Load("five", typeof(Sprite)) as Sprite;
		}
		else if(6 == rnd){
			myFruit = Resources.Load("six", typeof(Sprite)) as Sprite;
		}
		else if(7 == rnd){
			myFruit = Resources.Load("seven", typeof(Sprite)) as Sprite;
		}
		else if(8 == rnd){
			myFruit = Resources.Load("eight", typeof(Sprite)) as Sprite;
		}
		else{
			myFruit = Resources.Load("nine", typeof(Sprite)) as Sprite;
		}
		
		wordBonus.GetComponent<SpriteRenderer> ().sprite = myFruit;
	}

	public void DoBeforeDestroy(){
//				GameObject cloner = Instantiate (clone, transform.position, transform.rotation) as GameObject;
//		GameObject power = Instantiate (powerup, transform.position, transform.rotation) as GameObject;
		hitPoints--;
		if(hitPoints <= 0){
			CreateWord();
			if(!mapControllerScript.isLastStroke){
				mapControllerScript.CallNextStrokeAfter1Sec();
			}

			Destroy (mesh);
		}
	}

	void OnCollisionEnter(Collision col){
		DoBeforeDestroy ();

	}

//	void OnTriggerEnter(Collider col){
//		//		GameObject cloner = Instantiate (clone, transform.position, transform.rotation) as GameObject;
//		GameObject power = Instantiate (powerup, transform.position, transform.rotation) as GameObject;
//		hitPoints--;
//		if(hitPoints <= 0){
//			CreateWord();
//			Destroy (gameObject);
//		}
//		
//	}
}
