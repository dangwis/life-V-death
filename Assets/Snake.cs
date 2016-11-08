using UnityEngine;
using System.Collections;

public class Snake : Enemy {

	GameObject targetPlayer = null;

	// Use this for initialization
	void Start () {
		InvokeRepeating("ChangeTarget", 0, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		MoveTowardsObject (targetPlayer);
	}

	void ChangeTarget() {
		targetPlayer = FindClosestPlayer ();
	}
}
