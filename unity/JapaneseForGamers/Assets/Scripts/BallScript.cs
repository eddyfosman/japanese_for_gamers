using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

	public PhysicMaterial physBall;
	float targetSqrMag = 25.0f;

	// Use this for initialization
	void Start () {
		foreach(Transform transform in GameObject.Find("love").transform){
			transform.gameObject.AddComponent<MeshCollider>();
			transform.gameObject.GetComponent<MeshCollider>().material = physBall ;
		}


		rigidbody.AddForce (0f, 300f, 0f);
		Debug.Log (gameObject.rigidbody.velocity.sqrMagnitude);	
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (gameObject.rigidbody.velocity.sqrMagnitude);	
		Vector3 dir = rigidbody.velocity;
		
		float curSqrMag = gameObject.rigidbody.velocity.sqrMagnitude;
		
		if( curSqrMag < targetSqrMag )
			
		{

			
			gameObject.rigidbody.AddForce( dir * 1.2f );
			Debug.Log ("GIA TRI 0 la " + dir);
			Debug.Log ("GIA TRI 1 la " + (-dir * 1.2f));
//			Debug.Log (gameObject.rigidbody.velocity.sqrMagnitude);	
		}
		
//		if( curSqrMag > targetSqrMag )
//			
//		{
//			
//			gameObject.rigidbody.AddForce( -dir * 1.2f );
//			Debug.Log (gameObject.rigidbody.velocity.sqrMagnitude);
//				
//		}

//		Debug.Log (gameObject.rigidbody.velocity.sqrMagnitude);
//		Vector3 movement = new Vector3 (0f, 5f, 0 );
//		
//		movement *= Time.deltaTime;
//		
//		gameObject.transform.Translate (movement);
	}
}
