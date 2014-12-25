using UnityEngine;
using System.Collections;

public class PlayerCharacter1 : MonoBehaviour {
	private Animator _animator;
	private CharacterController _charController;
	
	private const float _deadzone = 0.2f;
	
	private float _horizontalInput, _verticalInput;
	
	private const float _speed = 5f;
	
	void Start () 
	{
		_animator = GetComponent<Animator>();
		_charController = GetComponent<CharacterController>();
	}
	
	void Update () 
	{
		if(Application.platform == RuntimePlatform.Android){
//			 _horizontalInput = Input.GetAxis("Mouse X");
//			 _verticalInput = Input.GetAxis("Mouse Y");
			
			if (Input.touchCount > 0)
			{
//				_horizontalInput = Input.touches[0].deltaPosition.x;
//				_horizontalInput = Input.touches[0].deltaPosition.y;

				_horizontalInput = Input.touches[0].position.x;
				_horizontalInput = Input.touches[0].position.y;

			}

//			if (Input.touchCount > 0 && 
//			    Input.GetTouch(0).phase == TouchPhase.Moved) {
//				
//				// Get movement of the finger since last frame
//				Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
//				
//				// Move object across XY plane
////				transform.Translate (-touchDeltaPosition.x * _speed, 
////				                     -touchDeltaPosition.y * _speed, 0f);
//				_horizontalInput = touchDeltaPosition.x ;
//				_verticalInput = touchDeltaPosition.y ;
//			}
		}
		else{
			_horizontalInput = Input.GetAxis("Horizontal");
			_verticalInput = Input.GetAxis("Vertical");
		}



		_animator.SetFloat("HorizontalInput", _horizontalInput);
		_animator.SetFloat("VerticalInput", _verticalInput);
		
		bool canMoveHorizontally = _horizontalInput > _deadzone || _horizontalInput < -_deadzone;
		bool canMoveVertically = _verticalInput > _deadzone || _verticalInput < -_deadzone;
		
		if (canMoveHorizontally || canMoveVertically)
		{
			_charController.Move(new Vector3(_horizontalInput * _speed * Time.deltaTime, _verticalInput * _speed * Time.deltaTime, 0.0f));
		}
	}
}
