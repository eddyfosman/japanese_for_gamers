using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectManager : MonoBehaviour {

	public GameObject questionPanel;
	private QuestionManager questionManagerScript;

    public GameObject visualEffectInformGO;
    private VisualEffectInform visualEffectInformScript;

    public delegate void OnPoisonEnd (string turn);
	public static event OnPoisonEnd onPoisonEnd;
    public static event OnPoisonEnd onEnemyPoisonEnd;

    public delegate void OnRegenEnd (string turn);
	public static event OnRegenEnd onRegenEnd;
    public static event OnRegenEnd onEnemyRegenEnd;

    public delegate void OnEvadeEnd (string turn);
	public static event OnEvadeEnd onEvadeEnd;
    public static event OnEvadeEnd onEnemyEvadeEnd;

    public delegate void OnAtkBuffEnd (string turn);
	public static event OnAtkBuffEnd onAtkBuffEnd;
    public static event OnAtkBuffEnd onEnemyAtkBuffEnd;

    public delegate void OnAtkBuffStart(int value);
	public static event OnAtkBuffStart onAtkBuffStart;
    public static event OnAtkBuffStart onEnemyAtkBuffStart;

    public delegate void OnAtkBuffRemove ();
	public static event OnAtkBuffRemove onAtkBuffRemove;
    public static event OnAtkBuffRemove onEnemyAtkBuffRemove;

    public delegate void OnPoisonRemove();
    public static event OnPoisonRemove onPoisonRemove;
    public static event OnPoisonRemove onEnemyPoisonRemove;

    public delegate void OnRegenRemove();
    public static event OnRegenRemove onRegenRemove;
    public static event OnRegenRemove onEnemyRegenRemove;

    public delegate void OnEvadeRemove();
    public static event OnEvadeRemove onEvadeRemove;
    public static event OnEvadeRemove onEnemyEvadeRemove;

    public GameObject playerHealthBarGO;
	private PlayerHealthBar playerHealthBarScript;

	public GameObject enemyHealthBarGO;
	private EnemyHealthBar enemyHealthBarScript;

	private List<Effect> effectList;
    private List<Effect> effectListMonster;

	private bool isEnemyAtkBuffAdded = false;
	public bool IsEnemyAtkBuffAdded
    {
		get { return isEnemyAtkBuffAdded; }
	}

	private bool isEnemyPoisonAdded = false;
	public bool IsEnemyPoisonAdded
    {
		get { return isEnemyPoisonAdded; }
	}


	private bool isEnemyRegenAdded = false;
	public bool IsEnemyRegenAdded
    {
		get { return isEnemyRegenAdded; }
	}

	private bool isEnemyEvadeAdded = false;
	public bool IsEnemyEvadeAdded
    {
		get { return isEnemyEvadeAdded; }
	}

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

    private bool isEvadeAdded = false;
    public bool IsEvadeAdded
    {
        get { return isEvadeAdded; }
    }

    public bool IsEffectAdded()
	{

			return (isRegenAdded || isEvadeAdded);

	}

	public void ExecuteOnEvadeEvent(Effect effect)
	{
		if (onEvadeEnd != null)
		{
			onEvadeEnd(effect.Turn.ToString());
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

    private void SetRegenParticleFalse()
    {
        questionManagerScript.SetParticleGO(questionManagerScript.regenGO, false);
    }

    public void AddEffectIntoList2(Effect effect)
	{
		if (effect != null)
		{

			if (effect.Type == "regen" && !isRegenAdded && effect.Apply == "me")
			{
				effectList.Add(effect);
				if (onRegenEnd != null)
				{
					onRegenEnd(effect.Turn.ToString());
				}
				isRegenAdded = true;
			}

            if (effect.Type == "regen" && !isEnemyRegenAdded && effect.Apply == "enemy")
            {
                effectList.Add(effect);
                if (onEnemyRegenEnd != null)
                {
                    onEnemyRegenEnd(effect.Turn.ToString());
                }
                isEnemyRegenAdded = true;
            }

            if (effect.Type == "poison" && !isPoisonAdded && effect.Apply == "me")
			{
				effectList.Add(effect);
				if (onPoisonEnd != null)
				{
					onPoisonEnd(effect.Turn.ToString());
				}
				isPoisonAdded = true;
			}


            if (effect.Type == "poison" && !isEnemyPoisonAdded && effect.Apply == "enemy")
            {
                effectList.Add(effect);
                if (onEnemyPoisonEnd != null)
                {
                    onEnemyPoisonEnd(effect.Turn.ToString());
                }
                isEnemyPoisonAdded = true;
            }

            if (effect.Type == "evade" && !isEvadeAdded && effect.Apply == "me")
			{
				effectList.Add(effect);
				if (onEvadeEnd != null)
				{
					onEvadeEnd(effect.Turn.ToString());
				}
				isEvadeAdded = true;
			}

            if (effect.Type == "evade" && !isEnemyEvadeAdded && effect.Apply == "enemy")
            {
                effectList.Add(effect);
                if (onEnemyEvadeEnd != null)
                {
                    onEnemyEvadeEnd(effect.Turn.ToString());
                }
                isEnemyEvadeAdded = true;
            }

            if (effect.Type == "atkBuff" && !isAtkBuffAdded && effect.Apply == "me")
			{
				effectList.Add(effect);
				if (onAtkBuffEnd != null)
				{
					onAtkBuffEnd(effect.Turn.ToString());
				}
				isAtkBuffAdded = true;
			}

            if (effect.Type == "atkBuff" && !isEnemyAtkBuffAdded && effect.Apply == "enemy")
            {
                effectList.Add(effect);
                if (onEnemyAtkBuffEnd != null)
                {
                    onEnemyAtkBuffEnd(effect.Turn.ToString());
                }
                isEnemyAtkBuffAdded = true;
            }

            if (effect.Type == "regen" && isRegenAdded && effect.Apply == "me")
			{
				for (int i = 0; i < effectList.Count; i++)
				{
					if (effectList[i].Type == "regen" && effectList[i].Apply == "me")
					{
						effectList[i].Turn = 3;
					}
				}
			}

            if (effect.Type == "regen" && isEnemyRegenAdded && effect.Apply == "enemy")
            {
                for (int i = 0; i < effectList.Count; i++)
                {
                    if (effectList[i].Type == "regen" && effect.Apply == "enemy")
                    {
                        effectList[i].Turn = 3;
                    }
                }
            }

            if (effect.Type == "evade" && isEvadeAdded && effect.Apply == "me")
			{
				for(int i = 0; i < effectList.Count; i++)
				{
					if (effectList[i].Type == "evade" && effectList[i].Apply == "me")
					{
						effectList[i].Turn = 1;
					}
				}
			}

            if (effect.Type == "evade" && isEnemyEvadeAdded && effect.Apply == "enemy")
            {
                for (int i = 0; i < effectList.Count; i++)
                {
                    if (effectList[i].Type == "evade" && effect.Apply == "enemy")
                    {
                        effectList[i].Turn = 1;
                    }
                }
            }

            if (effect.Type == "atkBuff" && isAtkBuffAdded && effect.Apply == "me")
			{
				for(int i = 0; i < effectList.Count; i++)
				{
					if (effectList[i].Type == "atkBuff" && effectList[i].Apply == "me")
					{
						effectList[i].Turn = 3;
					}
				}
			}

            if (effect.Type == "atkBuff" && isEnemyAtkBuffAdded && effect.Apply == "enemy")
            {
                for (int i = 0; i < effectList.Count; i++)
                {
                    if (effectList[i].Type == "atkBuff" && effect.Apply == "enemy")
                    {
                        effectList[i].Turn = 3;
                    }
                }
            }

            if (effect.Type == "poison" && isPoisonAdded && effect.Apply == "me")
			{
				for(int i = 0; i < effectList.Count; i++)
				{
					if (effectList[i].Type == "poison" && effectList[i].Apply == "me")
					{
						effectList[i].Turn = 3;
					}
				}
			}

            if (effect.Type == "poison" && isEnemyPoisonAdded && effect.Apply == "enemy")
            {
                for (int i = 0; i < effectList.Count; i++)
                {
                    if (effectList[i].Type == "poison" && effect.Apply == "enemy")
                    {
                        effectList[i].Turn = 3;
                    }
                }
            }

        }
	}

    public bool AddEffectIntoList(Effect effect)
    {
        if (effect != null)
        {

            if (effect.Type == "regen" && !isRegenAdded && effect.Apply == "me")
            {
                effectList.Add(effect);
                if (onRegenEnd != null)
                {
                    onRegenEnd(effect.Turn.ToString());
                }
                isRegenAdded = true;
                return true;
            }

            if (effect.Type == "regen" && !isEnemyRegenAdded && effect.Apply == "enemy")
            {
                effectList.Add(effect);
                if (onEnemyRegenEnd != null)
                {
                    onEnemyRegenEnd(effect.Turn.ToString());
                }
                isEnemyRegenAdded = true;
                return true;
            }

            if (effect.Type == "poison" && !isPoisonAdded && effect.Apply == "me")
            {
                effectList.Add(effect);
                if (onPoisonEnd != null)
                {
                    onPoisonEnd(effect.Turn.ToString());
                }
                isPoisonAdded = true;
                return true;
            }


            if (effect.Type == "poison" && !isEnemyPoisonAdded && effect.Apply == "enemy")
            {
                effectList.Add(effect);
                if (onEnemyPoisonEnd != null)
                {
                    onEnemyPoisonEnd(effect.Turn.ToString());
                }
                isEnemyPoisonAdded = true;
                return true;
            }

            if (effect.Type == "evade" && !isEvadeAdded && effect.Apply == "me")
            {
                effectList.Add(effect);
                if (onEvadeEnd != null)
                {
                    onEvadeEnd(effect.Turn.ToString());
                }
                isEvadeAdded = true;
                return true;
            }

            if (effect.Type == "evade" && !isEnemyEvadeAdded && effect.Apply == "enemy")
            {
                effectList.Add(effect);
                if (onEnemyEvadeEnd != null)
                {
                    onEnemyEvadeEnd(effect.Turn.ToString());
                }
                isEnemyEvadeAdded = true;
                return true;
            }

            if (effect.Type == "atkBuff" && !isAtkBuffAdded && effect.Apply == "me")
            {
                effectList.Add(effect);
                if (onAtkBuffEnd != null)
                {
                    onAtkBuffEnd(effect.Turn.ToString());
                }
                isAtkBuffAdded = true;
                return true;
            }

            if (effect.Type == "atkBuff" && !isEnemyAtkBuffAdded && effect.Apply == "enemy")
            {
                effectList.Add(effect);
                if (onEnemyAtkBuffEnd != null)
                {
                    onEnemyAtkBuffEnd(effect.Turn.ToString());
                }
                isEnemyAtkBuffAdded = true;
                return true;
            }

            if (effect.Type == "regen" && isRegenAdded && effect.Apply == "me")
            {
                for (int i = 0; i < effectList.Count; i++)
                {
                    if (effectList[i].Type == "regen" && effectList[i].Apply == "me")
                    {
                        effectList[i].Turn = 3;
                        if (onRegenEnd != null)
                        {
                            onRegenEnd(effect.Turn.ToString());
                        }
                        return false;
                    }
                }
            }

            if (effect.Type == "regen" && isEnemyRegenAdded && effect.Apply == "enemy")
            {
                for (int i = 0; i < effectList.Count; i++)
                {
                    if (effectList[i].Type == "regen" && effect.Apply == "enemy")
                    {
                        effectList[i].Turn = 3;
                        if (onEnemyRegenEnd != null)
                        {
                            onEnemyRegenEnd(effect.Turn.ToString());
                        }
                        return false;
                    }
                }
            }

            if (effect.Type == "evade" && isEvadeAdded && effect.Apply == "me")
            {
                for (int i = 0; i < effectList.Count; i++)
                {
                    if (effectList[i].Type == "evade" && effectList[i].Apply == "me")
                    {
                        effectList[i].Turn = 1;
                        if (onEvadeEnd != null)
                        {
                            onEvadeEnd(effect.Turn.ToString());
                        }
                        return false;
                    }
                }
            }

            if (effect.Type == "evade" && isEnemyEvadeAdded && effect.Apply == "enemy")
            {
                for (int i = 0; i < effectList.Count; i++)
                {
                    if (effectList[i].Type == "evade" && effect.Apply == "enemy")
                    {
                        effectList[i].Turn = 1;
                        if (onEnemyEvadeEnd != null)
                        {
                            onEnemyEvadeEnd(effect.Turn.ToString());
                        }
                        return false;
                    }
                }
            }

            if (effect.Type == "atkBuff" && isAtkBuffAdded && effect.Apply == "me")
            {
                for (int i = 0; i < effectList.Count; i++)
                {
                    if (effectList[i].Type == "atkBuff" && effectList[i].Apply == "me")
                    {
                        effectList[i].Turn = 3;
                        if (onAtkBuffEnd != null)
                        {
                            onAtkBuffEnd(effect.Turn.ToString());
                        }
                        return false;
                    }
                }
            }

            if (effect.Type == "atkBuff" && isEnemyAtkBuffAdded && effect.Apply == "enemy")
            {
                for (int i = 0; i < effectList.Count; i++)
                {
                    if (effectList[i].Type == "atkBuff" && effect.Apply == "enemy")
                    {
                        effectList[i].Turn = 3;
                        if (onEnemyAtkBuffEnd != null)
                        {
                            onEnemyAtkBuffEnd(effect.Turn.ToString());
                        }
                        return false;
                    }
                }
            }

            if (effect.Type == "poison" && isPoisonAdded && effect.Apply == "me")
            {
                for (int i = 0; i < effectList.Count; i++)
                {
                    if (effectList[i].Type == "poison" && effectList[i].Apply == "me")
                    {
                        effectList[i].Turn = 3;
                        if (onPoisonEnd != null)
                        {
                            onPoisonEnd(effect.Turn.ToString());
                        }
                        return false;
                    }
                }
            }

            if (effect.Type == "poison" && isEnemyPoisonAdded && effect.Apply == "enemy")
            {
                for (int i = 0; i < effectList.Count; i++)
                {
                    if (effectList[i].Type == "poison" && effect.Apply == "enemy")
                    {
                        effectList[i].Turn = 3;
                        if (onEnemyPoisonEnd != null)
                        {
                            onEnemyPoisonEnd(effect.Turn.ToString());
                        }
                        return false;
                    }
                }
            }

        }
        return false;

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
                        if (onRegenRemove != null)
                        {
                            onRegenRemove();
                        }
                        isRegenAdded = false;
					}

				}
			}
		}
		if (effectType == "evade")
		{
			for(int i = 0; i < effectList.Count; i++)
			{
				if (effectList[i].Type == "evade")
				{
					if (effectList[i].Turn > 0)
					{
						effectList[i].Turn--;

						if (onEvadeEnd != null)
						{
							onEvadeEnd(effectList[i].Turn.ToString());
						}
					}
					if (effectList[i].Turn == 0)
					{
						effectList.Remove(effectList[i]);
                        if (onEvadeRemove != null)
                        {
                            onEvadeRemove();
                        }
                        isEvadeAdded = false;
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
                        if (onPoisonRemove != null)
                        {
                            onPoisonRemove();
                        }
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
                        if (onAtkBuffRemove != null)
                        {
                            onAtkBuffRemove();
                        }
                        isAtkBuffAdded = false;
					}
				}
			}
		}
	}

    public void ApplyEffect()
    {
        
        
        for (int i = 0; i < effectList.Count; i++)
        {
            if (effectList[i].Type == "regen" && effectList[i].Apply == "me")
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
                    if (onRegenRemove != null)
                    {
                        onRegenRemove();
                    }
                    isRegenAdded = false;
                }

            }

            if (effectList[i].Type == "regen" && effectList[i].Apply == "enemy")
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
                    if (onRegenRemove != null)
                    {
                        onRegenRemove();
                    }
                    isRegenAdded = false;
                }

            }

            if (effectList[i].Type == "evade" && effectList[i].Apply == "me")
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEvadeEnd != null)
                    {
                        onEvadeEnd(effectList[i].Turn.ToString());
                    }
                }
                if (effectList[i].Turn == 0)
                {
                    effectList.Remove(effectList[i]);
                    if (onEvadeRemove != null)
                    {
                        onEvadeRemove();
                    }
                    isEvadeAdded = false;
                }
            }

            if (effectList[i].Type == "evade" && effectList[i].Apply == "enemy")
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEvadeEnd != null)
                    {
                        onEvadeEnd(effectList[i].Turn.ToString());
                    }
                }
                if (effectList[i].Turn == 0)
                {
                    effectList.Remove(effectList[i]);
                    if (onEvadeRemove != null)
                    {
                        onEvadeRemove();
                    }
                    isEvadeAdded = false;
                }
            }

            if (effectList[i].Type == "poison" && effectList[i].Apply == "me")
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
                    if (onPoisonRemove != null)
                    {
                        onPoisonRemove();
                    }
                    isPoisonAdded = false;
                }

            }

            if (effectList[i].Type == "poison" && effectList[i].Apply == "enemy")
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
                    if (onPoisonRemove != null)
                    {
                        onPoisonRemove();
                    }
                    isPoisonAdded = false;
                }

            }

            if (effectList[i].Type == "atkBuff" && effectList[i].Apply == "me")
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
                    if (onAtkBuffRemove != null)
                    {
                        onAtkBuffRemove();
                    }
                    isAtkBuffAdded = false;
                }
            }

            if (effectList[i].Type == "atkBuff" && effectList[i].Apply == "enemy")
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
                    if (onAtkBuffRemove != null)
                    {
                        onAtkBuffRemove();
                    }
                    isAtkBuffAdded = false;
                }
            }
        }
        
    
    }

    public void ApplyEffect(bool val)
    {
        // val to check whether in playerphase/true or enemyphase/false

        for (int i = 0; i < effectList.Count; i++)
        {
            if (effectList[i].Type == "regen" && effectList[i].Apply == "me" && val)
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
                    if (onRegenRemove != null)
                    {
                        onRegenRemove();
                    }
                    isRegenAdded = false;
                    continue;
                }

            }

            if (effectList[i].Type == "regen" && effectList[i].Apply == "enemy" && !val)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEnemyRegenEnd != null)
                    {
                        onEnemyRegenEnd(effectList[i].Turn.ToString());
                    }
                    enemyHealthBarScript.Heal(effectList[i].Value);
                }

                if (effectList[i].Turn == 0)
                {
                    effectList.Remove(effectList[i]);
                    if (onEnemyRegenRemove != null)
                    {
                        onEnemyRegenRemove();
                    }
                    isEnemyRegenAdded = false;
                    continue;
                }

            }

            if (effectList[i].Type == "evade" && effectList[i].Apply == "me" && val)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEvadeEnd != null)
                    {
                        onEvadeEnd(effectList[i].Turn.ToString());
                    }
                }
                if (effectList[i].Turn == 0)
                {
                    effectList.Remove(effectList[i]);
                    if (onEvadeRemove != null)
                    {
                        onEvadeRemove();
                    }
                    isEvadeAdded = false;
                    continue;
                }
            }

            if (effectList[i].Type == "evade" && effectList[i].Apply == "enemy" && !val)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEnemyEvadeEnd != null)
                    {
                        onEnemyEvadeEnd(effectList[i].Turn.ToString());
                    }
                }
                if (effectList[i].Turn == 0)
                {
                    effectList.Remove(effectList[i]);
                    if (onEnemyEvadeRemove != null)
                    {
                        onEnemyEvadeRemove();
                    }
                    isEnemyEvadeAdded = false;
                    continue;
                }
            }

            if (effectList[i].Type == "poison" && effectList[i].Apply == "me" && val)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onPoisonEnd != null)
                    {
                        onPoisonEnd(effectList[i].Turn.ToString());
                    }
                    enemyHealthBarScript.Damage(effectList[i].Value);
                    Debug.Log("TRU MAU QUAI NE TAI SAO TRU 2 LAN");
                }

                if (effectList[i].Turn == 0)
                {
                    questionManagerScript.SetPoisonEffectOff();
                    effectList.Remove(effectList[i]);
                    if (onPoisonRemove != null)
                    {
                        onPoisonRemove();
                    }
                    isPoisonAdded = false;
                    continue;
                }

            }

            if (effectList[i].Type == "poison" && effectList[i].Apply == "enemy" && !val)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEnemyPoisonEnd != null)
                    {
                        onEnemyPoisonEnd(effectList[i].Turn.ToString());
                    }
                    playerHealthBarScript.Damage(effectList[i].Value);
                    Debug.Log("TRU MAU QUAI NE");
                }

                if (effectList[i].Turn == 0)
                {
                    questionManagerScript.SetPoisonEffectOff();
                    effectList.Remove(effectList[i]);
                    if (onEnemyPoisonRemove != null)
                    {
                        onEnemyPoisonRemove();
                    }
                    isEnemyPoisonAdded = false;
                    continue;
                }

            }

            if (effectList[i].Type == "atkBuff" && effectList[i].Apply == "me" && val)
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
                    if (onAtkBuffRemove != null)
                    {
                        onAtkBuffRemove();
                    }
                    isAtkBuffAdded = false;
                    continue;
                }
            }

            if (effectList[i].Type == "atkBuff" && effectList[i].Apply == "enemy" && !val)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEnemyAtkBuffStart != null)
                    {
                        onEnemyAtkBuffStart(effectList[i].Value);
                    }

                    if (onEnemyAtkBuffEnd != null)
                    {
                        onEnemyAtkBuffEnd(effectList[i].Turn.ToString());
                    }
                }
                if (effectList[i].Turn == 0)
                {
                    effectList.Remove(effectList[i]);
                    if (onEnemyAtkBuffRemove != null)
                    {
                        onEnemyAtkBuffRemove();
                    }
                    isEnemyAtkBuffAdded = false;
                    continue;
                }
            }
        }


    }

    private void FadeOutVisualEffect()
    {
        visualEffectInformScript.FadeOutVisualEffect();
    }

    public IEnumerator DelayApplyEffect(bool val)
    {
    
        // val to check whether in playerphase/true or enemyphase/false

        for (int i = 0; i < effectList.Count; i++)
        {
            if (effectList[i].Type == "regen" && effectList[i].Apply == "me" && val)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onRegenEnd != null)
                    {
                        onRegenEnd(effectList[i].Turn.ToString());
                    }

                    

                    playerHealthBarScript.Heal(effectList[i].Value);
                    
                    visualEffectInformScript.SetSprite("regen");
                    visualEffectInformScript.FadeInVisualEffect();
                    questionManagerScript.SetParticleGO(questionManagerScript.regenGO, true);
                    Invoke("SetRegenParticleFalse", questionManagerScript.regenParticle.duration);
                    Invoke("FadeOutVisualEffect", 1f);
                    if (effectList[i].Turn == 0)
                    {
                        effectList.Remove(effectList[i]);
                        if (onRegenRemove != null)
                        {
                            onRegenRemove();
                        }
                        isRegenAdded = false;

                    }
                    yield return new WaitForSeconds(questionManagerScript.regenParticle.duration);
                }

                

            }

            if (effectList[i].Type == "regen" && effectList[i].Apply == "enemy" && !val)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEnemyRegenEnd != null)
                    {
                        onEnemyRegenEnd(effectList[i].Turn.ToString());
                    }
                    enemyHealthBarScript.Heal(effectList[i].Value);
                    visualEffectInformScript.SetSprite("regen");
                    visualEffectInformScript.FadeInVisualEffect();
                    questionManagerScript.SetParticleGO(questionManagerScript.regenGO, true);
                    Invoke("SetRegenParticleFalse", questionManagerScript.regenParticle.duration);
                    Invoke("FadeOutVisualEffect", 1f);
                    if (effectList[i].Turn == 0)
                    {
                        effectList.Remove(effectList[i]);
                        if (onEnemyRegenRemove != null)
                        {
                            onEnemyRegenRemove();
                        }
                        isEnemyRegenAdded = false;
                        
                    }

                    yield return new WaitForSeconds(questionManagerScript.regenParticle.duration);
                }

                

            }

            if (effectList[i].Type == "evade" && effectList[i].Apply == "me" && val)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEvadeEnd != null)
                    {
                        onEvadeEnd(effectList[i].Turn.ToString());
                    }
                    if (effectList[i].Turn == 0)
                    {
                        effectList.Remove(effectList[i]);
                        if (onEvadeRemove != null)
                        {
                            onEvadeRemove();
                        }
                        isEvadeAdded = false;
                        
                    }
                    yield return new WaitForSeconds(1f);
                }
                //if (effectList[i].Turn == 0)
                //{
                //    effectList.Remove(effectList[i]);
                //    if (onEvadeRemove != null)
                //    {
                //        onEvadeRemove();
                //    }
                //    isEvadeAdded = false;
                //    yield return new WaitForSeconds(1f);
                //    continue;
                //}
            }

            if (effectList[i].Type == "evade" && effectList[i].Apply == "enemy" && !val)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEnemyEvadeEnd != null)
                    {
                        onEnemyEvadeEnd(effectList[i].Turn.ToString());
                    }
                    if (effectList[i].Turn == 0)
                    {
                        effectList.Remove(effectList[i]);
                        if (onEnemyEvadeRemove != null)
                        {
                            onEnemyEvadeRemove();
                        }
                        isEnemyEvadeAdded = false;
                        
                    }
                    yield return new WaitForSeconds(1f);
                }
                //if (effectList[i].Turn == 0)
                //{
                //    effectList.Remove(effectList[i]);
                //    if (onEnemyEvadeRemove != null)
                //    {
                //        onEnemyEvadeRemove();
                //    }
                //    isEnemyEvadeAdded = false;
                //    yield return new WaitForSeconds(1f);
                //    continue;
                //}
            }

            if (effectList[i].Type == "poison" && effectList[i].Apply == "me" && val)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onPoisonEnd != null)
                    {
                        onPoisonEnd(effectList[i].Turn.ToString());
                    }
                    enemyHealthBarScript.Damage(effectList[i].Value);

                    if (effectList[i].Turn == 0)
                    {
                        questionManagerScript.SetPoisonEffectOff();
                        effectList.Remove(effectList[i]);
                        if (onPoisonRemove != null)
                        {
                            onPoisonRemove();
                        }
                        isPoisonAdded = false;
                        
                    }
                    yield return new WaitForSeconds(1f);
                    Debug.Log("TRU MAU QUAI NE TAI SAO TRU 2 LAN");
                }

                

            }

            if (effectList[i].Type == "poison" && effectList[i].Apply == "enemy" && !val)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEnemyPoisonEnd != null)
                    {
                        onEnemyPoisonEnd(effectList[i].Turn.ToString());
                    }
                    playerHealthBarScript.Damage(effectList[i].Value);
                    if (effectList[i].Turn == 0)
                    {
                        questionManagerScript.SetPoisonEffectOff();
                        effectList.Remove(effectList[i]);
                        if (onEnemyPoisonRemove != null)
                        {
                            onEnemyPoisonRemove();
                        }
                        isEnemyPoisonAdded = false;
                        
                    }
                    yield return new WaitForSeconds(1f);
                    Debug.Log("TRU MAU QUAI NE");
                }

               

            }

            if (effectList[i].Type == "atkBuff" && effectList[i].Apply == "me" && val)
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
                    if (effectList[i].Turn == 0)
                    {
                        effectList.Remove(effectList[i]);
                        if (onAtkBuffRemove != null)
                        {
                            onAtkBuffRemove();
                        }
                        isAtkBuffAdded = false;
                        
                    }
                    yield return new WaitForSeconds(1f);
                }
                
            }

            if (effectList[i].Type == "atkBuff" && effectList[i].Apply == "enemy" && !val)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEnemyAtkBuffStart != null)
                    {
                        onEnemyAtkBuffStart(effectList[i].Value);
                    }

                    if (onEnemyAtkBuffEnd != null)
                    {
                        onEnemyAtkBuffEnd(effectList[i].Turn.ToString());
                    }
                    if (effectList[i].Turn == 0)
                    {
                        effectList.Remove(effectList[i]);
                        if (onEnemyAtkBuffRemove != null)
                        {
                            onEnemyAtkBuffRemove();
                        }
                        isEnemyAtkBuffAdded = false;
                        
                    }
                    yield return new WaitForSeconds(1f);
                }
                
            }
        }


    }

    public IEnumerator DelayApplyEffect(string effectType, string source)
    {
       
   
        // val to check whether in playerphase/true or enemyphase/false

        for (int i = 0; i < effectList.Count; i++)
        {
            if (effectList[i].Type == "regen" && effectList[i].Apply == "me" && effectList[i].Apply == source && effectList[i].Type == effectType)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onRegenEnd != null)
                    {
                        onRegenEnd(effectList[i].Turn.ToString());
                    }
                    playerHealthBarScript.Heal(effectList[i].Value);
                    if (effectList[i].Turn == 0)
                    {
                        effectList.Remove(effectList[i]);
                        if (onRegenRemove != null)
                        {
                            onRegenRemove();
                        }
                        isRegenAdded = false;
                        
                    }
                    yield return new WaitForSeconds(1f);
                }

                

            }

            if (effectList[i].Type == "regen" && effectList[i].Apply == "enemy" && effectList[i].Apply == source && effectList[i].Type == effectType)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEnemyRegenEnd != null)
                    {
                        onEnemyRegenEnd(effectList[i].Turn.ToString());
                    }
                    enemyHealthBarScript.Heal(effectList[i].Value);
                    if (effectList[i].Turn == 0)
                    {
                        effectList.Remove(effectList[i]);
                        if (onEnemyRegenRemove != null)
                        {
                            onEnemyRegenRemove();
                        }
                        isEnemyRegenAdded = false;
                        
                    }
                    yield return new WaitForSeconds(1f);
                }

               

            }

            if (effectList[i].Type == "evade" && effectList[i].Apply == "me" && effectList[i].Apply == source && effectList[i].Type == effectType)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEvadeEnd != null)
                    {
                        onEvadeEnd(effectList[i].Turn.ToString());
                    }
                    if (effectList[i].Turn == 0)
                    {
                        effectList.Remove(effectList[i]);
                        if (onEvadeRemove != null)
                        {
                            onEvadeRemove();
                        }
                        isEvadeAdded = false;
                        
                    }
                    yield return new WaitForSeconds(1f);
                }
               
            }

            if (effectList[i].Type == "evade" && effectList[i].Apply == "enemy" && effectList[i].Apply == source && effectList[i].Type == effectType)
            {
                if (effectList[i].Turn > 0)
                {
                    Debug.Log("TRU LUOT HIEU UNG EVADE");
                    effectList[i].Turn--;

                    if (onEnemyEvadeEnd != null)
                    {
                        Debug.Log("HIEN LUOT HIEU UNG EVADE");
                        onEnemyEvadeEnd(effectList[i].Turn.ToString());
                    }

                    if (effectList[i].Turn == 0)
                    {
                        Debug.Log("XOA HIEU UNG EVADE");
                        effectList.Remove(effectList[i]);
                        if (onEnemyEvadeRemove != null)
                        {
                            Debug.Log("XOA HIEN LUOT HIEU UNG EVADE");
                            onEnemyEvadeRemove();
                        }
                        isEnemyEvadeAdded = false;
                        
                    }
                    yield return new WaitForSeconds(1f);
                }
                
            }

            if (effectList[i].Type == "poison" && effectList[i].Apply == "me" && effectList[i].Apply == source && effectList[i].Type == effectType)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onPoisonEnd != null)
                    {
                        onPoisonEnd(effectList[i].Turn.ToString());
                    }
                    enemyHealthBarScript.Damage(effectList[i].Value);

                    if (effectList[i].Turn == 0)
                    {
                        questionManagerScript.SetPoisonEffectOff();
                        effectList.Remove(effectList[i]);
                        if (onPoisonRemove != null)
                        {
                            onPoisonRemove();
                        }
                        isPoisonAdded = false;
                       
                    }

                    yield return new WaitForSeconds(1f);
                    Debug.Log("TRU MAU QUAI NE TAI SAO TRU 2 LAN");
                }

                

            }

            if (effectList[i].Type == "poison" && effectList[i].Apply == "enemy" && effectList[i].Apply == source && effectList[i].Type == effectType)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEnemyPoisonEnd != null)
                    {
                        onEnemyPoisonEnd(effectList[i].Turn.ToString());
                    }
                    playerHealthBarScript.Damage(effectList[i].Value);

                    if (effectList[i].Turn == 0)
                    {
                        questionManagerScript.SetPoisonEffectOff();
                        effectList.Remove(effectList[i]);
                        if (onEnemyPoisonRemove != null)
                        {
                            onEnemyPoisonRemove();
                        }
                        isEnemyPoisonAdded = false;
                        
                    }

                    yield return new WaitForSeconds(1f);
                    Debug.Log("TRU MAU QUAI NE");
                }

                

            }

            if (effectList[i].Type == "atkBuff" && effectList[i].Apply == "me" && effectList[i].Apply == source && effectList[i].Type == effectType)
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
                    if (effectList[i].Turn == 0)
                    {
                        effectList.Remove(effectList[i]);
                        if (onAtkBuffRemove != null)
                        {
                            onAtkBuffRemove();
                        }
                        isAtkBuffAdded = false;
                       
                    }

                    yield return new WaitForSeconds(1f);
                }
               
            }

            if (effectList[i].Type == "atkBuff" && effectList[i].Apply == "enemy" && effectList[i].Apply == source && effectList[i].Type == effectType)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEnemyAtkBuffStart != null)
                    {
                        onEnemyAtkBuffStart(effectList[i].Value);
                    }

                    if (onEnemyAtkBuffEnd != null)
                    {
                        onEnemyAtkBuffEnd(effectList[i].Turn.ToString());
                    }
                    if (effectList[i].Turn == 0)
                    {
                        effectList.Remove(effectList[i]);
                        if (onEnemyAtkBuffRemove != null)
                        {
                            onEnemyAtkBuffRemove();
                        }
                        isEnemyAtkBuffAdded = false;
                    }

                    yield return new WaitForSeconds(1f);
                }
                
            }
        }


    }


    public void ApplyEffect(string effectType, string source)
    {
        // val to check whether in playerphase/true or enemyphase/false

        for (int i = 0; i < effectList.Count; i++)
        {
            if (effectList[i].Type == "regen" && effectList[i].Apply == "me" && effectList[i].Apply == source && effectList[i].Type == effectType)
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
                    if (onRegenRemove != null)
                    {
                        onRegenRemove();
                    }
                    isRegenAdded = false;
                    continue;
                }

            }

            if (effectList[i].Type == "regen" && effectList[i].Apply == "enemy" && effectList[i].Apply == source && effectList[i].Type == effectType)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEnemyRegenEnd != null)
                    {
                        onEnemyRegenEnd(effectList[i].Turn.ToString());
                    }
                    enemyHealthBarScript.Heal(effectList[i].Value);
                }

                if (effectList[i].Turn == 0)
                {
                    effectList.Remove(effectList[i]);
                    if (onEnemyRegenRemove != null)
                    {
                        onEnemyRegenRemove();
                    }
                    isEnemyRegenAdded = false;
                    continue;
                }

            }

            if (effectList[i].Type == "evade" && effectList[i].Apply == "me" && effectList[i].Apply == source && effectList[i].Type == effectType)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEvadeEnd != null)
                    {
                        onEvadeEnd(effectList[i].Turn.ToString());
                    }
                }
                if (effectList[i].Turn == 0)
                {
                    effectList.Remove(effectList[i]);
                    if (onEvadeRemove != null)
                    {
                        onEvadeRemove();
                    }
                    isEvadeAdded = false;
                    continue;
                }
            }

            if (effectList[i].Type == "evade" && effectList[i].Apply == "enemy" && effectList[i].Apply == source && effectList[i].Type == effectType)
            {
                if (effectList[i].Turn > 0)
                {
                    Debug.Log("TRU LUOT HIEU UNG EVADE");
                    effectList[i].Turn--;

                    if (onEnemyEvadeEnd != null)
                    {
                        Debug.Log("HIEN LUOT HIEU UNG EVADE");
                        onEnemyEvadeEnd(effectList[i].Turn.ToString());
                    }
                }
                if (effectList[i].Turn == 0)
                {
                    Debug.Log("XOA HIEU UNG EVADE");
                    effectList.Remove(effectList[i]);
                    if (onEnemyEvadeRemove != null)
                    {
                        Debug.Log("XOA HIEN LUOT HIEU UNG EVADE");
                        onEnemyEvadeRemove();
                    }
                    isEnemyEvadeAdded = false;
                    continue;
                }
            }

            if (effectList[i].Type == "poison" && effectList[i].Apply == "me" && effectList[i].Apply == source && effectList[i].Type == effectType)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onPoisonEnd != null)
                    {
                        onPoisonEnd(effectList[i].Turn.ToString());
                    }
                    enemyHealthBarScript.Damage(effectList[i].Value);
                    Debug.Log("TRU MAU QUAI NE TAI SAO TRU 2 LAN");
                }

                if (effectList[i].Turn == 0)
                {
                    questionManagerScript.SetPoisonEffectOff();
                    effectList.Remove(effectList[i]);
                    if (onPoisonRemove != null)
                    {
                        onPoisonRemove();
                    }
                    isPoisonAdded = false;
                    continue;
                }

            }

            if (effectList[i].Type == "poison" && effectList[i].Apply == "enemy" && effectList[i].Apply == source && effectList[i].Type == effectType)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEnemyPoisonEnd != null)
                    {
                        onEnemyPoisonEnd(effectList[i].Turn.ToString());
                    }
                    playerHealthBarScript.Damage(effectList[i].Value);
                    Debug.Log("TRU MAU QUAI NE");
                }

                if (effectList[i].Turn == 0)
                {
                    questionManagerScript.SetPoisonEffectOff();
                    effectList.Remove(effectList[i]);
                    if (onEnemyPoisonRemove != null)
                    {
                        onEnemyPoisonRemove();
                    }
                    isEnemyPoisonAdded = false;
                    continue;
                }

            }

            if (effectList[i].Type == "atkBuff" && effectList[i].Apply == "me" && effectList[i].Apply == source && effectList[i].Type == effectType)
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
                    if (onAtkBuffRemove != null)
                    {
                        onAtkBuffRemove();
                    }
                    isAtkBuffAdded = false;
                    continue;
                }
            }

            if (effectList[i].Type == "atkBuff" && effectList[i].Apply == "enemy" && effectList[i].Apply == source && effectList[i].Type == effectType)
            {
                if (effectList[i].Turn > 0)
                {
                    effectList[i].Turn--;

                    if (onEnemyAtkBuffStart != null)
                    {
                        onEnemyAtkBuffStart(effectList[i].Value);
                    }

                    if (onEnemyAtkBuffEnd != null)
                    {
                        onEnemyAtkBuffEnd(effectList[i].Turn.ToString());
                    }
                }
                if (effectList[i].Turn == 0)
                {
                    effectList.Remove(effectList[i]);
                    if (onEnemyAtkBuffRemove != null)
                    {
                        onEnemyAtkBuffRemove();
                    }
                    isEnemyAtkBuffAdded = false;
                    continue;
                }
            }
        }


    }

    void Start()
    {
		effectList = new List<Effect>();
        effectListMonster = new List<Effect>();
		playerHealthBarScript = playerHealthBarGO.GetComponent<PlayerHealthBar>();
		enemyHealthBarScript = enemyHealthBarGO.GetComponent<EnemyHealthBar>();
		questionManagerScript = questionPanel.GetComponent<QuestionManager>();
        visualEffectInformScript = visualEffectInformGO.GetComponent<VisualEffectInform>();
	}
}