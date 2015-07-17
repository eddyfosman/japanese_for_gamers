using UnityEngine;
using System.Collections;

public class FlipCard : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.RotateTo (gameObject, new Vector3 (0, 90f, 0), 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
