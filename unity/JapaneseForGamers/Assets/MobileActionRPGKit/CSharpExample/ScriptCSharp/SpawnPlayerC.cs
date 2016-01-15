using UnityEngine;
using System.Collections;

public class SpawnPlayerC : MonoBehaviour {

	public GameObject player;
	//public GameObject mainCamPrefab;
	private Transform mainCam;
	
	void Start() {
		//Check for Current Player in the scene
		GameObject currentPlayer = GameObject.FindWithTag ("Player");
		if(currentPlayer){
			// If there are the player in the scene already. Check for the Spawn Point Name
			// If it match then Move Player to the SpawnpointPosition
			string spawnPointName = currentPlayer.GetComponent<StatusC>().spawnPointName;
			GameObject spawnPoint = GameObject.Find(spawnPointName);
			if(spawnPoint){
				currentPlayer.transform.position = spawnPoint.transform.position;
				currentPlayer.transform.rotation = spawnPoint.transform.rotation;
			}
			GameObject oldCam = currentPlayer.GetComponent<AttackTriggerC>().Maincam.gameObject;
			if(!oldCam){
				return;
			}
			GameObject[] cam = GameObject.FindGameObjectsWithTag("MainCamera"); 
			foreach (GameObject cam2 in cam) { 
				if(cam2 != oldCam){
					Destroy(cam2.gameObject);
				}
			}
			
			if(currentPlayer.GetComponent<SpawnPartnerC>()){
				currentPlayer.GetComponent<SpawnPartnerC>().MoveToMaster();
			}
			// If there are the player in the scene already. We will not spawn the new player.
			return;
		}
		//Spawn Player
		Instantiate(player, transform.position , transform.rotation);

		//Set Target for All Enemy to Player
		/* var mon : GameObject[]; 
  		 mon = GameObject.FindGameObjectsWithTag("Enemy"); 
  			 for (var mo : GameObject in mon) { 
  			 	if(mo){
  			 		mo.GetComponent(AIset).followTarget = spawnPlayer.transform;
  			 	}
  			 }*/
	}

}