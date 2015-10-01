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

    void SelfDestroy()
    {
        Destroy(gameObject);
    }

    void OnDisable()
    {
        switch (effectType)
        {
            case EffectType.Evade:
                EffectManager.onEvadeEnd -= DisplayTurn;
                EffectManager.onEvadeRemove -= SelfDestroy;
                break;
            case EffectType.Regen:
                EffectManager.onRegenEnd -= DisplayTurn;
                EffectManager.onRegenRemove -= SelfDestroy;
                break;
            case EffectType.Poison:
                EffectManager.onPoisonEnd -= DisplayTurn;
                EffectManager.onPoisonRemove -= SelfDestroy;
                break;
            case EffectType.AtkBuff:
                EffectManager.onAtkBuffEnd -= DisplayTurn;
                EffectManager.onAtkBuffRemove -= SelfDestroy;
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
