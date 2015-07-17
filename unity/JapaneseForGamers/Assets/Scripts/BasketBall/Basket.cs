using UnityEngine;
using System.Collections;

public class Basket : MonoBehaviour {

	public AudioClip basket;
	public UnityEngine.UI.Text score;

	void OnCollisonEnter(){
		GetComponent<AudioSource>().Play ();
	}

	void OnTriggerEnter(){
		int currentScore = int.Parse (score.text) + 1;
		score.text = currentScore.ToString ();
		AudioSource.PlayClipAtPoint (basket, transform.position);
	}
}
