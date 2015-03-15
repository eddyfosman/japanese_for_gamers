using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Prefab : MonoBehaviour {
	List<Transform> listChild = new List<Transform>();
	List<Transform> listChildCurStroke = new List<Transform>();
	int i = 1;
	int j = 0;
	public Material clickedMaterial;
	bool touched = false;

	private void CheckTouch(){
//		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//		RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
//		if (hit != null && hit.collider != null) {
//			Debug.Log ("I'm hitting "+hit.collider.name);
//		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
//		if(Physics.Raycast(ray,out hit))
		if(Physics.SphereCast(ray,1f,out hit))
		{
			if(Application.platform == RuntimePlatform.WindowsEditor){
				if(Input.GetMouseButtonDown(0))
				
				{

					if(hit.collider.name == listChildCurStroke[j].name){
						
						Debug.Log ("I'm hitting "+ listChildCurStroke[j].name);
						listChildCurStroke[j].gameObject.GetComponent<MeshRenderer>().renderer.material = clickedMaterial;
						Destroy (listChildCurStroke[j].collider);
						if(j < listChildCurStroke.Count - 1 ){
							j++;
						}
						else{
							j = 0;
							i++;

							listChildCurStroke.Clear();
							foreach(Transform child in listChild){
								
								if(child.name.StartsWith("S" + i + ".")){
									listChildCurStroke.Add(child);
									child.renderer.enabled = true;
								}
								
								
							}
							foreach(Transform child in listChildCurStroke){
								child.gameObject.AddComponent<MeshCollider>();
							}
						}

					}
					//				Debug.Log ("I'm hitting "+hit.collider.name);
				}
			}
			if(Application.platform == RuntimePlatform.Android){
				if(Input.touchCount > 0){
					if(Input.GetTouch(0).phase == TouchPhase.Moved){

						if(hit.collider.name == listChildCurStroke[j].name && !touched){
							
							Debug.Log ("I'm hitting "+ listChildCurStroke[j].name);
							listChildCurStroke[j].gameObject.GetComponent<MeshRenderer>().renderer.material = clickedMaterial;
							Destroy (listChildCurStroke[j].collider);
							if(j < listChildCurStroke.Count - 1 ){
								j++;
							}
							else{
								j = 0;
								i++;
								touched = true;
								listChildCurStroke.Clear();
								foreach(Transform child in listChild){
									
									if(child.name.StartsWith("S" + i + ".")){
										listChildCurStroke.Add(child);
										child.renderer.enabled = true;
									}
									
									
								}
								foreach(Transform child in listChildCurStroke){
									child.gameObject.AddComponent<MeshCollider>();
								}
							}

						}
						//				Debug.Log ("I'm hitting "+hit.collider.name);
					}
					else if(Input.GetTouch(0).phase == TouchPhase.Began){
						touched = false;
					}
				}
			}

		}

	}

	// Use this for initialization
	void Start () {
		GameObject monster = (GameObject)Instantiate(Resources.Load("love"));
		monster.transform.rotation = Quaternion.Euler(90f, -180f, -720f);
		monster.transform.position = new Vector3 (0f, 0f, 0f);
		monster.transform.localScale = new Vector3 (10f, 10f, 10f);

		foreach(Transform child in monster.transform){
			if(child.name != "S1.000"){
				listChild.Add(child);
			}


		}
		foreach(Transform child in listChild){

			if(child.name.StartsWith("S" + i + ".")){
				listChildCurStroke.Add(child);
			}
			if(!child.name.StartsWith("S" + i + ".")){
				child.renderer.enabled = false;
			}

		}
		foreach(Transform child in listChildCurStroke){
			child.gameObject.AddComponent<MeshCollider>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		CheckTouch ();
	}
}
