using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectManager : MonoBehaviour {

	public GameObject questionPanel;
	private QuestionManager questionManagerScript;

	public delegate void OnPoisonEnd (string turn);
	public static event OnPoisonEnd onPoisonEnd;

	public delegate void OnRegenEnd (string turn);
	public static event OnRegenEnd onRegenEnd;

	public delegate void OnStunEnd (string turn);
	public static event OnStunEnd onStunEnd;

	public delegate void OnAtkBuffEnd (string turn);
	public static event OnAtkBuffEnd onAtkBuffEnd;

	public delegate void OnAtkBuffStart(int value);
	public static event OnAtkBuffStart onAtkBuffStart;

	public delegate void OnAtkBuffRemove ();
	public static event OnAtkBuffRemove onAtkBuffRemove;

	public GameObject playerHealthBarGO;
	private PlayerHealthBar playerHealthBarScript;

	public GameObject enemyHealthBarGO;
	private EnemyHealthBar enemyHealthBarScript;

	private List<Effect> effectList;

	private bool isAtkBuffAdded = false;
	public bool IsAtkBuffAdded
	{
		get { return isAtkBuffAdded; }
	}

	private bool isPoisonAdded = false;
	public bool IsPoisonAdded
	{
		get { return isPoisonAdded; }
	}


	private bool isRegenAdded = false;
	public bool IsRegenAdded
	{
		get { return isRegenAdded; }
	}

	private bool isStunAdded = false;
	public bool IsStunAdded
	{
		get { return isStunAdded; }
	}

	public bool IsEffectAdded()
	{

			return (isRegenAdded || isStunAdded);

	}

	public void ExecuteOnStunEvent(Effect effect)
	{
		if (onStunEnd != null)
		{
			onStunEnd(effect.Turn.ToString());
		}

	}

	public void ExecuteOnPoisonEvent(Effect effect)
	{
		if (onPoisonEnd != null)
		{
			onPoisonEnd(effect.Turn.ToString());
		}
		
	}

	public void ExecuteOnAtkBuffEvent(Effect effect)
	{
		if (onAtkBuffEnd != null)
		{
			onAtkBuffEnd(effect.Turn.ToString());
		}

		if (onAtkBuffStart != null)
		{
			onAtkBuffStart(effect.Value);
		}
	}

	public void AddEffectIntoList(Effect effect)
	{
		if (effect != null)
		{

			if (effect.Type == "regen" && !isRegenAdded)
			{
				effectList.Add(effect);
				if (onRegenEnd != null)
				{
					onRegenEnd(effect.Turn.ToString());
				}
				isRegenAdded = true;
			}

			if (effect.Type == "poison" && !isPoisonAdded)
			{
				effectList.Add(effect);
				if (onPoisonEnd != null)
				{
					onPoisonEnd(effect.Turn.ToString());
				}
				isPoisonAdded = true;
			}

			if (effect.Type == "stun" && !isStunAdded)
			{
				effectList.Add(effect);
				if (onStunEnd != null)
				{
					onStunEnd(effect.Turn.ToString());
				}
				isStunAdded = true;
			}

			if(effect.Type == "atkBuff" && !isAtkBuffAdded)
			{
				effectList.Add(effect);
				if (onAtkBuffEnd != null)
				{
					onAtkBuffEnd(effect.Turn.ToString());
				}
				isAtkBuffAdded = true;
			}

			if (effect.Type == "regen" && isRegenAdded)
			{
				for (int i = 0; i < effectList.Count; i++)
				{
					if (effectList[i].Type == "regen")
					{
						effectList[i].Turn = 3;
					}
				}
			}

			if (effect.Type == "stun" && isStunAdded)
			{
				for(int i = 0; i < effectList.Count; i++)
				{
					if (effectList[i].Type == "stun")
					{
						effectList[i].Turn = 1;
					}
				}
			}

			if (effect.Type == "atkBuff" && isAtkBuffAdded)
			{
				for(int i = 0; i < effectList.Count; i++)
				{
					if (effectList[i].Type == "atkBuff")
					{
						effectList[i].Turn = 3;
					}
				}
			}

			if (effect.Type == "poison" && isPoisonAdded)
			{
				for(int i = 0; i < effectList.Count; i++)
				{
					if (effectList[i].Type == "poison")
					{
						effectList[i].Turn = 3;
					}
				}
			}

		}
	}

	public void ApplyEffect(string effectType)
	{
		if (effectType == "regen")
		{
			for (int i = 0; i < effectList.Count; i++)
			{
				if (effectList[i].Type == "regen")
				{
					if (effectList[i].Turn > 0)
					{
						effectList[i].Turn--; 

						if (onRegenEnd != null)
						{
							onRegenEnd(effectList[i].Turn.ToString());
						}
						playerHealthBarScript.Heal(effectList[i].Value);
					}

					if (effectList[i].Turn == 0)
					{
						effectList.Remove(effectList[i]);
						isRegenAdded = false;
					}

				}
			}
		}
		if (effectType == "stun")
		{
			for(int i = 0; i < effectList.Count; i++)
			{
				if (effectList[i].Type == "stun")
				{
					if (effectList[i].Turn > 0)
					{
						effectList[i].Turn--;

						if (onStunEnd != null)
						{
							onStunEnd(effectList[i].Turn.ToString());
						}
					}
					if (effectList[i].Turn == 0)
					{
						effectList.Remove(effectList[i]);
						isStunAdded = false;
					}
				}
			}
		}
		if (effectType == "poison")
		{
			for (int i = 0; i < effectList.Count; i++)
			{
				if (effectList[i].Type == "poison")
				{
					if (effectList[i].Turn > 0)
					{
						effectList[i].Turn--; 
						
						if (onPoisonEnd != null)
						{
							onPoisonEnd(effectList[i].Turn.ToString());
						}
						enemyHealthBarScript.Damage(effectList[i].Value);
						Debug.Log("TRU MAU QUAI NE");
					}
					
					if (effectList[i].Turn == 0)
					{
						questionManagerScript.SetPoisonEffectOff();
						effectList.Remove(effectList[i]);
						isPoisonAdded = false;
					}
					
				}
			}
		}
		if (effectType == "atkBuff")
		{
			for(int i = 0; i < effectList.Count; i++)
			{
				if (effectList[i].Type == "atkBuff")
				{
					if (effectList[i].Turn > 0)
					{
						effectList[i].Turn--;

						if (onAtkBuffStart != null)
						{
							onAtkBuffStart(effectList[i].Value);
						}

						if (onAtkBuffEnd != null)
						{
							onAtkBuffEnd(effectList[i].Turn.ToString());
						}
					}
					if (effectList[i].Turn == 0)
					{
						effectList.Remove(effectList[i]);
						isAtkBuffAdded = false;
					}
				}
			}
		}
	}
	
	void Start(){
		effectList = new List<Effect> ();
		playerHealthBarScript = playerHealthBarGO.GetComponent<PlayerHealthBar>();
		enemyHealthBarScript = enemyHealthBarGO.GetComponent<EnemyHealthBar>();
		questionManagerScript = questionPanel.GetComponent<QuestionManager>();
	}
}