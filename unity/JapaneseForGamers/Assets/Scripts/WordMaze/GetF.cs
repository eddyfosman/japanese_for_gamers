using UnityEngine;
using System.Collections;

public class GetF : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name == "Ninja"){
			Debug.Log("VAO DAY");
			GameObject.FindGameObjectWithTag("love").GetComponent<Flood>().CreateFood();
			Debug.Log("VAO DAY 2");
			//			floodScript = GetComponent<Flood>();
			//			floodScript.CreateFood();
			//			Destroy(gameObject.collider);
			
			gameObject.GetComponent<Renderer>().enabled = false;
			Debug.Log("VAO DAY 3");
			Destroy(gameObject.GetComponent<CheckCollision>());
			Debug.Log("VAO DAY 4");
			Destroy(gameObject);
			Debug.Log("VAO DAY 5");
			
			
		}
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "Ninja"){
			Debug.Log("VAO DAY");
			GameObject.FindGameObjectWithTag("love").GetComponent<Flood>().CreateFood();
			Debug.Log("VAO DAY 2");
			//			floodScript = GetComponent<Flood>();
			//			floodScript.CreateFood();
			//			Destroy(gameObject.collider);
			
			gameObject.GetComponent<Renderer>().enabled = false;
			Debug.Log("VAO DAY 3");
			Destroy(gameObject.GetComponent<CheckCollision>());
			Debug.Log("VAO DAY 4");
			Destroy(gameObject);
			Debug.Log("VAO DAY 5");
			

		}
		
	}

}
