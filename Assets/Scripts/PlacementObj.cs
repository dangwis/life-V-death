using UnityEngine;
using System.Collections;

public class PlacementObj : MonoBehaviour {

    public bool overlapping;

    void Start()
    {
        overlapping = false;
    }

    void OnTriggerEnter(Collider coll)
    {
        GameObject go = coll.gameObject;
        if(go.tag == "Trap")
        {
            overlapping = true;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        GameObject go = coll.gameObject;
        if(go.tag == "Trap")
        {
            overlapping = false;
        }
    }
}
