using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snake : Enemy {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		List<GameObject> players = DetectPlayers ();
		GameObject targetPlayer = FindClosestPlayer (players);
		MoveTowardsObject (targetPlayer);
	}
}
