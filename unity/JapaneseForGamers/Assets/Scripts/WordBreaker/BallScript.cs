using UnityEngine;
using System.Collections;


public class BallScript : MonoBehaviour {
	//int superBallTime = 0;
	bool isFirstLaunch = false;
	Vector3 mousePos;
	bool didLaunch = false;
	public GameObject paddle1;
	public PhysicMaterial physBall;
	float targetSqrMag = 25.0f;
	float ballSpeed = 1000f;
	int a;
	PaddleScript paddleScript;
	Rigidbody rigid;
	public bool boostSpeed = false;


	bool CheckXOffset(){
		return Mathf.Abs (mousePos.x - paddle1.transform.position.x) < 0.2f;
	}

	bool CheckYOffset(){
		return Mathf.Abs (mousePos.y - paddle1.transform.position.y) < 0.2f;
	}
	// Use this for initialization
	void Start () {
		//doan code nay dung cho chu 3d
//		foreach(Transform transform in GameObject.Find("love").transform){
//			transform.gameObject.AddComponent<MeshCollider>();
//			transform.gameObject.AddComponent<SelfDestroy>();
//			transform.gameObject.GetComponent<MeshCollider>().material = physBall ;
//		}
		rigid = GetComponent<Rigidbody>();
		paddle1 = GameObject.FindGameObjectWithTag ("Paddle1");

//		GetComponent<Rigidbody>().AddForce (0f, 300f, 0f);

//		Debug.Log (gameObject.GetComponent<Rigidbody>().velocity.sqrMagnitude);	
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log ("Gia tri cua a la: " + a );
		if(0 == a){
//			GetComponent<Rigidbody>().AddForce (4f, 0, 0f);
		}else{
//			GetComponent<Rigidbody>().AddForce (-4f, 0, 0f);
		}


//		GetComponent<Rigidbody>().AddForce( 10f*Vector3.Cross(GetComponent<Rigidbody>().velocity,GetComponent<Rigidbody>().angularVelocity), ForceMode.Force);
//		iTween.PutOnPath(gameObject, iTweenPath.GetPath("Gamba1"), .5);

		if(!isFirstLaunch){
			if(Input.GetMouseButtonDown(0)){
				mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				if(CheckXOffset() && CheckYOffset()){
					isFirstLaunch = true;
					didLaunch = true;
					rigid.AddForce (0f, 300f, 0f);
				}
			}
		}

		if(!didLaunch){
			gameObject.transform.position = paddle1.transform.position;
		}


		//		Debug.Log (gameObject.rigidbody.velocity.sqrMagnitude);	
//		Vector3 dir = rigidbody.velocity;
//		
//		float curSqrMag = gameObject.rigidbody.velocity.sqrMagnitude;
//		
//		if( curSqrMag < targetSqrMag )
//			
//		{
//
//			
//			gameObject.rigidbody.AddForce( dir * Time.deltaTime);
//			Debug.Log ("GIA TRI 0 la " + dir);
//			Debug.Log ("GIA TRI 1 la " + (-dir * 1.2f));
//			Debug.Log (rigidbody.velocity);	
//		}
//
//		if( curSqrMag > 30f )
//		{
//			
//			
//			gameObject.rigidbody.AddForce( -dir );
//
//		}
		
	}

	void FixedUpdate(){
		if(boostSpeed){
			ballSpeed = 1500f;
		}
		else{
			ballSpeed = 1000f;
		}

		rigid.velocity = ballSpeed * Time.deltaTime * (rigid.velocity.normalized);
//		Debug.Log ("GOC HIEN TAI CUA BONG LA: " + rigid.velocity);
		if(isFirstLaunch && rigid.velocity.y < 5f && rigid.velocity.y > -5f){
			if(rigid.velocity.y < 0){
				rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y - 4f, rigid.velocity.z);
			}
			else{
				rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y + 4f, rigid.velocity.z);
			}

		}
		if(isFirstLaunch && rigid.velocity.x < 1f && rigid.velocity.x > -1f){
			if(rigid.velocity.y < 0){
				rigid.velocity = new Vector3(rigid.velocity.x - 1f, rigid.velocity.y, rigid.velocity.z);
			}
			else{
				rigid.velocity = new Vector3(rigid.velocity.x + 1f, rigid.velocity.y, rigid.velocity.z);
			}
			
		}
	}

	void OnCollisionEnter(Collision other){
//		if(other.gameObject.name == "TopWall" || other.gameObject.name == "RightWall" || other.gameObject.name == "LeftWall"){
//			Debug.Log ("CO CHAY VAO DAY KHONG");
//			
//			a = Random.Range (0,2);
//		}
	}


	void OnTriggerEnter(Collider other){

		if(other.tag == "Brick"){
			other.gameObject.GetComponent<SelfDestroy>().hitPoints = 0;
			other.gameObject.GetComponent<SelfDestroy>().DoBeforeDestroy();

		}
	}
}
