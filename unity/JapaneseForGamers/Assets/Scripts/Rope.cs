using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rope : MonoBehaviour {
	float i = 0f;
	float t = 10f;
	float i2 = 1f;
	bool a = false;
	bool b = false;
	bool c = false;
	public bool d = false;
	public bool shoot = false;
	public bool isPulling = false;
	public bool isShooting = true;
	public bool isMissed = false;
	bool isChangedVelocity = false;
	private float cachedX;
	float gocQuay;
	bool storedGrabberPos = false;
	Vector3 cachedPosRope;
	private Renderer render;
	Renderer grabberRender;
	Vector3 from;
	Vector3 to;
	Vector3 position;
	public Rigidbody2D grabberRigid2d;
	Vector2 direction;
	Vector2 grabberCachedPos;
	Transform grabberTransform;
	KanjiManager kanjiManagerScript;
	Transform flameTransform;
	public ParticleSystem particleSystem;
	FairyFly fairyFlyScript;

	public void FaceTheOtherWay(){
		Debug.Log ("QUAY DAU 1 LAN");
		grabberTransform.localScale = new Vector3(-grabberTransform.localScale.x, grabberTransform.localScale.y, grabberTransform.localScale.z);
	}

	bool CheckXMargin(){
		return Mathf.Abs (grabberTransform.position.x - grabberCachedPos.x) < 0.3;
	}

	bool CheckYMargin(){
		return Mathf.Abs (grabberTransform.position.y - grabberCachedPos.y) < 0.3;
	}

	// Use this for initialization
	void Start () {
		flameTransform = GameObject.Find ("GrabberFlame").transform;
		cachedX = gameObject.GetComponentInChildren<Transform>().localScale.x;
		render = GameObject.Find ("Staff").GetComponent<Renderer> ();
		grabberRender = GameObject.FindGameObjectWithTag("Grabber").GetComponent<Renderer>();
		grabberRigid2d = GameObject.FindGameObjectWithTag("Grabber").GetComponent<Rigidbody2D>();
		grabberTransform = GameObject.FindGameObjectWithTag("Grabber").transform;
		kanjiManagerScript = GameObject.Find ("KanjiManager").GetComponent<KanjiManager>();
		fairyFlyScript = GameObject.FindGameObjectWithTag ("FairyFly").GetComponent<FairyFly>();

	}
	
	// Update is called once per frame
	void Update () {
		if(!shoot){
			particleSystem.enableEmission = false;
			grabberRender.enabled = false;
		}
		else{
			if(!fairyFlyScript.didFairyArrive){
				return;
			}
			particleSystem.enableEmission = true;
			grabberRender.enabled = true;
		}

		
		if(Application.platform == RuntimePlatform.Android){
			if(Input.touchCount > 0){
				if(Input.GetTouch(0).phase == TouchPhase.Began){
					gameObject.GetComponentInChildren<Transform>().localScale = new Vector3(cachedX, i2++);
				}
			}
		}
		
		if(Application.platform == RuntimePlatform.WindowsEditor){
			if(Input.GetMouseButtonDown(0)){
				shoot = true;
				if(!storedGrabberPos){
					storedGrabberPos = true;
					grabberCachedPos = GameObject.FindGameObjectWithTag ("Grabber").transform.position;
				}
			}
		}

		if(shoot){


			

			float i3 = GameObject.Find("Staff").transform.localScale.y;

			gocQuay = gameObject.transform.eulerAngles.z;

		}

		if(!shoot){
			if(i > -30f && !a){
				t = -1f;
				a = true;
//				Debug.Log("DIEU KIEN 1");
			}
			else if(i < -150f){
				t = 1f;
				a = false;
//				Debug.Log("DIEU KIEN 2");
				
			}
			
			i += t;

			gameObject.transform.localRotation = Quaternion.Euler(0f,0f,i);
		}
		position = GameObject.Find ("Staff").transform.position;
		from = GameObject.FindGameObjectWithTag("Grabber").transform.position;
		Vector3 directionv3 = from - position;
		direction = new Vector2 (directionv3.x, directionv3.y);

		to = GameObject.FindGameObjectWithTag ("Grabber").transform.position;
		if(shoot){
//			
			GameObject.Find ("Staff").transform.position = position + (to - from);
//			
		}



//		Debug.Log ("Kich co chieu ngang : " + render.bounds.size.x);
//		Debug.Log ("Kich co chieu doc : " + render.bounds.size.y);
//		Debug.Log ("Goc quay cua day: " + gameObject.transform.localRotation);
//		Debug.Log ("Goc quay cua day2: " + gameObject.transform.eulerAngles);
//		Debug.Log ("Goc quay cua day3: " + gameObject.transform.localEulerAngles);
//		Debug.Log ("GDo rong man hinh: " + Screen.width);
//		Debug.Log ("Vi tri cua day: " + GameObject.Find ("Staff").transform.localPosition);
//		Debug.Log ("Vi tri cua day2: " + GameObject.Find ("Staff").transform.position);

	}

	void ResetRotating(){

			grabberRigid2d.velocity = Vector2.zero;
			grabberTransform.position = grabberCachedPos;
			FaceTheOtherWay ();
			Debug.Log("MA SO 00000000000000004");
			shoot = false;
			kanjiManagerScript.isKanjiGrabbed = false;
			storedGrabberPos = false;
			isChangedVelocity = false;
			isPulling = false;
			i = Random.Range (-150, -30);
			b = false;
			c = false;
			d = false;

	}

	void FixedUpdate(){
//		if(grabberRigid2d.velocity != Vector2.zero){
			flameTransform.rotation = Quaternion.LookRotation (-grabberRigid2d.velocity);
//		}

		foreach(GameObject go in kanjiManagerScript.wordList2){
			if(go != null){
				KanjiWord kanjiWordScript = go.GetComponent<KanjiWord>();
				if(kanjiWordScript.grabbed){
					kanjiManagerScript.isKanjiGrabbed = true;
				}
			}
			
		}

		if(shoot){
			if(!fairyFlyScript.didFairyArrive){
				return;
			}
			if(!isChangedVelocity){
//				Debug.Log("VECTOR HUONG "  + direction);
				grabberRigid2d.AddForce(direction * 1000f);
				isChangedVelocity = true;
//				Debug.Log("VECTOR VELOCITY : " + grabberRigid2d.velocity);
			}

			if(!isShooting){
				Debug.Log("DAP DAU VAO TUONG NEN PHAI QUAY DAU");
				isShooting = true;
				grabberRigid2d.velocity = -grabberRigid2d.velocity;
			}
//			
		}

		if(isMissed){

			if(isPulling){
				isMissed = false;
				FaceTheOtherWay();
				Debug.Log("MA SO 00000000000000001");
				return;
			}
			if(!d){
				d = true;
				FaceTheOtherWay();
				Debug.Log("MA SO 00000000000000002");
//				Debug.Log("CHAY VAO MAY LAN VAY");
			}
//			Debug.Log("CO VAO DAY KHONG THE");
			if(CheckXMargin() && CheckYMargin()){
				ResetRotating();
				isMissed = false;
//				Debug.Log("CO VAO DAY KHONG THE");
			}


			return;
		}

		if(isPulling){
			if(!b){
				FaceTheOtherWay();
				Debug.Log("MA SO 00000000000000003");
				grabberRigid2d.velocity *= 0.1f;
				b = true;
			}


			if(CheckXMargin() && CheckYMargin()){
				foreach(GameObject go in kanjiManagerScript.wordList2){
					if(go != null){
						KanjiWord kanjiWordScript = go.GetComponent<KanjiWord>();
						if(kanjiWordScript.grabbed){
							
							go.GetComponent<Renderer>().enabled = false;
							
							Destroy(go);
						}
					}
					
				}
				ResetRotating();
			}
		}
	}




}
