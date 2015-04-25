using UnityEngine;
using System.Collections;

public class KanjiWord : MonoBehaviour {
	bool isMoveable = false;
	bool onCD = true;
	bool switchCo = false;
	public bool grabbed = false;
	Vector2 grabberPos;
	Rope ropeScript;
	GameObject grabber;
	BoxCollider2D grabberBoxCol;
	public GameObject starParticle;
	float cachedY;
	float multiplier = 0.2f;
	KanjiManager kanjiManagerScript;

	void InstantiateStarDust(){
		GameObject starDust = Instantiate (starParticle) as GameObject;
		starDust.transform.position = gameObject.transform.position;
		Destroy (starDust, 0.33f);

	}

	IEnumerator CoolDown(){
		onCD = false;
		yield return new WaitForSeconds (5f);
		onCD = true;
		Debug.Log ("SAU 5 GIAY CHAY VAO DAY");

	}

	// Use this for initialization
	void Start () {
		kanjiManagerScript = GameObject.Find ("KanjiManager").GetComponent<KanjiManager>();

		cachedY = transform.position.y;
		grabber = GameObject.FindGameObjectWithTag ("Grabber");
		if(grabber != null){
			grabberBoxCol = grabber.GetComponent<BoxCollider2D>();
		}
		ropeScript = GameObject.Find ("Hand_L").GetComponent<Rope>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!grabbed || isMoveable ){
			if(Mathf.Abs(transform.position.y - cachedY) > 1.5f){

				Debug.Log("BAT DAU CHO 5 GIAY");
				multiplier *= -1;
				StartCoroutine(CoolDown());



			}
			if(onCD){
				transform.position = Vector3.Lerp(transform.position, (transform.position + Vector3.up * multiplier),10f * Time.deltaTime);

			}

			
		}

		grabberPos = GameObject.FindGameObjectWithTag ("Grabber").transform.position;
	}

	void FixedUpdate(){
		if(grabbed){
			transform.position = new Vector2((grabberPos.x - grabberBoxCol.size.x*2f), grabberPos.y);
		}

	}

	void OnTriggerEnter2D(Collider2D other){

		if(ropeScript.isPulling && kanjiManagerScript.isKanjiGrabbed){

			return;
		}
		if(other.gameObject.tag == "Explosion"){
			Destroy(gameObject);
		}

		if(other.gameObject.tag == "Grabber"){

			InstantiateStarDust();
			Debug.Log("Khong va cham cai gi ca");
			grabbed = true;
			ropeScript.isPulling = true;
			if(!ropeScript.isMissed){
				ropeScript.isShooting = false;
			}

		}
		
	}
}
