using UnityEngine;
using System.Collections;

public class LifePlayer : MonoBehaviour {

    CharacterController charController;
    public float speed;
    public int playerNum;
    public bool hasWeapon;
    public bool canPickupWeapon;
    public int pickupType = 0;
    public int weapontype = 0;
    public GameObject weaponPickupObj;
    public bool attacking = false;
    public bool attackFinishing = false;
    public Animator lifeAnimator;

    public GameObject sword;
    public Quaternion swordStart;
    public GameObject arrowPrefab;
    public GameObject arrow;
    public GameObject bow;
    public Quaternion bowStart;
    public GameObject hammer;
    public Quaternion hammerStart;

    public float cooldown = 1.5f;
    private float lastattacktime;

    public int health = 100;
    public float weaponTime = 0;
    public int _state = 0;
    public bool canTakeDamage = true;
    public Material playerMat;

    // Use this for initialization
    void Start () {
        charController = this.transform.GetComponent<CharacterController>();
        hasWeapon = false;
        lastattacktime = Time.time;
        sword = transform.Find("Weapon Sword").gameObject;
        swordStart = sword.transform.localRotation;
        hammer = transform.Find("Weapon Hammer").gameObject;
        hammerStart = hammer.transform.localRotation;
        bow = transform.Find("Weapon Bow").gameObject;
        arrow = bow.transform.Find("Arrow").gameObject;
        bowStart = bow.transform.localRotation;
        sword.SetActive(false);
        hammer.SetActive(false);
        bow.SetActive(false);
        arrow.SetActive(false);
        lifeAnimator = transform.Find("body").GetComponent<Animator>();
	}

	void OnDestory() {
		
	}
	
    void Update() {
        if (state == 2 || state == 3) {
            if (health <= 0) {
                state = 3;
            }
            return;
        }

        // Picking up your weapon
        if (Input.GetButtonDown(XInput.XboxA(playerNum)) && canPickupWeapon && !hasWeapon && weapontype == 0) {
            Destroy (weaponPickupObj);
            hasWeapon = true;
            canPickupWeapon = false;
            weapontype = pickupType;
            switch (weapontype) {
                case 1:
                    sword.SetActive(true);
                    break;
                case 2:
                    hammer.SetActive(true);
                    break;
                case 3:
                    bow.SetActive(true);
                    arrow.SetActive(true);
                    break;
            }
        }

        // Attack Handlers
        if (XInput.x.RTDown(playerNum) && hasWeapon && !attacking) {
            attacking = true;
        }

        // Set state
        if (state != 3 && state != 4) {
            if (Mathf.Abs(Input.GetAxis(XInput.XboxLStickX(playerNum))) > 0.1f || Mathf.Abs(Input.GetAxis(XInput.XboxLStickY(playerNum))) > 0.1f) {
                state = 1;
            } else {
                state = 0;
            }
        }
    }

    void SwordAttack() {
        if (attackFinishing) return;
        sword.layer = 10;
        weaponTime += Time.fixedDeltaTime;
        sword.transform.localRotation = Quaternion.Lerp(swordStart, Quaternion.Euler(0, -20, -90), 10 * weaponTime);
        if (sword.transform.localRotation == Quaternion.Euler(0, -20, -90)) {
            attackFinishing = true;
            sword.layer = 8;
            Invoke("FinishSword", 0.2f);
        }
    }

    void FinishSword() {
        attacking = false;
        attackFinishing = false;
        sword.transform.localRotation = swordStart;
        weaponTime = 0;
    }

    void HammerAttack() {
        if (attackFinishing) return;
        hammer.layer = 10;
        weaponTime += Time.fixedDeltaTime;
        hammer.transform.localRotation = Quaternion.Lerp(hammerStart, Quaternion.Euler(0, 0, 90), 10f * Mathf.Pow(weaponTime, 8));
        if (hammer.transform.localRotation == Quaternion.Euler(0, 0, 90)) {
            attackFinishing = true;
            hammer.layer = 8;
            Invoke("FinishHammer", 0.25f);
        }
    }

