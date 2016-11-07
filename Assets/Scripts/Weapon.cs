using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public float start_time;
    public float lifetime;
    public int damage = 10;
    public bool melee;

	// Use this for initialization
	void Start () {
        start_time = Time.time;

        if (melee)
        {
            //Quaternion new_weapon_rotation = new Quaternion();
            //new_weapon_rotation.eulerAngles = this.gameObject.transform.rotation.eulerAngles + new Vector3(0, -85, 0);
            //this.gameObject.transform.rotation = new_weapon_rotation;
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody>().velocity += (10 * this.gameObject.transform.forward);
        }


    }
	
	// Update is called once per frame
	void Update () {
        if(Time.time - start_time > lifetime) {
            Destroy(this.gameObject);
        }

        if (melee)
        {
            //Quaternion new_weapon_rotation = new Quaternion();
            //new_weapon_rotation.eulerAngles = this.gameObject.transform.rotation.eulerAngles + new Vector3(0, 2, 0);
            //this.gameObject.transform.rotation = new_weapon_rotation;

            this.gameObject.transform.RotateAround(this.gameObject.transform.position, new Vector3(0, 1, 0), -130f * Time.deltaTime);
        }

    }

    void OnCollisionEnter(Collision coll)
    {
    }

}
