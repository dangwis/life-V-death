using UnityEngine;
using System.Collections;

public class DamageTrap : MonoBehaviour {

    public int damageDealt;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision coll)
    {
        GameObject go = coll.gameObject;
        if(go.tag == "Life")
        {
            go.GetComponent<LifePlayer>().health -= damageDealt;
            Destroy(this.gameObject);
        }
    }
}
