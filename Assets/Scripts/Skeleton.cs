using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Basic swordsman & archer
public class Skeleton : Enemy {

	public enum Type {
		Swordsman, Archer
	}

	public Type skeletonType;
	public float minArcherDistance = 5f; //prevents archer from going too close
	public float mintimeBetweenMovementStateChange = 1f;
	public float maxtimeBetweenMovementStateChange = 3f;

	private float movementDet; //float that determines the current movement in Move
	private float changeMovementTimer;
	private Vector3 randMovementDirection;
	private Rigidbody rigid;
	private GameObject targetPlayer;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody> ();
		ChangeMove ();
		if (skeletonType == Type.Archer) {
			tryAttackRange = minArcherDistance + 2f;
		}
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if (DetectPlayers ().Count != 0) {
			Move ();
			TryAttack ();
		}
	}

	//called by parent from TryAttack(), DO NOT CALL MANUALLY!
	protected override void Attack() {
		print ("Attack!");
	}

	void Move() {
		
		if (movementDet < 0.3f) {
			//move randomly
			// TODO: handle archer
			rigid.velocity = randMovementDirection * movementSpeed;
		} else if (movementDet > 0.3f && movementDet < 0.6f) {
			//move toward player
			rigid.velocity = Vector3.zero;
			if (targetPlayer == null) {
				ChangeMove ();
				return;
			}
			//if archer, don't go as close so stop calling move towards if dist at minArcherDistance
			if (!(skeletonType == Type.Archer && Vector3.Distance (transform.position, targetPlayer.transform.position) <= minArcherDistance))
				MoveTowardsObject (targetPlayer);
		} else {
			// stay stationary, look at player
			transform.LookAt(targetPlayer.transform.position);
			rigid.velocity = Vector3.zero;
		}
		changeMovementTimer -= Time.fixedDeltaTime;
		if (changeMovementTimer < 0) {
			ChangeMove ();
		}
	}

	void ChangeMove() {
		// move in either a random direction, towards a player, or remain stationary
		movementDet = Random.value;
		//movementDet = 0.6f;
		randMovementDirection = new Vector3 (Random.value, 0, Random.value);
		targetPlayer = FindClosestPlayer (DetectPlayers ());
		changeMovementTimer = Random.Range (mintimeBetweenMovementStateChange, maxtimeBetweenMovementStateChange);
	}


}
