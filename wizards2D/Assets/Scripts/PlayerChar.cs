﻿using UnityEngine;
using System;

public class PlayerChar : MonoBehaviour {
	public int hp; //the player's current hp
	public int mana; //the player's current mana
	public int es; //"0" if no element is stored, 1=fire, 2=water, 3=earth, 4=wind
	private int maxHp; //player's max hp
	private int maxMana; //player's max mana
	public float moveForce = 40f; // Amount of force added to move the player left and right.
	public float maxSpeed = 5f;	// The fastest the player can travel in the x axis.
	public bool jump; //true if player is currently jumping?
	public int dotVal; //damage taken per second
	public int dotT; //"0" when player is not burned or poisoned, else duration of DoT
	public bool isDead; //true if player is dead, else false
	public int stunT; //"0" when player is not stunned, else duration of stun remaining
	public bool facingRight; //true if sprite is facing right
	private bool block; //true if player is currenty blocking
	public int playerNum; //the player and controller number
	public Animator anim; //

	//instantiate new instance of player char. @param isP1 determines start location
	public PlayerChar() {
		maxHp = 100;
		maxMana = 100;
		es = 0;
//		if (isP1) {
//			xPos = 5;
//			yPos = 5;
//		} 
//		else {
//			xPos = 15;
//			yPos = 5;
//		}
		jump=false;
		dotVal=0;
		hp=maxHp;
		mana=maxMana;
		isDead=false;
		stunT=0;
		playerNum = 1;
	}

	public void Awake () {
		anim = this.GetComponent<Animator> ();
		//anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	public void Update () {
	
	}

	// FixedUpdate is called once per physics step 
	public void FixedUpdate () {
		// Cache the contoller input input.
		float rawHorizontal = Input.GetAxis ("Player"+playerNum+"_Move_Horizontal_Mac");
		float rawVertical = Input.GetAxis ("Player" + playerNum + "_Move_Vertical_Mac");
		Vector3 direction = new Vector3(rawHorizontal, 0f, rawVertical);
		float speed = (direction).magnitude;

		
//		Debug.Log ("Test speed: "+speed);
 		if((speed * rigidbody.velocity).magnitude < maxSpeed)
			rigidbody.AddForce (direction * moveForce);

//		if (rigidbody.velocity.magnitude > maxSpeed)
//						rigidbody.velocity = direction * maxSpeed;

		anim.SetFloat("Speed", speed*5);
	}

	//check if the amount of incoming damage is greater than the player's current hp. if so, player is die
	//else, subtract player's hp by damage
	public void takeDamage(int dmg, Spell s){
		int bd;
		if(!block)
			bd=dmg;
		else if(s is Slash && block)
			bd=dmg/2;
		else
			bd=dmg-(dmg/4);
		if (bd>=hp)
			isDead=true;
		else
			hp-=bd;
		if(s.getDotB()){
			dotVal=s.getDotV();
			dotT=s.getDotT();
		}
	}
	
	public void setBlock(bool b){block=b;}

	public bool getBlock(){return block;}
		
	public void stunned(int d){
		if(d<=stunT)
			return;
		else{
			stunT=d;
		}
	}

}
