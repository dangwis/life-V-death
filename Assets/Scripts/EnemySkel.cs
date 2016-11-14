using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySkel : MonoBehaviour {

    public float detectRange = 5f;
    public float attackRange = 1f;
    public float moveSpeed = 1f;
    public float health = 3f;
    int _state = 0; // 0 = idle, 1 = walking, 2 = attacking
    Animator skelAnimator;
    public GameObject foreArm;
    public CapsuleCollider forearmCollider;


	// Use this for initialization
	void Start () {
        skelAnimator = GetComponent<Animator>();
        forearmCollider.enabled = (false);
	}

    void FixedUpdate() {
        if (state == 3 || state == 4) {
            if (health <= 0) {
                state = 4;
            }
            return;
        }

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
                Vector3 vec1 = transform.position, vec2 = player.transform.position;
                vec1.y = 1.6f;
                vec2.y = 1.6f;
                transform.position = Vector3.MoveTowards(vec1, vec2, moveSpeed * Time.fixedDeltaTime);
                transform.LookAt(vec2);
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

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.layer == 10) {
            // collision with lifeweapon
            if (col.gameObject.tag == "Arrow") {
                health--;
            } else if (col.gameObject.tag == "Hammer") {
                health -= 3;
            } else if (col.gameObject.tag == "Sword") {
                health -= 1.5f;
            }

            state = 3;
        }
    }

    void GotHit() {
        if (state != 4) {
            state = 0;
        }
    }

    void StartDeath() {
        Destroy(this.gameObject);
    }

    void ShowDamage() {
        transform.Find("SM_Skeleton_Base").GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 1f);
        Invoke("FinishDamage", 0.5f);
    }

    void FinishDamage() {
        transform.Find("SM_Skeleton_Base").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
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
                case 3: // hit
                    skelAnimator.SetInteger("State", 3);
                    Invoke("GotHit", 1.33f / 2f);
                    ShowDamage();
                    break;
                case 4: // dead
                    skelAnimator.SetInteger("State", 4);
                    Invoke("StartDeath", 3f);
                    break;
            }

            if (value == 2) {
                forearmCollider.enabled = (true);
            } else {
                forearmCollider.enabled = (false);
            }
            _state = value;
        }
    }
}
