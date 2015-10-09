using UnityEngine;
using System.Collections;

public class FadeAwayVisualDamage : MonoBehaviour {
	
	CanvasGroup canvas;
	
	// Use this for initialization
	void Start () {
		canvas = GetComponent<CanvasGroup> ();
	}
	
	IEnumerator FadeAway(){
		while(canvas.alpha > 0){
			canvas.alpha -= 0.1f;
			yield return null;
		}
	}
	
	public void ShowDamage(){
		canvas.alpha = 1f;
	}
	
	public void FadeAwayDamage(){
		StartCoroutine (FadeAway());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}