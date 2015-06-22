using UnityEngine;
using System.Collections;

public class RotateSample : MonoBehaviour
{	
	void Start(){

		float random = Random.Range (-0.001f, 0.005f);
		float random2 = Random.Range (0.8f, 1.1f);
		iTween.RotateBy(gameObject, iTween.Hash("x", random, "easeType", "easeInOutBack", "loopType", "pingPong", "delay", .4));
		iTween.ScaleBy(gameObject, iTween.Hash("x", random2, "easeType", "easeInOutBack", "loopType", "pingPong", "delay", .4));
	}
}

