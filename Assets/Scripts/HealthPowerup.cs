using UnityEngine;
using System.Collections;

public class HealthPowerup : MonoBehaviour {

	public int healthToRestore = 1;
	public float rotationSpeed = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up * Time.fixedDeltaTime * rotationSpeed);
		transform.Rotate (Vector3.forward * Time.fixedDeltaTime * rotationSpeed);
	}

	void OnTriggerEnter(Collider coll) {
		print ("power up hit");
		if (coll.gameObject.tag == "Life") {
			coll.gameObject.GetComponent<LifePlayer> ().health += healthToRestore;
			if (coll.gameObject.GetComponent<LifePlayer>().health > 100) 
				coll.gameObject.GetComponent<LifePlayer>().health = 100;
			Destroy (this.gameObject);
		}
	}
}
