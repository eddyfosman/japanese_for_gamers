using UnityEngine;
using System.Collections;


public class BallScript : MonoBehaviour {
	int superBallTime = 0;
	bool isFirstLaunch = false;
	Vector3 mousePos;
	bool didLaunch = false;
	public GameObject paddle1;
	public PhysicMaterial physBall;
	float targetSqrMag = 25.0f;
	float ballSpeed = 200f;


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

		paddle1 = GameObject.FindGameObjectWithTag ("Paddle1");
//		GetComponent<Rigidbody>().AddForce (0f, 300f, 0f);

//		Debug.Log (gameObject.GetComponent<Rigidbody>().velocity.sqrMagnitude);	
	}
	
	// Update is called once per frame
	void Update () {

		iTween.PutOnPath(gameObject, iTweenPath.GetPath("Gamba1"), .5);

		if(!isFirstLaunch){
			if(Input.GetMouseButtonDown(0)){
				mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				if(CheckXOffset() && CheckYOffset()){
					isFirstLaunch = true;
					didLaunch = true;
					GetComponent<Rigidbody>().AddForce (0f, 300f, 0f);
				}
			}
		}

		if(!didLaunch){
			gameObject.transform.position = paddle1.transform.position;
		}

		GetComponent<Rigidbody>().velocity = ballSpeed * Time.deltaTime * (GetComponent<Rigidbody>().velocity.normalized);
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

	void OnTriggerEnter(Collider other){
		if(other.tag == "Brick"){
			other.gameObject.GetComponent<SelfDestroy>().hitPoints = 0;
			other.gameObject.GetComponent<SelfDestroy>().DoBeforeDestroy();

		}
	}
}
