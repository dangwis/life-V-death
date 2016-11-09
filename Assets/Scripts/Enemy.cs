using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	public float movementSpeed = 1f;
	public float detectionRange = 5f;
	public float tryAttackRange = 2f;
	public float maxAttackCooldown = 3f;

	protected float _attackCooldown;

	void Awake() {
		_attackCooldown = Random.Range (0, maxAttackCooldown);
	}

	protected virtual void Update() {
		
	}

	protected List<GameObject> DetectPlayers() {
		// Detect players
		Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange, LayerMask.GetMask("Life"));
		List<GameObject> players = new List<GameObject>();
		foreach (Collider hit in hits) {
			if (hit.gameObject.tag == "Life") {
				players.Add (hit.gameObject);
			}
		}
		return players;
	}

	protected void MoveTowardsObject(GameObject P) {
		if (P == null)
			return;
		float step = movementSpeed * Time.fixedDeltaTime;
		transform.position =  Vector3.MoveTowards (transform.position, P.transform.position, step);
		//print (P.transform.position);
		transform.LookAt (P.transform.position);
	}

	protected GameObject FindClosestPlayer(List<GameObject> players) {
		float best = 0;
		GameObject bestP = null;
		foreach (GameObject p in players) {
			float dist = Vector3.Distance (transform.position, p.transform.position);
			if (dist > best) {
				best = dist;
				bestP = p;
			}
		}
		return bestP;
	}

	protected virtual void Attack () {
		// Do nothing by default
	}

	protected void TryAttack() {
		//if close enough to a life, lower attack time until 0, then attack
		RaycastHit hit;
		if (Physics.SphereCast (transform.position, 3f, transform.forward, out hit, tryAttackRange, LayerMask.GetMask("Life"))) {
			_attackCooldown -= Time.fixedDeltaTime;
			if (_attackCooldown < 0) {
				Attack ();
			}
		}

	}

	protected void MeleeAttack(GameObject weapon) {
		//spawn weapon and perform a simple melee attack (probably have weapons contain attack animation)
	}

	protected void RangedAttack(GameObject sourceWeapon, GameObject projectile) {
		//spawn sourceWeapon and shoot projectile (probably handle projectile firing in a projectile class)
	}
}
