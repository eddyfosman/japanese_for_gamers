using UnityEngine;
using System.Collections;

public class MovementEnergy : MonoBehaviour {

	public float energy = 100f;
	public float energyLoss = 0.1f;
	public bool isMoving = false;

	private SpriteRenderer energyBar;
	private Vector3 energyScale;



	private void Awake(){
		energyBar = GameObject.Find ("EnergyBar").GetComponent<SpriteRenderer> ();

		energyScale = energyBar.transform.localScale;
	}

	public void UpdateEnergyBar(){
		energyBar.material.color = Color.Lerp (Color.green, Color.red, 1 - energy * 0.01f);
		energyBar.transform.localScale = new Vector3(energyScale.x * energy * 0.01f, 1, 1);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(isMoving){
			energy -= energyLoss * Time.deltaTime;
			UpdateEnergyBar();
//			Debug.Log("So nang luong con lai la: " + energy);
		}
	}
}
