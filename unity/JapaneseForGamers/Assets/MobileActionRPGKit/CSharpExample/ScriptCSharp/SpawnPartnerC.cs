using UnityEngine;
using System.Collections;

public class SpawnPartnerC : MonoBehaviour {

	public GameObject[] mercenariesPrefab = new GameObject[2];
	public int spawnId = 0;

	[HideInInspector]
	public GameObject currentPartner;
	
	void Start() {
		Vector3 pos = transform.position;
		pos += Vector3.back * 3;
		if(mercenariesPrefab[spawnId]){
			GameObject m = (GameObject)Instantiate(mercenariesPrefab[spawnId] , pos , transform.rotation);
			m.GetComponent<AIfriendC>().master = this.transform;
			currentPartner = m;
		}
		
	}
	
	public void MoveToMaster() {
		if(currentPartner){
			Physics.IgnoreCollision(GetComponent<Collider>(), currentPartner.GetComponent<Collider>());
			currentPartner.transform.position = transform.position;
		}		
	}
}