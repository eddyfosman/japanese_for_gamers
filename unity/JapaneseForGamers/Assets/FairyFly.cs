using UnityEngine;
using System.Collections;

public class FairyFly : MonoBehaviour {

	float wanderRadius = -1f;
	Vector2 destination;
	Rope ropeScript;
	public bool didFairyArrive = false;
	Transform grabberTransform;
	Renderer fairyRender;

	bool CheckXMargin(){
		return Mathf.Abs (transform.position.x - destination.x) < 0.1f;
	}

	bool CheckYMargin(){
		return Mathf.Abs (transform.position.y - destination.y) < 0.1f;
	}

	void ChooseRandomDestination(){
		float x = Random.Range (-6.99f, -5.99f);
		float y = Random.Range (0.94f, 1.62f);
		destination = new Vector2 (x, y);
	}
	// Use this for initialization
	void Start () {
		fairyRender = gameObject.GetComponent<Renderer>();
		grabberTransform = GameObject.FindGameObjectWithTag ("Grabber").transform;
		ropeScript = GameObject.FindGameObjectWithTag ("Hand").GetComponent<Rope>();
		ChooseRandomDestination ();

	}
	
	// Update is called once per frame
	void Update () {
		if(ropeScript.shoot){
			destination = grabberTransform.position;
			transform.position = Vector2.Lerp (gameObject.transform.position, destination, 10f * Time.deltaTime);
			if(CheckXMargin() && CheckYMargin()){
				fairyRender.enabled = false;
				didFairyArrive = true;
			}
			return;
		}
		if(!ropeScript.shoot){
			fairyRender.enabled = true;
			didFairyArrive = false;
		}
		if(CheckXMargin() && CheckYMargin()){
			ChooseRandomDestination ();
		}
		transform.position = Vector2.Lerp (gameObject.transform.position, destination, 10f * Time.deltaTime);
	}
}
