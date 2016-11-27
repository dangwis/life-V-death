using UnityEngine;
using System.Collections;

public class TeleportPad : MonoBehaviour {

    public Vector3 endingUpPosition;
    public GameObject endingPosPrefab;
    GameObject endingObj;

    public void ShowEndPos()
    {
        endingObj = Instantiate(endingPosPrefab);
        endingObj.transform.position = endingUpPosition;
    }

	void OnTriggerEnter(Collider coll)
    {
        GameObject go = coll.gameObject;
        if (go.tag == "Life")
        {
            go.transform.position = endingUpPosition;
            Death.S.DecrementTrap();
            Destroy(endingObj);
            Destroy(this.gameObject);
        }
    }
}
