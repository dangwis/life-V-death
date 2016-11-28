using UnityEngine;
using System.Collections;

public class SlowTrap : MonoBehaviour {

    public float slowAmount;

    void OnTriggerEnter(Collider coll)
    {
        GameObject go = coll.gameObject;
        if (go.tag == "Life")
        {
            go.GetComponent<LifePlayer>().SlowPlayer(slowAmount);
        }
    }

    void OnTriggerExit(Collider coll)
    {
        GameObject go = coll.gameObject;
        if (go.tag == "Life")
        {
            go.GetComponent<LifePlayer>().SlowPlayer(1);
        }
    }
}
