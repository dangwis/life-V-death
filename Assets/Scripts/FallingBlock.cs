using UnityEngine;
using System.Collections;

public class FallingBlock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.tag == "Clickable")
					hit.collider.gameObject.GetComponent<Rigidbody> ().useGravity = true;
			}
		}
	}

//	void OnMouseOver() {
//		print ("Down");
//	}
}
