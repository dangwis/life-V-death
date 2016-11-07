using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public float start_time;
    public float lifetime;
    public int damage = 10;

	// Use this for initialization
	void Start () {
        start_time = Time.time;
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Time.time - start_time > lifetime) {
            Destroy(this.gameObject);
        }
	
	}

    void OnCollisionEnter(Collision coll)
    {
    }

}
