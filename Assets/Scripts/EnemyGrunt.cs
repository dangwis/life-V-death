using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Basic chasing enemy
public class EnemyGrunt : MonoBehaviour {

	public float health = 1f;
	public float movementSpeed = 5f;
	public float detectRange = 5f;

	List<GameObject> players;
	GameObject targetPlayer;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("ChangeTarget", 0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		if (targetPlayer == null)
			ChangeTarget ();
		Enemy.MoveTowardsObject (this.gameObject, targetPlayer, movementSpeed);
		//snake has no attack, just moves toward play to do dmg
	}

	void ChangeTarget() {
		players = Enemy.DetectPlayers (this.gameObject, detectRange);
		targetPlayer = Enemy.FindClosestPlayer (this.gameObject, players);
	}
}
