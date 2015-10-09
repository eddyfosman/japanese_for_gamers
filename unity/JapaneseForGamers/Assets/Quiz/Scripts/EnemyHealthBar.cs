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

	private float boostDamage = 0;
	public float BoostDamage
	{
		get { return boostDamage; }
		set { boostDamage = value; }
	}

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
		damage = damage + damage * boostDamage;
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

    public void Heal(int healValue)
    {
        CurrentHealth = ((currentHealth + (long)healValue) > maxHealth ? maxHealth : CurrentHealth + (long)healValue);

    }

    public void Damage(int damageValue)
	{
		CurrentHealth = ((currentHealth - (long)damageValue) < 0 ? 0 : CurrentHealth - (long)damageValue);
	}

	private void ApplyBoostDamage(int value)
	{
		BoostDamage = (float)value / (float)100;
	}

	private void ResetDamage()
	{
		BoostDamage = 0;
	}

	// Use this for initialization
	void Start () {
		questionManagerScript = questionManager.GetComponent<QuestionManager>();
		monster = questionManagerScript.GetMonsterData ();
		EffectManager.onAtkBuffStart += ApplyBoostDamage;
		EffectManager.onAtkBuffRemove += ResetDamage;
		player = questionManagerScript.GetPlayerData ();
		cachedY = enemyHealthTranform.position.y;
		maxXValue = enemyHealthTranform.position.x;
		minXValue = enemyHealthTranform.position.x - enemyHealthTranform.rect.width;
		currentHealth = maxHealth;
	}

	void OnDisable()
	{
		EffectManager.onAtkBuffStart -= ApplyBoostDamage;
		EffectManager.onAtkBuffRemove -= ResetDamage;
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