using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {


	Vector3 oldPos;
	public GameObject questionManager;
	private QuestionManager questionManagerScript;

	public RectTransform playerHealthTransform;
	public long maxHealth;
	private long tempHealth;
	private long currentHealth;
	private long CurrentHealth{
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

	public Text healthText;

	private MonsterBean monster;
	private PlayerData player;

	float ratio;
	float baseDmg;
	float damage;

	private void CalculateDamage(){
		Debug.Log ("Monster Attack " + monster.Atk);
		ratio = monster.Atk / player.Def;
		
		if(ratio < 6/91){
			baseDmg = (monster.Atk*449/480 - player.Def/112) / 8;
		}
		else if(ratio < 30/119){
			baseDmg = monster.Atk*113/480 - player.Def/112;
		}
		else if(ratio < 10/21){
			baseDmg = monster.Atk*33/80 - player.Def*3/56;
		}
		else if(ratio < 6/7){
			baseDmg = monster.Atk*3/4 - player.Def*3/14;
		}
		else{
			baseDmg = monster.Atk - player.Def*3/7;
		}
		
		damage = baseDmg *  Random.Range(0.9f, 1.1f) + (monster.Atk / 400) * Random.Range (1, 10) + Random.Range (-2, 2);
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
		StartCoroutine (MoveHealthBar(new Vector3(currentXValue, cachedY)));
	}

	IEnumerator MoveHealthBar(Vector3 newPos){
		while(tempHealth > currentHealth || (tempHealth < currentHealth && currentHealth == maxHealth)){

			float tempXValue = MapValues(tempHealth, 0 , maxHealth,minXValue, maxXValue);
			long temp = (tempHealth - currentHealth)/100;
			tempHealth -= temp;
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
			CalculateDamage();
			CurrentHealth -= (long)damage;
		}

	}

	// Use this for initialization
	void Start () {
		questionManagerScript = questionManager.GetComponent<QuestionManager>();
		player = questionManagerScript.GetPlayerData ();
		monster = questionManagerScript.GetMonsterData ();
		Debug.Log ("Monster Attack " + monster.Atk);
		
		cachedY = playerHealthTransform.position.y;
		maxXValue = playerHealthTransform.position.x;
		minXValue = playerHealthTransform.position.x - playerHealthTransform.rect.width;
		currentHealth = maxHealth;

	}
	
	// Update is called once per frame
	void Update () {
		healthText.text = currentHealth.ToString ();
//		if(!onCD && currentHealth > 0){
//			StartCoroutine(CoolDownHealth());
//			CurrentHealth -= 1;
//		}
		if(CurrentHealth <= 0){
			CurrentHealth = maxHealth;
		}
	}
}
