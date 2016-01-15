using UnityEngine;
using System.Collections;

public class characterRotate : MonoBehaviour {

	public GameObject frog;
	
	
	
	private Rect FpsRect ;
	private string frpString;
	
	private GameObject instanceObj;
	public GameObject[] gameObjArray=new GameObject[10];
	public AnimationClip[] AniList  = new AnimationClip[4];
	
	float minimum = 2.0f;
	float maximum = 50.0f;
	float touchNum = 0f;
	string touchDirection ="forward"; 
	private GameObject toad;
	
	// Use this for initialization
	void Start () {
		
		//frog.animation["dragon_03_ani01"].blendMode=AnimationBlendMode.Blend;
		//frog.animation["dragon_03_ani02"].blendMode=AnimationBlendMode.Blend;
		//Debug.Log(frog.GetComponent("dragon_03_ani01"));
		
		//Instantiate(gameObjArray[0], gameObjArray[0].transform.position, gameObjArray[0].transform.rotation);
	}
	
 void OnGUI() {
	  if (GUI.Button(new Rect(20, 20, 90, 50), "Idle")){
		 frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Idle");
	  }
		 
		  if (GUI.Button(new Rect(110, 20, 90, 50), "Walk")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Walk");
	  }
	    if (GUI.Button(new Rect(200, 20, 90, 50), "Run")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Run");
	  }
		  if (GUI.Button(new Rect(290, 20, 90, 50), "Jump")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Default;
		  	frog.GetComponent<Animation>().CrossFade("Jump");
	 
	  }
			  if (GUI.Button(new Rect(380, 20, 90, 50), "Jump1")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Jump1");
	  }
			  if (GUI.Button(new Rect(470, 20, 90, 50), "Pickup")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Pickup");
	  }
	    if (GUI.Button(new Rect(560, 20, 90, 50), "Drawsword")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Default;
		  	frog.GetComponent<Animation>().CrossFade("Drawsword");
	  }
	    if (GUI.Button(new Rect(650, 20, 90, 50), "Sheathesword")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Sheathesword");
	  } 
		if (GUI.Button(new Rect(740, 20, 90, 50), "Attack-st")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Attackstanding");
	  }
		if (GUI.Button(new Rect(20, 80, 90, 50), "Attack01")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Attack01");
	  }
		if (GUI.Button(new Rect(110, 80, 90, 50), "Attack02")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Attack02");
	  }  
		if (GUI.Button(new Rect(200, 80, 90, 50), "Attack03-1")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Attack03-1");
	  }
		if (GUI.Button(new Rect(290, 80, 90, 50), "Attack03-2")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Attack03-2");
	  }
		if (GUI.Button(new Rect(380, 80, 90, 50), "Attack03-3")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Attack03-3");
	  }
		if (GUI.Button(new Rect(470, 80, 90, 50), "Skill01")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Skill01");
	  }
		if (GUI.Button(new Rect(560, 80, 90, 50), "Skill01-1")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Skill01-1");
	  }
		if (GUI.Button(new Rect(650, 80, 90, 50), "Damage")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Damage");
	  }
		if (GUI.Button(new Rect(740, 80, 90, 50), "DamageDodge")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Damagedodge");
			
	  }
		if (GUI.Button(new Rect(20, 140, 90, 50), "Stun")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Stun");
			
	  }
		if (GUI.Button(new Rect(110, 140, 90, 50), "Dead")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Dead");
			
	  }
		if (GUI.Button(new Rect(740, 400, 90, 50), "ver1.0")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("0");
	  }
 }
	
	// Update is called once per frame
	void Update () {
		
		//if(Input.GetMouseButtonDown(0)){
		
			//touchNum++;
			//touchDirection="forward";
		 // transform.position = new Vector3(0, 0,Mathf.Lerp(minimum, maximum, Time.time));
			//Debug.Log("touchNum=="+touchNum);
		//}
		/*
		if(touchDirection=="forward"){
			if(Input.touchCount>){
				touchDirection="back";
			}
		}
	*/
		 
		//transform.position = Vector3(Mathf.Lerp(minimum, maximum, Time.time), 0, 0);
	if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
		//frog.transform.Rotate(Vector3.up * Time.deltaTime*30);
	}
	
}
