using UnityEngine;
using System.Collections;

public class CheckCollision : MonoBehaviour {


	public Flood floodScript;
	public bool getFlood = false;



	void OnTriggerStay(Collider other)
	{


		getFlood = true;
		if (other.gameObject.name != null)
		{
			if(other.gameObject.name != "Ninja"){
				gameObject.GetComponent<Cube>().isCollided = true;
				
				Debug.Log("THIET LAP TRUE");
				Destroy(gameObject.GetComponent<CheckCollision>());

			}

		}
	}



}
