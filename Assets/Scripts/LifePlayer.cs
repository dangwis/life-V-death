using UnityEngine;
using System.Collections;

public class LifePlayer : MonoBehaviour {

    Rigidbody rigid;
    public float speed;
    public int playerNum;
    public bool hasWeapon;
	// Use this for initialization
	void Start () {
        rigid = this.transform.GetComponent<Rigidbody>();
        hasWeapon = false;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 vel = rigid.velocity;
        float ret = Input.GetAxis(XInput.XboxLStickX(playerNum));
        if (ret != 0)
        {

            vel.x = Input.GetAxis(XInput.XboxLStickX(playerNum)) * speed;
        }
        if(Input.GetAxis(XInput.XboxLStickY(playerNum)) != 0)
        {
            vel.z = Input.GetAxis(XInput.XboxLStickY(playerNum)) * -speed;
        }
        rigid.velocity = vel;
	}

    void OnCollisionEnter(Collision coll)
    {
        GameObject go = coll.gameObject;
        if(go.tag == "PowerUp")
        {
            hasWeapon = true;
            Destroy(go.gameObject);
        }
    }
}
