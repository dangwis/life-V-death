using UnityEngine;
using System.Collections;

public class DamageTrap : MonoBehaviour {

    public int damageDealt;
    Animation animate;
    public bool armed, showing;

	// Use this for initialization
	void Start () {
        animate = GetComponent<Animation>();
        animate.Play("Anim_TrapNeedle_Hide");
        armed = true;
        showing = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider coll)
    {
        GameObject go = coll.gameObject;
        if(go.tag == "Life" && armed == true)
        {
            go.GetComponent<LifePlayer>().health -= damageDealt;
            go.GetComponent<LifePlayer>().state = 2;
            showing = true;
            this.gameObject.transform.Find("Trap_Needle").gameObject.layer = 0;
            animate.Play("Anim_TrapNeedle_Show");
        }
    }

    void OnTriggerExit(Collider coll)
    {
        GameObject go = coll.gameObject;
        if (go.tag == "Life" && showing)
        {
            animate.Play("Anim_TrapNeedle_Hide");
            showing = false;
        }
    }

    public void Disarm()
    {
        armed = false;
        Death.S.DecrementTrap();
        this.gameObject.transform.Find("Trap_Needle").gameObject.layer = 0;
    }
}
