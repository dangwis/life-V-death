using UnityEngine;
using System.Collections;

public class DamageTrap : MonoBehaviour {

    public int damageDealt;
    Animation animate;
    public bool armed = true, showing = false;
    public ParticleSystem[] flame;

    // Use this for initialization
    void Start () {
        animate = GetComponent<Animation>();
        //animate.Play("Anim_TrapNeedle_Hide");
        armed = true;
        showing = false;
        foreach (ParticleSystem fire in flame) {
            fire.Stop();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider coll)
    {
        GameObject go = coll.gameObject;
        if(go.tag == "Life" && armed == true)
        {
            go.GetComponent<LifePlayer>().health -= damageDealt;
            go.GetComponent<LifePlayer>().state = 2;
            showing = true;
            this.gameObject.transform.Find("Trap_Needle").gameObject.layer = 0;
            //animate.Play("Anim_TrapNeedle_Show");
            foreach (ParticleSystem fire in flame) {
                fire.Play();
            }

            armed = false;
            Invoke("SetArmed", 1f);
        }
    }

    void SetArmed() {
        armed = true;
    }

    void OnTriggerExit(Collider coll)
    {
        GameObject go = coll.gameObject;
        if (go.tag == "Life" && showing)
        {
            //animate.Play("Anim_TrapNeedle_Hide");
            foreach (ParticleSystem fire in flame) {
                fire.Stop();
            }
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
