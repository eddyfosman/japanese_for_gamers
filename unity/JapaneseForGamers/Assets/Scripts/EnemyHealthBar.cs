using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {

	public RectTransform enemyHealthTranform;
	public int maxHealth;
	private int currentHealth;
	public int CurrentHealth{
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
	public Text healthText;
	public GameObject questionManager ;
	private QuestionManager questionManagerScript;




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
			CurrentHealth -= 30;
		}

	}

	// Use this for initialization
	void Start () {
		questionManagerScript = questionManager.GetComponent<QuestionManager>();
		cachedY = enemyHealthTranform.position.y;
		maxXValue = enemyHealthTranform.position.x;
		minXValue = enemyHealthTranform.position.x - enemyHealthTranform.rect.width;
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {

		healthText.text = currentHealth.ToString();
		if(currentHealth <= 0){
			questionManagerScript.GainExp();
			questionManagerScript.SetMonster();
			CurrentHealth = maxHealth;
		}
	}
}
