using UnityEngine;
using System.Collections;

public class KanjiWord : MonoBehaviour {
	public bool isMoveable = false;

	public float SpeedMultiplier{ get; set;}

	public float TimeDelay{ get; set;}

    float direction;
    public float Direction{ get; set;}

	float invokeTime;
	public float InvokeTime{ get; set;}

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
	bool test = false;

	void InstantiateStarDust(){
		GameObject starDust = Instantiate (starParticle) as GameObject;
		starDust.transform.position = gameObject.transform.position;
		Destroy (starDust, 0.33f);

	}

	IEnumerator CoolDown(){
		onCD = false;
		Debug.Log ("CHAY 5 GIAY BAT DAU");
		yield return new WaitForSeconds (TimeDelay);
		onCD = true;
		Invoke ("DoThisAfter1Sec", InvokeTime);
		Debug.Log ("SAU 5 GIAY CHAY VAO DAY");

	}

	void DoThisAfter1Sec(){
		test = false;
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
			if(onCD){
				transform.position = Vector3.Lerp(transform.position, (transform.position + Vector3.up * multiplier * direction ),10f * Time.deltaTime);
				Debug.Log("DANG D CHUYEN DAY NE");
			}

			if((Mathf.Abs(transform.position.y - cachedY) > 1.5f) && !test){

				Debug.Log("BAT DAU CHO 5 GIAY");
				test = true;
				multiplier *= -1;
				if(onCD){
					StartCoroutine(CoolDown());
				}




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
