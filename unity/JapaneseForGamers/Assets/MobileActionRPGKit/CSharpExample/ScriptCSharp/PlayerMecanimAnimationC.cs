﻿using UnityEngine;
using System.Collections;


[RequireComponent (typeof(AttackTriggerC))]

public class PlayerMecanimAnimationC : MonoBehaviour {

	private GameObject player;
	private GameObject mainModel;
	public Animator animator;
	private CharacterController controller;
	
	public string moveHorizontalState = "horizontal";
	public string moveVerticalState = "vertical";
	public string jumpState = "jump";
	public string hurtState = "hurt";
	private bool jumping = false;
	private bool attacking = false;
	private bool flinch = false;
	
	GameObject joyStick;
	
	void Start() {
		if(!player){
			player = this.gameObject;
		}
		if(!joyStick){
			joyStick = GameObject.FindWithTag("JoyStick");
		}
		mainModel = GetComponent<AttackTriggerC>().mainModel;
		if(!mainModel){
			mainModel = this.gameObject;
		}
		if(!animator){
			animator = mainModel.GetComponent<Animator>();
		}
		controller = player.GetComponent<CharacterController>();
		
	}
	
	void Update () {
		//Set attacking variable = isCasting in AttackTrigger
		attacking = GetComponent<AttackTriggerC>().isCasting;
		flinch = GetComponent<StatusC>().flinch;
		//Set Hurt Animation
		animator.SetBool(hurtState , flinch);
		
		if(attacking || flinch){
			return;
		}

		float h = 0.0f;
		float v = 0.0f;

		if ((controller.collisionFlags & CollisionFlags.Below) != 0){
			if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")){
				h = Input.GetAxis("Horizontal");
				v = Input.GetAxis("Vertical");
			}else if(joyStick){
				h = joyStick.GetComponent<MobileJoyStickC>().position.x;
				v = joyStick.GetComponent<MobileJoyStickC>().position.y;
			}
			
			animator.SetFloat(moveHorizontalState , h);
			animator.SetFloat(moveVerticalState , v);
			if(jumping){
				jumping = false;
				animator.SetBool(jumpState , jumping);
				//animator.StopPlayback(jumpState);
			}
			
		}else{
			jumping = true;
			animator.SetBool(jumpState , jumping);
			//animator.Play(jumpState);
		}
		
	}
	
	public void AttackAnimation(string anim){
		animator.SetBool(jumpState , false);
		animator.Play(anim);
	}
	
	public void PlayAnim(string anim){
		animator.Play(anim);		
	}
	
	
	public void SetWeaponType(int val, string idle){
		animator.SetInteger("weaponType" , val);
		animator.Play(idle);
	}

}