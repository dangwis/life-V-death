using UnityEngine;
using System.Collections;

public class TeleportPad : MonoBehaviour {

    public Vector3 endingUpPosition;

	void OnTriggerEnter(Collider coll)
    {
        GameObject go = coll.gameObject;
        if (go.tag == "Life")
        {
            go.transform.position = endingUpPosition;
            Destroy(this.gameObject);
        }
    }
}
