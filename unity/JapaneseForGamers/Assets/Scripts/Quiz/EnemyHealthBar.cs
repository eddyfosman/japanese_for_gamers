using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {

	public RectTransform enemyHealthTranform;
	public long maxHealth;
	private long currentHealth;
	public long CurrentHealth{
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
	private MonsterBean monster;
	private PlayerData player;
	float ratio;
	float baseDmg;
	float damage;


	private int test;

	private void CalculateDamage(){
//		Debug.Log ("Monster Attack " + monster.Atk);
		ratio = player.Atk / monster.Def;

		if(ratio < 6/91){
			baseDmg = (player.Atk*449/480 - monster.Def/112) / 8;
		}
		else if(ratio < 30/119){
			baseDmg = player.Atk*113/480 - monster.Def/112;
		}
		else if(ratio < 10/21){
			baseDmg = player.Atk*33/80 - monster.Def*3/56;
		}
		else if(ratio < 6/7){
			baseDmg = player.Atk*3/4 - monster.Def*3/14;
		}
		else{
			baseDmg = player.Atk - monster.Def*3/7;
		}

		damage = baseDmg *  Random.Range(0.9f, 1.1f) + (player.Atk / 400) * Random.Range (1, 10) + Random.Range (-2, 2);
		//		if ratio < 6/91 (roughly .066) : baseDmg = (eneatk*449/480 - mydef/112) / 8
//			
//			else if R < 30/119 (roughly .252) : baseDmg = eneatk*113/480 - mydef/112
//				
//				else if R < 10/21 (roughly .476) : baseDmg = eneatk*33/80 - mydef*3/56
//				
//				else if R < 6/7 (roughly .857) : baseDmg = eneatk*3/4 - mydef*3/14
//				
//				else: baseDmg = eneatk - mydef*3/7
//				
//				damage = baseDmg * randomInRange(.9,1.1) + (eneatk/400) * randomInRange(1,10) + randomInRange(-2,2)
//				
//				if(dmg <= 0)
//					
//				___ if(myMaxHP < 100){ //How is that even possible???
//					
//					______ damage = Math.random() * myMaxHP * 0.09 + 1;
//					
//					___ }else{
//			
//			______ damage = randomInRange(1,11)
//				
//			___ }
	}


	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax){
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}

	private void HandleHealth(){
		float currentXValue = MapValues (currentHealth, 0, maxHealth, minXValue, maxXValue);
//		enemyHealthTranform.position = new Vector3 (currentXValue, cachedY);
		enemyHealthTranform.GetComponent<RectTransform> ().offsetMax = new Vector2 (currentXValue, GetComponent<RectTransform> ().offsetMax.y);
		enemyHealthTranform.GetComponent<RectTransform> ().offsetMin = new Vector2 (currentXValue, GetComponent<RectTransform> ().offsetMin.y);
	}

	IEnumerator CoolDownHealth(){
		onCD = true;
		yield return new WaitForSeconds (coolDown);
		onCD = false;
	}

	public void Damage(){
		if(currentHealth > 0){
			CalculateDamage();
			CurrentHealth -= (long)damage;
		}

	}


	// Use this for initialization
	void Start () {
		questionManagerScript = questionManager.GetComponent<QuestionManager>();
		monster = questionManagerScript.GetMonsterData ();

		player = questionManagerScript.GetPlayerData ();
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
			Debug.Log("Giet mot con quai!!!");
			questionManagerScript.SetMonster();
			CurrentHealth = maxHealth;
		}
	}
}
