using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EffectScript : MonoBehaviour {

    public Sprite[] effectIcon;

    public Image visualEffect;

    public Text turnCount;

    private enum EffectType { Evade, Regen, Poison, AtkBuff };

    private enum SourceType { Me, Enemy };

    private EffectType effectType;

    private SourceType sourceType;

    public void DisplayTurn(string turn)
    {
        turnCount.text = turn;
    }

    public void SetEffectType(string s)
    {
        switch (s)
        {
            case "evade":
                effectType = EffectType.Evade;
                visualEffect.sprite = effectIcon[0];
                EffectManager.onEvadeEnd += DisplayTurn;
                EffectManager.onEvadeRemove += SelfDestroy;
                break;
            case "regen":
                effectType = EffectType.Regen;
                visualEffect.sprite = effectIcon[1];
                EffectManager.onRegenEnd += DisplayTurn;
                EffectManager.onRegenRemove += SelfDestroy;
                break;
            case "poison":
                effectType = EffectType.Poison;
                visualEffect.sprite = effectIcon[2];
                EffectManager.onPoisonEnd += DisplayTurn;
                EffectManager.onPoisonRemove += SelfDestroy;
                break;
            case "atkBuff":
                effectType = EffectType.AtkBuff;
                visualEffect.sprite = effectIcon[3];
                EffectManager.onAtkBuffEnd += DisplayTurn;
                EffectManager.onAtkBuffRemove += SelfDestroy;
                break;
        }
    }

    public void SetEffectType(string s, string source)
    {
        switch (source)
        {
            case "me":
                sourceType = SourceType.Me;
                break;
            case "enemy":
                sourceType = SourceType.Enemy;
                break;
        }

        switch (s)
        {
            case "evade":
                effectType = EffectType.Evade;
                visualEffect.sprite = effectIcon[0];
                if (source == "me")
                {
                    EffectManager.onEvadeEnd += DisplayTurn;
                    EffectManager.onEvadeRemove += SelfDestroy;
                }
                else
                {
                    EffectManager.onEnemyEvadeEnd += DisplayTurn;
                    EffectManager.onEnemyEvadeRemove += SelfDestroy;
                }
                break;
            case "regen":
                effectType = EffectType.Regen;
                visualEffect.sprite = effectIcon[1];
                if (source == "me")
                {
                    EffectManager.onRegenEnd += DisplayTurn;
                    EffectManager.onRegenRemove += SelfDestroy;
                }
                else
                {
                    EffectManager.onEnemyRegenEnd += DisplayTurn;
                    EffectManager.onEnemyRegenRemove += SelfDestroy;
                }
                break;
            case "poison":
                effectType = EffectType.Poison;
                visualEffect.sprite = effectIcon[2];
                if (source == "me")
                {
                    EffectManager.onPoisonEnd += DisplayTurn;
                    EffectManager.onPoisonRemove += SelfDestroy;
                }
                else
                {
                    EffectManager.onEnemyPoisonEnd += DisplayTurn;
                    EffectManager.onEnemyPoisonRemove += SelfDestroy;
                }
                break;
            case "atkBuff":
                effectType = EffectType.AtkBuff;
                visualEffect.sprite = effectIcon[3];
                if (source == "me")
                {
                    EffectManager.onAtkBuffEnd += DisplayTurn;
                    EffectManager.onAtkBuffRemove += SelfDestroy;
                }
                else
                {
                    EffectManager.onEnemyAtkBuffEnd += DisplayTurn;
                    EffectManager.onEnemyAtkBuffRemove += SelfDestroy;
                }
                break;
        }
    }

    void SelfDestroy()
    {
        Destroy(gameObject);
    }

    void OnDisable()
    {
        switch (effectType)
        {
            case EffectType.Evade:
                if (sourceType == SourceType.Me)
                {
                    EffectManager.onEvadeEnd -= DisplayTurn;
                    EffectManager.onEvadeRemove -= SelfDestroy;
                }
                else
                {
                    EffectManager.onEnemyEvadeEnd -= DisplayTurn;
                    EffectManager.onEnemyEvadeRemove -= SelfDestroy;
                }
                break;
            case EffectType.Regen:
                if (sourceType == SourceType.Me)
                {
                    EffectManager.onRegenEnd -= DisplayTurn;
                    EffectManager.onRegenRemove -= SelfDestroy;
                }
                else
                {
                    EffectManager.onEnemyRegenEnd -= DisplayTurn;
                    EffectManager.onEnemyRegenRemove -= SelfDestroy;
                }
                break;
            case EffectType.Poison:
                if (sourceType == SourceType.Me)
                {
                    EffectManager.onPoisonEnd -= DisplayTurn;
                    EffectManager.onPoisonRemove -= SelfDestroy;
                }
                else
                {
                    EffectManager.onEnemyPoisonEnd -= DisplayTurn;
                    EffectManager.onEnemyPoisonRemove -= SelfDestroy;
                }
                break;
            case EffectType.AtkBuff:
                if (sourceType == SourceType.Me)
                {
                    EffectManager.onAtkBuffEnd -= DisplayTurn;
                    EffectManager.onAtkBuffRemove -= SelfDestroy;
                }
                else
                {
                    EffectManager.onEnemyAtkBuffEnd -= DisplayTurn;
                    EffectManager.onEnemyAtkBuffRemove -= SelfDestroy;
                }
                break;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
