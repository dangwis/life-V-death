using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Basic chasing enemy
public class Snake : Enemy {

	List<GameObject> players;
	GameObject targetPlayer;

	// Use this for initialization
	/*void Start () {
		InvokeRepeating ("ChangeTarget", 0f, 1f);
	}*/
	
	// Update is called once per frame
	/*protected override void Update () {
		base.Update ();
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		if (targetPlayer == null)
			ChangeTarget ();
		MoveTowardsObject (targetPlayer);
		//snake has no attack, just moves toward play to do dmg
	}*/

	/*void ChangeTarget() {
	//	players = DetectPlayers ();
		targetPlayer = FindClosestPlayer (players);
	}*/
}
