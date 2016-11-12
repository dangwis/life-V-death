using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmenySkel : MonoBehaviour {

    public float detectRange = 5f;
    public float attackRange = 1f;
    public float moveSpeed = 1f;
    int _state = 0; // 0 = idle, 1 = walking, 2 = attacking
    Animator skelAnimator;


	// Use this for initialization
	void Start () {
        skelAnimator = GetComponent<Animator>();
	}

    void FixedUpdate() {
        // Check if can attack
        List<GameObject> nearby = Enemy.DetectPlayers(this.gameObject, attackRange);
        
        if (nearby.Count == 0) {
            // Check if can move
            nearby = Enemy.DetectPlayers(this.gameObject, detectRange);

            if (nearby.Count == 0) { // Idle
                state = 0;
            } else { // Walk
                state = 1;
            }

        } else { // Attack
            state = 2;
        }

        // Get the closest player
        GameObject player = Enemy.FindClosestPlayer(this.gameObject, nearby);

        switch (state) {
            case 0: // Idle
                // Do nothing?
                break;
            case 1: // Walking towards closest player
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.fixedDeltaTime);
                transform.LookAt(player.transform.position);
                break;
            case 2: // Attacking closest player
                Vector3 vec = player.transform.position - transform.position;
                vec.y = 0;
                Quaternion rotation = Quaternion.LookRotation(vec);
                rotation *= Quaternion.Euler(0, -45, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime);
                break;
        }
    }

    public int state {
        get { return _state; }
        set {
            if (value == _state) return;

            switch (value) {
                case 0: // idle
                    skelAnimator.SetInteger("State", 0);
                    break;
                case 1: // walking
                    skelAnimator.SetInteger("State", 1);
                    break;
                case 2: // attacking
                    skelAnimator.SetInteger("State", 2);
                    break;
            }
            _state = value;
        }
    }
}
