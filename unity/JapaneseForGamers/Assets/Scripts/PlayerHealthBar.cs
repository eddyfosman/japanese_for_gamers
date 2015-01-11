using UnityEngine;
using System.Collections;

public class PlayerHealthBar : MonoBehaviour {


	Vector3 oldPos;
	public RectTransform playerHealthTransform;
	public int maxHealth;
	private float tempHealth;
	private int currentHealth;
	private int CurrentHealth{
		get{return currentHealth;}
		set{
			tempHealth = currentHealth;
			Debug.Log("TempHealth " + tempHealth);
			oldPos = playerHealthTransform.position;
			currentHealth = value;
			Debug.Log("CurrentpHealth " + currentHealth);
			HandleHealth();
		}
	}
	private float cachedY;
	private float minXValue;
	private float maxXValue;
	private bool onCD;
	public float coolDown;


	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax){
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}

	private void HandleHealth(){
		float currentXValue = MapValues (currentHealth, 0, maxHealth, minXValue, maxXValue);
		StartCoroutine (MoveHealthBar(new Vector3(currentXValue, cachedY)));
	}

	IEnumerator MoveHealthBar(Vector3 newPos){
		while(tempHealth > currentHealth || (tempHealth < currentHealth && currentHealth == maxHealth)){

			float tempXValue = MapValues(tempHealth, 0 , maxHealth,minXValue, maxXValue);
			tempHealth -= 1;
			playerHealthTransform.position =  new Vector3(tempXValue, cachedY);
			yield return null;
		}
	}

	IEnumerator CoolDownHealth(){
//		onCD = true;
		yield return new WaitForSeconds (coolDown);
//		onCD = false;
	}

	public void Damage(){
		if(currentHealth >= 0){
			CurrentHealth -= 20;
		}

	}

	// Use this for initialization
	void Start () {
		cachedY = playerHealthTransform.position.y;
		maxXValue = playerHealthTransform.position.x;
		minXValue = playerHealthTransform.position.x - playerHealthTransform.rect.width;
		currentHealth = maxHealth;

	}
	
	// Update is called once per frame
	void Update () {
//		if(!onCD && currentHealth > 0){
//			StartCoroutine(CoolDownHealth());
//			CurrentHealth -= 1;
//		}
		if(CurrentHealth == 0){
			CurrentHealth = maxHealth;
		}
	}
}
