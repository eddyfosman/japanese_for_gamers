﻿#pragma strict
    var target : Transform;
    var targetHeight : float = 1.2;
    var distance : float = 4.0;
    var maxDistance : float = 6;
    var minDistance : float = 1.0;
    var xSpeed : float = 250.0;
    var ySpeed : float = 120.0;
    var yMinLimit : float = -10;
    var yMaxLimit : float = 70;
    var zoomRate : float = 80;
    var rotationDampening : float = 3.0;
    private var x : float = 20.0;
    private var y : float = 32.0;
    var lockOn : boolean = false;
    var freeze : boolean = false;
    
    //var attackPoint : Transform;
    var targetBody : Transform;
         
    function Start () {
     if(!target){
    	target = GameObject.FindWithTag ("Player").transform;
    	}
        var angles : Vector3 = transform.eulerAngles;
        x = angles.y;
        y = 32.0;
     
          if (GetComponent.<Rigidbody>())
          	GetComponent.<Rigidbody>().freezeRotation = true;
    }
     
function LateUpdate () {
       if(!target)
          return;

        if(!targetBody){
        	targetBody = target;
        }
    if (Time.timeScale == 0.0){
		return;
	}
       if(Input.GetButton("Fire2")){
         x += Input.GetAxis("Mouse X") * xSpeed * 0.02;
      	 y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02;
      }
        
       distance -= (Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
       distance = Mathf.Clamp(distance, minDistance, maxDistance);
       
       y = ClampAngle(y, yMinLimit, yMaxLimit);
       
      // Rotate Camera
       var rotation:Quaternion = Quaternion.Euler(y, x, 0);
       transform.rotation = rotation;
       
       var stat : Status = targetBody.GetComponent(Status);
       
       //Camera Position
       var position : Vector3 = target.position - (rotation * Vector3.forward * distance + Vector3(0 ,-targetHeight,0));
       transform.position = position;
       
        var hit : RaycastHit;
        var trueTargetPosition : Vector3 = target.transform.position - Vector3(0,-targetHeight,0);
        if (Physics.Linecast (trueTargetPosition, transform.position, hit)) {  
        	if(hit.transform.tag == "Wall"){
            	var tempDistance : float = Vector3.Distance (trueTargetPosition, hit.point) - 0.28;

            	position = target.position - (rotation * Vector3.forward * tempDistance + Vector3(0,-targetHeight,0));
            	transform.position = position;
            }
        }
}
     
static function ClampAngle (angle : float, min : float, max : float) {
       if (angle < -360)
          angle += 360;
       if (angle > 360)
          angle -= 360;
       return Mathf.Clamp (angle, min, max);
       
}