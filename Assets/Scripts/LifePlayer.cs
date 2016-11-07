using UnityEngine;
using System.Collections;

public class LifePlayer : MonoBehaviour {

    Rigidbody rigid;
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
        rigid = this.transform.GetComponent<Rigidbody>();
        hasWeapon = false;
        lastattacktime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 vel = rigid.velocity;
        float ret = Input.GetAxis(XInput.XboxLStickX(playerNum));

        Quaternion new_player_rotation = new Quaternion();
        new_player_rotation.eulerAngles = this.gameObject.transform.rotation.eulerAngles;

        if (ret != 0)
        {

            vel.x = Input.GetAxis(XInput.XboxLStickX(playerNum)) * speed;

            if(vel.x > 0)
            {
                new_player_rotation.eulerAngles = new Vector3(0, 0, 180);
            }
            else if(vel.x < 0)
            {
                new_player_rotation.eulerAngles = new Vector3(0, 0, 0);
            }

        }
        if(Input.GetAxis(XInput.XboxLStickY(playerNum)) != 0)
        {
            vel.z = Input.GetAxis(XInput.XboxLStickY(playerNum)) * -speed;

            if (vel.z > 0)
            {
                new_player_rotation.eulerAngles = new Vector3(0, 0, 90);
            }
            else if (vel.z < 0)
            {
                new_player_rotation.eulerAngles = new Vector3(0, 0, 270);
            }
        }
        rigid.velocity = vel;
        this.gameObject.transform.rotation = new_player_rotation;

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
