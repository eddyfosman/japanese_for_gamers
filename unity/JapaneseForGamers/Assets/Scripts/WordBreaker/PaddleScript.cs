using UnityEngine;
using System.Collections;

public class PaddleScript : MonoBehaviour {

	public Animator anim;
	public Transform swappingFaceTransform;

	private int attackID = Animator.StringToHash("attack");
	private int attackTypeID = Animator.StringToHash("attackType");
	private bool isFacingLeft = true;

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
		renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Horizontal") < 0){
			transform.Translate(-10f * Time.deltaTime, 0f, 0f);
		}

		if(Input.GetAxis("Horizontal") > 0){
			transform.Translate(10f * Time.deltaTime, 0f, 0f);
		}

		if(transform.position.x < -0.2f){
			FaceLeft();
		}
		else if(transform.position.x > -0.2f){
			FaceRight();
		}
	}

	void OnCollisionEnter(Collision col){
		foreach(ContactPoint contact in col.contacts){
			if(contact.thisCollider == collider){
				float english = contact.point.x - transform.position.x;
				contact.otherCollider.rigidbody.AddForce(10f * english, 0, 0);
				anim.SetFloat( attackTypeID, UnityEngine.Random.Range(0, 2) );
				anim.SetTrigger( attackID );
			}
		}
	}
}
