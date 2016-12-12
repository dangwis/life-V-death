using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemySkel : MonoBehaviour {

    public float detectRange = 5f;
    public float attackRange = 1f;
    public float moveSpeed = 1f;
    public float health = 3f;
    public float healthDecay;
    int _state = 0; // 0 = idle, 1 = walking, 2 = attacking
    Animator skelAnimator;
    public GameObject foreArm;
    public CapsuleCollider forearmCollider;

	public GameObject popupNotificationPrefab;
	private GameObject activePopup; //health bar
	private float maxHealth;
    List<GameObject> attackNearby = new List<GameObject>(), moveNearby = new List<GameObject>();
    GameObject nearbyPlayer;
    public float checkRate = 0.1f;


    // Use this for initialization
    void Start () {
        skelAnimator = GetComponent<Animator>();
        forearmCollider.enabled = (false);
		ShowPopupNotification ("", true);
		UpdatePopupNotification ("", 1);
		maxHealth = health;
        InvokeRepeating("CheckNearby", checkRate, checkRate);
    }

    void CheckNearby() {
        // Check if can attack
        attackNearby = Enemy.DetectPlayers(this.gameObject, attackRange);
        moveNearby = Enemy.DetectPlayers(this.gameObject, detectRange);
    }

    void FixedUpdate() {
        if (GameManager.S.gameStart)
        {
            health -= healthDecay;
            UpdatePopupNotification("", health / maxHealth);
            if (health <= 0)
            {
                state = 4;
                return;
            }
        }
        if (state == 3 || state == 4) {
            if (health <= 0) {
                state = 4;
            }
            return;
        }
        
        if (attackNearby.Count == 0) {
            // Check if can move
            nearbyPlayer = Enemy.FindClosestPlayer(this.gameObject, moveNearby);
            if (moveNearby.Count == 0) { // Idle
                state = 0;
            } else { // Walk
                state = 1;
            }

        } else { // Attack
            nearbyPlayer = Enemy.FindClosestPlayer(this.gameObject, attackNearby);
            state = 2;
        }

        switch (state) {
            case 0: // Idle
                // Do nothing?
                break;
            case 1: // Walking towards closest player
                Vector3 vec1 = transform.position, vec2 = nearbyPlayer.transform.position;
                vec1.y = 1.6f;
                vec2.y = 1.6f;
                transform.position = Vector3.MoveTowards(vec1, vec2, moveSpeed * Time.fixedDeltaTime);
                transform.LookAt(vec2);
                break;
            case 2: // Attacking closest player
                Vector3 vec = nearbyPlayer.transform.position - transform.position;
                vec.y = 0;
                Quaternion rotation = Quaternion.LookRotation(vec);
                rotation *= Quaternion.Euler(0, -45, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime);
                break;
        }

		//fix health bar above head
		if (activePopup != null) {
			activePopup.transform.rotation = popupNotificationPrefab.transform.rotation;
			Vector3 pos = transform.position;
			pos.y = 2;
			pos.z += 2;
			activePopup.transform.position = pos;
		}
    }

	public void ShowPopupNotification(string txt, bool showBar = false) {
		Vector3 pos = transform.position;
		pos.y = 6;
		pos.z += 2;
		Destroy (activePopup);
		activePopup = Instantiate (popupNotificationPrefab, pos, popupNotificationPrefab.transform.rotation, transform.parent) as GameObject;
		activePopup.transform.FindChild ("Panel").FindChild ("Text").GetComponent<TextMesh> ().text = txt;
		activePopup.transform.FindChild ("Panel").FindChild ("Slider").gameObject.SetActive (showBar);
	}

	public void UpdatePopupNotification(string txt, float barVal = 0) {
		activePopup.transform.FindChild ("Panel").FindChild ("Text").GetComponent<TextMesh> ().text = txt;
		activePopup.transform.FindChild ("Panel").FindChild ("Slider").GetComponent<Slider> ().value = barVal;
	}

	void RemovePopupNotification() {
		Debug.Log ("remove popup");
		Destroy (activePopup);
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

            if (health > 0 ) {
                GameObject.Find("Audio").transform.Find("SkelHit").GetComponent<AudioSource>().Play();
            }

            UpdatePopupNotification ("", health / maxHealth);
            state = 3;
        }
    }

    void GotHit() {
        if (state != 4) {
            state = 0;
        }
    }

    void StartDeath() {
        Death.S.DecrementBigEnemy();
		RemovePopupNotification ();
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
                    GameObject.Find("Audio").transform.Find("SkelAttack").GetComponent<AudioSource>().Play();
                    break;
                case 3: // hit
                    skelAnimator.SetInteger("State", 3);
                    Invoke("GotHit", 1.33f / 2f);
                    ShowDamage();
                    break;
                case 4: // dead
                    skelAnimator.SetInteger("State", 4);
                    GameObject.Find("Audio").transform.Find("SkelDeath").GetComponent<AudioSource>().Play();
                    Invoke("StartDeath", 3f);
                    transform.GetComponent<CapsuleCollider>().enabled = false;
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
