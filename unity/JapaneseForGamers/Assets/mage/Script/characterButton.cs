using UnityEngine;
using System.Collections;

public class characterButton : MonoBehaviour {

	public GameObject frog;
	public GUISkin customSkin;

	
	
	private Rect FpsRect ;
	private string frpString;


	

	void Start () 
	{
	
			}
	
 void OnGUI() 
	{
		GUI.skin = customSkin;

		GUI.Box (new Rect (0, 0, 440, 115),"");
		
		if (GUI.Button(new Rect(30, 20, 70, 30),"BattleStay")){
		 frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("pc_battlestay");
	  }
//		if (GUI.Button(new Rect(105, 20, 70, 30),"Walk")){
//		 frog.animation.wrapMode= WrapMode.Loop;
//		  	frog.animation.CrossFade("SKG_Walk");
//	  }
		if (GUI.Button(new Rect(105, 20, 70, 30),"Run")){
		 frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("pc_run");
	  }
		
		if (GUI.Button(new Rect(180, 20, 70, 30),"Attack1")){
		 frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
			frog.GetComponent<Animation>().CrossFade("pc_atk01");
	  }
		 if (GUI.Button(new Rect(255, 20, 70, 30),"Skill01")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
			frog.GetComponent<Animation>().CrossFade("pc_skill_01");
			//effect.animation.CrossFade ("test");
	  }
		if (GUI.Button(new Rect(330, 20, 70, 30),"Casting")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
			frog.GetComponent<Animation>().CrossFade("pc_skill_01_casting");
			//effect.animation.CrossFade ("test");
	  }
//		   if (GUI.Button(new Rect(405, 20, 70, 30),"RushAtk")){
//		  frog.animation.wrapMode= WrapMode.Loop;
//			frog.animation.CrossFade("PC_rushatk");
//	  }

//	     if (GUI.Button(new Rect(480, 20, 70, 30),"ShieldAtk")){
//		  frog.animation.wrapMode= WrapMode.Loop;
//			frog.animation.CrossFade("PC_shieldatk");
//	  } 
//		if (GUI.Button(new Rect(555, 20, 70, 30),"Combo1_1")){
//		  frog.animation.wrapMode= WrapMode.Once;
//			frog.animation.CrossFade("CG_Combo1_1");
//	  }
//		if (GUI.Button(new Rect(630, 20, 70, 30),"Combo1_2")){
//		  frog.animation.wrapMode= WrapMode.Once;
//			frog.animation.CrossFade("CG_Combo1_2");
//
//
//
//	  }
//		if (GUI.Button(new Rect(705, 20, 70, 30),"Combo1_3")){
//		 frog.animation.wrapMode= WrapMode.Once;
//			frog.animation.CrossFade("CG_Combo1_3");
//	  }
		
//		if (GUI.Button(new Rect(780, 20, 70, 30),"Skill")){
//		 frog.animation.wrapMode= WrapMode.Once;
//		  	frog.animation.CrossFade("CG_Skill");
//	  }
		if (GUI.Button(new Rect(30, 60, 70, 30),"Buff")){
		 frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
			frog.GetComponent<Animation>().CrossFade("pc_skill_buff");
	  }
		
		if (GUI.Button(new Rect(105, 60, 70, 30),"Fireball")){
		 frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
			frog.GetComponent<Animation>().CrossFade("pc_skill_fireball");
	  }
		
		
		
	    if (GUI.Button(new Rect(180, 60, 70, 30),"Stun")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("pc_stun");
	
	  }
		if (GUI.Button(new Rect(255, 60, 70, 30),"Damage")){
			frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
			frog.GetComponent<Animation>().CrossFade("pc_damage");
			
		}
		if (GUI.Button(new Rect(330, 60, 70, 30),"Dead")){
			frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
			frog.GetComponent<Animation>().CrossFade("pc_dead");
			
		}
//		if (GUI.Button(new Rect(405, 60, 70, 30),"Dead")){
//			frog.animation.wrapMode= WrapMode.Once;
//			frog.animation.CrossFade("PC_dead");
//			
//		}
//		   if (GUI.Button(new Rect(480, 60, 70, 30),"Damage")){
//		  frog.animation.wrapMode= WrapMode.Loop;
//		  	frog.animation.CrossFade("CG_Damage");
//	  }
//			   if (GUI.Button(new Rect(555, 60, 70, 30),"Damage1")){
//		  frog.animation.wrapMode= WrapMode.Loop;
//		  	frog.animation.CrossFade("CG_Damage1");
//	  }
//			   if (GUI.Button(new Rect(630, 60, 70, 30),"Death")){
//		  frog.animation.wrapMode= WrapMode.Once;
//		  	frog.animation.CrossFade("CG_Death");
//	  }
	    
				if (GUI.Button (new Rect (20, 820, 140, 40), "Ver 1.0")) {
						frog.GetComponent<Animation>().wrapMode = WrapMode.Loop;
						frog.GetComponent<Animation>().CrossFade ("CG_Idle");
				}

	
		
 }
	
	// Update is called once per frame
	void Update () 
	{
		
	
	if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

	}





	
}
