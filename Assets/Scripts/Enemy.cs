using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float movementSpeed;

	protected GameObject[] players;

	void Awake() {
		players = GameObject.FindGameObjectsWithTag ("Life");
	}

	protected void MoveTowardsObject(GameObject P) {
		if (P == null)
			return;
		float step = movementSpeed * Time.fixedDeltaTime;
		transform.position =  Vector3.MoveTowards (transform.position, P.transform.position, step);
		//print (P.transform.position);
		transform.LookAt (P.transform.position);
	}

	protected GameObject FindClosestPlayer() {
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
}
