﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using XboxCtrlrInput;

public class LifePlayer : MonoBehaviour {

	CharacterController charController;
    public float speed;
    public bool immortal = false;
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
    public GameObject shield;

    public GameObject popupNotificationPrefab;
	private GameObject activePopup;

    public float cooldown = 1.5f;
    private float lastattacktime;

    public int health = 100;
    public float weaponTime = 0;
    public int _state = 0;
    public bool canTakeDamage = true;
    public Material playerMat;

    bool disarming;
    bool releasedRT;
    float slowedAmount;
    public float disarmTime;
    float disarmTimeStart;
    GameObject disarmingTrap;
	public bool stunned = false;
    int totalReached;

	public XboxController controller;

    // Use this for initialization
    void Start () {
        totalReached = 0;
        charController = this.transform.GetComponent<CharacterController>();
        hasWeapon = false;
        lastattacktime = Time.time;
        //sword = transform.Find("Weapon Sword").gameObject;

        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.gameObject.name == "Weapon Sword")
            {
                    sword = child.gameObject;
            }
            else if (child.gameObject.name == "Weapon Hammer")
            {
                hammer = child.gameObject;
            }
            else if (child.gameObject.name == "Weapon Bow")
            {
                bow = child.gameObject;
            }
            else if (child.gameObject.name == "Weapon Shield")
            {
                shield = child.gameObject;
            }
        }


        swordStart = sword.transform.localRotation;
        //hammer = transform.Find("Weapon Hammer").gameObject;
        hammerStart = hammer.transform.localRotation;
        //bow = transform.Find("Weapon Bow").gameObject;
        arrow = bow.transform.Find("Arrow").gameObject;
        bowStart = bow.transform.localRotation;
        sword.SetActive(false);
        hammer.SetActive(false);
        bow.SetActive(false);
        arrow.SetActive(false);
        shield.SetActive(false);
        lifeAnimator = transform.Find("body").GetComponent<Animator>();
        disarming = false;
        releasedRT = true;
        immortal = false;
        slowedAmount = 1;
	}

	void OnDestroy() {

    }
	
    void Update() {
        if (state == 2 || state == 3) {
            if (health <= 0) {
                state = 3;
            }
            return;
        }
        if(totalReached < WinCondition.S.totalReached)
        {
            Invoke("ShowTeammateFoundFountain", 5f);
            totalReached = WinCondition.S.totalReached;
        }
        if(weaponTime > 1.0f && state > 3)
        {
            if(state == 4)
            {
                Invoke("FinishSword", 0.0f);
            }
            if (state == 5)
            {
                Invoke("FinishHammer", 0.0f);
            }
            if (state == 6)
            {
                Invoke("FinishBow", 0.0f);
            }

        }

        // Picking up your weapon
		if (XCI.GetButtonDown(XboxButton.A, controller) && canPickupWeapon && !hasWeapon && weapontype == 0) {
            Destroy (weaponPickupObj);
            hasWeapon = true;
            canPickupWeapon = false;
			ShowPopupNotification ("Press RT to attack");
			Invoke ("ChangePopTxtToCrates", 7f);
            weapontype = pickupType;
            switch (weapontype) {
                case 1:
                    sword.SetActive(true);
                    shield.SetActive(true);
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

		if (XCI.GetButton(XboxButton.A, controller))
        {

            if (!disarming)
            {
                disarmingTrap = IsNearTrap();

                if (disarmingTrap != null)
                {
                    disarmTimeStart = Time.time;
                    disarming = true;
                    //Debug.Log("disarming");
					ShowPopupNotification ("Disarming", true);
                }
                else
                {
                    //Debug.Log("not near trap");
                }        
            }
            else if(Vector3.Distance(disarmingTrap.transform.position, transform.position) < 3f)
            {
				//update notification
				float val = (Time.time - disarmTimeStart) / disarmTime;
				float div = 1/8f;
				string txt = "Disarming";
				if (val / div > 1 && val / div < 2) {
					txt = "Disarming.";
				} else if (val / div > 2 && val / div < 3) {
					txt = "Disarming..";
				} else if (val / div > 3 && val / div < 4) {
					txt = "Disarming...";
				} 
				// between 4 and 5 is "Disarming" which is default
				else if (val / div > 5 && val / div < 6) {
					txt = "Disarming.";
				} else if (val / div > 6 && val / div < 7) {
					txt = "Disarming..";
				} else if (val / div > 7 && val / div < 8) {
					txt = "Disarming..";
				}
				UpdatePopupNotification (txt, val);

                if(Time.time - disarmTimeStart >= disarmTime)
                {
                    disarmingTrap.GetComponent<DamageTrap>().Disarm();
                    disarming = false;
                    Debug.Log("Disarmed");
					RemovePopupNotification ();
                }
            }
            else
            {
                disarming = false;
                Debug.Log("Don't walk away");
				RemovePopupNotification ();
            }
		} else if (XCI.GetButtonUp(XboxButton.A, controller) && disarming) {
			disarming = false;
			RemovePopupNotification ();
		}

        // Attack Handlers
		if (XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0.8f && hasWeapon && !attacking && !disarming && releasedRT) {
            lastattacktime = Time.time;
            attacking = true;
            releasedRT = false;
        }
		if (XCI.GetAxis(XboxAxis.RightTrigger, controller) <= 0.8f && hasWeapon && !releasedRT)
        {
            releasedRT = true;
        }

        // Set state
            if (state != 3 && state != 4 && state != 5 && state != 6) {
			if (Mathf.Abs(XCI.GetAxis(XboxAxis.LeftStickX, controller)) > 0.1f || Mathf.Abs(XCI.GetAxis(XboxAxis.LeftStickY, controller)) > 0.1f) {
                state = 1;
            } else {
                state = 0;
            }
        }
    }

    GameObject IsNearTrap()
    {
        Collider[] traps = Physics.OverlapSphere(this.transform.position, 2f);
        for (int i = 0; i < traps.Length; i++)
        {
            if (traps[i].tag == "Trap")
            {
                if(traps[i].GetComponent<DamageTrap>().armed)
                    return traps[i].gameObject;
            }
        }
        return null;
    }

    void SwordAttack() {
        if (attackFinishing) return;
        state = 4;
        sword.layer = 10;
        weaponTime += Time.fixedDeltaTime;

        if (this.lifeAnimator.GetCurrentAnimatorStateInfo(0).IsName("anim_attack_1h (4)") &&
            this.lifeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f
            )
        {
            attackFinishing = true;
            GameObject.Find("Audio").transform.Find("AttackSword").GetComponent<AudioSource>().time = 0.1f;
            GameObject.Find("Audio").transform.Find("AttackSword").GetComponent<AudioSource>().Play();
            Invoke("FinishSword", 0.2f);
            this.lifeAnimator.SetInteger("State", 0);
        }
    }

    void FinishSword() {
        attacking = false;
        attackFinishing = false;
        weaponTime = 0;
        state = 0;
        sword.layer = 8;
    }

    void HammerAttack() {
        if (attackFinishing) return;
        state = 5;
        hammer.layer = 10;
        weaponTime += Time.fixedDeltaTime;

        if (this.lifeAnimator.GetCurrentAnimatorStateInfo(0).IsName("anim_attack_2h (5)") &&
            this.lifeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
        {
            attackFinishing = true;
            GameObject.Find("Audio").transform.Find("AttackHammer").GetComponent<AudioSource>().time = 0.1f;
            GameObject.Find("Audio").transform.Find("AttackHammer").GetComponent<AudioSource>().Play();
            Invoke("FinishHammer", 0.25f);
            this.lifeAnimator.SetInteger("State", 0);
        }
    }

    void FinishHammer() {
        attacking = false;
        attackFinishing = false;
        weaponTime = 0;
        state = 0;
        hammer.layer = 8;
    }

    void BowAttack() {
        if (attackFinishing) return;
        state = 6;
        weaponTime += Time.fixedDeltaTime;

        if (this.lifeAnimator.GetCurrentAnimatorStateInfo(0).IsName("anim_attack_bow (6)") &&
            this.lifeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
        {
            attackFinishing = true;
            GameObject go = Instantiate<GameObject>(arrowPrefab);
            go.transform.position = bow.transform.position;
            go.transform.rotation = this.transform.rotation;
            arrow.SetActive(false);
            GameObject.Find("Audio").transform.Find("AttackArrow").GetComponent<AudioSource>().Play();
            Invoke("FinishBow", 0.5f);
            this.lifeAnimator.SetInteger("State", 0);
        }
    }

    void FinishBow() {
        attackFinishing = false;
        attacking = false;
        arrow.SetActive(true);
        weaponTime = 0;
        state = 0;
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (state == 2 || state == 3) {
            if (health <= 0) {
                state = 3;
            }
            return;
        }

		if (!stunned && state != 3 && state != 4 && state != 5) {
			//movement
			float Xinput = XCI.GetAxis(XboxAxis.LeftStickX, controller);
			float Yinput = XCI.GetAxis (XboxAxis.LeftStickY, controller);
			Vector3 movementDir = new Vector3 (Xinput, 0, Yinput).normalized;
			if (movementDir != Vector3.zero) {
				charController.SimpleMove (movementDir * speed / slowedAmount);
				transform.rotation = Quaternion.LookRotation (movementDir);
			}
			Xinput = XCI.GetAxis (XboxAxis.RightStickX, controller);
			Yinput = XCI.GetAxis (XboxAxis.RightStickY, controller);
			if (Xinput != 0 || Yinput != 0) {
				Vector3 lookDir = new Vector3 (Xinput, 0, Yinput).normalized;
				transform.rotation = Quaternion.LookRotation (lookDir);
			}
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
		//keep popup upright
		if (activePopup != null) {
			activePopup.transform.rotation = popupNotificationPrefab.transform.rotation;
			Vector3 pos = transform.position;
			pos.y = 6;
			pos.z += 1;
			activePopup.transform.position = pos;
		}
    }

    void SetLayerRecursively(GameObject obj, string name)
    {
        if (obj == null) return;
        obj.layer = LayerMask.NameToLayer(name);
        foreach(Transform child in obj.transform)
        {
            if (child == null) continue;
            SetLayerRecursively(child.gameObject, name);
        }
    }

	public void ShowPopupNotification(string txt, bool showBar = false) {
		Vector3 pos = transform.position;
		pos.y = 6;
		pos.z += 1;
		Destroy (activePopup);
		activePopup = Instantiate (popupNotificationPrefab, pos, popupNotificationPrefab.transform.rotation, transform) as GameObject;
        if(playerNum == 1)
        {   
            activePopup.layer = LayerMask.NameToLayer("Life1");
            SetLayerRecursively(activePopup, "Life1");
        }
        if (playerNum == 2)
        {
            SetLayerRecursively(activePopup, "Life2");
            activePopup.layer = LayerMask.NameToLayer("Life2");
        }
        if (playerNum == 3)
        {
            SetLayerRecursively(activePopup, "Life3");
            activePopup.layer = LayerMask.NameToLayer("Life3");
        }
        activePopup.transform.FindChild ("Panel").FindChild ("Text").GetComponent<TextMesh> ().text = txt;
		activePopup.transform.FindChild ("Panel").FindChild ("Slider").gameObject.SetActive (showBar);
	}

	public void UpdatePopupNotification(string txt, float barVal = 0) {
		activePopup.transform.FindChild ("Panel").FindChild ("Text").GetComponent<TextMesh> ().text = txt;
		activePopup.transform.FindChild ("Panel").FindChild ("Slider").GetComponent<Slider> ().value = barVal;
	}

	void ChangePopTxtToStrafe() {
		ShowPopupNotification ("Use the right stick to strafe");
        if (SetupCameras.PlayerCount == 2)
        {
            Invoke("ChangePopTxtToObj", 5f);
        }
        else
        {
            Invoke("ChangePopTxtToImmortal", 5f);
        }
	}

    void ChangePopTxtToCrates()
    {
        ShowPopupNotification("Attack crates and vases!"+ '\n' + "They might drop health!");
        
        Invoke("ChangePopTxtToStrafe", 5f);
    }

	void ChangePopTxtToObj() {
        if (SetupCameras.PlayerCount == 2)
        {
            ShowPopupNotification("Find the fountain of youth" + '\n' + "before death finds you!");
            Invoke("RemovePopupNotification", 7f);
        }
        else
        {
            ShowPopupNotification("All of your friends must" + "\n" + "survive to win!");
            Invoke("RemovePopupNotification", 5f);
        }
	}

    void ChangePopTxtToImmortal()
    {
        ShowPopupNotification("Gain immortality by reaching" + "\n" + " the fountain of youth");
        Invoke("ChangePopTxtToObj", 5f);
    }

    public void ShowPlayerImmortal()
    {
        ShowPopupNotification("You have gained immortality!" + "\n" + "Death cannot harm you!");
        Invoke("RemovePopupNotification", 5f);
    }

    void ShowTeammateFoundFountain()
    {
        ShowPopupNotification("Someone has found the fountain!" + "\n" + (SetupCameras.PlayerCount - 1 - totalReached) + " more players need to reach it!");
        Invoke("RemovePopupNotification", 5f);
    }

	public void RemovePopupNotification() {
		//Debug.Log ("remove popup");
		Destroy (activePopup);
	}

    void OnTriggerEnter(Collider col) {
        if (col.tag == "PowerUp") {
            hasWeapon = true;
            Destroy(col.gameObject);
        } else if (col.tag == "Sword" && !hasWeapon) {
            canPickupWeapon = true;
            pickupType = 1;
            weaponPickupObj = col.gameObject;
			ShowPopupNotification ("Press A to pick up Sword");
        } else if (col.tag == "Hammer" && !hasWeapon) {
            canPickupWeapon = true;
            pickupType = 2;
            weaponPickupObj = col.gameObject;
			ShowPopupNotification ("Press A to pick up Hammer");
        } else if (col.tag == "Bow" && !hasWeapon) {
            canPickupWeapon = true;
            pickupType = 3;
            weaponPickupObj = col.gameObject;
			ShowPopupNotification ("Press A to pick up Bow");
        } else if (col.tag == "LifeFountain") {
            WinCondition.S.UpdateWinCondition(this);
        } else if (col.tag == "Skeleton" && col.gameObject.layer == 13 && state != 2 && state != 3 && !immortal) {
            state = 2;
            health -= 20;
        } else if (col.tag == "Minotaur" && col.gameObject.layer == 13 && state != 2 && state != 3 && !immortal) {
            state = 2;
            health -= 40;
        } else if (col.tag == "Grunt" && col.gameObject.layer == 13 && state != 2 && state != 3 && !immortal) {
            state = 2;
            health -= 15;
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.tag == "Hammer") {
            canPickupWeapon = false;
			if (!hasWeapon) {
				RemovePopupNotification ();
			}
        } else if (col.tag == "Bow") {
            canPickupWeapon = false;
			if (!hasWeapon) {
				RemovePopupNotification ();
			}       
		} else if (col.tag == "Sword") {
            canPickupWeapon = false;
			if (!hasWeapon) {
				RemovePopupNotification ();
			}  
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
        transform.Find("body").transform.Find("armor_").GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 1f);
        Invoke("FinishDamage", 0.5f);
    }

    void FinishDamage() {
        transform.Find("body").transform.Find("armor_").GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
    }

    void Death() {
        WinCondition.S.DeathWins();
        Destroy(this.gameObject);
    }

    public void SlowPlayer(float amountSlow)
    {
        slowedAmount = amountSlow;
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
                case 4:
                    lifeAnimator.SetInteger("State", 4);
                    break;
                case 5:
                    lifeAnimator.SetInteger("State", 5);
                    break;
                case 6:
                    lifeAnimator.SetInteger("State", 6);
                    break;
            }
            _state = value;
        }
    }
}
