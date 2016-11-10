using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    public float start_time;
    public int damage = 10;

	// Use this for initialization
	void Start () {
        start_time = Time.time;

        this.gameObject.GetComponent<Rigidbody>().velocity += (50 * this.gameObject.transform.forward);
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnCollisionEnter(Collision coll) {
        Destroy(this.gameObject);
    }

}
