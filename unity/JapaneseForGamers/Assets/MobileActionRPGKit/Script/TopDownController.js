#pragma strict
private var motor : CharacterMotor;
private var moveDir : float = 0.0;
var joyStick : GameObject;
var walkingSound : AudioClip;

// Use this for initialization
function Awake () {
	motor = GetComponent(CharacterMotor);
	if(!joyStick){
		joyStick = GameObject.FindWithTag("JoyStick");
	}
}

// Update is called once per frame
function Update () {
	var stat : Status = GetComponent(Status);
	if(stat.freeze || stat.flinch){
		motor.inputMoveDirection = Vector3(0,0,0);
		return;
	}
		if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")){
			var moveHorizontal : float = Input.GetAxis("Horizontal");
			var moveVertical : float = Input.GetAxis("Vertical");
		}else if(joyStick){
			moveHorizontal = joyStick.GetComponent(MobileJoyStick).position.x;
			moveVertical = joyStick.GetComponent(MobileJoyStick).position.y;
		}
		
		var cameraTransform = Camera.main.transform;
		var forward = cameraTransform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;
		var right = Vector3(forward.z, 0, -forward.x);
		var targetDirection = moveHorizontal * right + moveVertical * forward;
		
		//----------------------------------
		if(moveHorizontal != 0 || moveVertical != 0){
	    	//transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(moveHorizontal , moveVertical) * Mathf.Rad2Deg, transform.eulerAngles.z);
	    	transform.rotation = Quaternion.LookRotation(targetDirection.normalized);

	    }
		//-----------------------------------------------------------------------------
		if(moveVertical != 0 && walkingSound && !GetComponent.<AudioSource>().isPlaying|| moveHorizontal != 0 && walkingSound && !GetComponent.<AudioSource>().isPlaying){
			GetComponent.<AudioSource>().clip = walkingSound;
			GetComponent.<AudioSource>().Play();
		}
		
		motor.inputMoveDirection = targetDirection.normalized;
		motor.inputJump = Input.GetButton("Jump");
}

// Require a character controller to be attached to the same game object
@script RequireComponent (CharacterMotor)