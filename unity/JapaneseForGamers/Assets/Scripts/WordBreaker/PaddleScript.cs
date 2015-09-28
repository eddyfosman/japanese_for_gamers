using UnityEngine;
using System.Collections;

public class PaddleScript : MonoBehaviour {

	public Animator anim;
	public Transform swappingFaceTransform;
	PaddleManager paddleManagerScript;
	private int attackID = Animator.StringToHash("attack");
	private int attackTypeID = Animator.StringToHash("attackType");
	MapController mapControllerScript;
	public bool isFacingLeft = true;
	PaddleScript paddle1Script;
	float movingSpeed = 10f;
	bool didHit = false;
	BallScript ballScript;

	#region Facing
	public void FaceLeft(){
		Face (true);
	}
	public void FaceRight(){
		Face (false);
	}

	public void Face(bool faceLeft){
		if(faceLeft == isFacingLeft){
			return;
		}

		SwapFace ();
		isFacingLeft = faceLeft;
	}

	private void SwapFace(){
		swappingFaceTransform.localScale = new Vector3(-swappingFaceTransform.localScale.x, swappingFaceTransform.localScale.y, swappingFaceTransform.localScale.z);
	}
	#endregion

	// Use this for initialization
	void Start () {
		ballScript = GameObject.FindGameObjectWithTag ("Ball").GetComponent<BallScript>();
		mapControllerScript = GameObject.FindGameObjectWithTag("Map").GetComponent<MapController>();
		GetComponent<Renderer>().enabled = false;
		paddleManagerScript = GameObject.Find ("PaddleManager").GetComponent<PaddleManager>();
		if(gameObject.tag != "Paddle1"){
			paddle1Script = GameObject.FindGameObjectWithTag("Paddle1").GetComponent<PaddleScript>();
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(paddleManagerScript.paddleCount > 1){


			if(gameObject.tag == "Paddle3" && paddleManagerScript.paddleCount > 3){
				if(Mathf.Abs(transform.position.x - GameObject.FindGameObjectWithTag("Paddle4").transform.position.x) > 0f){
					GameObject.FindGameObjectWithTag("Paddle4").transform.position = new Vector3(gameObject.GetComponent<Collider>().bounds.size.x + transform.position.x, transform.position.y, transform.position.z);
				}
			}
			if(gameObject.tag == "Paddle1"){
				if(Mathf.Abs(transform.position.x - GameObject.FindGameObjectWithTag("Paddle2").transform.position.x) > 0f){
					GameObject.FindGameObjectWithTag("Paddle2").transform.position = new Vector3(gameObject.GetComponent<Collider>().bounds.size.x + transform.position.x, transform.position.y, transform.position.z);
				}
			}
			if(gameObject.tag == "Paddle2" && paddleManagerScript.paddleCount > 2){
				if(Mathf.Abs(transform.position.x - GameObject.FindGameObjectWithTag("Paddle3").transform.position.x) > 0f){
					GameObject.FindGameObjectWithTag("Paddle3").transform.position = new Vector3(gameObject.GetComponent<Collider>().bounds.size.x + transform.position.x, transform.position.y, transform.position.z);
				}
			}
		}
		if(transform.position.x < -7.28f){
//			transform.position = new Vector3(-7.28f, transform.position.y, transform.position.z);
//			return;
			paddleManagerScript.reachedLeft = true;
		}



		if(transform.position.x > 7.3f){
//			transform.position = new Vector3(7.33f, transform.position.y, transform.position.z);
//			return;
			paddleManagerScript.reachedRight = true;
		}

		if(Input.GetMouseButton(0)){
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//			Debug.Log("VI TRÍ CỦA CHUỘT LÀ : "  + mousePos);
			if(paddle1Script != null){
				if(paddle1Script.isFacingLeft){
					FaceLeft();
				}
				else{
					FaceRight();
				}
			}

			if(mousePos.y > -2f){
				ballScript.boostSpeed = true;
			}

			if(mousePos.y < -2f && mousePos.x > transform.position.x && !paddleManagerScript.reachedRight){
				if(Mathf.Abs(mousePos.x - transform.position.x) > 0.5f){
					transform.Translate(movingSpeed * Time.deltaTime, 0f, 0f);
					if(paddle1Script == null){
						FaceRight();
					}

					paddleManagerScript.reachedLeft = false;
				}

			}
			if(mousePos.y < -2f && mousePos.x < transform.position.x && !paddleManagerScript.reachedLeft){
				if(Mathf.Abs(mousePos.x - transform.position.x) > 0.5f){
					transform.Translate(-movingSpeed * Time.deltaTime, 0f, 0f);
					if(paddle1Script == null){
						FaceLeft();
					}

					paddleManagerScript.reachedRight = false;
				}

			}
		}
		else{
			ballScript.boostSpeed = false;
		}

		if(Input.GetAxis("Horizontal") > 0 && !paddleManagerScript.reachedRight){
			transform.Translate(movingSpeed * Time.deltaTime, 0f, 0f);
			FaceRight();
			paddleManagerScript.reachedLeft = false;
		}

		if(Input.GetAxis("Horizontal") < 0 && !paddleManagerScript.reachedLeft){
			transform.Translate(-movingSpeed * Time.deltaTime, 0f, 0f);
			FaceLeft();
			paddleManagerScript.reachedRight = false;
		}




	}



	void OnCollisionEnter(Collision col){
		foreach(ContactPoint contact in col.contacts){
			if(contact.thisCollider == GetComponent<Collider>()){
				//float english = contact.point.x - transform.position.x;
//				if((-0.5f >= english && -0.3f < english) || (0.5f <= english && 0.3f > english)){
//					contact.otherCollider.GetComponent<Rigidbody>().AddForce(300f * english, 200f, 0);
//				}
//				else{
//					contact.otherCollider.GetComponent<Rigidbody>().AddForce(300f * english, 0, 0);
//				}
				Vector3 dir;
				if(1 == mapControllerScript.currentStroke){
					dir = Vector3.zero;
				}
				else{
					dir = mapControllerScript.listPos[mapControllerScript.currentStroke - 1] - transform.position;
				}

				dir = dir.normalized;
				contact.otherCollider.GetComponent<Rigidbody>().AddForce(dir * 1000f);
//				Debug.Log("THU TU CUA NET VE LA : " + mapControllerScript.currentStroke);
				anim.SetFloat( attackTypeID, UnityEngine.Random.Range(0, 2) );
				anim.SetTrigger( attackID );
			}
		}
	}
}
