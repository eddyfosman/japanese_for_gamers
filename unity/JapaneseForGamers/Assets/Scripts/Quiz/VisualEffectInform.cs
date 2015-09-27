using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VisualEffectInform : MonoBehaviour {
	public Sprite regenIcon;
	public Sprite stunIcon;
	public Sprite atkBuffIcon;
	public Sprite poisonIcon;

	public Image regen;

	private CanvasGroup canvasGroup;

	IEnumerator FadeInCanvas(){
		while(canvasGroup.alpha < 0.5f){
			canvasGroup.alpha += 0.02f;
			yield return null;
		}
	}

	IEnumerator FadeOutCanvas(){
		while(canvasGroup.alpha > 0){
			canvasGroup.alpha -= 0.1f;
			yield return null;
		}
	}

	public void FadeOutVisualEffect(){
		StartCoroutine (FadeOutCanvas());
	}

	public void FadeInVisualEffect(){
		StartCoroutine (FadeInCanvas());
	}

	public void SetSprite(string name){
		if(!string.IsNullOrEmpty(name)){
			switch(name){
			case "regen":
				regen.sprite = regenIcon;
				break;
			case "stun":
				regen.sprite = stunIcon;
				break;
			case "poison":
				regen.sprite = poisonIcon;
				break;
			case "atkBuff":
				regen.sprite = atkBuffIcon;
				break;
			}

		}
	}

	// Use this for initialization
	void Start () {
		canvasGroup = gameObject.GetComponent<CanvasGroup>();
		canvasGroup.blocksRaycasts = false;
		canvasGroup.interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
