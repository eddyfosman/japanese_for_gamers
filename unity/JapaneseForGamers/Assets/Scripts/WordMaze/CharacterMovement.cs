using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	public Vector2 speed = new Vector2 (1, 0);
	public float xMargin = 0.3f;
	public float yMargin = 0.3f;

	private Animator anim;

	// the vector that we want to measure an angle from
	private Vector3 referenceForward;	

	private Vector3 worldPoint;
	private bool clicked = false;
	private bool collided = false;
	private bool firstCal = true;
	private Rigidbody rigid;
	private MovementEnergy movementEnergyScript;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody>();
		movementEnergyScript = gameObject.GetComponent<MovementEnergy> ();
	}

	bool CheckXMargin(){
		return Mathf.Abs (worldPoint.x - gameObject.transform.position.x) < xMargin;
	}

	bool CheckYMargin(){
		return Mathf.Abs (worldPoint.y - gameObject.transform.position.y) < yMargin;
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
				firstCal = true;
				rigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX;
				Debug.Log("CLICKED");
			}
		}

		if(Application.platform == RuntimePlatform.Android){
			if(Input.touchCount > 0){
				worldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				clicked = true;
				firstCal = true;
				rigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX;
			}
		}



		Vector3 movement;
		if(CheckXMargin() && CheckYMargin()){
			movementEnergyScript.isMoving = false;
		}
		else{
			movementEnergyScript.isMoving = true;
		}
		Debug.DrawRay(transform.position, new Vector3(worldPoint.x, worldPoint.y, 0) - transform.position, Color.red, 5.0f);

		if(firstCal){
			referenceForward = new Vector3(transform.position.x, 0f , 0f);
			float angle = Vector3.Angle(new Vector3(worldPoint.x, worldPoint.y, 0) - transform.position, referenceForward);
			Vector3 referenceRight = Vector3.Cross(Vector3.up, new Vector3(worldPoint.x, worldPoint.y, 0) - transform.position);
			float sign = Mathf.Sign(Vector3.Dot(Vector3.Cross(new Vector3(worldPoint.x, worldPoint.y, 0) - transform.position, referenceForward), referenceRight));
			
			float finalAngle = sign * angle;
			Debug.DrawRay(transform.position, Vector3.Cross(Vector3.down, new Vector3(worldPoint.x, worldPoint.y, 0) - transform.position), Color.yellow, 5.0f);
			Debug.Log (new Vector3(0f, transform.position.y, 0f));
			Debug.Log ("GOC CUA 2 VECTOR " + angle);
			Debug.Log ("GOC CUA 2 VECTOR THUC" + finalAngle);
			firstCal = false;
		}

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

//		worldPoint = transform.position;
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
