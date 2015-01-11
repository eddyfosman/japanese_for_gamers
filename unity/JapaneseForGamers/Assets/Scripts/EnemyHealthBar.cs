using UnityEngine;
using System.Collections;

public class EnemyHealthBar : MonoBehaviour {

	public RectTransform enemyHealthTranform;
	public int maxHealth;
	private int currentHealth;
	private int CurrentHealth{
		get{return currentHealth;}
		set{
			currentHealth = value;
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
		enemyHealthTranform.position = new Vector3 (currentXValue, cachedY);
	}

	IEnumerator CoolDownHealth(){
		onCD = true;
		yield return new WaitForSeconds (coolDown);
		onCD = false;
	}

	public void Damage(){
		if(currentHealth > 0){
			CurrentHealth -= 5;
		}

	}

	// Use this for initialization
	void Start () {
		cachedY = enemyHealthTranform.position.y;
		maxXValue = enemyHealthTranform.position.x;
		minXValue = enemyHealthTranform.position.x - enemyHealthTranform.rect.width;
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
//		if(!onCD && currentHealth > 0){
//			StartCoroutine(CoolDownHealth());
//			CurrentHealth -= 1;
//		}
		if(currentHealth == 0){
			currentHealth = maxHealth;
		}
	}
}
