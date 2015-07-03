using UnityEngine;
using System.Collections;

public class MoveSample : MonoBehaviour
{	
	void Start(){
//		iTween.MoveBy(gameObject, iTween.Hash("x", 0.5, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .001));
		iTween.ShakePosition(gameObject, iTween.Hash("x", 0.5, "easeType", "easeOutCubic", "loopType", "pingPong", "delay", .01));

	}
}

