using UnityEngine;
using System.Collections;

public class MonsterFadeScript : MonoBehaviour {
	
	public CanvasGroup fadeCanvasGroup;
	
	
	public bool onCD;
	
	public IEnumerator FadeToBlack(float speed)
	{
		onCD = true;
		while (fadeCanvasGroup.alpha > 0f)
		{
			fadeCanvasGroup.alpha -= speed * Time.deltaTime;
			
			yield return null;
		}
		
		//		onCD = true;
		onCD = false;
	}
	
	// Use this for initialization
	void Start () {
		StartCoroutine (FadeToBlack(0.5f));
		//		onCD = true;
	}
	
	// Update is called once per frame
	void Update () {
		//		onCD = true;
	}
}