using UnityEngine;
using System.Collections;

public class LifePlayer : MonoBehaviour {

    CharacterController charController;
    public float speed;
    public int playerNum;
    public bool hasWeapon;
    public int weapontype = 0;

    public GameObject sword;
    public GameObject arrow;
    public GameObject bow;
    public GameObject hammer;

    public float cooldown = 1.5f;
    private float lastattacktime;

    public int health = 100;


    // Use this for initialization
    void Start () {
        charController = this.transform.GetComponent<CharacterController>();
        hasWeapon = false;
        lastattacktime = Time.time;
	}

	void OnDestory() {
		
	}
	
	// Update is called once per frame
	void Update () {
        float Xinput = Input.GetAxis (XInput.XboxLStickX (playerNum));
		float Yinput = -Input.GetAxis (XInput.XboxLStickY (playerNum));
		Vector3 movementDir = new Vector3 (Xinput, 0, Yinput).normalized;
		if (movementDir != Vector3.zero) {
			charController.SimpleMove (movementDir * speed);
			transform.rotation = Quaternion.LookRotation (movementDir);
		}

		if (Input.GetButtonDown(XInput.XboxA(playerNum)) && hasWeapon && (Time.time - lastattacktime > cooldown) )
        {
            lastattacktime = Time.time;
            if(weapontype == 0)
            {
                // hammer attack
                GameObject weapon_instance = MonoBehaviour.Instantiate(hammer, this.transform.position + this.transform.forward, Quaternion.identity) as GameObject;
            }
            else if(weapontype == 1){
                // bow
                GameObject weapon_instance = MonoBehaviour.Instantiate(bow, this.transform.position + this.transform.forward, Quaternion.identity) as GameObject;
                GameObject weapon_instance2 = MonoBehaviour.Instantiate(arrow, this.transform.position + this.transform.forward, Quaternion.identity) as GameObject;

                weapon_instance2.GetComponent<Rigidbody>().velocity += (10 * this.transform.forward); 
            }
            else if(weapontype == 2)
            {
                //sword
                GameObject weapon_instance = MonoBehaviour.Instantiate(sword, this.transform.position + this.transform.forward, Quaternion.identity) as GameObject;
            }
        }

	}

    void OnCollisionEnter(Collision coll)
    {
        GameObject go = coll.gameObject;
        if(go.tag == "PowerUp")
        {
            hasWeapon = true;
            Destroy(go.gameObject);
        }
        else if (go.tag == "Hammer")
        {
            hasWeapon = true;
            Destroy(go.gameObject);
            weapontype = 0;
        }
        else if (go.tag == "Bow")
        {
            hasWeapon = true;
            Destroy(go.gameObject);
            weapontype = 1;
        }
        else if (go.tag == "Sword")
        {
            hasWeapon = true;
            Destroy(go.gameObject);
            weapontype = 2;
        }
    }
}
