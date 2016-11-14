using UnityEngine;
using System.Collections;

public class DamageTrap : MonoBehaviour {

    public int damageDealt;
    Animation animate;
	// Use this for initialization
	void Start () {
        animate = GetComponent<Animation>();
        animate.Play("Anim_TrapNeedle_Hide");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider coll)
    {
        GameObject go = coll.gameObject;
        if(go.tag == "Life")
        {
            go.GetComponent<LifePlayer>().health -= damageDealt;
            go.GetComponent<LifePlayer>().state = 2;
            animate.Play("Anim_TrapNeedle_Show");
        }
    }

    void OnTriggerExit(Collider coll)
    {
        GameObject go = coll.gameObject;
        if (go.tag == "Life")
        {
            animate.Play("Anim_TrapNeedle_Hide");
        }
    }
}
