using UnityEngine;
using System.Collections;

// Require a character controller to be attached to the same game object
[RequireComponent (typeof(CharacterMotorC))]

public class TopDownControllerC : MonoBehaviour {

	private CharacterMotorC motor;
	//private float moveDir = 0.0f;
	public GameObject joyStick;
	public AudioClip walkingSound;
	
	// Use this for initialization
	void Awake() {
		motor = GetComponent<CharacterMotorC>();
		if(!joyStick){
			joyStick = GameObject.FindWithTag("JoyStick");
		}
	}
	
	// Update is called once per frame
	void Update() {
		StatusC stat = GetComponent<StatusC>();
		float moveHorizontal = 0.0f;
		float moveVertical = 0.0f;

		if(stat.freeze || stat.flinch){
			motor.inputMoveDirection = new Vector3(0,0,0);
			return;
		}
		if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")){
			moveHorizontal = Input.GetAxis("Horizontal");
			moveVertical = Input.GetAxis("Vertical");
		}else if(joyStick){
			moveHorizontal = joyStick.GetComponent<MobileJoyStickC>().position.x;
			moveVertical = joyStick.GetComponent<MobileJoyStickC>().position.y;
		}
		
		// Get the input vector from kayboard or analog stick
		Vector3 directionVector = new Vector3(moveHorizontal, 0, moveVertical);
		//Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		
		if (directionVector != Vector3.zero) {
			// Get the length of the directon vector and then normalize it
			// Dividing by the length is cheaper than normalizing when we already have the length anyway
			float directionLength = directionVector.magnitude;
			directionVector = directionVector / directionLength;
			
			// Make sure the length is no bigger than 1
			directionLength = Mathf.Min(1, directionLength);
			
			// Make the input vector more sensitive towards the extremes and less sensitive in the middle
			// This makes it easier to control slow speeds when using analog sticks
			directionLength = directionLength * directionLength;
			
			// Multiply the normalized direction vector by the modified length
			directionVector = directionVector * directionLength;
		}
		
		if(moveHorizontal != 0 || moveVertical != 0)
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(moveHorizontal , moveVertical) * Mathf.Rad2Deg, transform.eulerAngles.z);
		//-----------------------------------------------------------------------------
		if(moveVertical != 0 && walkingSound && !GetComponent<AudioSource>().isPlaying|| moveHorizontal != 0 && walkingSound && !GetComponent<AudioSource>().isPlaying){
			GetComponent<AudioSource>().clip = walkingSound;
			GetComponent<AudioSource>().Play();
		}
		
		motor.inputMoveDirection = new Vector3(moveHorizontal , 0, moveVertical);
		motor.inputJump = Input.GetButton("Jump");
	}	
}