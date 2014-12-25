using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour {
	private Animator _animator;
	private Rigidbody2D _charController;
	private Vector3 wp;
	Vector2 touchPos;
	private const float _deadzone = 0.2f;
	private bool moving = false;
	
	private float _horizontalInput, _verticalInput;
	
	public float _speed = 5f;

	private bool flag = false;
	private Vector3 endPoint;
	private float duration = 50.0f;
	private float yAxis;

	public static float Round(float value, int digits)
	{
		float mult = Mathf.Pow(10.0f, (float)digits);
		return Mathf.Round(value * mult) / mult;
	}

	void Start () 
	{
		_animator = GetComponent<Animator>();
		_charController = GetComponent<Rigidbody2D>();
		Physics2D.gravity = Vector2.zero;
		yAxis = gameObject.transform.position.y;
	


	}
	
	void Update () 
	{
	


			if (Application.platform == RuntimePlatform.Android)
			{
				if (Input.touchCount > 0)
				{
					if (Input.GetTouch(0).phase == TouchPhase.Began)
					{ 
						wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position); 
						flag = true;
					} 
				}
			}
			
			/* Check if the user is touching the button on the Editor, change OSXEditor value if you are on Windows */
			
			if (Application.platform == RuntimePlatform.WindowsEditor)
			{

				if (Input.GetMouseButtonDown(0))
				{
					wp = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
					flag = true;
				}
			}
				touchPos = new Vector2(wp.x, wp.y);
				
				endPoint = touchPos;
		Debug.Log (touchPos.y);
		if (!flag) {


						_verticalInput = 0f;
						_horizontalInput = 0f;


				} else {

						if (touchPos.y > gameObject.transform.position.y) {
								if(Mathf.Abs(touchPos.x - gameObject.transform.position.x) > Mathf.Abs(touchPos.y - gameObject.transform.position.y)){
									if(touchPos.x > gameObject.transform.position.x){
										_horizontalInput = 1f;
									}
									else if(touchPos.x < gameObject.transform.position.x){
										_horizontalInput = -1f;
									}
								}
								else if(Mathf.Abs(touchPos.x - gameObject.transform.position.x) < Mathf.Abs(touchPos.y - gameObject.transform.position.y)){
									_verticalInput = 1f;
								}
				
//						} else if (touchPos.y < gameObject.transform.position.y) {
//								_verticalInput = -1f;
//								Debug.Log ("2");
						} else if (touchPos.y < gameObject.transform.position.y) {

								if(Mathf.Abs(touchPos.x - gameObject.transform.position.x) > Mathf.Abs(touchPos.y - gameObject.transform.position.y)){
									if(touchPos.x > gameObject.transform.position.x){
										_horizontalInput = 1f;
									}
									else if(touchPos.x < gameObject.transform.position.x){
										_horizontalInput = -1f;
									}
								}
								else if(Mathf.Abs(touchPos.x - gameObject.transform.position.x) < Mathf.Abs(touchPos.y - gameObject.transform.position.y)){
									_verticalInput = -1f;
								}
						}
				}

//			_horizontalInput = Input.GetAxis("Horizontal");
//			_verticalInput = Input.GetAxis("Vertical");




		_animator.SetFloat("HorizontalInput", _horizontalInput);
		_animator.SetFloat("VerticalInput", _verticalInput);
		
		bool canMoveHorizontally = _horizontalInput > _deadzone || _horizontalInput < -_deadzone;
		bool canMoveVertically = _verticalInput > _deadzone || _verticalInput < -_deadzone;
//		bool canMoveCheoTrenTrai = _horizontalInput < -_deadzone*3 && _verticalInput > _deadzone*1.5 ;
//		bool canMoveCheoTrenPhai = _horizontalInput > _deadzone*3 && _verticalInput > _deadzone*1.5;
//		bool canMoveCheoDuoiTrai = _horizontalInput < -_deadzone*1.5 && _verticalInput < -_deadzone*3;
//		bool canMoveCheoDuoiPhai = _horizontalInput > _deadzone*1.5 && _verticalInput < -_deadzone*3;

		//check if the flag for movement is true and the current gameobject position is not same as the clicked / tapped position
//		if(flag && !Mathf.Approximately((int)gameObject.transform.position.y, (int)endPoint.y)){ //&& !(V3Equal(transform.position, endPoint))){
		if(flag && !(Round(gameObject.transform.position.y,2) == Round(endPoint.y,2)) && !(Round(gameObject.transform.position.x,2) == Round(endPoint.x,2))){
			//move the gameobject to the desired position


//			if ((canMoveHorizontally || canMoveVertically) && (!canMoveCheoTrenTrai && !canMoveCheoTrenPhai && !canMoveCheoDuoiTrai && !canMoveCheoDuoiPhai))
			if (canMoveHorizontally || canMoveVertically )
			{
				
				_charController.AddForce(new Vector2(_horizontalInput * _speed * Time.deltaTime, _verticalInput * _speed * Time.deltaTime), ForceMode2D.Impulse);
			}

		}
		//set the movement indicator flag to false if the endPoint and current gameobject position are equal
//		else if(flag && Mathf.Approximately((int)gameObject.transform.position.y, (int)endPoint.y)) {
		else if(flag && (Round(gameObject.transform.position.y,2) == Round(endPoint.y,2)) && !(Round(gameObject.transform.position.x,2) == Round(endPoint.x,2))) {
			flag = false;
			Debug.Log("I am here");
			moving = false;
			touchPos.y = gameObject.transform.position.y; 

		}
		

	}
}
