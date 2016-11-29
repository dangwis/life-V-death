using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	/*
    
    public float movementSpeed = 1f;
	public float detectionRange = 5f;
	public float tryAttackRange = 2f;
	public float maxAttackCooldown = 3f;

	protected float _attackCooldown;

    */

	/*void Awake() {
		_attackCooldown = Random.Range (0, maxAttackCooldown);
	}*/

	/*protected virtual void Update() {
		
	}*/

	public static List<GameObject> DetectPlayers(GameObject enemy, float detectRange) {
		// Detect players
		Collider[] hits = Physics.OverlapSphere(enemy.transform.position, detectRange, LayerMask.GetMask("Life"));
		List<GameObject> players = new List<GameObject>();
		foreach (Collider hit in hits) {
			if (hit.gameObject.tag == "Life") {
                RaycastHit hitObj;
                //Debug.DrawRay(enemy.transform.position, hit.gameObject.transform.position - enemy.transform.position, Color.green);
                if (Physics.Raycast(enemy.transform.position, hit.gameObject.transform.position - enemy.transform.position, out hitObj, detectRange, LayerMask.GetMask("Life", "Wall")) && hitObj.transform.tag == "Life") {
                    players.Add(hit.gameObject);
                }
                //Debug.Log(hitObj.transform.tag);
			}
		}
		return players;
	}

	public static GameObject FindClosestPlayer(GameObject enemy, List<GameObject> players) {
		float best = 999999999;
		GameObject bestP = null;
		foreach (GameObject p in players) {
			float dist = Vector3.Distance (enemy.transform.position, p.transform.position);
			if (dist < best) {
				best = dist;
				bestP = p;
			}
		}
		return bestP;
	}

    public static void MoveTowardsObject(GameObject enemy, GameObject P, float movementSpeed) {
        if (P == null)
            return;
        float step = movementSpeed * Time.fixedDeltaTime;
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, P.transform.position, step);
        //print (P.transform.position);
        enemy.transform.LookAt(P.transform.position);
    }

    /*protected virtual void Attack () {
		// Do nothing by default
	}*/

    /*protected void TryAttack() {
		//if close enough to a life, lower attack time until 0, then attack
		RaycastHit hit;
		if (Physics.SphereCast (transform.position, 3f, transform.forward, out hit, tryAttackRange, LayerMask.GetMask("Life"))) {
			_attackCooldown -= Time.fixedDeltaTime;
			if (_attackCooldown < 0) {
				Attack ();
			}
		}

	}*/

    /*protected void MeleeAttack(GameObject weapon) {
		//spawn weapon and perform a simple melee attack (probably have weapons contain attack animation)
	}*/

    /*protected void RangedAttack(GameObject sourceWeapon, GameObject projectile) {
		//spawn sourceWeapon and shoot projectile (probably handle projectile firing in a projectile class)
	}*/
}
