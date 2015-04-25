using UnityEngine;
using System.Collections;

public class Tnt : MonoBehaviour {

	public GameObject explosion;
	Animator exploAnimator;
	Rope ropeScript;

	void InstantiateExplosion(){
		GameObject exploGo = Instantiate (explosion) as GameObject;
		exploGo.transform.position = transform.position;
//		exploAnimator.SetTrigger ("Explode");
		Destroy (exploGo, 1f);
	}

	// Use this for initialization
	void Start () {
		exploAnimator = explosion.GetComponent<Animator>();
		ropeScript = GameObject.FindGameObjectWithTag("Hand").GetComponent<Rope>();
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Explosion"){
			Destroy(gameObject);
			InstantiateExplosion();
		}
		if(collider.gameObject.tag == "Grabber"){
			Destroy(gameObject);
			InstantiateExplosion();
			ropeScript.isMissed = true;
			ropeScript.isShooting = false;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
