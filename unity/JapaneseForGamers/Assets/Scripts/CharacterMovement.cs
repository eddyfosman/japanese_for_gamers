using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	private Animator anim;
	public Vector2 speed = new Vector2 (1, 0);
	Vector3 worldPoint;
	bool clicked = false;
	bool collided = false;
	Rigidbody rigid;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");

		if(inputX < 0){
			anim.SetFloat ("SpeedX", -1f);
		}
		else if(inputX > 0){
			anim.SetFloat ("SpeedX", 1f);
		}

		if(inputX < 0){
			anim.SetFloat ("SpeedY", -1f);
		}
		else if(inputX > 0){
			anim.SetFloat ("SpeedY", 1f);
		}


		if(Application.platform == RuntimePlatform.WindowsEditor){
			if(Input.GetMouseButtonDown(0)){
				worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				clicked = true;
				rigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX;
				Debug.Log("CLICKED");
			}
		}

		if(Application.platform == RuntimePlatform.Android){
			if(Input.touchCount > 0){
				worldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				clicked = true;
				rigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX;
			}
		}



		Vector3 movement;
//		if(!clicked){
//			movement = new Vector3 (speed.x * inputX, speed.y * inputY, 0 );
//			movement *= Time.deltaTime;
//		}
//		else{
			
//		}
		Debug.DrawRay(transform.position, worldPoint - transform.position, Color.red, 1.0f);
//		Debug.DrawRay(transform.position, movement - transform.position, Color.red, 1.0f);
//		Debug.Log ("VI TRI GAMEOBJECT"+transform.position);
//		Debug.Log ("VI TRI CHUOT"+worldPoint);
//		Debug.Log ("HIEU 2 VECTOR"+movement);
		rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
//		rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
		movement = (worldPoint - transform.position) * Time.deltaTime * 50;
//		transform.position = Vector3.Lerp (transform.position, new Vector3(worldPoint.x,worldPoint.y,0),Time.deltaTime);
//		rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
		if(!collided){
//			rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;

		}
		if(collided){
//			rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
//			movement = new Vector3(0f,0f,0f);
			collided = false;
			Debug.Log("FALSE");
		}
		rigid.velocity = new Vector3 (movement.x, movement.y,0f);
//		transform.Translate (new Vector3(movement.x, movement.y,0f));
		clicked = false;

	}

	void OnCollisionEnter(Collision collision){
		if(collision.collider.gameObject.name != null){
			collided = true;
			Debug.Log("TRUE");
		}

	}

	void OnCollisionExit(Collision collision) {
//		rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
		Debug.Log ("Khong VA CHAM NUA");
	}

	void FixedUpdate(){

		float lastInputX = Input.GetAxis ("Horizontal");
		float lastInputY = Input.GetAxis ("Vertical");

		if(lastInputX != 0 || lastInputY != 0){
			anim.SetBool("walking", true);
			if(lastInputX > 0){
				anim.SetFloat("LastMoveX", 1f);
			}
			else if(lastInputX < 0){
				anim.SetFloat("LastMoveX", -1f);
			}
			else{
				anim.SetFloat("LastMoveX", 0f);
			}
		}
		else{
			anim.SetBool("walking", false);
			if(lastInputY > 0){
				anim.SetFloat("LastMoveY", 1f);
			}
			else if(lastInputY < 0){
				anim.SetFloat("LastMoveY", -1f);
			}
			else{
				anim.SetFloat("LastMoveY", 0f);
			}
		}

	}
}
