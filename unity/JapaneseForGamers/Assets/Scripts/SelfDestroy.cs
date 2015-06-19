using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour {
	public int hitPoints = 1;
	public GameObject clone;
	public GameObject word;

	public GameObject powerup;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CreateWord(){
		GameObject wordBonus = Instantiate (word, transform.parent.localPosition, Quaternion.Euler(0f, 0f, 0f)) as GameObject;

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
			Destroy (gameObject);
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
