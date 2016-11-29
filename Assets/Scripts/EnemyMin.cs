using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemyMin : MonoBehaviour {

    public float detectRange = 5f;
    public float attackRange = 1f;
    public float moveSpeed = 1f;
    int _state = 0; // 0 = idle, 1 = running, 2 = death
    Animator minAnimator;
    Vector3 RunDir;
    bool canRun = false;
    bool isRunning = false;
    public float health = 6f;

	public GameObject popupNotificationPrefab;
	private GameObject activePopup; //health bar
	private Vector3 popupPosition;
	private float maxHealth;

    // Use this for initialization
    void Start () {
        Invoke("SetCanRun", 1.367f);
        minAnimator = GetComponent<Animator>();
		ShowPopupNotification ("", true);
		UpdatePopupNotification ("", 1);
		maxHealth = health;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (health <= 0 && state != 2) {
            state = 2;
        }

        // Check if can run
        List<GameObject> nearby = Enemy.DetectPlayers(this.gameObject, detectRange);

        if (nearby.Count != 0 && canRun && state == 0) { // Run
            // Get the closest player
            GameObject player = Enemy.FindClosestPlayer(this.gameObject, nearby);

            if (Mathf.Abs((player.transform.position - transform.position).x) > Mathf.Abs((player.transform.position - transform.position).z)) {
                if ((player.transform.position - transform.position).x < 0) { // left
                    RunDir.x = -1;
                    RunDir.z = 0;
                } else { // right
                    RunDir.x = 1;
                    RunDir.z = 0;
                }
            } else {
                if ((player.transform.position - transform.position).z < 0) { // down
                    RunDir.x = 0;
                    RunDir.z = -1;
                } else { // up
                    RunDir.x = 0;
                    RunDir.z = 1;
                }
            }

            state = 1;
            canRun = false;
        }

        switch (state) {
            case 0: // Idle
                // Do nothing?
                break;
            case 1: // Running towards run direction
                if (isRunning) {
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + RunDir, moveSpeed * Time.fixedDeltaTime);
                }
                transform.LookAt(transform.position + RunDir);
                break;
            case 2: // Death
                // Do nothing?
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

    void FinishDeath() {
        Death.S.DecrementBigEnemy();
		RemovePopupNotification ();
        Destroy(this.gameObject);
    }

    public int state {
        get { return _state; }
        set {
            if (value == _state || state == 2) return;

            switch (value) {
                case 0: // idle
                    Invoke("SetCanRun", 1);
                    minAnimator.SetInteger("State", 0);
                    break;
                case 1: // walking
                    Invoke("SetIsRunning", 1.267f);
                    minAnimator.SetInteger("State", 1);
                    break;
                case 2: // death
                    minAnimator.SetInteger("State", 2);
                    transform.GetComponent<CapsuleCollider>().enabled = false;
                    Invoke("FinishDeath", 3f);
                    break;
            }
            _state = value;
        }
    }

    void SetCanRun() {
        canRun = true;
    }

    void SetIsRunning() {
        isRunning = true;
    }

    void ShowDamage() {
        transform.Find("mesh_1").GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 1f);
        Invoke("FinishDamage", 0.5f);
    }

    void FinishDamage() {
        transform.Find("mesh_1").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
    }

	public void ShowPopupNotification(string txt, bool showBar = false) {
		Vector3 pos = transform.position;
		pos.y = 6;
		pos.z += 2;
		Destroy (activePopup);
		activePopup = Instantiate (popupNotificationPrefab, pos, popupNotificationPrefab.transform.rotation, transform.parent) as GameObject;
		activePopup.transform.FindChild ("Panel").FindChild ("Text").GetComponent<TextMesh> ().text = txt;
		activePopup.transform.FindChild ("Panel").FindChild ("Slider").gameObject.SetActive (showBar);
		popupPosition = pos;
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
        if (col.gameObject.tag == "Wall" && state != 0) {
            state = 0;
            isRunning = false;

            // Bounce back
            Vector3 vec = transform.position;
            if (RunDir.x == -1) { // Bounce right
                vec.x += 1f;
            } else if (RunDir.x == 1) { // Bounce left
                vec.x -= 1f;
            } else if (RunDir.z == -1) { // Bounce up
                vec.z += 1f;
            } else if (RunDir.z == 1) { // Bounce down
                vec.z -= 1f;
            }
            transform.position = vec;
        }

        if (col.gameObject.layer == 10 && state != 2) {
            // collision with lifeweapon
            if (col.gameObject.tag == "Arrow") {
                health--;
            } else if (col.gameObject.tag == "Hammer") {
                health -= 3;
            } else if (col.gameObject.tag == "Sword") {
                health -= 1.5f;
            }
			UpdatePopupNotification ("", health / maxHealth);
            ShowDamage();
        }
    }
}
