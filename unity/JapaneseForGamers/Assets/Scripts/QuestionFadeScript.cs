using UnityEngine;
using System.Collections;

public class QuestionFadeScript : MonoBehaviour {

	public GameObject monsterPanel;
	private MonsterFadeScript monsterFadeScript;
	public CanvasGroup fadeCanvasGroup;
	public bool onCD;

	public IEnumerator FadeToScreen(float speed){
		onCD = true;
		while(fadeCanvasGroup.alpha < 1f){
			fadeCanvasGroup.alpha += speed * Time.deltaTime;
			yield return null;
		}
		onCD = false;
	}

	// Use this for initialization
	void Start () {
		monsterFadeScript = monsterPanel.GetComponent<MonsterFadeScript> ();


	}
	
	// Update is called once per frame
	void Update () {
		if(!monsterFadeScript.onCD){
			StartCoroutine(FadeToScreen(0.5f));
		}
	}
}
