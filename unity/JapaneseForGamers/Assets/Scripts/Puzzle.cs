using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Puzzle : MonoBehaviour {

	public GameObject emptyObjectPrefab;
	GameObject loveGO;
	GameObject newGO;
	List<Transform> listStroke = new List<Transform>();
	List<Transform> listAllStroke = new List<Transform>();
	Dictionary<string, Vector3> originalPos = new Dictionary<string, Vector3>();
	Dictionary<string, Vector3> currentPos = new Dictionary<string, Vector3>();
	int i = 1;
	int j = 1;

	float dist;
	Transform toDrag;
	bool dragging = false;
	Vector3 offset;

	private void CheckDrag(){


		if(Application.platform == RuntimePlatform.WindowsEditor){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			Vector3 v3;
			if(Input.GetMouseButtonDown(0)){

				if(Physics.SphereCast(ray, 1f, out hit)){

					if(hit.collider != null){
						Debug.Log(hit.collider.name);
						Debug.Log(GameObject.Find(hit.collider.name).transform.parent.transform.name);
						toDrag = GameObject.Find(hit.collider.name).transform.parent.transform;
						dist = toDrag.position.z - Camera.main.transform.position.z;
						v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
						v3 = Camera.main.ScreenToWorldPoint(v3);
						offset = toDrag.position - v3;
						dragging = true;
					}
				}

			}

			if (Input.GetMouseButton(0))
			{
				if (dragging)
				{
					v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
					v3 = Camera.main.ScreenToWorldPoint(v3);
					toDrag.position = v3 + offset;
				}
			}
			if (Input.GetMouseButtonUp(0))
			{
				dragging = false;
				float distance = Mathf.Infinity;
				float range = 1.2f;
				Vector3 oriPos;
				Vector3 curPos;
				if(originalPos.TryGetValue(toDrag.name, out oriPos)){
					float dist = Vector3.Distance(toDrag.position, oriPos);
					if(dist < range && dist < distance) // check if close enough and closer than any other already checked points.
					{
						toDrag.position = oriPos;
						foreach(Transform transform in toDrag.transform){
							Destroy(transform.GetComponent<Collider>());

						}
						distance = dist;
					}
					else {
						if(currentPos.TryGetValue(toDrag.name, out curPos)){
							toDrag.position = curPos;
						}
					}
				}

			}
		}

		if(Application.platform == RuntimePlatform.Android){
			if(Input.touchCount > 0){

			
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				Vector3 v3;
				if(Input.GetTouch(0).phase == TouchPhase.Moved){
					
					if(Physics.SphereCast(ray, 1f, out hit)){
						
						if(hit.collider != null){
							Debug.Log(hit.collider.name);
							Debug.Log(GameObject.Find(hit.collider.name).transform.parent.transform.name);
							toDrag = GameObject.Find(hit.collider.name).transform.parent.transform;
							dist = toDrag.position.z - Camera.main.transform.position.z;
							v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
							v3 = Camera.main.ScreenToWorldPoint(v3);
							offset = toDrag.position - v3;
							dragging = true;
						}
					}
					
				}
				
				if (Input.GetTouch(0).phase == TouchPhase.Moved)
				{
					if (dragging)
					{
						v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
						v3 = Camera.main.ScreenToWorldPoint(v3);
						toDrag.position = v3 + offset;
					}
				}
				if (Input.GetTouch(0).phase == TouchPhase.Ended)
				{
					dragging = false;
					float distance = Mathf.Infinity;
					float range = 1.2f;
					Vector3 oriPos;
					Vector3 curPos;
					if(originalPos.TryGetValue(toDrag.name, out oriPos)){
						float dist = Vector3.Distance(toDrag.position, oriPos);
						if(dist < range && dist < distance) // check if close enough and closer than any other already checked points.
						{
							toDrag.position = oriPos;
							foreach(Transform transform in toDrag.transform){
								Destroy(transform.GetComponent<Collider>());
								
							}
							distance = dist;
						}
						else {
							if(currentPos.TryGetValue(toDrag.name, out curPos)){
								toDrag.position = curPos;
							}
						}
					}
					
				}
			}
		}

	}

	// Use this for initialization
	void Start () {
		loveGO = GameObject.Find ("love");
		foreach(Transform transform in loveGO.transform){
			if(System.Int32.Parse(transform.name.Split('.')[0].Substring(1)) > i){
				i = System.Int32.Parse(transform.name.Split('.')[0].Substring(1));
			}


		}


		for(int e = 1; e <= i; e++){
			foreach(Transform transform in loveGO.transform){

				if(transform.name.StartsWith("S" + e + ".")){
					listStroke.Add(transform);
					transform.gameObject.AddComponent<MeshCollider>();
				}
			
			}
			newGO = Instantiate (emptyObjectPrefab) as GameObject;
			newGO.transform.name = "S" + e;

			for(int a = 0; a < listStroke.Count; a++){
				listStroke[a].parent = newGO.transform;
				
			}
			originalPos.Add(newGO.transform.name, newGO.transform.position);
			newGO.transform.position = new Vector3(Random.Range(9f, 10f), Random.Range(2f, 2.5f), 0f);
			currentPos.Add(newGO.transform.name, newGO.transform.position);
//			newGO.AddComponent<BoxCollider>();
			listAllStroke.Add(newGO.transform);
			listStroke.Clear();
		}



	}
	
	// Update is called once per frame
	void Update () {
		CheckDrag ();
	}
}
