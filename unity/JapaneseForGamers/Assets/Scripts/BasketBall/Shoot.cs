using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject meter;
	public GameObject arrow;
	public GameObject ballPrefab;
	GameObject ballClone;
	bool thrown = false;
	bool right = true;
	float arrowSpeed = 1.6f;
	Vector3 throwSpeed = new Vector3(-5f, 16f, 0f);
	public Vector3 ballPos;
	public UnityEngine.UI.Text availableShotsGO;
	int availableShots = 5;


	// Use this for initialization
	void Start () {
		Physics.gravity = new Vector3 (0, -20f, 0);
	}

	void FixedUpdate(){
		if(arrow.transform.position.x < 0.7887f && right){
			arrow.transform.position += new Vector3(arrowSpeed*Time.deltaTime, 0, 0);
		}
		if(arrow.transform.position.x >= 0.7887f){
			right = false;
		}
		if(!right){
			arrow.transform.position -= new Vector3(arrowSpeed*Time.deltaTime, 0, 0);
		}
		if(arrow.transform.position.x <= -1.213f){
			right = true;
		}

		if(Input.GetButton("Fire1") && !thrown && availableShots > 0){
			thrown = true;
			availableShots--;
			availableShotsGO.text = availableShots.ToString();

			ballClone = Instantiate(ballPrefab, ballPos, transform.rotation) as GameObject;
			throwSpeed.y = arrow.transform.position.x + throwSpeed.y;
			throwSpeed.z = arrow.transform.position.x + throwSpeed.z;

			ballClone.GetComponent<Rigidbody>().AddForce(throwSpeed, ForceMode.Impulse);
			GetComponent<AudioSource>().Play();
		}

		if(ballClone != null && ballClone.transform.position.y < -16f){
			Destroy(ballClone);
			thrown = false;
			throwSpeed = new Vector3(-5f, 16f, 0f);
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
