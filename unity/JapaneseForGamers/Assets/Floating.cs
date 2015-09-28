using UnityEngine;
using System.Collections;

public class Floating : MonoBehaviour {

	public float FloatStrenght;
	public float RandomRotationStrenght;
    //bool didFinish = true;
    //bool didReverseFinish = true;
    //bool didWait = false;
    bool isRotating = false;
	Vector3 rotation;
    //float degree = 100f;
    //float angle;
    //bool didRandom = false;
    float random;

//	IEnumerator Woobling(){
//		didFinish = false;
//		rotation = new Vector3 (Mathf.Lerp (-20f, 20f, Time.time), 360f, 360f);
//		didFinish = true;
//	}

//	IEnumerator ReverseWoobling(){
//		didReverseFinish = false;
//		rotation = new Vector3 (Mathf.Lerp (20f, -20f, Time.time), 360f, 360f);
//		didReverseFinish = true;
//	}

	IEnumerator Delay(){
		//didWait = true;
		transform.rotation = Quaternion.Euler (Mathf.Lerp(100f, 80f, Time.time), 180f ,360f);
		yield return new WaitForSeconds (1f);
		//didWait = false;

	}

	void DoThisAfter1Sec(){
		StartCoroutine (Delay ());
	}

	void DoThis(){
		isRotating = false;
	}

	void DoThat(){
		isRotating = true;
	}
	void Update () {
//		if(didReverseFinish = true){
//			StartCoroutine (Woobling());
//		}
//		if(didFinish = true){
//			StartCoroutine (ReverseWoobling());
//		}
//		transform.GetComponent<Rigidbody>().AddForce(Vector3.up *FloatStrenght);
//		transform.Rotate(RandomRotationStrenght,RandomRotationStrenght,RandomRotationStrenght);
//		rotation = new Vector3 ( Mathf.Lerp (350f, 360f, Time.time), 360f ,360f);
//		transform.rotation = Quaternion.Euler (rotation);
//		if(!didWait){
		if(!isRotating){
			random = Random.Range(70, 90);
//			angle = Mathf.LerpAngle(transform.rotation.x, random, Time.deltaTime);
//			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(random,180f, 360f), Time.deltaTime);
//			transform.rotation = Quaternion.Euler (Mathf.Lerp(90f, 100f, Time.time), 180f ,360f);
			Invoke("DoThat", 3f);
		}
		else if(isRotating){
//			transform.rotation = Quaternion.Euler (Mathf.Lerp(100f, 90f, Time.time), 180f ,360f);
			random = Random.Range(80, 120);
		//angle = Mathf.LerpAngle(transform.rotation.x, random, Time.deltaTime);
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(random,180f, 360f), Time.deltaTime);
			Invoke("DoThis", 3f);
		}
			
//			Invoke("DoThisAfter1Sec", 2f);
//		}

//		if(!isRotating){
//			if(didRandom){
//				random = Random.Range(70, 90);
//			}
//			while(Mathf.Abs(transform.rotation.x - random) > 3f){
//				angle = Mathf.LerpAngle(transform.rotation.x, random, Time.deltaTime);
//				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(random,180f, 360f), Time.deltaTime);
//			}
//		}
//		else if(isRotating){
//			if(didRandom){
//				random = Random.Range(90, 120);
//			}
//			while(Mathf.Abs(transform.rotation.x - random) > 3f){
//				angle = Mathf.LerpAngle(transform.rotation.x, random, Time.deltaTime);
//				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(random,180f, 360f), Time.deltaTime);
//			}
//		}


		Debug.Log (transform.rotation);
	}
}
