using UnityEngine;
using System.Collections;

public class TipPoint : MonoBehaviour {

	Rope ropeScript;

	void OnTriggerEnter2D(Collider2D other){


		if(other != null){
			if(other.gameObject.tag == "Walls"){
				ropeScript.isShooting = false;
				ropeScript.isMissed = true;
			}

		}


			if(other.gameObject.tag == "Kanji"){
				Debug.Log("Khong va cham cai gi ca");
				
			}

	}

	// Use this for initialization
	void Start () {
		ropeScript = GameObject.Find ("Hand_L").GetComponent<Rope>();;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
