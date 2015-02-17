using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour {
	float i = 0f;
	float t = 1f;
	float i2 = 1f;
	bool a = false;
	bool shoot = false;
	bool isPulling = false;
	private float cachedX;
	float gocQuay;
	Vector3 cachedPosRope;
	private Renderer render;
	Vector3 from;
	Vector3 to;
	Vector3 position;


	// Use this for initialization
	void Start () {
		cachedX = gameObject.GetComponentInChildren<Transform>().localScale.x;
		render = GameObject.Find ("Rope").GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {



		if(i > 80f && !a){
			t = -1f;
			a = true;
			Debug.Log("DIEU KIEN 1");
		}
		else if(i < -80f){
			t = 1f;
			a = false;
			Debug.Log("DIEU KIEN 2");

		}

		i += t;
		if(!shoot){
			gameObject.transform.localRotation = Quaternion.Euler(0f,0f,i);
		}
		position = GameObject.Find ("Rope").transform.position;
		from = GameObject.Find ("TipPoint").transform.position;
		if(shoot){
//			gameObject.GetComponentInChildren<Transform>().localScale = new Vector3(cachedX, i2++);
			GameObject.Find("Rope").transform.localScale = new Vector3(cachedX, (i2+=1f));
			float i3 = GameObject.Find("Rope").transform.localScale.y;
			GameObject.Find("TipPoint").transform.localScale = new Vector3(cachedX, (1f/i3));
			gocQuay = gameObject.transform.eulerAngles.z;
//			GameObject.Find("Rope").transform.position = new Vector3(0f, -0.5f);
		}
		to = GameObject.Find ("TipPoint").transform.position;
		if(shoot){
			GameObject.Find ("Rope").transform.position = position + (to - from);
			Debug.Log("TIMEDELTA TIME " + Time.deltaTime);
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
			}
		}
		Debug.Log ("Kich co chieu ngang : " + render.bounds.size.x);
		Debug.Log ("Kich co chieu doc : " + render.bounds.size.y);
		Debug.Log ("Goc quay cua day: " + gameObject.transform.localRotation);
		Debug.Log ("Goc quay cua day2: " + gameObject.transform.eulerAngles);
		Debug.Log ("Goc quay cua day3: " + gameObject.transform.localEulerAngles);
		Debug.Log ("GDo rong man hinh: " + Screen.width);
		Debug.Log ("Vi tri cua day: " + GameObject.Find ("Rope").transform.localPosition);
		Debug.Log ("Vi tri cua day2: " + GameObject.Find ("Rope").transform.position);

	}
}