    void FinishHammer() {
        attacking = false;
        attackFinishing = false;
        hammer.transform.localRotation = hammerStart;
        weaponTime = 0;
    }

    void BowAttack() {
        if (attackFinishing) return;
        attackFinishing = true;
        GameObject go = Instantiate<GameObject>(arrowPrefab);
        go.transform.position = bow.transform.position;
        go.transform.rotation = bow.transform.rotation;
        arrow.SetActive(false);
        Invoke("FinishBow", 0.5f);
    }

    void FinishBow() {
        attackFinishing = false;
        attacking = false;
        arrow.SetActive(true);
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (state == 2 || state == 3) {
            if (health <= 0) {
                state = 3;
            }
            return;
        }

        float Xinput = Input.GetAxis (XInput.XboxLStickX (playerNum));
        float Yinput = -Input.GetAxis(XInput.XboxLStickY(playerNum));
		Vector3 movementDir = new Vector3 (Xinput, 0, Yinput).normalized;
		if (movementDir != Vector3.zero) {
			charController.SimpleMove (movementDir * speed);
			transform.rotation = Quaternion.LookRotation (movementDir);
		}
        Xinput = Input.GetAxis(XInput.XboxRStickX(playerNum));
        Yinput = -Input.GetAxis(XInput.XboxRStickY(playerNum));
        if (Xinput != 0 || Yinput != 0) {
            Vector3 lookDir = new Vector3(Xinput, 0, Yinput).normalized;
            transform.rotation = Quaternion.LookRotation(lookDir);
        }

        // Call attack handlers
        if (attacking) {
            switch (weapontype) {
                case 1:
                    SwordAttack();
                    break;
                case 2:
                    HammerAttack();
                    break;
                case 3:
                    BowAttack();
                    break;
            }
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "PowerUp") {
            hasWeapon = true;
            Destroy(col.gameObject);
        } else if (col.tag == "Sword" && !hasWeapon) {
            canPickupWeapon = true;
            pickupType = 1;
            weaponPickupObj = col.gameObject;
        } else if (col.tag == "Hammer" && !hasWeapon) {
            canPickupWeapon = true;
            pickupType = 2;
            weaponPickupObj = col.gameObject;
        } else if (col.tag == "Bow" && !hasWeapon) {
            canPickupWeapon = true;
            pickupType = 3;
            weaponPickupObj = col.gameObject;
        } else if (col.tag == "LifeFountain") {
            Debug.Log("You found the fountain of youth!");
        } else if (col.tag == "Skeleton" && col.gameObject.layer == 12 && state != 2 && state != 3) {
            state = 2;
            health -= 20;
        } else if (col.tag == "Minotaur" && col.gameObject.layer == 12 && state != 2 && state != 3) {
            state = 2;
            health -= 30;
            Debug.Log("Test");
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.tag == "Hammer") {
            canPickupWeapon = false;
        } else if (col.tag == "Bow") {
            canPickupWeapon = false;
        } else if (col.tag == "Sword") {
            canPickupWeapon = false;
        }
    }

    void GotHit() {
        if (state != 3) {
            state = 0;
        }
    }
    
    void SetDamage() {
        canTakeDamage = true;
    }

    void ShowDamage() {
        transform.Find("body").transform.Find("mesh_1").GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 1f);
        Invoke("FinishDamage", 0.5f);
    }

    void FinishDamage() {
        transform.Find("body").transform.Find("mesh_1").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
    }

    void Death() {
        Destroy(this.gameObject);
    }

    public int state {
        get { return _state; }
        set {
            if (value == _state) return;

            switch (value) {
                case 0: // idle
                    lifeAnimator.SetInteger("State", 0);
                    break;
                case 1: // walking
                    lifeAnimator.SetInteger("State", 1);
                    break;
                case 2: // hit
                    lifeAnimator.SetInteger("State", 2);
                    Invoke("GotHit", 0.33f);
                    Invoke("SetDamage", 1.5f);
                    canTakeDamage = false;
                    ShowDamage();
                    break;
                case 3: // death
                    lifeAnimator.SetInteger("State", 3);
                    Invoke("Death", 3f);
                    break;
            }
            _state = value;
        }
    }
}
